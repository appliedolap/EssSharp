"""Outline-derived help for the model that answers Essbase's AI questions.

Essbase tells the model only the names of a cube's dimensions. It then looks up each word the model writes (such as
"region" or "quarterly") as a member, by embedding similarity, so a word that names a level of the outline lands on one
member that happens to be close. This module reads the cube's outline through the Essbase REST API - its dimensions,
the names of their generations and the members directly below each dimension - and uses it twice:

  * rules_for() describes the levels to the model, ahead of Essbase's prompt.
  * correct() rewrites the model's answer before it goes back to Essbase, so that a word naming a level becomes the
    dimension (or the member it is limited to) with the matching hierarchy_scope, whatever the model chose.

Only named generations take part: a level still called Gen2 or Gen3 gives nothing to match. A cube can add words of its
own in hints/<App>.<Cube>.json: {"levelWords": {"product family": "Category"}} maps a word to a generation name, and
{"phrases": {"second half": ["Qtr3", "Qtr4"]}} maps a phrase that names no member to the members it stands for.
"""
import base64
import json
import math
import os
import re
import threading
import time
import urllib.parse
import urllib.request

ESSBASE_URL = (os.environ.get("ESSBASE_URL") or "").rstrip("/")
AUTHORIZATION = "Basic " + base64.b64encode(("%s:%s" % (os.environ.get("ESSBASE_USER", ""),
                                                         os.environ.get("ESSBASE_PASSWORD", ""))).encode()).decode()

# How long an outline is trusted before it is read again.
CACHE_SECONDS = int(os.environ.get("AI_OUTLINE_CACHE_SECONDS", "600"))

# How close (cosine similarity) a word must be to a generation name to be taken for it when it does not match by spelling.
# Set to 0 to match by spelling only.
SIMILARITY = float(os.environ.get("AI_LEVEL_SIMILARITY", "0.8"))

# How many members below a dimension to name in the rules.
CHILDREN_SHOWN = 12

# How many members to read by name and alias, so that a member the question names can be told from a level word.
MEMBER_LIMIT = int(os.environ.get("AI_OUTLINE_MEMBER_LIMIT", "3000"))

# Words that ask for the members below a member ("by region", "each state") when they come just before it.
BREAKDOWN_WORDS = {"by", "each", "every", "per", "within", "under"}

# Words in a question that ask for an actual difference, rather than a side by side comparison.
DIFFERENCE_WORDS = re.compile(r"\b(differen\w*|variance\w*|gap|minus|delta|change\w*|subtract\w*)\b", re.I)
# Words that make "all X" the total of X ("total profit across all regions") rather than a list of X ("all months").
TOTAL_WORDS = re.compile(r"\b(across|total|overall|sum|altogether|combined)\b", re.I)
COMPARE_WORDS = re.compile(r"\b(compare\w*|comparison|versus|vs\.?|against)\b", re.I)

GENERIC_NAME = re.compile(r"^(gen|lev)\d+$", re.I)

# Marks, while an answer is being corrected, a member whose scope a correction set, so later checks leave it alone.
FIXED = "_corrected"

# The alias table of synonyms written for the AI lookup (hints/<App>.<Cube>.alt).
EMBEDDINGS_ALIAS_TABLE = "Embeddings"

# A year number ("2026", "FY2025") that a model may write for "this year" or "last year".
YEAR_NUMBER = re.compile(r"^(?:fy\s*|year\s*)?(?:19|20)\d\d$", re.I)
YEAR_MEMBER = re.compile(r"^(?:fy\s*|year\s*)?(?:(?:19|20)\d\d|\d\d)$", re.I)
TIME_LEVEL = re.compile(r"quarter|month|week|period|day|half", re.I)
TIME_DIMENSION = re.compile(r"^(year|years|time|period|periods|fiscal year)$", re.I)

# Words in front of a level word that do not change it ("all months", "each quarter").
LEADING_WORDS = {"all", "each", "every", "the", "per", "by"}

# Ranking questions: "which quarter had the highest sales", "the five products with the highest margin".
RANKING_OPERATORS = ("top count", "bottom count", "top percent", "bottom percent")
HIGH_WORDS = r"highest|most|best|largest|biggest|greatest|top|maximum"
LOW_WORDS = r"lowest|least|worst|smallest|fewest|bottom|minimum"
WHICH_QUESTION = re.compile(r"\bwhich\s+([a-z ]+?)\s+(?:had|has|have|was|is|were|did|sold|made|showed|shows|got)\b.*?"
                            r"\b(%s|%s)\b" % (HIGH_WORDS, LOW_WORDS), re.I)
