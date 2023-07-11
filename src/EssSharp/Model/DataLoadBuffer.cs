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
    /// DataLoadBuffer
    /// </summary>
    [DataContract(Name = "DataLoadBuffer")]
    public partial class DataLoadBuffer : IEquatable<DataLoadBuffer>, IValidatableObject
    {
        /// <summary>
        /// Defines DuplicateAggregationMethod
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum DuplicateAggregationMethodEnum
        {
            /// <summary>
            /// Enum ADD for value: ADD
            /// </summary>
            [EnumMember(Value = "ADD")]
            ADD = 1,

            /// <summary>
            /// Enum ASSUMEEQUAL for value: ASSUME_EQUAL
            /// </summary>
            [EnumMember(Value = "ASSUME_EQUAL")]
            ASSUMEEQUAL = 2,

            /// <summary>
            /// Enum USELAST for value: USE_LAST
            /// </summary>
            [EnumMember(Value = "USE_LAST")]
            USELAST = 3

        }


        /// <summary>
        /// Gets or Sets DuplicateAggregationMethod
        /// </summary>
        [DataMember(Name = "duplicateAggregationMethod", EmitDefaultValue = false)]
        public DuplicateAggregationMethodEnum? DuplicateAggregationMethod { get; set; }
        /// <summary>
        /// Defines LoadBufferOptions
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum LoadBufferOptionsEnum
        {
            /// <summary>
            /// Enum NONE for value: IGNORE_NONE
            /// </summary>
            [EnumMember(Value = "IGNORE_NONE")]
            NONE = 1,

            /// <summary>
            /// Enum MISSINGVALUES for value: IGNORE_MISSING_VALUES
            /// </summary>
            [EnumMember(Value = "IGNORE_MISSING_VALUES")]
            MISSINGVALUES = 2,

            /// <summary>
            /// Enum ZEROVALUES for value: IGNORE_ZERO_VALUES
            /// </summary>
            [EnumMember(Value = "IGNORE_ZERO_VALUES")]
            ZEROVALUES = 3,

            /// <summary>
            /// Enum MISSINGANDZEROVALUES for value: IGNORE_MISSING_AND_ZERO_VALUES
            /// </summary>
            [EnumMember(Value = "IGNORE_MISSING_AND_ZERO_VALUES")]
            MISSINGANDZEROVALUES = 4

        }


        /// <summary>
        /// Gets or Sets LoadBufferOptions
        /// </summary>
        [DataMember(Name = "loadBufferOptions", EmitDefaultValue = false)]
        public LoadBufferOptionsEnum? LoadBufferOptions { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="DataLoadBuffer" /> class.
        /// </summary>
        /// <param name="bufferId">bufferId.</param>
        /// <param name="duplicateAggregationMethod">duplicateAggregationMethod.</param>
        /// <param name="loadBufferOptions">loadBufferOptions.</param>
        /// <param name="resourceUsage">Percentage of the total load buffer resources that the load buffer will be allowed to use; must be within [0, 100], and the value of 0 is interpreted as default, which is currently 100..</param>
        public DataLoadBuffer(long bufferId = default(long), DuplicateAggregationMethodEnum? duplicateAggregationMethod = default(DuplicateAggregationMethodEnum?), LoadBufferOptionsEnum? loadBufferOptions = default(LoadBufferOptionsEnum?), long resourceUsage = default(long))
        {
            this.BufferId = bufferId;
            this.DuplicateAggregationMethod = duplicateAggregationMethod;
            this.LoadBufferOptions = loadBufferOptions;
            this.ResourceUsage = resourceUsage;
        }

        /// <summary>
        /// Gets or Sets BufferId
        /// </summary>
        [DataMember(Name = "bufferId", EmitDefaultValue = false)]
        public long BufferId { get; set; }

        /// <summary>
        /// Percentage of the total load buffer resources that the load buffer will be allowed to use; must be within [0, 100], and the value of 0 is interpreted as default, which is currently 100.
        /// </summary>
        /// <value>Percentage of the total load buffer resources that the load buffer will be allowed to use; must be within [0, 100], and the value of 0 is interpreted as default, which is currently 100.</value>
        [DataMember(Name = "resourceUsage", EmitDefaultValue = false)]
        public long ResourceUsage { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class DataLoadBuffer {\n");
            sb.Append("  BufferId: ").Append(BufferId).Append("\n");
            sb.Append("  DuplicateAggregationMethod: ").Append(DuplicateAggregationMethod).Append("\n");
            sb.Append("  LoadBufferOptions: ").Append(LoadBufferOptions).Append("\n");
            sb.Append("  ResourceUsage: ").Append(ResourceUsage).Append("\n");
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
            return this.Equals(input as DataLoadBuffer);
        }

        /// <summary>
        /// Returns true if DataLoadBuffer instances are equal
        /// </summary>
        /// <param name="input">Instance of DataLoadBuffer to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(DataLoadBuffer input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.BufferId == input.BufferId ||
                    this.BufferId.Equals(input.BufferId)
                ) && 
                (
                    this.DuplicateAggregationMethod == input.DuplicateAggregationMethod ||
                    this.DuplicateAggregationMethod.Equals(input.DuplicateAggregationMethod)
                ) && 
                (
                    this.LoadBufferOptions == input.LoadBufferOptions ||
                    this.LoadBufferOptions.Equals(input.LoadBufferOptions)
                ) && 
                (
                    this.ResourceUsage == input.ResourceUsage ||
                    this.ResourceUsage.Equals(input.ResourceUsage)
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
                hashCode = (hashCode * 59) + this.BufferId.GetHashCode();
                hashCode = (hashCode * 59) + this.DuplicateAggregationMethod.GetHashCode();
                hashCode = (hashCode * 59) + this.LoadBufferOptions.GetHashCode();
                hashCode = (hashCode * 59) + this.ResourceUsage.GetHashCode();
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
