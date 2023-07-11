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
    /// EssbaseASODbCompression
    /// </summary>
    [DataContract(Name = "EssbaseASODbCompression")]
    public partial class EssbaseASODbCompression : IEquatable<EssbaseASODbCompression>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EssbaseASODbCompression" /> class.
        /// </summary>
        /// <param name="name">name.</param>
        /// <param name="artifactType">artifactType.</param>
        /// <param name="nodeName">nodeName.</param>
        /// <param name="appName">appName.</param>
        /// <param name="dbName">dbName.</param>
        /// <param name="locked">locked.</param>
        /// <param name="lockedByUser">lockedByUser.</param>
        /// <param name="dimensionName">dimensionName.</param>
        /// <param name="isCompression">isCompression.</param>
        /// <param name="storedLevel0Members">storedLevel0Members.</param>
        /// <param name="averageBundleFill">averageBundleFill.</param>
        /// <param name="averageValueLength">averageValueLength.</param>
        /// <param name="level0MB">level0MB.</param>
        public EssbaseASODbCompression(string name = default(string), int artifactType = default(int), string nodeName = default(string), string appName = default(string), string dbName = default(string), bool locked = default(bool), string lockedByUser = default(string), string dimensionName = default(string), bool isCompression = default(bool), double storedLevel0Members = default(double), double averageBundleFill = default(double), double averageValueLength = default(double), double level0MB = default(double))
        {
            this.Name = name;
            this.ArtifactType = artifactType;
            this.NodeName = nodeName;
            this.AppName = appName;
            this.DbName = dbName;
            this.Locked = locked;
            this.LockedByUser = lockedByUser;
            this.DimensionName = dimensionName;
            this.IsCompression = isCompression;
            this.StoredLevel0Members = storedLevel0Members;
            this.AverageBundleFill = averageBundleFill;
            this.AverageValueLength = averageValueLength;
            this.Level0MB = level0MB;
        }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets ArtifactType
        /// </summary>
        [DataMember(Name = "artifactType", EmitDefaultValue = false)]
        public int ArtifactType { get; set; }

        /// <summary>
        /// Gets or Sets NodeName
        /// </summary>
        [DataMember(Name = "nodeName", EmitDefaultValue = false)]
        public string NodeName { get; set; }

        /// <summary>
        /// Gets or Sets AppName
        /// </summary>
        [DataMember(Name = "appName", EmitDefaultValue = false)]
        public string AppName { get; set; }

        /// <summary>
        /// Gets or Sets DbName
        /// </summary>
        [DataMember(Name = "dbName", EmitDefaultValue = false)]
        public string DbName { get; set; }

        /// <summary>
        /// Gets or Sets Locked
        /// </summary>
        [DataMember(Name = "locked", EmitDefaultValue = true)]
        public bool Locked { get; set; }

        /// <summary>
        /// Gets or Sets LockedByUser
        /// </summary>
        [DataMember(Name = "lockedByUser", EmitDefaultValue = false)]
        public string LockedByUser { get; set; }

        /// <summary>
        /// Gets or Sets DimensionName
        /// </summary>
        [DataMember(Name = "dimensionName", EmitDefaultValue = false)]
        public string DimensionName { get; set; }

        /// <summary>
        /// Gets or Sets IsCompression
        /// </summary>
        [DataMember(Name = "isCompression", EmitDefaultValue = true)]
        public bool IsCompression { get; set; }

        /// <summary>
        /// Gets or Sets StoredLevel0Members
        /// </summary>
        [DataMember(Name = "storedLevel0Members", EmitDefaultValue = false)]
        public double StoredLevel0Members { get; set; }

        /// <summary>
        /// Gets or Sets AverageBundleFill
        /// </summary>
        [DataMember(Name = "averageBundleFill", EmitDefaultValue = false)]
        public double AverageBundleFill { get; set; }

        /// <summary>
        /// Gets or Sets AverageValueLength
        /// </summary>
        [DataMember(Name = "averageValueLength", EmitDefaultValue = false)]
        public double AverageValueLength { get; set; }

        /// <summary>
        /// Gets or Sets Level0MB
        /// </summary>
        [DataMember(Name = "level0MB", EmitDefaultValue = false)]
        public double Level0MB { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class EssbaseASODbCompression {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  ArtifactType: ").Append(ArtifactType).Append("\n");
            sb.Append("  NodeName: ").Append(NodeName).Append("\n");
            sb.Append("  AppName: ").Append(AppName).Append("\n");
            sb.Append("  DbName: ").Append(DbName).Append("\n");
            sb.Append("  Locked: ").Append(Locked).Append("\n");
            sb.Append("  LockedByUser: ").Append(LockedByUser).Append("\n");
            sb.Append("  DimensionName: ").Append(DimensionName).Append("\n");
            sb.Append("  IsCompression: ").Append(IsCompression).Append("\n");
            sb.Append("  StoredLevel0Members: ").Append(StoredLevel0Members).Append("\n");
            sb.Append("  AverageBundleFill: ").Append(AverageBundleFill).Append("\n");
            sb.Append("  AverageValueLength: ").Append(AverageValueLength).Append("\n");
            sb.Append("  Level0MB: ").Append(Level0MB).Append("\n");
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
            return this.Equals(input as EssbaseASODbCompression);
        }

        /// <summary>
        /// Returns true if EssbaseASODbCompression instances are equal
        /// </summary>
        /// <param name="input">Instance of EssbaseASODbCompression to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(EssbaseASODbCompression input)
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
                    this.ArtifactType == input.ArtifactType ||
                    this.ArtifactType.Equals(input.ArtifactType)
                ) && 
                (
                    this.NodeName == input.NodeName ||
                    (this.NodeName != null &&
                    this.NodeName.Equals(input.NodeName))
                ) && 
                (
                    this.AppName == input.AppName ||
                    (this.AppName != null &&
                    this.AppName.Equals(input.AppName))
                ) && 
                (
                    this.DbName == input.DbName ||
                    (this.DbName != null &&
                    this.DbName.Equals(input.DbName))
                ) && 
                (
                    this.Locked == input.Locked ||
                    this.Locked.Equals(input.Locked)
                ) && 
                (
                    this.LockedByUser == input.LockedByUser ||
                    (this.LockedByUser != null &&
                    this.LockedByUser.Equals(input.LockedByUser))
                ) && 
                (
                    this.DimensionName == input.DimensionName ||
                    (this.DimensionName != null &&
                    this.DimensionName.Equals(input.DimensionName))
                ) && 
                (
                    this.IsCompression == input.IsCompression ||
                    this.IsCompression.Equals(input.IsCompression)
                ) && 
                (
                    this.StoredLevel0Members == input.StoredLevel0Members ||
                    this.StoredLevel0Members.Equals(input.StoredLevel0Members)
                ) && 
                (
                    this.AverageBundleFill == input.AverageBundleFill ||
                    this.AverageBundleFill.Equals(input.AverageBundleFill)
                ) && 
                (
                    this.AverageValueLength == input.AverageValueLength ||
                    this.AverageValueLength.Equals(input.AverageValueLength)
                ) && 
                (
                    this.Level0MB == input.Level0MB ||
                    this.Level0MB.Equals(input.Level0MB)
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
                hashCode = (hashCode * 59) + this.ArtifactType.GetHashCode();
                if (this.NodeName != null)
                {
                    hashCode = (hashCode * 59) + this.NodeName.GetHashCode();
                }
                if (this.AppName != null)
                {
                    hashCode = (hashCode * 59) + this.AppName.GetHashCode();
                }
                if (this.DbName != null)
                {
                    hashCode = (hashCode * 59) + this.DbName.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.Locked.GetHashCode();
                if (this.LockedByUser != null)
                {
                    hashCode = (hashCode * 59) + this.LockedByUser.GetHashCode();
                }
                if (this.DimensionName != null)
                {
                    hashCode = (hashCode * 59) + this.DimensionName.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.IsCompression.GetHashCode();
                hashCode = (hashCode * 59) + this.StoredLevel0Members.GetHashCode();
                hashCode = (hashCode * 59) + this.AverageBundleFill.GetHashCode();
                hashCode = (hashCode * 59) + this.AverageValueLength.GetHashCode();
                hashCode = (hashCode * 59) + this.Level0MB.GetHashCode();
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
