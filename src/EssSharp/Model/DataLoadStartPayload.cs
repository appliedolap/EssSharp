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
    /// DataLoadStartPayload
    /// </summary>
    [DataContract(Name = "DataLoadStartPayload")]
    public partial class DataLoadStartPayload : IEquatable<DataLoadStartPayload>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataLoadStartPayload" /> class.
        /// </summary>
        /// <param name="ruleFileName">ruleFileName.</param>
        /// <param name="delimiter">Currently only Comma is supported as delimiter.</param>
        public DataLoadStartPayload(string ruleFileName = default(string), string delimiter = default(string))
        {
            this.RuleFileName = ruleFileName;
            this.Delimiter = delimiter;
        }

        /// <summary>
        /// Gets or Sets RuleFileName
        /// </summary>
        [DataMember(Name = "ruleFileName", EmitDefaultValue = false)]
        public string RuleFileName { get; set; }

        /// <summary>
        /// Currently only Comma is supported as delimiter
        /// </summary>
        /// <value>Currently only Comma is supported as delimiter</value>
        [DataMember(Name = "delimiter", EmitDefaultValue = false)]
        public string Delimiter { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class DataLoadStartPayload {\n");
            sb.Append("  RuleFileName: ").Append(RuleFileName).Append("\n");
            sb.Append("  Delimiter: ").Append(Delimiter).Append("\n");
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
            return this.Equals(input as DataLoadStartPayload);
        }

        /// <summary>
        /// Returns true if DataLoadStartPayload instances are equal
        /// </summary>
        /// <param name="input">Instance of DataLoadStartPayload to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(DataLoadStartPayload input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.RuleFileName == input.RuleFileName ||
                    (this.RuleFileName != null &&
                    this.RuleFileName.Equals(input.RuleFileName))
                ) && 
                (
                    this.Delimiter == input.Delimiter ||
                    (this.Delimiter != null &&
                    this.Delimiter.Equals(input.Delimiter))
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
                if (this.RuleFileName != null)
                {
                    hashCode = (hashCode * 59) + this.RuleFileName.GetHashCode();
                }
                if (this.Delimiter != null)
                {
                    hashCode = (hashCode * 59) + this.Delimiter.GetHashCode();
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
