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
    /// Cube
    /// </summary>
    [DataContract(Name = "Cube")]
    public partial class Cube : IEquatable<Cube>, IValidatableObject
    {
        /// <summary>
        /// Defines Type
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum TypeEnum
        {
            /// <summary>
            /// Enum ASO for value: ASO
            /// </summary>
            [EnumMember(Value = "ASO")]
            ASO = 1,

            /// <summary>
            /// Enum BSO for value: BSO
            /// </summary>
            [EnumMember(Value = "BSO")]
            BSO = 2,

            /// <summary>
            /// Enum CURRENCY for value: CURRENCY
            /// </summary>
            [EnumMember(Value = "CURRENCY")]
            CURRENCY = 3

        }


        /// <summary>
        /// Gets or Sets Type
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public TypeEnum? Type { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Cube" /> class.
        /// </summary>
        /// <param name="name">name.</param>
        /// <param name="application">application.</param>
        /// <param name="owner">owner.</param>
        /// <param name="creationTime">creationTime.</param>
        /// <param name="modifiedBy">modifiedBy.</param>
        /// <param name="modifiedTime">modifiedTime.</param>
        /// <param name="status">status.</param>
        /// <param name="startTime">startTime.</param>
        /// <param name="description">description.</param>
        /// <param name="type">type.</param>
        /// <param name="applicationRole">applicationRole.</param>
        /// <param name="easManagedApp">easManagedApp.</param>
        /// <param name="startStopDBAllowed">startStopDBAllowed.</param>
        /// <param name="inspectDBAllowed">inspectDBAllowed.</param>
        /// <param name="dbVariablesSetting">dbVariablesSetting.</param>
        /// <param name="links">links.</param>
        public Cube(string name = default(string), string application = default(string), string owner = default(string), long creationTime = default(long), string modifiedBy = default(string), long modifiedTime = default(long), string status = default(string), long startTime = default(long), string description = default(string), TypeEnum? type = default(TypeEnum?), string applicationRole = default(string), bool easManagedApp = default(bool), bool startStopDBAllowed = default(bool), bool inspectDBAllowed = default(bool), VariablesSetting dbVariablesSetting = default(VariablesSetting), List<Link> links = default(List<Link>))
        {
            this.Name = name;
            this.Application = application;
            this.Owner = owner;
            this.CreationTime = creationTime;
            this.ModifiedBy = modifiedBy;
            this.ModifiedTime = modifiedTime;
            this.Status = status;
            this.StartTime = startTime;
            this.Description = description;
            this.Type = type;
            this.ApplicationRole = applicationRole;
            this.EasManagedApp = easManagedApp;
            this.StartStopDBAllowed = startStopDBAllowed;
            this.InspectDBAllowed = inspectDBAllowed;
            this.DbVariablesSetting = dbVariablesSetting;
            this.Links = links;
        }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Application
        /// </summary>
        [DataMember(Name = "application", EmitDefaultValue = false)]
        public string Application { get; set; }

        /// <summary>
        /// Gets or Sets Owner
        /// </summary>
        [DataMember(Name = "owner", EmitDefaultValue = false)]
        public string Owner { get; set; }

        /// <summary>
        /// Gets or Sets CreationTime
        /// </summary>
        [DataMember(Name = "creationTime", EmitDefaultValue = false)]
        public long CreationTime { get; set; }

        /// <summary>
        /// Gets or Sets ModifiedBy
        /// </summary>
        [DataMember(Name = "modifiedBy", EmitDefaultValue = false)]
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Gets or Sets ModifiedTime
        /// </summary>
        [DataMember(Name = "modifiedTime", EmitDefaultValue = false)]
        public long ModifiedTime { get; set; }

        /// <summary>
        /// Gets or Sets Status
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }

        /// <summary>
        /// Gets or Sets StartTime
        /// </summary>
        [DataMember(Name = "startTime", EmitDefaultValue = false)]
        public long StartTime { get; set; }

        /// <summary>
        /// Gets or Sets Description
        /// </summary>
        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets ApplicationRole
        /// </summary>
        [DataMember(Name = "applicationRole", EmitDefaultValue = false)]
        public string ApplicationRole { get; set; }

        /// <summary>
        /// Gets or Sets EasManagedApp
        /// </summary>
        [DataMember(Name = "easManagedApp", EmitDefaultValue = true)]
        public bool EasManagedApp { get; set; }

        /// <summary>
        /// Gets or Sets StartStopDBAllowed
        /// </summary>
        [DataMember(Name = "startStopDBAllowed", EmitDefaultValue = true)]
        public bool StartStopDBAllowed { get; set; }

        /// <summary>
        /// Gets or Sets InspectDBAllowed
        /// </summary>
        [DataMember(Name = "inspectDBAllowed", EmitDefaultValue = true)]
        public bool InspectDBAllowed { get; set; }

        /// <summary>
        /// Gets or Sets DbVariablesSetting
        /// </summary>
        [DataMember(Name = "dbVariablesSetting", EmitDefaultValue = false)]
        public VariablesSetting DbVariablesSetting { get; set; }

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
            sb.Append("class Cube {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Application: ").Append(Application).Append("\n");
            sb.Append("  Owner: ").Append(Owner).Append("\n");
            sb.Append("  CreationTime: ").Append(CreationTime).Append("\n");
            sb.Append("  ModifiedBy: ").Append(ModifiedBy).Append("\n");
            sb.Append("  ModifiedTime: ").Append(ModifiedTime).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("  StartTime: ").Append(StartTime).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
            sb.Append("  ApplicationRole: ").Append(ApplicationRole).Append("\n");
            sb.Append("  EasManagedApp: ").Append(EasManagedApp).Append("\n");
            sb.Append("  StartStopDBAllowed: ").Append(StartStopDBAllowed).Append("\n");
            sb.Append("  InspectDBAllowed: ").Append(InspectDBAllowed).Append("\n");
            sb.Append("  DbVariablesSetting: ").Append(DbVariablesSetting).Append("\n");
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
            return this.Equals(input as Cube);
        }

        /// <summary>
        /// Returns true if Cube instances are equal
        /// </summary>
        /// <param name="input">Instance of Cube to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Cube input)
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
                    this.Application == input.Application ||
                    (this.Application != null &&
                    this.Application.Equals(input.Application))
                ) && 
                (
                    this.Owner == input.Owner ||
                    (this.Owner != null &&
                    this.Owner.Equals(input.Owner))
                ) && 
                (
                    this.CreationTime == input.CreationTime ||
                    this.CreationTime.Equals(input.CreationTime)
                ) && 
                (
                    this.ModifiedBy == input.ModifiedBy ||
                    (this.ModifiedBy != null &&
                    this.ModifiedBy.Equals(input.ModifiedBy))
                ) && 
                (
                    this.ModifiedTime == input.ModifiedTime ||
                    this.ModifiedTime.Equals(input.ModifiedTime)
                ) && 
                (
                    this.Status == input.Status ||
                    (this.Status != null &&
                    this.Status.Equals(input.Status))
                ) && 
                (
                    this.StartTime == input.StartTime ||
                    this.StartTime.Equals(input.StartTime)
                ) && 
                (
                    this.Description == input.Description ||
                    (this.Description != null &&
                    this.Description.Equals(input.Description))
                ) && 
                (
                    this.Type == input.Type ||
                    this.Type.Equals(input.Type)
                ) && 
                (
                    this.ApplicationRole == input.ApplicationRole ||
                    (this.ApplicationRole != null &&
                    this.ApplicationRole.Equals(input.ApplicationRole))
                ) && 
                (
                    this.EasManagedApp == input.EasManagedApp ||
                    this.EasManagedApp.Equals(input.EasManagedApp)
                ) && 
                (
                    this.StartStopDBAllowed == input.StartStopDBAllowed ||
                    this.StartStopDBAllowed.Equals(input.StartStopDBAllowed)
                ) && 
                (
                    this.InspectDBAllowed == input.InspectDBAllowed ||
                    this.InspectDBAllowed.Equals(input.InspectDBAllowed)
                ) && 
                (
                    this.DbVariablesSetting == input.DbVariablesSetting ||
                    (this.DbVariablesSetting != null &&
                    this.DbVariablesSetting.Equals(input.DbVariablesSetting))
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
                if (this.Name != null)
                {
                    hashCode = (hashCode * 59) + this.Name.GetHashCode();
                }
                if (this.Application != null)
                {
                    hashCode = (hashCode * 59) + this.Application.GetHashCode();
                }
                if (this.Owner != null)
                {
                    hashCode = (hashCode * 59) + this.Owner.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.CreationTime.GetHashCode();
                if (this.ModifiedBy != null)
                {
                    hashCode = (hashCode * 59) + this.ModifiedBy.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.ModifiedTime.GetHashCode();
                if (this.Status != null)
                {
                    hashCode = (hashCode * 59) + this.Status.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.StartTime.GetHashCode();
                if (this.Description != null)
                {
                    hashCode = (hashCode * 59) + this.Description.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.Type.GetHashCode();
                if (this.ApplicationRole != null)
                {
                    hashCode = (hashCode * 59) + this.ApplicationRole.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.EasManagedApp.GetHashCode();
                hashCode = (hashCode * 59) + this.StartStopDBAllowed.GetHashCode();
                hashCode = (hashCode * 59) + this.InspectDBAllowed.GetHashCode();
                if (this.DbVariablesSetting != null)
                {
                    hashCode = (hashCode * 59) + this.DbVariablesSetting.GetHashCode();
                }
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
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}