COUNT_PHRASE = re.compile(r"\b(?:top|bottom|the|first|last)?\s*(\d+|one|two|three|four|five|six|seven|eight|nine|ten)\s+"
                          r"([a-z]+(?:\s+[a-z]+)?)", re.I)


def rest(path):
    """Returns the parsed JSON response to a GET on the Essbase REST API."""
    request = urllib.request.Request(ESSBASE_URL + "/essbase/rest/v1/" + path,
                                     headers={"Authorization": AUTHORIZATION, "Accept": "application/json"})
    with urllib.request.urlopen(request, timeout=60) as response:
        return json.loads(response.read().decode())


def singular(word):
    """Returns a word in the singular, as far as simple English endings allow."""
    if len(word) > 4 and word.endswith("ies"):
        return word[:-3] + "y"
    if len(word) > 4 and word.endswith(("sses", "shes", "ches", "xes")):
        return word[:-2]
    if len(word) > 3 and word.endswith("s") and not word.endswith("ss"):
        return word[:-1]
    return word


def phrase_keys(text, adverbs=True):
    """Returns the forms under which a phrase can match a level name: its words in the singular, and (with adverbs) the
    same with an adverb ending dropped from the last word ("quarterly" is "quarter")."""
    words = [singular(word) for word in re.findall(r"[a-z0-9%]+", (text or "").lower())]
    # "all months" or "each quarter" is the level itself.
    while len(words) > 1 and words[0] in LEADING_WORDS:
        words = words[1:]
    if not words:
        return []
    keys = [" ".join(words)]
    if adverbs and len(words[-1]) > 5 and words[-1].endswith("ly"):
        keys.append(" ".join(words[:-1] + [words[-1][:-2]]))
    return keys


class Level:
    """A named generation of a dimension that a hierarchy_scope can express."""

    def __init__(self, dimension, name, scope):
        self.dimension = dimension
        self.name = name
        self.scope = scope

    def __repr__(self):
        return "%s %s (%s)" % (self.dimension, self.name, self.scope)


