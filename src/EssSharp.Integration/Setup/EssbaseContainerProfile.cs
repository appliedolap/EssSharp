using System;
using System.Collections.Generic;
using System.Linq;

namespace EssSharp.Integration.Setup
{
    /// <summary>
    /// Describes the version-varying details required to build and configure an Essbase test container.
    /// </summary>
    /// <remarks>
    /// Every value defaults to the contract published by the Oracle Essbase 21.x container images, which is the
    /// behavior the harness has always used. A new image whose contract differs - such as a 26 image that exposes a
    /// different port, startup command, or repository database - is accommodated by adding an entry to the
    /// <c>ImageSettings</c> section of the test settings rather than by editing the harness.
    /// </remarks>
    public class EssbaseContainerProfile
    {
        #region Defaults

        /// <summary>
        /// The startup command published by the Oracle Essbase container images.
        /// </summary>
        public const string DefaultStartupCommand = @"/u01/container-scripts/createAndStartDomain.sh";

        /// <summary>
        /// The managed server port published by the Oracle Essbase container images.
        /// </summary>
        public const ushort DefaultManagedServerPort = 9000;

        /// <summary>
        /// The docker network shared by the Essbase container and its repository database container.
        /// </summary>
        public const string DefaultNetwork = @"standalone";

        /// <summary>
        /// The repository database type requested of the Essbase container.
        /// </summary>
        public const string DefaultDatabaseType = @"sqlserver";

        /// <summary>
        /// The repository database image used to back the Essbase container.
        /// </summary>
        public const string DefaultDatabaseImage = @"mcr.microsoft.com/mssql/server:2022-latest";

        /// <summary>
        /// The repository database name created by the database container setup scripts.
        /// </summary>
        public const string DefaultDatabaseName = @"CertDB";

        /// <summary>
        /// The repository database administrator username.
        /// </summary>
        public const string DefaultDatabaseAdminUsername = @"sa";

        /// <summary>
        /// The repository database administrator password.
        /// </summary>
        public const string DefaultDatabaseAdminPassword = @"StrongPassw0rd";

        /// <summary>
        /// The repository database port.
        /// </summary>
        public const ushort DefaultDatabasePort = 1433;

        /// <summary>
        /// The number of seconds the Essbase container waits for its repository database.
        /// </summary>
        public const string DefaultDatabaseWaitTimeout = @"240";

        #endregion

        #region Public Properties

        /// <summary>
        /// The fully qualified image reference this profile was resolved for.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// The version parsed from <see cref="Image" />, or <see langword="null" /> when the tag carries no version.
        /// </summary>
        public Version Version { get; set; }

        /// <summary>
        /// The name given to the Essbase container.
        /// </summary>
        public string ContainerName { get; set; }

        /// <summary>
        /// The command used to start the Essbase container.
        /// </summary>
        public string StartupCommand { get; set; } = DefaultStartupCommand;

        /// <summary>
        /// The container port the Essbase managed server listens on.
        /// </summary>
        public ushort ManagedServerPort { get; set; } = DefaultManagedServerPort;

        /// <summary>
        /// The docker network shared by the Essbase container and its repository database container.
        /// </summary>
        public string Network { get; set; } = DefaultNetwork;

        /// <summary>
        /// The repository database type requested of the Essbase container.
        /// </summary>
        public string DatabaseType { get; set; } = DefaultDatabaseType;

        /// <summary>
        /// The repository database image used to back the Essbase container.
        /// </summary>
        public string DatabaseImage { get; set; } = DefaultDatabaseImage;

        /// <summary>
        /// The repository database name.
        /// </summary>
        public string DatabaseName { get; set; } = DefaultDatabaseName;

        /// <summary>
        /// The repository database administrator username.
        /// </summary>
        public string DatabaseAdminUsername { get; set; } = DefaultDatabaseAdminUsername;

        /// <summary>
        /// The repository database administrator password.
        /// </summary>
        public string DatabaseAdminPassword { get; set; } = DefaultDatabaseAdminPassword;

        /// <summary>
        /// The repository database port.
        /// </summary>
        public ushort DatabasePort { get; set; } = DefaultDatabasePort;

        /// <summary>
        /// The number of seconds the Essbase container waits for its repository database.
        /// </summary>
        public string DatabaseWaitTimeout { get; set; } = DefaultDatabaseWaitTimeout;

