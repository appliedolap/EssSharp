using System;

namespace EssSharp.Integration.Setup
{
    /// <summary />
    public class IntegrationTestSettings
    {
        /// <summary />
        public IntegrationTestAiQuerySettings AiQuery { get; set; }

        /// <summary />
        public IntegrationTestSettingsConnection[] Connections { get; set; }

        /// <summary />
        public string[] Images { get; set; }
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