class CubeHints:
    """What the outline of one cube says about its levels."""

    def __init__(self, application, cube, hints_dir, embed):
        self.application = application
        self.cube = cube
        self.embed = embed
        self.read_at = time.time()
        self.dimensions = []  # (name, [(generation number, name)], [(child name, alias)])
        self.words = {}  # phrase key -> Level
        self.levels = []
        self.level_vectors = None
        self.known = set()  # dimension and member names (and aliases) that are never taken for a level

        quoted_app = urllib.parse.quote(application)
        quoted_cube = urllib.parse.quote(cube)
        for item in rest("outline/%s/%s?fields=name,attribute,dimension" % (quoted_app, quoted_cube)).get("items", []):
            # Attribute dimensions, and the dimension Essbase adds to calculate them, have no levels to speak of.
            if item.get("attribute") or item["name"] == "Attribute Calculations":
                continue
            name = item["name"]
            generations = rest("applications/%s/databases/%s/dimensions/%s/generations"
                               % (quoted_app, quoted_cube, urllib.parse.quote(name))).get("items", [])
            generations = sorted((generation["number"], generation.get("name") or "") for generation in generations)
            children = rest("outline/%s/%s?parent=%s&limit=%d" % (quoted_app, quoted_cube, urllib.parse.quote(name),
                                                                  CHILDREN_SHOWN + 1)).get("items", [])
            children = [(child["name"], ((child.get("aliases") or {}).get("Default") or "")) for child in children]
            self.known.update(text.lower() for text in [name] + [part for child in children for part in child] if text)
            self.dimensions.append((name, generations, children))

            deepest = generations[-1][0] if generations else 1
            for number, generation_name in generations:
                if number == 1 or GENERIC_NAME.match(generation_name) or not generation_name:
                    continue
                scope = "children" if number == 2 else "leaves" if number == deepest else None
                if scope is None:
                    continue  # a middle level: no hierarchy_scope expresses it
                level = Level(name, generation_name, scope)
                self.levels.append(level)
                for key in phrase_keys(generation_name, adverbs=False):
                    self.words[key] = level
                # "Product SKU" is also just "SKU".
                words = generation_name.split()
                last_word = phrase_keys(words[-1], adverbs=False) if len(words) > 1 else []
                for key in last_word:
                    self.words.setdefault(key, level)

        self.members = self.read_members(quoted_app, quoted_cube)

        # The time dimension: the one with a level named for a period of time, or failing that, one named like it.
        self.time_dimension = next(
            (level.dimension for level in self.levels if TIME_LEVEL.search(level.name)),
            next((name for name, _, _ in self.dimensions if TIME_DIMENSION.match(name)), None))

        # Whether the outline has members for years ("FY25", "2026"). If it does, a year number in an answer may well be one
        # of them, and is left for Essbase to look up.
        self.has_year_members = any(YEAR_MEMBER.match(name) for name in self.members)

        # Phrases that stand for a set of members ("second half": Qtr3 and Qtr4), from the cube's own file.
        self.phrases = {}
        extra = os.path.join(hints_dir, "%s.%s.json" % (application, cube))
        if os.path.exists(extra):
            with open(extra, encoding="utf-8") as source:
                settings = json.load(source)
            for word, generation_name in (settings.get("levelWords") or {}).items():
                level = next((level for level in self.levels if level.name.lower() == generation_name.lower()), None)
                if level:
                    for key in phrase_keys(word, adverbs=False):
                        self.words[key] = level
            for phrase, names in (settings.get("phrases") or {}).items():
                keys = phrase_keys(phrase, adverbs=False)
                if keys and names:
                    self.phrases[keys[0]] = list(names)

    def read_members(self, quoted_app, quoted_cube):
        """Returns the dimension of each member, by its name and its default alias in lower case, for up to MEMBER_LIMIT
        members (the rest of a larger outline is left out)."""
        members = {}
        self.texts_of = {}  # any name of a member, lower case -> all its names (name, default alias, synonyms)
        labels = {}  # member name -> how to call it (its default alias, or its name)
        home = {}  # member name -> the parent of its stored (not shared) instance
        shared_children = {}  # parent -> the names of its children, when every one of them is shared
        pending = [name for name, _, _ in self.dimensions]
        while pending and len(members) < MEMBER_LIMIT:
            parent = pending.pop(0)
            items = rest("outline/%s/%s?parent=%s&limit=%d&fields=name,aliases,dimensionName,numberOfChildren,"
                         "dataStorageType" % (quoted_app, quoted_cube, urllib.parse.quote(parent), MEMBER_LIMIT)
                         ).get("items", [])
            for item in items:
                dimension = item.get("dimensionName") or parent
                aliases = item.get("aliases") or {}
                alias = aliases.get("Default")
                texts = [text for text in (item.get("name"), alias) if text]
                # The synonyms of the Embeddings alias table ("Q4, fourth quarter") name the member too, unless one is
                # a level word ("States" on Market), which stays a level.
                for synonym in (aliases.get(EMBEDDINGS_ALIAS_TABLE) or "").split(","):
                    synonym = synonym.strip()
                    keys = phrase_keys(synonym, adverbs=False)
                    if synonym and keys and keys[0] not in self.words:
                        texts.append(synonym)
                for text in texts:
                    members.setdefault(text.lower(), dimension)
                    self.texts_of.setdefault(text.lower(), [other.lower() for other in texts])
                if item.get("dataStorageType") != "SHAREDMEMBER":
                    labels[item["name"]] = alias or item["name"]
                    home[item["name"]] = parent
                if item.get("numberOfChildren"):
                    pending.append(item["name"])
            if items and all(item.get("dataStorageType") == "SHAREDMEMBER" for item in items):
                shared_children[parent] = [item["name"] for item in items]

        # An alternate rollup: a member made only of shared copies of members stored elsewhere (Sample.Basic's Diet
        # Drinks). Listed next to the members it overlaps, its rows count those members twice.
        self.alternates = []
        for name, children in shared_children.items():
            overlapped = sorted({labels.get(home[child], home[child]) for child in children if child in home})
            self.alternates.append({"name": name, "label": labels.get(name, name), "parent": home.get(name),
                                    "overlaps": overlapped})
        return members

    def rules(self):
        """Describes the levels of the cube for the model."""
        lines = ["Levels of the %s.%s outline. When a question names one of these levels, write the dimension name as "
                 "member_name with the hierarchy_scope given; when it limits the level to part of the dimension (such as "
                 "the states of one region), write that member with the hierarchy_scope instead."
                 % (self.application, self.cube)]
        for name, generations, children in self.dimensions:
            named = [level for level in self.levels if level.dimension == name]
            shown = ", ".join("%s (%s)" % (alias, child) if alias and alias != child else child
                              for child, alias in children[:CHILDREN_SHOWN])
            if len(children) > CHILDREN_SHOWN:
                shown += ", ..."
            parts = ['"%s" means hierarchy_scope "%s" of %s' % (level.name, level.scope, name) for level in named]
            lines.append("- %s: members directly below it are %s.%s" % (name, shown or "none",
                                                                      (" " + "; ".join(parts) + ".") if parts else ""))
        return "\n".join(lines)

    def level_for(self, member_name, scope):
        """Returns the level a member name in the model's answer stands for, or None."""
        for key in phrase_keys(member_name):
            if key in self.words:
                return self.words[key]
        # Not spelled like a level: when the model treated it as a set (gave it a scope), compare its meaning - unless it
        # names a dimension or a member, which "Product" with "children" does, and which is already what it should be.
        if not scope or not SIMILARITY or not self.levels or self.embed is None:
            return None
        if (member_name or "").strip().lower() in self.known:
            return None
        if self.level_vectors is None:
            self.level_vectors = [unit(vector) for vector in self.embed([level.name for level in self.levels])]
        vector = unit(self.embed([member_name])[0])
        score, level = max((sum(a * b for a, b in zip(vector, other)), level)
                           for other, level in zip(self.level_vectors, self.levels))
        print("LEVEL similarity %r -> %s: %.3f" % (member_name, level, score), flush=True)
        return level if score >= SIMILARITY else None

    def correct(self, reply, question=""):
        """Returns the model's reply corrected against the outline and the question:

          * every word that names a level is replaced by its dimension (or the member of that dimension the question
            limits it to) with the level's hierarchy_scope;
          * a member the question only names ("for root beer") loses a hierarchy_scope the question does not ask for;
          * "compare A and B" answered with the "difference" operator becomes A and B side by side.

        Leaves a reply it cannot read unchanged."""
        start = reply.find("{")
        if start < 0:
            return reply
        try:
            answer, end = json.JSONDecoder().raw_decode(reply[start:])
        except ValueError:
            return reply
        if not isinstance(answer, dict):
            return reply
        info = reply[start + end:]
        info = info[info.find("info:"):] if "info:" in info else ""
        changes = []
        self.correct_operator(answer, changes)
        if question:
            self.clear_unasked_scopes(answer, question, changes)
            if not self.use_named_member(answer, question, changes):
                self.compare_side_by_side(answer, question, changes)
            self.rank_what_is_asked(answer, question, changes)
            self.total_of_all(answer, question, changes)
        drop_empty_operators(answer, changes)
        notes = self.overlap_notes(answer)
        if not changes and not notes:
            return reply
        for member in all_members(answer):
            member.pop(FIXED, None)
        if changes:
            print("CORRECTED %s" % "; ".join(changes), flush=True)
        if notes:
            print("NOTED %s" % " ".join(notes), flush=True)
            info = (info.strip() if info else "info:") + " " + " ".join(notes)
        return json.dumps(answer) + ("\n" + info.strip() if info else "")

    def ranked_set(self, noun):
        """Returns the member and hierarchy_scope a noun in a ranking question stands for ("quarter" is Year's children,
        "products" Product's leaves), or None."""
        for words in (noun, noun.split()[0]):
            level = self.level_for(words, "")
            if level:
                return level.dimension, level.scope
            for dimension_name, _, _ in self.dimensions:
                if phrase_keys(words) and phrase_keys(words)[0] == phrase_keys(dimension_name)[0]:
                    return dimension_name, "leaves"
        return None

    def rank_what_is_asked(self, answer, question, changes):
        """Makes a ranking answer rank what the question asks to rank.

        * "the five products with the highest margin" ranked by Margin, with Margin itself as the operand: the operand
          becomes the products.
        * "which quarter had the highest sales" answered without a ranking: it becomes a top (or bottom) count of one
          over the quarters, sorted by the rest of the POV.
        """
        operator = (answer.get("operator") or "").lower()
        operands = answer.get("operands") or []
        sort_names = {(member.get("member_name") or "").lower() for items in (answer.get("sort") or {}).values()
                      if isinstance(items, list) for member in items if is_member(member)}
        if operator in RANKING_OPERATORS and len(operands) > 1 and is_member(operands[1]) \
                and (operands[1].get("member_name") or "").lower() in sort_names:
            count = COUNT_PHRASE.search(question)
            target = self.ranked_set(count.group(2).lower()) if count else None
            if target:
                replaced = operands[1].get("member_name")
                operands[1] = {"member_name": target[0], "hierarchy_scope": target[1], FIXED: True}
                changes.append("ranked %s instead of %s" % (target[0], replaced))
            return

        which = WHICH_QUESTION.search(question)
        if operator not in ("", "none") or not which:
            return
        target = self.ranked_set(which.group(1).lower())
        if not target:
            return
        dimension, scope = target
        pov = answer.setdefault("POV", {})
        # Keep a member of that dimension the question itself names ("which state in the West"); drop others.
        limited = [member for member in pov.get(dimension, []) if is_member(member) and re.search(
            r"\b%s\b" % re.escape((member.get("member_name") or "").lower()), question.lower())
            and member.get("member_name", "").lower() != dimension.lower()]
        pov.pop(dimension, None)
        ranked = {"member_name": limited[0]["member_name"] if limited else dimension, "hierarchy_scope": scope,
                  FIXED: True}
        high = re.search(r"\b(%s)\b" % HIGH_WORDS, which.group(2), re.I)
        answer["operator"] = "top count" if high else "bottom count"
        answer["operands"] = [1, ranked]
        answer["sort"] = json.loads(json.dumps(pov))
        changes.append("%s of 1 over %s %s" % (answer["operator"], ranked["member_name"], scope))

    def overlap_notes(self, answer):
        """Returns a sentence for each alternate rollup the answer lists next to the members it overlaps: the children of
        its parent, or every member below an ancestor of its parent."""
        notes = []
        for alternate in self.alternates:
            parent = (alternate["parent"] or "").lower()
            listed = any((member.get("member_name") or "").strip().lower() == parent
                         and (member.get("hierarchy_scope") or "").lower() in ("children", "descendants")
                         for member in all_members(answer))
            if listed and alternate["overlaps"]:
                notes.append("%s is an alternate grouping: its members also belong to %s, so its row overlaps theirs."
                             % (alternate["label"], join_names(alternate["overlaps"])))
        return notes

    def clear_unasked_scopes(self, answer, question, changes):
        """Clears the hierarchy_scope of each member the question names without asking for what is below it: no "by",
        "each" or similar just before it, no scope keyword and no level of its dimension anywhere in the question."""
        text = " ".join(re.findall(r"[a-z0-9%]+", question.lower()))
        ranked = {id(operand) for operator in all_operators(answer)
                  if (operator.get("operator") or "").lower() in RANKING_OPERATORS
                  for operand in operator.get("operands") or [] if is_member(operand)}
        for member in all_members(answer):
            scope = member.get("hierarchy_scope")
            name = (member.get("member_name") or "").strip().lower()
            if not scope or scope.lower() in text or member.get(FIXED):
                continue
            dimension_name = next((dimension for dimension, _, _ in self.dimensions if dimension.lower() == name), None)
            if dimension_name:
                # A whole dimension with a breakdown ("last year" answered as Year's months): keep the breakdown only
                # when the question asks for one, or when it is the set a ranking ranks.
                if id(member) in ranked or self.level_asked(dimension_name, text):
                    continue
                member["hierarchy_scope"] = ""
                changes.append("%s: dropped %s (not asked for)" % (member.get("member_name"), scope))
                continue
            dimension = self.members.get(name)
            if dimension is None or self.level_asked(dimension, text):
                continue
            # The question may call the member by another of its names ("fourth quarter" for Qtr4).
            position = -1
            for candidate in [name] + self.texts_of.get(name, []):
                words = " ".join(re.findall(r"[a-z0-9%]+", candidate))
                position = (" " + text + " ").find(" %s " % words) if words else -1
                if position >= 0:
                    break
            if position < 0:
                continue  # the model's words, not the question's: no way to tell what the question asked
            before = text[:position].split()[-3:]
            if BREAKDOWN_WORDS.intersection(before):
                continue
            member["hierarchy_scope"] = ""
            changes.append("%s: dropped %s (not asked for)" % (member.get("member_name"), scope))

    def total_of_all(self, answer, question, changes):
        """Gives "all X" the total of X when the question asks for a total ("total profit across all regions"): a
        correction that turned "all regions" into Market's children is taken back to Market itself."""
        if not TOTAL_WORDS.search(question):
            return
        for member in all_members(answer):
            original = member.get(FIXED)
            if isinstance(original, str) and original.strip().lower().startswith(("all ", "every ")) \
                    and member.get("hierarchy_scope"):
                changes.append("%s: total, not %s" % (original, member["hierarchy_scope"]))
                member["hierarchy_scope"] = ""

    def level_asked(self, dimension, text):
        """Returns whether the question (lower-cased words) names a level of the dimension as a level ("the states in
        the West"), rather than as part of a member's name ("the East region", "the Colas family")."""
        raw = text.split()
        words = [singular(word) for word in raw]
        # Each word in its plain form and, for an adverb, without "ly" ("monthly" asks for the months).
        forms = [{word, word[:-2]} if len(word) > 5 and word.endswith("ly") else {word} for word in words]
        # "each product in the Colas family": the dimension's own name after "each" or "by" asks for its members.
        dimension_key = phrase_keys(dimension, adverbs=False)
        if dimension_key:
            for index, word in enumerate(words):
                if word == dimension_key[0] and index > 0 and raw[index - 1] in BREAKDOWN_WORDS:
                    return True
        for key, level in self.words.items():
            if level.dimension != dimension:
                continue
            key_words = key.split()
            for start in range(len(words) - len(key_words) + 1):
                if not all(key_word in forms[start + offset] for offset, key_word in enumerate(key_words)):
                    continue
                if start > 0 and raw[start - 1] == "all" and TOTAL_WORDS.search(text):
                    continue  # "total profit across all regions" asks for the total
                # Part of a member's name: "East region" (a name, then the level word), or "fourth quarter" (a name that
                # ends with the level word).
                end = start + len(key_words)
                names_member = any(self.members.get(" ".join(raw[start - count:start])) == dimension
                                   or self.members.get(" ".join(raw[start - count:end])) == dimension
                                   for count in (1, 2, 3) if start - count >= 0)
                if not names_member:
                    return True
        return False

    def use_named_member(self, answer, question, changes):
        """Uses a member the question names when the answer works it out from two of its siblings instead: "the
        variance percent between actual and budget profit" answered with the difference of Actual and Budget, or with
        both side by side, becomes the Variance % member. Returns whether it rewrote the answer."""
        operator = (answer.get("operator") or "").lower()
        pov = answer.get("POV") if isinstance(answer.get("POV"), dict) else {}
        if operator == "difference":
            members = [operand for operand in answer.get("operands") or [] if is_member(operand)]
        elif operator in ("", "none"):
            members = [member for items in pov.values() if isinstance(items, list) for member in items
                       if is_member(member)]
        else:
            return False
        by_dimension = {}
        for member in members:
            dimension = self.members.get((member.get("member_name") or "").strip().lower())
            if dimension:
                by_dimension.setdefault(dimension, []).append(member)
        for dimension, siblings in by_dimension.items():
            if len(siblings) < 2:
                continue
            used = {text for sibling in siblings
                    for text in self.texts_of.get((sibling.get("member_name") or "").strip().lower(), [])}
            # Blank out the siblings' own names first, so that "Diet" inside "Diet Cola" is not taken for Diet.
            named = normalized_words(question)
            for text in sorted(used, key=len, reverse=True):
                words = normalized_words(text).strip()
                if words:
                    named = named.replace(" %s " % words, " | ")
            # The longest name of another member of the dimension that the question uses ("variance %" over
            # "variance").
            candidates = sorted((text for text, other in self.members.items()
                                 if other == dimension and text not in used and normalized_words(text).strip()
                                 and " %s " % normalized_words(text).strip() in named), key=len, reverse=True)
            if not candidates:
                continue
            target = self.texts_of.get(candidates[0], [candidates[0]])[0]
            if operator == "difference":
                answer["operator"] = "none"
                answer["operands"] = []
                answer["sort"] = {}
            pov[dimension] = [member for member in pov.get(dimension, []) if member not in siblings] + [
                {"member_name": target, "hierarchy_scope": ""}]
            answer["POV"] = pov
            changes.append("%s of %s -> %s" % (operator or "none", ", ".join(sibling.get("member_name")
                                                                           for sibling in siblings), target))
            return True
        return False

    def compare_side_by_side(self, answer, question, changes):
        """Rewrites a "difference" answer to a question that only compares into a side by side one: the operands join
        the POV under their dimension, and the operator, operands and sort are cleared."""
        if (answer.get("operator") or "").lower() != "difference" or not COMPARE_WORDS.search(question) \
                or DIFFERENCE_WORDS.search(question):
            return
        operands = [operand for operand in answer.get("operands") or [] if is_member(operand)]
        dimensions = [self.members.get((operand.get("member_name") or "").strip().lower()) for operand in operands]
        if not operands or None in dimensions:
            return
        pov = answer.setdefault("POV", {})
        for operand, dimension in zip(operands, dimensions):
            members = pov.setdefault(dimension, [])
            if not any((member.get("member_name") or "").lower() == operand["member_name"].lower()
                       for member in members if is_member(member)):
                members.append({"member_name": operand["member_name"], "hierarchy_scope": operand.get("hierarchy_scope", "")})
        answer["operator"] = "none"
        answer["operands"] = []
        answer["sort"] = {}
        changes.append("difference of %s -> side by side" % ", ".join(operand["member_name"] for operand in operands))

    def correct_operator(self, operator, changes):
        """Corrects one operator object - its POV, its sort and its operands - in place."""
        if not isinstance(operator, dict):
            return
        pov = operator.get("POV") if isinstance(operator.get("POV"), dict) else {}
        for key in ("POV", "sort"):
            if isinstance(operator.get(key), dict):
                self.correct_by_dimension(operator[key], changes)
        operands = operator.get("operands")
        if isinstance(operands, list):
            for index, operand in enumerate(operands):
                if isinstance(operand, dict) and "member_name" in operand:
                    operands[index] = self.correct_member(operand, pov, changes)
                elif isinstance(operand, dict):
                    self.correct_operator(operand, changes)

    def correct_member(self, member, pov, changes):
        """Returns the corrected form of a member that stands alone (an operand): the level's dimension, or the one member
        of that dimension the POV limits it to (which then leaves the POV)."""
        level = self.level_for(member.get("member_name"), member.get("hierarchy_scope"))
        if level is None:
            return member
        plain = [item for item in pov.get(level.dimension, []) if is_member(item) and self.level_for(
            item.get("member_name"), "") is None]
        if len(plain) == 1:
            pov[level.dimension] = [item for item in pov[level.dimension] if item is not plain[0]]
            if not pov[level.dimension]:
                del pov[level.dimension]
            corrected = {"member_name": plain[0]["member_name"], "hierarchy_scope": level.scope,
                         FIXED: member.get("member_name")}
        else:
            corrected = {"member_name": level.dimension, "hierarchy_scope": level.scope, FIXED: member.get("member_name")}
        changes.append("%s -> %s %s" % (member.get("member_name"), corrected["member_name"], level.scope))
        return corrected

    def correct_by_dimension(self, members_by_dimension, changes):
        """Corrects a POV or sort: {dimension: [members or operators]}."""
        for key in list(members_by_dimension):
            items = members_by_dimension.get(key)
            if not isinstance(items, list):
                continue
            for item in list(items):
                if not is_member(item):
                    self.correct_operator(item, changes)
                    continue
                if self.replace_phrase(item, items, members_by_dimension, changes) \
                        or self.replace_year_number(item, items, members_by_dimension, changes):
                    if not members_by_dimension.get(key):
                        members_by_dimension.pop(key, None)
                    continue
                level = self.level_for(item.get("member_name"), item.get("hierarchy_scope"))
                if level is None:
                    continue
                items.remove(item)
                target = members_by_dimension.setdefault(level.dimension, [])
                plain = [other for other in target if is_member(other) and self.level_for(
                    other.get("member_name"), "") is None]
                if plain:
                    # "each state of the Central region": the level applies to the member it is limited to.
                    for other in plain:
                        other["hierarchy_scope"] = level.scope
                        other[FIXED] = item.get("member_name")
                    changes.append("%s -> %s %s" % (item.get("member_name"), "/".join(other["member_name"]
                                                                                    for other in plain), level.scope))
                else:
                    target.append({"member_name": level.dimension, "hierarchy_scope": level.scope,
                                   FIXED: item.get("member_name")})
                    changes.append("%s -> %s %s" % (item.get("member_name"), level.dimension, level.scope))
                if not members_by_dimension.get(key):
                    members_by_dimension.pop(key, None)


    def replace_phrase(self, item, items, members_by_dimension, changes):
        """Replaces a member named by a phrase of the cube's file ("second half") with the members it stands for.
        Returns whether it did."""
        keys = phrase_keys(item.get("member_name"), adverbs=False)
        phrase = next((phrase for phrase in self.phrases if keys and (keys[0] == phrase
                                                                      or keys[0].startswith(phrase + " "))), None)
        if phrase is None:
            return False
        names = self.phrases[phrase]
        dimension = self.members.get(names[0].lower()) or next(
            (key for key, value in members_by_dimension.items() if value is items), None)
        items.remove(item)
        target = members_by_dimension.setdefault(dimension, [])
        for name in names:
            if not any(is_member(other) and (other.get("member_name") or "").lower() == name.lower()
                       for other in target):
                target.append({"member_name": name, "hierarchy_scope": ""})
        changes.append("%s -> %s" % (item.get("member_name"), ", ".join(names)))
        return True

    def replace_year_number(self, item, items, members_by_dimension, changes):
        """Replaces a year number that is not a member ("2026" for "this year") with the time dimension. Returns whether
        it did."""
        name = (item.get("member_name") or "").strip()
        if not self.time_dimension or self.has_year_members or not YEAR_NUMBER.match(name) or name.lower() in self.members:
            return False
        items.remove(item)
        target = members_by_dimension.setdefault(self.time_dimension, [])
        if not any(is_member(other) and (other.get("member_name") or "").lower() == self.time_dimension.lower()
                   for other in target):
            target.append({"member_name": self.time_dimension, "hierarchy_scope": ""})
        changes.append("%s -> %s (no such member)" % (name, self.time_dimension))
        return True