        /// <summary>
        /// Additional environment variables applied to the Essbase container, which override any variable the
        /// harness sets by the same name.
        /// </summary>
        public Dictionary<string, string> Environment { get; set; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Commands executed inside the Essbase container after the harness starts it, in the order given.
        /// </summary>
        /// <remarks>
        /// Each entry is run through <c>/bin/bash -lc</c>, so an entry may be any shell expression the image supports.
        /// Commands are only run against a container the harness started; a container that was already running is
        /// assumed to have been configured already.
        /// </remarks>
        public List<string> SetupCommands { get; set; } = new List<string>();

        /// <summary>
        /// The name of the repository database container, derived from <see cref="ContainerName" />.
        /// </summary>
        public string DatabaseContainerName => $@"{ContainerName}-database";

        /// <summary>
        /// The connect string the Essbase container uses to reach its repository database.
        /// </summary>
        public string DatabaseConnectString => $@"{DatabaseContainerName}:{DatabasePort}:{DatabaseName}";

        #endregion

        #region Public Methods

        /// <summary>
        /// Resolves the profile for the given image, applying any matching overrides from the test settings.
        /// </summary>
        /// <param name="image">The image reference to resolve a profile for.</param>
        /// <param name="overrides">The image settings declared by the test settings, if any.</param>
        /// <remarks>
        /// Overrides are applied in the order declared, so a broad entry may be followed by a narrower one. An entry
        /// with neither an <c>Image</c> nor a <c>VersionPrefix</c> matches every image and serves as a global default.
        /// </remarks>
        public static EssbaseContainerProfile Resolve( string image, IEnumerable<IntegrationTestSettingsImage> overrides = null )
        {
            var profile = new EssbaseContainerProfile()
            {
                Image         = image,
                Version       = ParseVersion(image),
                ContainerName = IntegrationTestFactory.GetContainerNameForImage(image)
            };

            foreach ( var setting in overrides ?? Enumerable.Empty<IntegrationTestSettingsImage>() )
                if ( setting?.Matches(profile) is true )
                    profile.Apply(setting);

            return profile;
        }

        /// <summary>
        /// Parses the version carried by an image tag, or returns <see langword="null" /> when the tag carries none.
        /// </summary>
        /// <param name="image">The image reference to parse.</param>
        /// <remarks>
        /// Tags are expected to lead with a dotted version and may carry a trailing qualifier, so <c>21.7-latest</c>,
        /// <c>21.8.2-dev</c>, and <c>26</c> all parse. A tag with no leading digits - such as <c>latest</c> - yields
        /// <see langword="null" />.
        /// </remarks>
        public static Version ParseVersion( string image )
        {
            // Reduce the image reference to its tag, guarding against a registry host that carries a port.
            if ( image?.Split('/').LastOrDefault() is not { Length: > 0 } repoAndTag )
                return null;

            if ( repoAndTag.Split(':') is not { Length: > 1 } segments )
                return null;

            // Take the leading run of version characters, discarding any trailing qualifier.
            var number = new string(segments.Last().TakeWhile(c => char.IsDigit(c) || c == '.').ToArray()).Trim('.');

            if ( number is not { Length: > 0 } )
                return null;

            // Version.TryParse requires at least a major and a minor component.
            if ( !number.Contains('.') )
                number += ".0";

            return Version.TryParse(number, out var version) ? version : null;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Applies the non-null values of the given override to this profile.
        /// </summary>
        /// <param name="setting" />
        private void Apply( IntegrationTestSettingsImage setting )
        {
            if ( setting.StartupCommand is { Length: > 0 } startupCommand )
                StartupCommand = startupCommand;

            if ( setting.ManagedServerPort is { } managedServerPort )
                ManagedServerPort = managedServerPort;

            if ( setting.Network is { Length: > 0 } network )
                Network = network;

            if ( setting.DatabaseType is { Length: > 0 } databaseType )
                DatabaseType = databaseType;

            if ( setting.DatabaseImage is { Length: > 0 } databaseImage )
                DatabaseImage = databaseImage;

            if ( setting.DatabaseName is { Length: > 0 } databaseName )
                DatabaseName = databaseName;

            if ( setting.DatabaseAdminUsername is { Length: > 0 } databaseAdminUsername )
                DatabaseAdminUsername = databaseAdminUsername;

            if ( setting.DatabaseAdminPassword is { Length: > 0 } databaseAdminPassword )
                DatabaseAdminPassword = databaseAdminPassword;

            if ( setting.DatabasePort is { } databasePort )
                DatabasePort = databasePort;

            if ( setting.DatabaseWaitTimeout is { Length: > 0 } databaseWaitTimeout )
                DatabaseWaitTimeout = databaseWaitTimeout;

            foreach ( var variable in setting.Environment ?? new Dictionary<string, string>() )
                Environment[variable.Key] = variable.Value;

            if ( setting.SetupCommands is { Count: > 0 } setupCommands )
                SetupCommands.AddRange(setupCommands);
        }

        #endregion
    }
}
