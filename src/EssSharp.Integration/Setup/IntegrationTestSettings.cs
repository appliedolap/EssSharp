using System;
using System.Collections.Generic;

namespace EssSharp.Integration.Setup
{
    /// <summary />
    public class IntegrationTestSettings
    {
        /// <summary />
        public IntegrationTestAiQuerySettings AiQuery { get; set; }

        /// <summary>
        /// Whether, and how, the server under test accepts OAuth bearer tokens, for the OAuth tests.
        /// </summary>
        public IntegrationTestOAuthSettings OAuth { get; set; }

        /// <summary />
        public IntegrationTestSettingsConnection[] Connections { get; set; }

        /// <summary>
        /// The Essbase images available to the harness. The first entry is the default image, and is the one the
        /// harness builds a container from.
        /// </summary>
        public string[] Images { get; set; }

        /// <summary>
        /// Per-image overrides describing how a given image differs from the Oracle Essbase 21.x container contract.
        /// </summary>
        /// <remarks>
        /// An image whose contract matches 21.x needs no entry here. Entries are applied in the order declared, so a
        /// broad entry may be followed by a narrower one.
        /// </remarks>
        public IntegrationTestSettingsImage[] ImageSettings { get; set; }
    }

    /// <summary>
    /// Describes how a particular Essbase image differs from the harness defaults.
    /// </summary>
    /// <remarks>
    /// An entry is matched by <see cref="Image" /> or by <see cref="VersionPrefix" />. An entry that declares neither
    /// matches every image, and so acts as a global default. Every remaining property is optional; a property left
    /// unset leaves the corresponding default in place.
    /// </remarks>
    public class IntegrationTestSettingsImage
    {
        /// <summary>
        /// Matches a single image by its full reference, such as <c>appliedolap/essbase:21.7-latest</c>.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Matches every image whose parsed version begins with this value, such as <c>26</c> or <c>21.7</c>.
        /// </summary>
        public string VersionPrefix { get; set; }

        /// <summary>
        /// The name to give the Essbase container, instead of the one derived from the image (<c>essbase-26-1</c> for
        /// <c>appliedolap/essbase:26.1-latest</c>). A running container of the resulting name is reused.
        /// </summary>
        public string ContainerName { get; set; }

        /// <summary>
        /// The command used to start the Essbase container.
        /// </summary>
        public string StartupCommand { get; set; }

        /// <summary>
        /// The container port the Essbase managed server listens on.
        /// </summary>
        public ushort? ManagedServerPort { get; set; }

        /// <summary>
        /// The docker network shared by the Essbase container and its repository database container.
        /// </summary>
        public string Network { get; set; }

        /// <summary>
        /// The repository database type requested of the Essbase container.
        /// </summary>
        public string DatabaseType { get; set; }

        /// <summary>
        /// The repository database image used to back the Essbase container.
        /// </summary>
        public string DatabaseImage { get; set; }

        /// <summary>
        /// The repository database name.
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// The repository database administrator username.
        /// </summary>
        public string DatabaseAdminUsername { get; set; }

        /// <summary>
        /// The repository database administrator password.
        /// </summary>
        public string DatabaseAdminPassword { get; set; }

        /// <summary>
        /// The repository database port.
        /// </summary>
        public ushort? DatabasePort { get; set; }

        /// <summary>
        /// The number of seconds the Essbase container waits for its repository database.
        /// </summary>
        public string DatabaseWaitTimeout { get; set; }

        /// <summary>
        /// The schema prefix the Essbase container uses when it creates its repository schemas.
        /// </summary>
        public string DatabaseSchemaPrefix { get; set; }

        /// <summary>
        /// Essbase registry settings to force after the harness starts a container, keyed by registry entry name -
        /// for example <c>AI_MDX</c> to <c>TRUE</c>. Unsupported, and only appropriate for a disposable container.
        /// </summary>
        public Dictionary<string, string> RegistrySettings { get; set; }

        /// <summary>
        /// Additional environment variables applied to the Essbase container.
        /// </summary>
        public Dictionary<string, string> Environment { get; set; }

        /// <summary>
        /// Commands executed inside the Essbase container after the harness starts it, in the order given.
        /// </summary>
        public List<string> SetupCommands { get; set; }

