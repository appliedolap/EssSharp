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
    /// StatisticsList
    /// </summary>
    [DataContract(Name = "StatisticsList")]
    public partial class StatisticsList : IEquatable<StatisticsList>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticsList" /> class.
        /// </summary>
        /// <param name="general">general.</param>
        /// <param name="storage">storage.</param>
        /// <param name="runtime">runtime.</param>
        /// <param name="links">links.</param>
        public StatisticsList(GeneralStatistics general = default(GeneralStatistics), StorageStatistics storage = default(StorageStatistics), RuntimeStatistics runtime = default(RuntimeStatistics), List<Link> links = default(List<Link>))
        {
            this.General = general;
            this.Storage = storage;
            this.Runtime = runtime;
            this.Links = links;
        }

        /// <summary>
        /// Gets or Sets General
        /// </summary>
        [DataMember(Name = "general", EmitDefaultValue = false)]
        public GeneralStatistics General { get; set; }

        /// <summary>
        /// Gets or Sets Storage
        /// </summary>
        [DataMember(Name = "storage", EmitDefaultValue = false)]
        public StorageStatistics Storage { get; set; }

        /// <summary>
        /// Gets or Sets Runtime
        /// </summary>
        [DataMember(Name = "runtime", EmitDefaultValue = false)]
        public RuntimeStatistics Runtime { get; set; }

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
            sb.Append("class StatisticsList {\n");
            sb.Append("  General: ").Append(General).Append("\n");
            sb.Append("  Storage: ").Append(Storage).Append("\n");
            sb.Append("  Runtime: ").Append(Runtime).Append("\n");
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
            return this.Equals(input as StatisticsList);
        }

        /// <summary>
        /// Returns true if StatisticsList instances are equal
        /// </summary>
        /// <param name="input">Instance of StatisticsList to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(StatisticsList input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.General == input.General ||
                    (this.General != null &&
                    this.General.Equals(input.General))
                ) && 
                (
                    this.Storage == input.Storage ||
                    (this.Storage != null &&
                    this.Storage.Equals(input.Storage))
                ) && 
                (
                    this.Runtime == input.Runtime ||
                    (this.Runtime != null &&
                    this.Runtime.Equals(input.Runtime))
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
                if (this.General != null)
                {
                    hashCode = (hashCode * 59) + this.General.GetHashCode();
                }
                if (this.Storage != null)
                {
                    hashCode = (hashCode * 59) + this.Storage.GetHashCode();
                }
                if (this.Runtime != null)
                {
                    hashCode = (hashCode * 59) + this.Runtime.GetHashCode();
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
        public IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}