def is_member(item):
    return isinstance(item, dict) and "member_name" in item


def drop_empty_operators(operator, changes):
    """Turns an operator with nothing to operate on ("sum" with no operands) into "none": Essbase builds no query from
    it, and its own prompt says the operands of any other operator must not be empty."""
    if not isinstance(operator, dict):
        return
    name = (operator.get("operator") or "").lower()
    if name not in ("", "none") and not [operand for operand in operator.get("operands") or [] if operand is not None]:
        operator["operator"] = "none"
        operator["operands"] = []
        operator["sort"] = {}
        changes.append("%s with no operands -> none" % name)


def normalized_words(text):
    """Returns text as space-padded lower-case words, with "%", "percentage" and "pct" all written "percent", so that
    "variance percent" in a question matches the member "Variance %"."""
    text = (text or "").lower().replace("%", " percent ")
    words = ["percent" if word in ("percentage", "pct") else word for word in re.findall(r"[a-z0-9]+", text)]
    return " %s " % " ".join(words)


def join_names(names):
    """Joins names as English does: "A", "A and B", "A, B, and C"."""
    if len(names) < 3:
        return " and ".join(names)
    return ", ".join(names[:-1]) + ", and " + names[-1]


def all_operators(operator):
    """Yields every operator object in an answer, the answer itself first, at any depth."""
    if not isinstance(operator, dict):
        return
    yield operator
    for key in ("POV", "sort"):
        for items in (operator.get(key) or {}).values() if isinstance(operator.get(key), dict) else []:
            for item in items if isinstance(items, list) else []:
                if isinstance(item, dict) and not is_member(item):
                    yield from all_operators(item)
    for operand in operator.get("operands") or []:
        if isinstance(operand, dict) and not is_member(operand):
            yield from all_operators(operand)


