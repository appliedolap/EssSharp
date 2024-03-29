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
    /// LayoutHeader
    /// </summary>
    [DataContract(Name = "LayoutHeader")]
    public partial class LayoutHeader : IEquatable<LayoutHeader>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LayoutHeader" /> class.
        /// </summary>
        /// <param name="name">name.</param>
        /// <param name="user">user.</param>
        /// <param name="databaseDefault">databaseDefault.</param>
        /// <param name="userDefault">userDefault.</param>
        /// <param name="session">session.</param>
        public LayoutHeader(string name = default(string), string user = default(string), bool databaseDefault = default(bool), bool userDefault = default(bool), bool session = default(bool))
        {
            this.Name = name;
            this.User = user;
            this.DatabaseDefault = databaseDefault;
            this.UserDefault = userDefault;
            this.Session = session;
        }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets User
        /// </summary>
        [DataMember(Name = "user", EmitDefaultValue = false)]
        public string User { get; set; }

        /// <summary>
        /// Gets or Sets DatabaseDefault
        /// </summary>
        [DataMember(Name = "databaseDefault", EmitDefaultValue = true)]
        public bool DatabaseDefault { get; set; }

        /// <summary>
        /// Gets or Sets UserDefault
        /// </summary>
        [DataMember(Name = "userDefault", EmitDefaultValue = true)]
        public bool UserDefault { get; set; }

        /// <summary>
        /// Gets or Sets Session
        /// </summary>
        [DataMember(Name = "session", EmitDefaultValue = true)]
        public bool Session { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class LayoutHeader {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  User: ").Append(User).Append("\n");
            sb.Append("  DatabaseDefault: ").Append(DatabaseDefault).Append("\n");
            sb.Append("  UserDefault: ").Append(UserDefault).Append("\n");
            sb.Append("  Session: ").Append(Session).Append("\n");
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
            return this.Equals(input as LayoutHeader);
        }

        /// <summary>
        /// Returns true if LayoutHeader instances are equal
        /// </summary>
        /// <param name="input">Instance of LayoutHeader to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(LayoutHeader input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.Name == input.Name ||
                    (this.Name != null &&
                    this.Name.Equals(input.Name))
                ) && 
                (
                    this.User == input.User ||
                    (this.User != null &&
                    this.User.Equals(input.User))
                ) && 
                (
                    this.DatabaseDefault == input.DatabaseDefault ||
                    this.DatabaseDefault.Equals(input.DatabaseDefault)
                ) && 
                (
                    this.UserDefault == input.UserDefault ||
                    this.UserDefault.Equals(input.UserDefault)
                ) && 
                (
                    this.Session == input.Session ||
                    this.Session.Equals(input.Session)
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
                if (this.Name != null)
                {
                    hashCode = (hashCode * 59) + this.Name.GetHashCode();
                }
                if (this.User != null)
                {
                    hashCode = (hashCode * 59) + this.User.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.DatabaseDefault.GetHashCode();
                hashCode = (hashCode * 59) + this.UserDefault.GetHashCode();
                hashCode = (hashCode * 59) + this.Session.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}
