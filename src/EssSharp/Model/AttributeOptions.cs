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
    /// AttributeOptions
    /// </summary>
    [DataContract(Name = "AttributeOptions")]
    public partial class AttributeOptions : IEquatable<AttributeOptions>, IValidatableObject
    {
        /// <summary>
        /// Defines Type
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum TypeEnum
        {
            /// <summary>
            /// Enum NUMERIC for value: NUMERIC
            /// </summary>
            [EnumMember(Value = "NUMERIC")]
            NUMERIC = 1,

            /// <summary>
            /// Enum BOOLEAN for value: BOOLEAN
            /// </summary>
            [EnumMember(Value = "BOOLEAN")]
            BOOLEAN = 2,

            /// <summary>
            /// Enum TEXT for value: TEXT
            /// </summary>
            [EnumMember(Value = "TEXT")]
            TEXT = 3,

            /// <summary>
            /// Enum DATE for value: DATE
            /// </summary>
            [EnumMember(Value = "DATE")]
            DATE = 4,

            /// <summary>
            /// Enum EXISTING for value: EXISTING
            /// </summary>
            [EnumMember(Value = "EXISTING")]
            EXISTING = 5

        }


        /// <summary>
        /// Gets or Sets Type
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public TypeEnum? Type { get; set; }
        /// <summary>
        /// Defines ScadisAssociationMode
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum ScadisAssociationModeEnum
        {
            /// <summary>
            /// Enum NOOVERWRITE for value: NOOVERWRITE
            /// </summary>
            [EnumMember(Value = "NOOVERWRITE")]
            NOOVERWRITE = 1,

            /// <summary>
            /// Enum OVERWRITE for value: OVERWRITE
            /// </summary>
            [EnumMember(Value = "OVERWRITE")]
            OVERWRITE = 2,

            /// <summary>
            /// Enum EXTEND for value: EXTEND
            /// </summary>
            [EnumMember(Value = "EXTEND")]
            EXTEND = 3

        }


        /// <summary>
        /// Gets or Sets ScadisAssociationMode
        /// </summary>
        [DataMember(Name = "scadisAssociationMode", EmitDefaultValue = false)]
        public ScadisAssociationModeEnum? ScadisAssociationMode { get; set; }
        /// <summary>
        /// Defines ScaassociationMode
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum ScaassociationModeEnum
        {
            /// <summary>
            /// Enum NOOVERWRITE for value: NOOVERWRITE
            /// </summary>
            [EnumMember(Value = "NOOVERWRITE")]
            NOOVERWRITE = 1,

            /// <summary>
            /// Enum OVERWRITE for value: OVERWRITE
            /// </summary>
            [EnumMember(Value = "OVERWRITE")]
            OVERWRITE = 2

        }


        /// <summary>
        /// Gets or Sets ScaassociationMode
        /// </summary>
        [DataMember(Name = "scaassociationMode", EmitDefaultValue = false)]
        public ScaassociationModeEnum? ScaassociationMode { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeOptions" /> class.
        /// </summary>
        /// <param name="indepDimensions">indepDimensions.</param>
        /// <param name="type">type.</param>
        /// <param name="baseDimension">baseDimension.</param>
        /// <param name="modified">modified.</param>
        /// <param name="scadisAssociationMode">scadisAssociationMode.</param>
        /// <param name="scaassociationMode">scaassociationMode.</param>
        public AttributeOptions(List<IndepDimension> indepDimensions = default(List<IndepDimension>), TypeEnum? type = default(TypeEnum?), string baseDimension = default(string), bool modified = default(bool), ScadisAssociationModeEnum? scadisAssociationMode = default(ScadisAssociationModeEnum?), ScaassociationModeEnum? scaassociationMode = default(ScaassociationModeEnum?))
        {
            this.IndepDimensions = indepDimensions;
            this.Type = type;
            this.BaseDimension = baseDimension;
            this.Modified = modified;
            this.ScadisAssociationMode = scadisAssociationMode;
            this.ScaassociationMode = scaassociationMode;
        }

        /// <summary>
        /// Gets or Sets IndepDimensions
        /// </summary>
        [DataMember(Name = "indepDimensions", EmitDefaultValue = false)]
        public List<IndepDimension> IndepDimensions { get; set; }

        /// <summary>
        /// Gets or Sets BaseDimension
        /// </summary>
        [DataMember(Name = "baseDimension", EmitDefaultValue = false)]
        public string BaseDimension { get; set; }

        /// <summary>
        /// Gets or Sets Modified
        /// </summary>
        [DataMember(Name = "modified", EmitDefaultValue = true)]
        public bool Modified { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class AttributeOptions {\n");
            sb.Append("  IndepDimensions: ").Append(IndepDimensions).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
            sb.Append("  BaseDimension: ").Append(BaseDimension).Append("\n");
            sb.Append("  Modified: ").Append(Modified).Append("\n");
            sb.Append("  ScadisAssociationMode: ").Append(ScadisAssociationMode).Append("\n");
            sb.Append("  ScaassociationMode: ").Append(ScaassociationMode).Append("\n");
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
            return this.Equals(input as AttributeOptions);
        }

        /// <summary>
        /// Returns true if AttributeOptions instances are equal
        /// </summary>
        /// <param name="input">Instance of AttributeOptions to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(AttributeOptions input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.IndepDimensions == input.IndepDimensions ||
                    this.IndepDimensions != null &&
                    input.IndepDimensions != null &&
                    this.IndepDimensions.SequenceEqual(input.IndepDimensions)
                ) && 
                (
                    this.Type == input.Type ||
                    this.Type.Equals(input.Type)
                ) && 
                (
                    this.BaseDimension == input.BaseDimension ||
                    (this.BaseDimension != null &&
                    this.BaseDimension.Equals(input.BaseDimension))
                ) && 
                (
                    this.Modified == input.Modified ||
                    this.Modified.Equals(input.Modified)
                ) && 
                (
                    this.ScadisAssociationMode == input.ScadisAssociationMode ||
                    this.ScadisAssociationMode.Equals(input.ScadisAssociationMode)
                ) && 
                (
                    this.ScaassociationMode == input.ScaassociationMode ||
                    this.ScaassociationMode.Equals(input.ScaassociationMode)
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
                if (this.IndepDimensions != null)
                {
                    hashCode = (hashCode * 59) + this.IndepDimensions.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.Type.GetHashCode();
                if (this.BaseDimension != null)
                {
                    hashCode = (hashCode * 59) + this.BaseDimension.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.Modified.GetHashCode();
                hashCode = (hashCode * 59) + this.ScadisAssociationMode.GetHashCode();
                hashCode = (hashCode * 59) + this.ScaassociationMode.GetHashCode();
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