        /// <summary>
        /// Returns whether this entry applies to the given profile.
        /// </summary>
        /// <param name="profile" />
        internal bool Matches( EssbaseContainerProfile profile )
        {
            if ( Image is { Length: > 0 } image )
                return string.Equals(image, profile?.Image, StringComparison.OrdinalIgnoreCase);

            if ( VersionPrefix is { Length: > 0 } prefix )
                return profile?.Version?.ToString()?.StartsWith(prefix, StringComparison.OrdinalIgnoreCase) is true;

            // An entry that declares no matching criteria applies to every image.
            return true;
        }
    }

    /// <summary>
    /// How the environment the AI query tests need - an Essbase AI connection - is provided.
    /// </summary>
    public enum IntegrationTestAiQueryMode
    {
        /// <summary>
        /// The harness builds the environment itself: an Oracle AI Database container, and the AI gateway beside it,
        /// which answers the database's model calls with canned answers, so no OCI tenancy is needed. The gateway then
        /// configures Essbase to use them.
        /// </summary>
        Managed,

        /// <summary>
        /// The Essbase server already has an AI connection associated with the application - for instance one backed
        /// by a real OCI tenancy - and the harness leaves it alone.
        /// </summary>
        External,

        /// <summary>
        /// The AI query tests are skipped.
        /// </summary>
        Disabled
    }

    /// <summary />
    public class IntegrationTestAiQuerySettings
    {
        /// <summary>
        /// How the environment the AI query tests need is provided. Defaults to <see cref="IntegrationTestAiQueryMode.Managed" />.
        /// </summary>
        public IntegrationTestAiQueryMode Mode { get; set; } = IntegrationTestAiQueryMode.Managed;

        /// <summary>
        /// Whether, in <see cref="IntegrationTestAiQueryMode.Managed" /> mode, the environment is left in place when the run
        /// ends. By default what the run created is removed: the AI connection it set up in Essbase is taken out, then
        /// the containers and the network it created are removed. Containers, a network and an AI connection that were
        /// already there, for example started by a separate tool, are adopted and never removed, whatever this setting
        /// says. Keeping the environment makes the next run quicker, because the database and the vectors are already
        /// there, but it leaves a database running and an AI connection in Essbase until they are removed by hand.
        /// </summary>
        public bool KeepEnvironment { get; set; }

        /// <summary>
        /// The Oracle AI Database image used in <see cref="IntegrationTestAiQueryMode.Managed" /> mode. It must be the
        /// full flavor: the slim flavor has no Oracle Text, so DBMS_VECTOR_CHAIN, which Essbase calls, does not exist.
        /// </summary>
        public string DatabaseImage { get; set; } = "gvenzl/oracle-free:23.26.3-full-faststart";

        /// <summary>
        /// The image the AI gateway runs in, in <see cref="IntegrationTestAiQueryMode.Managed" /> mode.
        /// </summary>
        public string GatewayImage { get; set; } = "python:3.12-slim";

        /// <summary>
        /// The region of the inference endpoint the AI gateway answers for.
        /// </summary>
        public string Region { get; set; } = "us-chicago-1";

        /// <summary>
        /// The chat model named in the AI connection.
        /// </summary>
        public string ChatModel { get; set; } = "cohere.command-a-03-2025";

        /// <summary>
        /// The embedding model named in the AI connection.
        /// </summary>
        public string EmbedModel { get; set; } = "cohere.embed-english-v3.0";

        /// <summary />
        public string ApplicationName { get; set; } = "Sample";

        /// <summary />
        public string CubeName { get; set; } = "Basic";

        /// <summary />
        public string NaturalLanguageQuery { get; set; } = "Show actual sales by year.";

        /// <summary>
        /// A prefix for the AI profile names the tests create. Each test appends a unique suffix, because Essbase
        /// creates a database profile for every conversation and will not replace an existing one. The prefix must be
        /// a valid database identifier, so it cannot contain a hyphen.
        /// </summary>
        public string ProfileName { get; set; }
    }

