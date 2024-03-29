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
    /// JobStatisticsBean
    /// </summary>
    [DataContract(Name = "JobStatisticsBean")]
    public partial class JobStatisticsBean : IEquatable<JobStatisticsBean>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JobStatisticsBean" /> class.
        /// </summary>
        /// <param name="errCt">errCt.</param>
        /// <param name="succesCt">succesCt.</param>
        /// <param name="warningCt">warningCt.</param>
        /// <param name="runningCt">runningCt.</param>
        public JobStatisticsBean(long errCt = default(long), long succesCt = default(long), long warningCt = default(long), long runningCt = default(long))
        {
            this.ErrCt = errCt;
            this.SuccesCt = succesCt;
            this.WarningCt = warningCt;
            this.RunningCt = runningCt;
        }

        /// <summary>
        /// Gets or Sets ErrCt
        /// </summary>
        [DataMember(Name = "errCt", EmitDefaultValue = false)]
        public long ErrCt { get; set; }

        /// <summary>
        /// Gets or Sets SuccesCt
        /// </summary>
        [DataMember(Name = "succesCt", EmitDefaultValue = false)]
        public long SuccesCt { get; set; }

        /// <summary>
        /// Gets or Sets WarningCt
        /// </summary>
        [DataMember(Name = "warningCt", EmitDefaultValue = false)]
        public long WarningCt { get; set; }

        /// <summary>
        /// Gets or Sets RunningCt
        /// </summary>
        [DataMember(Name = "runningCt", EmitDefaultValue = false)]
        public long RunningCt { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class JobStatisticsBean {\n");
            sb.Append("  ErrCt: ").Append(ErrCt).Append("\n");
            sb.Append("  SuccesCt: ").Append(SuccesCt).Append("\n");
            sb.Append("  WarningCt: ").Append(WarningCt).Append("\n");
            sb.Append("  RunningCt: ").Append(RunningCt).Append("\n");
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
            return this.Equals(input as JobStatisticsBean);
        }

        /// <summary>
        /// Returns true if JobStatisticsBean instances are equal
        /// </summary>
        /// <param name="input">Instance of JobStatisticsBean to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(JobStatisticsBean input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.ErrCt == input.ErrCt ||
                    this.ErrCt.Equals(input.ErrCt)
                ) && 
                (
                    this.SuccesCt == input.SuccesCt ||
                    this.SuccesCt.Equals(input.SuccesCt)
                ) && 
                (
                    this.WarningCt == input.WarningCt ||
                    this.WarningCt.Equals(input.WarningCt)
                ) && 
                (
                    this.RunningCt == input.RunningCt ||
                    this.RunningCt.Equals(input.RunningCt)
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
                hashCode = (hashCode * 59) + this.ErrCt.GetHashCode();
                hashCode = (hashCode * 59) + this.SuccesCt.GetHashCode();
                hashCode = (hashCode * 59) + this.WarningCt.GetHashCode();
                hashCode = (hashCode * 59) + this.RunningCt.GetHashCode();
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