def all_members(operator):
    """Yields every member object in an answer: in its POV, its sort and its operands, at any depth."""
    if not isinstance(operator, dict):
        return
    for key in ("POV", "sort"):
        for items in (operator.get(key) or {}).values() if isinstance(operator.get(key), dict) else []:
            for item in items if isinstance(items, list) else []:
                if is_member(item):
                    yield item
                else:
                    yield from all_members(item)
    for operand in operator.get("operands") or []:
        if is_member(operand):
            yield operand
        elif isinstance(operand, dict):
            yield from all_members(operand)


def unit(vector):
    length = math.sqrt(sum(value * value for value in vector)) or 1.0
    return [value / length for value in vector]


_cache = {}
_lock = threading.Lock()


def known_dimensions():
    """Returns the names of the dimensions of every cube whose outline has been read."""
    with _lock:
        return {name for hints in _cache.values() for name, _, _ in hints.dimensions}


def cube_hints(application, cube, hints_dir, embed):
    """Returns the hints for a cube, reading its outline when it has not been read lately. Returns None when Essbase
    cannot be asked."""
    key = (application.lower(), cube.lower())
    with _lock:
        hints = _cache.get(key)
        if hints is not None and time.time() - hints.read_at < CACHE_SECONDS:
            return hints
    try:
        hints = CubeHints(application, cube, hints_dir, embed)
    except Exception as error:  # the outline is help, not a requirement
        print("Could not read the outline of %s.%s: %r" % (application, cube, error), flush=True)
        return None
    print("Read the outline of %s.%s: levels %s" % (application, cube, hints.levels), flush=True)
    with _lock:
        _cache[key] = hints
    return hints