    /// <summary>
    /// How the identity service whose bearer tokens the OAuth tests send to Essbase is provided.
    /// </summary>
    public enum IntegrationTestOAuthMode
    {
        /// <summary>
        /// The harness provides it: the Essbase container is created in Identity Cloud mode with the harness's OCI
        /// gateway as its Identity Cloud tenant, wired as the scoop <c>essbase</c> stack wires it, and the tests take
        /// their tokens from that gateway. An Essbase container that was already running must have been created that
        /// way (for example by the scoop stack with <c>essbase identity idcs</c>); otherwise the tests are skipped.
        /// </summary>
        Managed,

        /// <summary>
        /// The Essbase server already accepts the bearer tokens of an identity service, whose token endpoint
        /// <see cref="IntegrationTestOAuthSettings.TokenUrl" /> names, and the harness leaves both alone.
        /// </summary>
        External,

        /// <summary>
        /// The OAuth tests are skipped.
        /// </summary>
        Disabled
    }

    /// <summary />
    public class IntegrationTestOAuthSettings
    {
        /// <summary>
        /// How the identity service is provided. Defaults to <see cref="IntegrationTestOAuthMode.Managed" />.
        /// </summary>
        public IntegrationTestOAuthMode Mode { get; set; } = IntegrationTestOAuthMode.Managed;

        /// <summary>
        /// The identity service's token endpoint, in <see cref="IntegrationTestOAuthMode.External" /> mode. In
        /// <see cref="IntegrationTestOAuthMode.Managed" /> mode it is the harness's gateway's.
        /// </summary>
        public string TokenUrl { get; set; }

        /// <summary>
        /// The OAuth client the tests ask tokens as. The harness's identity service knows <c>esssharp</c> as a public
        /// client, with no secret.
        /// </summary>
        public string ClientId { get; set; } = "esssharp";

        /// <summary />
        public string ClientSecret { get; set; } = "";

        /// <summary>
        /// The scope to ask for. The harness's identity service does not check it; a real Identity Cloud tenant wants
        /// the Essbase scope of the tenant.
        /// </summary>
        public string Scope { get; set; } = "openid";

        /// <summary>
        /// The identity users the tests sign in as, with the Essbase role each is given before the tests run. One must
        /// hold the ServiceAdministrator role and one the User role. They are the users of the harness's identity service
        /// (<c>Scripts/Oci/gateway/users.json</c>), which the scoop stack's identity service also knows by default.
        /// </summary>
        public IntegrationTestOAuthUser[] Users { get; set; } =
        {
            new IntegrationTestOAuthUser() { Username = "alice", Password = "Password1", Role = EssServerRole.ServiceAdministrator },
            new IntegrationTestOAuthUser() { Username = "bob",   Password = "Password1", Role = EssServerRole.User, Applications = new Dictionary<string, string>() { ["Sample"] = "db_access" } },
            new IntegrationTestOAuthUser() { Username = "carol", Password = "Password1", Role = EssServerRole.Unkown }
        };
    }

    /// <summary>
    /// A user of the identity service, and the Essbase roles the tests expect it to hold.
    /// </summary>
    public class IntegrationTestOAuthUser
    {
        /// <summary />
        public string Username { get; set; }

        /// <summary />
        public string Password { get; set; }

        /// <summary>
        /// The service role the user is given; <see cref="EssServerRole.Unkown" /> for a user that is given none.
        /// </summary>
        public EssServerRole Role { get; set; }

        /// <summary>
        /// The role the user is given on each named application, such as <c>db_access</c> on <c>Sample</c>.
        /// </summary>
        public Dictionary<string, string> Applications { get; set; }
    }

    /// <summary />
    public class IntegrationTestSettingsConnection : ICloneable
    {
        /// <summary />
        public string Server { get; set; }

        /// <summary />
        public string Username { get; set; }

        /// <summary />
        public string Password { get; set; }

        /// <summary />
        public string AccessToken { get; set; }

        /// <summary />
        public EssServerRole Role { get; set; }

        /// <inheritdoc />
        public object Clone() => new IntegrationTestSettingsConnection()
        {
            Server      = this.Server,
            AccessToken = this.AccessToken,
            Username    = this.Username,
            Password    = this.Password,
            Role        = this.Role
        };
    }

    /// <summary />
    public enum Role
    {
        /// <summary />
        ServiceAdministrator,

        /// <summary />
        PowerUser,

        /// <summary />
        User
    }
}
