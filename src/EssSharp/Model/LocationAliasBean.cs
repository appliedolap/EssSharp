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
    /// LocationAliasBean
    /// </summary>
    [DataContract(Name = "LocationAliasBean")]
    public partial class LocationAliasBean : IEquatable<LocationAliasBean>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocationAliasBean" /> class.
        /// </summary>
        /// <param name="aliasName">aliasName.</param>
        /// <param name="connectionName">connectionName.</param>
        /// <param name="serverName">serverName.</param>
        /// <param name="userName">userName.</param>
        /// <param name="applicationName">applicationName.</param>
        /// <param name="databaseName">databaseName.</param>
        /// <param name="applicationLevelConnection">applicationLevelConnection.</param>
        /// <param name="links">links.</param>
        public LocationAliasBean(string aliasName = default(string), string connectionName = default(string), string serverName = default(string), string userName = default(string), string applicationName = default(string), string databaseName = default(string), bool applicationLevelConnection = default(bool), List<Link> links = default(List<Link>))
        {
            this.AliasName = aliasName;
            this.ConnectionName = connectionName;
            this.ServerName = serverName;
            this.UserName = userName;
            this.ApplicationName = applicationName;
            this.DatabaseName = databaseName;
            this.ApplicationLevelConnection = applicationLevelConnection;
            this.Links = links;
        }

        /// <summary>
        /// Gets or Sets AliasName
        /// </summary>
        [DataMember(Name = "aliasName", EmitDefaultValue = false)]
        public string AliasName { get; set; }

        /// <summary>
        /// Gets or Sets ConnectionName
        /// </summary>
        [DataMember(Name = "connectionName", EmitDefaultValue = false)]
        public string ConnectionName { get; set; }

        /// <summary>
        /// Gets or Sets ServerName
        /// </summary>
        [DataMember(Name = "serverName", EmitDefaultValue = false)]
        public string ServerName { get; set; }

        /// <summary>
        /// Gets or Sets UserName
        /// </summary>
        [DataMember(Name = "userName", EmitDefaultValue = false)]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or Sets ApplicationName
        /// </summary>
        [DataMember(Name = "applicationName", EmitDefaultValue = false)]
        public string ApplicationName { get; set; }

        /// <summary>
        /// Gets or Sets DatabaseName
        /// </summary>
        [DataMember(Name = "databaseName", EmitDefaultValue = false)]
        public string DatabaseName { get; set; }

        /// <summary>
        /// Gets or Sets ApplicationLevelConnection
        /// </summary>
        [DataMember(Name = "applicationLevelConnection", EmitDefaultValue = true)]
        public bool ApplicationLevelConnection { get; set; }

        /// <summary>
        /// Gets or Sets Links
        /// </summary>
        [DataMember(Name = "links", EmitDefaultValue = false)]
        public List<Link> Links { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class LocationAliasBean {\n");
            sb.Append("  AliasName: ").Append(AliasName).Append("\n");
            sb.Append("  ConnectionName: ").Append(ConnectionName).Append("\n");
            sb.Append("  ServerName: ").Append(ServerName).Append("\n");
            sb.Append("  UserName: ").Append(UserName).Append("\n");
            sb.Append("  ApplicationName: ").Append(ApplicationName).Append("\n");
            sb.Append("  DatabaseName: ").Append(DatabaseName).Append("\n");
            sb.Append("  ApplicationLevelConnection: ").Append(ApplicationLevelConnection).Append("\n");
            sb.Append("  Links: ").Append(Links).Append("\n");
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
            return this.Equals(input as LocationAliasBean);
        }

        /// <summary>
        /// Returns true if LocationAliasBean instances are equal
        /// </summary>
        /// <param name="input">Instance of LocationAliasBean to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(LocationAliasBean input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.AliasName == input.AliasName ||
                    (this.AliasName != null &&
                    this.AliasName.Equals(input.AliasName))
                ) && 
                (
                    this.ConnectionName == input.ConnectionName ||
                    (this.ConnectionName != null &&
                    this.ConnectionName.Equals(input.ConnectionName))
                ) && 
                (
                    this.ServerName == input.ServerName ||
                    (this.ServerName != null &&
                    this.ServerName.Equals(input.ServerName))
                ) && 
                (
                    this.UserName == input.UserName ||
                    (this.UserName != null &&
                    this.UserName.Equals(input.UserName))
                ) && 
                (
                    this.ApplicationName == input.ApplicationName ||
                    (this.ApplicationName != null &&
                    this.ApplicationName.Equals(input.ApplicationName))
                ) && 
                (
                    this.DatabaseName == input.DatabaseName ||
                    (this.DatabaseName != null &&
                    this.DatabaseName.Equals(input.DatabaseName))
                ) && 
                (
                    this.ApplicationLevelConnection == input.ApplicationLevelConnection ||
                    this.ApplicationLevelConnection.Equals(input.ApplicationLevelConnection)
                ) && 
                (
                    this.Links == input.Links ||
                    this.Links != null &&
                    input.Links != null &&
                    this.Links.SequenceEqual(input.Links)
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
                if (this.AliasName != null)
                {
                    hashCode = (hashCode * 59) + this.AliasName.GetHashCode();
                }
                if (this.ConnectionName != null)
                {
                    hashCode = (hashCode * 59) + this.ConnectionName.GetHashCode();
                }
                if (this.ServerName != null)
                {
                    hashCode = (hashCode * 59) + this.ServerName.GetHashCode();
                }
                if (this.UserName != null)
                {
                    hashCode = (hashCode * 59) + this.UserName.GetHashCode();
                }
                if (this.ApplicationName != null)
                {
                    hashCode = (hashCode * 59) + this.ApplicationName.GetHashCode();
                }
                if (this.DatabaseName != null)
                {
                    hashCode = (hashCode * 59) + this.DatabaseName.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.ApplicationLevelConnection.GetHashCode();
                if (this.Links != null)
                {
                    hashCode = (hashCode * 59) + this.Links.GetHashCode();
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
