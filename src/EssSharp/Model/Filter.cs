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
    /// Filter
    /// </summary>
    [DataContract(Name = "Filter")]
    public partial class Filter : IEquatable<Filter>, IValidatableObject
    {
        /// <summary>
        /// Defines Condition
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum ConditionEnum
        {
            /// <summary>
            /// Enum EQUALTO for value: EQUAL_TO
            /// </summary>
            [EnumMember(Value = "EQUAL_TO")]
            EQUALTO = 1,

            /// <summary>
            /// Enum NOTEQUALTO for value: NOT_EQUAL_TO
            /// </summary>
            [EnumMember(Value = "NOT_EQUAL_TO")]
            NOTEQUALTO = 2,

            /// <summary>
            /// Enum GREATERTHAN for value: GREATER_THAN
            /// </summary>
            [EnumMember(Value = "GREATER_THAN")]
            GREATERTHAN = 3,

            /// <summary>
            /// Enum GREATERTHANEQUALTO for value: GREATER_THAN_EQUAL_TO
            /// </summary>
            [EnumMember(Value = "GREATER_THAN_EQUAL_TO")]
            GREATERTHANEQUALTO = 4,

            /// <summary>
            /// Enum LESSTHAN for value: LESS_THAN
            /// </summary>
            [EnumMember(Value = "LESS_THAN")]
            LESSTHAN = 5,

            /// <summary>
            /// Enum LESSTHANEQUALTO for value: LESS_THAN_EQUAL_TO
            /// </summary>
            [EnumMember(Value = "LESS_THAN_EQUAL_TO")]
            LESSTHANEQUALTO = 6,

            /// <summary>
            /// Enum CONTAIN for value: CONTAIN
            /// </summary>
            [EnumMember(Value = "CONTAIN")]
            CONTAIN = 7,

            /// <summary>
            /// Enum DOESNOTCONTAIN for value: DOES_NOT_CONTAIN
            /// </summary>
            [EnumMember(Value = "DOES_NOT_CONTAIN")]
            DOESNOTCONTAIN = 8

        }


        /// <summary>
        /// Gets or Sets Condition
        /// </summary>
        [DataMember(Name = "condition", EmitDefaultValue = false)]
        public ConditionEnum? Condition { get; set; }
        /// <summary>
        /// Defines Type
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum TypeEnum
        {
            /// <summary>
            /// Enum REJECT for value: REJECT
            /// </summary>
            [EnumMember(Value = "REJECT")]
            REJECT = 1,

            /// <summary>
            /// Enum SELECT for value: SELECT
            /// </summary>
            [EnumMember(Value = "SELECT")]
            SELECT = 2

        }


        /// <summary>
        /// Gets or Sets Type
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public TypeEnum? Type { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Filter" /> class.
        /// </summary>
        /// <param name="stringFilter">stringFilter.</param>
        /// <param name="value">value.</param>
        /// <param name="condition">condition.</param>
        /// <param name="caseSensitive">caseSensitive.</param>
        /// <param name="type">type.</param>
        public Filter(bool stringFilter = default(bool), string value = default(string), ConditionEnum? condition = default(ConditionEnum?), bool caseSensitive = default(bool), TypeEnum? type = default(TypeEnum?))
        {
            this.StringFilter = stringFilter;
            this.Value = value;
            this.Condition = condition;
            this.CaseSensitive = caseSensitive;
            this.Type = type;
        }

        /// <summary>
        /// Gets or Sets StringFilter
        /// </summary>
        [DataMember(Name = "stringFilter", EmitDefaultValue = true)]
        public bool StringFilter { get; set; }

        /// <summary>
        /// Gets or Sets Value
        /// </summary>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }

        /// <summary>
        /// Gets or Sets CaseSensitive
        /// </summary>
        [DataMember(Name = "caseSensitive", EmitDefaultValue = true)]
        public bool CaseSensitive { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class Filter {\n");
            sb.Append("  StringFilter: ").Append(StringFilter).Append("\n");
            sb.Append("  Value: ").Append(Value).Append("\n");
            sb.Append("  Condition: ").Append(Condition).Append("\n");
            sb.Append("  CaseSensitive: ").Append(CaseSensitive).Append("\n");
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
            return this.Equals(input as Filter);
        }

        /// <summary>
        /// Returns true if Filter instances are equal
        /// </summary>
        /// <param name="input">Instance of Filter to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Filter input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.StringFilter == input.StringFilter ||
                    this.StringFilter.Equals(input.StringFilter)
                ) && 
                (
                    this.Value == input.Value ||
                    (this.Value != null &&
                    this.Value.Equals(input.Value))
                ) && 
                (
                    this.Condition == input.Condition ||
                    this.Condition.Equals(input.Condition)
                ) && 
                (
                    this.CaseSensitive == input.CaseSensitive ||
                    this.CaseSensitive.Equals(input.CaseSensitive)
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
                hashCode = (hashCode * 59) + this.StringFilter.GetHashCode();
                if (this.Value != null)
                {
                    hashCode = (hashCode * 59) + this.Value.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.Condition.GetHashCode();
                hashCode = (hashCode * 59) + this.CaseSensitive.GetHashCode();
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
