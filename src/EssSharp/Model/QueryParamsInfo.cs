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
    /// QueryParamsInfo
    /// </summary>
    [DataContract(Name = "QueryParamsInfo")]
    public partial class QueryParamsInfo : IEquatable<QueryParamsInfo>, IValidatableObject
    {
        /// <summary>
        /// Defines Type
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum TypeEnum
        {
            /// <summary>
            /// Enum STRING for value: STRING
            /// </summary>
            [EnumMember(Value = "STRING")]
            STRING = 1,

            /// <summary>
            /// Enum DOUBLE for value: DOUBLE
            /// </summary>
            [EnumMember(Value = "DOUBLE")]
            DOUBLE = 2,

            /// <summary>
            /// Enum DATE for value: DATE
            /// </summary>
            [EnumMember(Value = "DATE")]
            DATE = 3,

            /// <summary>
            /// Enum TIMESTAMP for value: TIMESTAMP
            /// </summary>
            [EnumMember(Value = "TIMESTAMP")]
            TIMESTAMP = 4,

            /// <summary>
            /// Enum LONG for value: LONG
            /// </summary>
            [EnumMember(Value = "LONG")]
            LONG = 5

        }


        /// <summary>
        /// Gets or Sets Type
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public TypeEnum? Type { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryParamsInfo" /> class.
        /// </summary>
        /// <param name="index">index.</param>
        /// <param name="name">name.</param>
        /// <param name="defaultValue">defaultValue.</param>
        /// <param name="required">required.</param>
        /// <param name="useSubVariable">useSubVariable.</param>
        /// <param name="subVariableName">subVariableName.</param>
        /// <param name="type">type.</param>
        public QueryParamsInfo(int index = default(int), string name = default(string), string defaultValue = default(string), bool required = default(bool), bool useSubVariable = default(bool), string subVariableName = default(string), TypeEnum? type = default(TypeEnum?))
        {
            this.Index = index;
            this.Name = name;
            this.DefaultValue = defaultValue;
            this.Required = required;
            this.UseSubVariable = useSubVariable;
            this.SubVariableName = subVariableName;
            this.Type = type;
        }

        /// <summary>
        /// Gets or Sets Index
        /// </summary>
        [DataMember(Name = "index", EmitDefaultValue = false)]
        public int Index { get; set; }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets DefaultValue
        /// </summary>
        [DataMember(Name = "defaultValue", EmitDefaultValue = false)]
        public string DefaultValue { get; set; }

        /// <summary>
        /// Gets or Sets Required
        /// </summary>
        [DataMember(Name = "required", EmitDefaultValue = true)]
        public bool Required { get; set; }

        /// <summary>
        /// Gets or Sets UseSubVariable
        /// </summary>
        [DataMember(Name = "useSubVariable", EmitDefaultValue = true)]
        public bool UseSubVariable { get; set; }

        /// <summary>
        /// Gets or Sets SubVariableName
        /// </summary>
        [DataMember(Name = "subVariableName", EmitDefaultValue = false)]
        public string SubVariableName { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class QueryParamsInfo {\n");
            sb.Append("  Index: ").Append(Index).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  DefaultValue: ").Append(DefaultValue).Append("\n");
            sb.Append("  Required: ").Append(Required).Append("\n");
            sb.Append("  UseSubVariable: ").Append(UseSubVariable).Append("\n");
            sb.Append("  SubVariableName: ").Append(SubVariableName).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
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
            return this.Equals(input as QueryParamsInfo);
        }

        /// <summary>
        /// Returns true if QueryParamsInfo instances are equal
        /// </summary>
        /// <param name="input">Instance of QueryParamsInfo to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(QueryParamsInfo input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.Index == input.Index ||
                    this.Index.Equals(input.Index)
                ) && 
                (
                    this.Name == input.Name ||
                    (this.Name != null &&
                    this.Name.Equals(input.Name))
                ) && 
                (
                    this.DefaultValue == input.DefaultValue ||
                    (this.DefaultValue != null &&
                    this.DefaultValue.Equals(input.DefaultValue))
                ) && 
                (
                    this.Required == input.Required ||
                    this.Required.Equals(input.Required)
                ) && 
                (
                    this.UseSubVariable == input.UseSubVariable ||
                    this.UseSubVariable.Equals(input.UseSubVariable)
                ) && 
                (
                    this.SubVariableName == input.SubVariableName ||
                    (this.SubVariableName != null &&
                    this.SubVariableName.Equals(input.SubVariableName))
                ) && 
                (
                    this.Type == input.Type ||
                    this.Type.Equals(input.Type)
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
                hashCode = (hashCode * 59) + this.Index.GetHashCode();
                if (this.Name != null)
                {
                    hashCode = (hashCode * 59) + this.Name.GetHashCode();
                }
                if (this.DefaultValue != null)
                {
                    hashCode = (hashCode * 59) + this.DefaultValue.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.Required.GetHashCode();
                hashCode = (hashCode * 59) + this.UseSubVariable.GetHashCode();
                if (this.SubVariableName != null)
                {
                    hashCode = (hashCode * 59) + this.SubVariableName.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.Type.GetHashCode();
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
