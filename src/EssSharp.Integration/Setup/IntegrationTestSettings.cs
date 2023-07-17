namespace EssSharp.Integration.Setup
{
    /// <summary />
    public class IntegrationTestSettings
    {
        /// <summary />
        public IntegrationTestSettingsConnection[] Connections { get; set; }

        /// <summary />
        public string[] Images { get; set; }
    }

    /// <summary />
    public class IntegrationTestSettingsConnection
    {
        /// <summary />
        public string Server { get; set; }
        /// <summary />
        public string Username { get; set; }
        /// <summary />
        public string Password { get; set; }
        /// <summary />
        public EssUserRole Role { get; set; }
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
