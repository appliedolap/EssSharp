using System;
using System.Collections.Generic;

namespace EssSharp.Integration.Setup
{
    /// <summary />
    public class IntegrationTestSettings
    {
        /// <summary />
        public IntegrationTestAiQuerySettings AiQuery { get; set; }

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

    /// <summary />
    public class IntegrationTestAiQuerySettings
    {
        /// <summary />
        public string ApplicationName { get; set; } = "Sample";

        /// <summary />
        public string CubeName { get; set; } = "Basic";

        /// <summary />
        public string NaturalLanguageQuery { get; set; } = "Show actual sales by year.";

        /// <summary />
        public string ProfileName { get; set; }
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
