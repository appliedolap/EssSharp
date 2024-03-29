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
    /// DimBuildOptions
    /// </summary>
    [DataContract(Name = "DimBuildOptions")]
    public partial class DimBuildOptions : IEquatable<DimBuildOptions>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DimBuildOptions" /> class.
        /// </summary>
        /// <param name="autoConfig">autoConfig.</param>
        /// <param name="arrangeDimensions">arrangeDimensions.</param>
        /// <param name="aliasTable">aliasTable.</param>
        /// <param name="smartLists">smartLists.</param>
        public DimBuildOptions(bool autoConfig = default(bool), bool arrangeDimensions = default(bool), string aliasTable = default(string), List<SmartList> smartLists = default(List<SmartList>))
        {
            this.AutoConfig = autoConfig;
            this.ArrangeDimensions = arrangeDimensions;
            this.AliasTable = aliasTable;
            this.SmartLists = smartLists;
        }

        /// <summary>
        /// Gets or Sets AutoConfig
        /// </summary>
        [DataMember(Name = "autoConfig", EmitDefaultValue = true)]
        public bool AutoConfig { get; set; }

        /// <summary>
        /// Gets or Sets ArrangeDimensions
        /// </summary>
        [DataMember(Name = "arrangeDimensions", EmitDefaultValue = true)]
        public bool ArrangeDimensions { get; set; }

        /// <summary>
        /// Gets or Sets AliasTable
        /// </summary>
        [DataMember(Name = "aliasTable", EmitDefaultValue = false)]
        public string AliasTable { get; set; }

        /// <summary>
        /// Gets or Sets SmartLists
        /// </summary>
        [DataMember(Name = "smartLists", EmitDefaultValue = false)]
        public List<SmartList> SmartLists { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class DimBuildOptions {\n");
            sb.Append("  AutoConfig: ").Append(AutoConfig).Append("\n");
            sb.Append("  ArrangeDimensions: ").Append(ArrangeDimensions).Append("\n");
            sb.Append("  AliasTable: ").Append(AliasTable).Append("\n");
            sb.Append("  SmartLists: ").Append(SmartLists).Append("\n");
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
            return this.Equals(input as DimBuildOptions);
        }

        /// <summary>
        /// Returns true if DimBuildOptions instances are equal
        /// </summary>
        /// <param name="input">Instance of DimBuildOptions to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(DimBuildOptions input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.AutoConfig == input.AutoConfig ||
                    this.AutoConfig.Equals(input.AutoConfig)
                ) && 
                (
                    this.ArrangeDimensions == input.ArrangeDimensions ||
                    this.ArrangeDimensions.Equals(input.ArrangeDimensions)
                ) && 
                (
                    this.AliasTable == input.AliasTable ||
                    (this.AliasTable != null &&
                    this.AliasTable.Equals(input.AliasTable))
                ) && 
                (
                    this.SmartLists == input.SmartLists ||
                    this.SmartLists != null &&
                    input.SmartLists != null &&
                    this.SmartLists.SequenceEqual(input.SmartLists)
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
                hashCode = (hashCode * 59) + this.AutoConfig.GetHashCode();
                hashCode = (hashCode * 59) + this.ArrangeDimensions.GetHashCode();
                if (this.AliasTable != null)
                {
                    hashCode = (hashCode * 59) + this.AliasTable.GetHashCode();
                }
                if (this.SmartLists != null)
                {
                    hashCode = (hashCode * 59) + this.SmartLists.GetHashCode();
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
