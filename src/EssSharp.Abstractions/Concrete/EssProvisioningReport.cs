using System.Collections.Generic;

namespace EssSharp
{
    /// <summary>
    /// A role, filter or script that a user or group holds, and any groups it holds it through.
    /// </summary>
    public class EssProvisionedItem
    {
        /// <summary />
        /// <param name="name">The name of the role, filter or script.</param>
        /// <param name="inheritedFromGroups">The groups the user or group holds it through, if any.</param>
        public EssProvisionedItem( string name, List<string> inheritedFromGroups = null )
        {
            Name                = name;
            InheritedFromGroups = inheritedFromGroups ?? new List<string>();
        }

        /// <summary>
        /// The groups the user or group holds the role, filter or script through. Empty when it is held directly.
        /// </summary>
        public List<string> InheritedFromGroups { get; }

        /// <summary>
        /// The name of the role, filter or script.
        /// </summary>
        public string Name { get; }
    }

    /// <summary>
    /// The filters or scripts that a user or group holds in one cube of an application.
    /// </summary>
    public class EssDatabaseProvisioning
    {
        /// <summary />
        /// <param name="database">The name of the cube.</param>
        /// <param name="items">The filters or scripts held in the cube.</param>
        public EssDatabaseProvisioning( string database, List<EssProvisionedItem> items = null )
        {
            Database = database;
            Items    = items ?? new List<EssProvisionedItem>();
        }

        /// <summary>
        /// The name of the cube.
        /// </summary>
        public string Database { get; }

        /// <summary>
        /// The filters or scripts held in the cube.
        /// </summary>
        public List<EssProvisionedItem> Items { get; }
    }

    /// <summary>
    /// What a user or group holds in one application: roles, and filters and scripts by cube.
    /// </summary>
    public class EssApplicationProvisioning
    {
        /// <summary />
        /// <param name="name">The name of the application.</param>
        /// <param name="roles">The application roles held.</param>
        /// <param name="filters">The filters held, by cube.</param>
        /// <param name="scripts">The calculation scripts held, by cube.</param>
        public EssApplicationProvisioning( string name, List<EssProvisionedItem> roles = null, List<EssDatabaseProvisioning> filters = null, List<EssDatabaseProvisioning> scripts = null )
        {
            Name    = name;
            Roles   = roles   ?? new List<EssProvisionedItem>();
            Filters = filters ?? new List<EssDatabaseProvisioning>();
            Scripts = scripts ?? new List<EssDatabaseProvisioning>();
        }

        /// <summary>
        /// The filters held, by cube.
        /// </summary>
        public List<EssDatabaseProvisioning> Filters { get; }

        /// <summary>
        /// The name of the application.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The application roles held.
        /// </summary>
        public List<EssProvisionedItem> Roles { get; }

        /// <summary>
        /// The calculation scripts held, by cube.
        /// </summary>
        public List<EssDatabaseProvisioning> Scripts { get; }
    }

    /// <summary>
    /// A provisioning report: what a user or group holds on the server, and in each application.
    /// </summary>
    public class EssProvisioningReport
    {
        /// <summary />
        /// <param name="serviceRoles">The service roles held.</param>
        /// <param name="applications">What is held in each application.</param>
        public EssProvisioningReport( List<EssProvisionedItem> serviceRoles = null, List<EssApplicationProvisioning> applications = null )
        {
            ServiceRoles = serviceRoles ?? new List<EssProvisionedItem>();
            Applications = applications ?? new List<EssApplicationProvisioning>();
        }

        /// <summary>
        /// What is held in each application.
        /// </summary>
        public List<EssApplicationProvisioning> Applications { get; }

        /// <summary>
        /// The service roles held.
        /// </summary>
        public List<EssProvisionedItem> ServiceRoles { get; }
    }
}
