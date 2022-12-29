/*
 * The REST API for Oracle Essbase enables you to automate management of Essbase resources and operations. All requests and responses are communicated over secured HTTP.
 *
 * The version of the OpenAPI document: 1.0
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using OpenAPIDateConverter = EssSharp.Client.OpenAPIDateConverter;

namespace EssSharp.Model
{
    /// <summary>
    /// GridRange
    /// </summary>
    [DataContract(Name = "GridRange")]
    public partial class GridRange : IEquatable<GridRange>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GridRange" /> class.
        /// </summary>
        /// <param name="statuses">statuses.</param>
        /// <param name="texts">texts.</param>
        /// <param name="enumIds">enumIds.</param>
        /// <param name="dataFormats">dataFormats.</param>
        /// <param name="filters">filters.</param>
        /// <param name="start">start.</param>
        /// <param name="types">types.</param>
        /// <param name="end">end.</param>
        /// <param name="values">values.</param>
        public GridRange(List<string> statuses = default(List<string>), List<string> texts = default(List<string>), List<string> enumIds = default(List<string>), List<string> dataFormats = default(List<string>), List<string> filters = default(List<string>), int start = default(int), List<string> types = default(List<string>), int end = default(int), List<string> values = default(List<string>))
        {
            this.Statuses = statuses;
            this.Texts = texts;
            this.EnumIds = enumIds;
            this.DataFormats = dataFormats;
            this.Filters = filters;
            this.Start = start;
            this.Types = types;
            this.End = end;
            this.Values = values;
        }

        /// <summary>
        /// Gets or Sets Statuses
        /// </summary>
        [DataMember(Name = "statuses", EmitDefaultValue = false)]
        public List<string> Statuses { get; set; }

        /// <summary>
        /// Gets or Sets Texts
        /// </summary>
        [DataMember(Name = "texts", EmitDefaultValue = false)]
        public List<string> Texts { get; set; }

        /// <summary>
        /// Gets or Sets EnumIds
        /// </summary>
        [DataMember(Name = "enumIds", EmitDefaultValue = false)]
        public List<string> EnumIds { get; set; }

        /// <summary>
        /// Gets or Sets DataFormats
        /// </summary>
        [DataMember(Name = "dataFormats", EmitDefaultValue = false)]
        public List<string> DataFormats { get; set; }

        /// <summary>
        /// Gets or Sets Filters
        /// </summary>
        [DataMember(Name = "filters", EmitDefaultValue = false)]
        public List<string> Filters { get; set; }

        /// <summary>
        /// Gets or Sets Start
        /// </summary>
        [DataMember(Name = "start", EmitDefaultValue = false)]
        public int Start { get; set; }

        /// <summary>
        /// Gets or Sets Types
        /// </summary>
        [DataMember(Name = "types", EmitDefaultValue = false)]
        public List<string> Types { get; set; }

        /// <summary>
        /// Gets or Sets End
        /// </summary>
        [DataMember(Name = "end", EmitDefaultValue = false)]
        public int End { get; set; }

        /// <summary>
        /// Gets or Sets Values
        /// </summary>
        [DataMember(Name = "values", EmitDefaultValue = false)]
        public List<string> Values { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class GridRange {\n");
            sb.Append("  Statuses: ").Append(Statuses).Append("\n");
            sb.Append("  Texts: ").Append(Texts).Append("\n");
            sb.Append("  EnumIds: ").Append(EnumIds).Append("\n");
            sb.Append("  DataFormats: ").Append(DataFormats).Append("\n");
            sb.Append("  Filters: ").Append(Filters).Append("\n");
            sb.Append("  Start: ").Append(Start).Append("\n");
            sb.Append("  Types: ").Append(Types).Append("\n");
            sb.Append("  End: ").Append(End).Append("\n");
            sb.Append("  Values: ").Append(Values).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as GridRange);
        }

        /// <summary>
        /// Returns true if GridRange instances are equal
        /// </summary>
        /// <param name="input">Instance of GridRange to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(GridRange input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.Statuses == input.Statuses ||
                    this.Statuses != null &&
                    input.Statuses != null &&
                    this.Statuses.SequenceEqual(input.Statuses)
                ) && 
                (
                    this.Texts == input.Texts ||
                    this.Texts != null &&
                    input.Texts != null &&
                    this.Texts.SequenceEqual(input.Texts)
                ) && 
                (
                    this.EnumIds == input.EnumIds ||
                    this.EnumIds != null &&
                    input.EnumIds != null &&
                    this.EnumIds.SequenceEqual(input.EnumIds)
                ) && 
                (
                    this.DataFormats == input.DataFormats ||
                    this.DataFormats != null &&
                    input.DataFormats != null &&
                    this.DataFormats.SequenceEqual(input.DataFormats)
                ) && 
                (
                    this.Filters == input.Filters ||
                    this.Filters != null &&
                    input.Filters != null &&
                    this.Filters.SequenceEqual(input.Filters)
                ) && 
                (
                    this.Start == input.Start ||
                    this.Start.Equals(input.Start)
                ) && 
                (
                    this.Types == input.Types ||
                    this.Types != null &&
                    input.Types != null &&
                    this.Types.SequenceEqual(input.Types)
                ) && 
                (
                    this.End == input.End ||
                    this.End.Equals(input.End)
                ) && 
                (
                    this.Values == input.Values ||
                    this.Values != null &&
                    input.Values != null &&
                    this.Values.SequenceEqual(input.Values)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.Statuses != null)
                {
                    hashCode = (hashCode * 59) + this.Statuses.GetHashCode();
                }
                if (this.Texts != null)
                {
                    hashCode = (hashCode * 59) + this.Texts.GetHashCode();
                }
                if (this.EnumIds != null)
                {
                    hashCode = (hashCode * 59) + this.EnumIds.GetHashCode();
                }
                if (this.DataFormats != null)
                {
                    hashCode = (hashCode * 59) + this.DataFormats.GetHashCode();
                }
                if (this.Filters != null)
                {
                    hashCode = (hashCode * 59) + this.Filters.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.Start.GetHashCode();
                if (this.Types != null)
                {
                    hashCode = (hashCode * 59) + this.Types.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.End.GetHashCode();
                if (this.Values != null)
                {
                    hashCode = (hashCode * 59) + this.Values.GetHashCode();
                }
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        public IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}
