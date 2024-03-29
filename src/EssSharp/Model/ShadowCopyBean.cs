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
    /// ShadowCopyBean
    /// </summary>
    [DataContract(Name = "ShadowCopyBean")]
    public partial class ShadowCopyBean : IEquatable<ShadowCopyBean>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShadowCopyBean" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected ShadowCopyBean() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ShadowCopyBean" /> class.
        /// </summary>
        /// <param name="primaryAppName">primaryAppName (required).</param>
        /// <param name="shadowAppName">shadowAppName (required).</param>
        /// <param name="hideShadow">hideShadow (required).</param>
        /// <param name="waitForOngoingUpdatesInSecs">waitForOngoingUpdatesInSecs (required).</param>
        /// <param name="runInBackground">runInBackground.</param>
        public ShadowCopyBean(string primaryAppName = default(string), string shadowAppName = default(string), bool hideShadow = default(bool), int waitForOngoingUpdatesInSecs = default(int), bool runInBackground = default(bool))
        {
            // to ensure "primaryAppName" is required (not null)
            if (primaryAppName == null)
            {
                throw new ArgumentNullException("primaryAppName is a required property for ShadowCopyBean and cannot be null");
            }
            this.PrimaryAppName = primaryAppName;
            // to ensure "shadowAppName" is required (not null)
            if (shadowAppName == null)
            {
                throw new ArgumentNullException("shadowAppName is a required property for ShadowCopyBean and cannot be null");
            }
            this.ShadowAppName = shadowAppName;
            this.HideShadow = hideShadow;
            this.WaitForOngoingUpdatesInSecs = waitForOngoingUpdatesInSecs;
            this.RunInBackground = runInBackground;
        }

        /// <summary>
        /// Gets or Sets PrimaryAppName
        /// </summary>
        [DataMember(Name = "primaryAppName", IsRequired = true, EmitDefaultValue = true)]
        public string PrimaryAppName { get; set; }

        /// <summary>
        /// Gets or Sets ShadowAppName
        /// </summary>
        [DataMember(Name = "shadowAppName", IsRequired = true, EmitDefaultValue = true)]
        public string ShadowAppName { get; set; }

        /// <summary>
        /// Gets or Sets HideShadow
        /// </summary>
        [DataMember(Name = "hideShadow", IsRequired = true, EmitDefaultValue = true)]
        public bool HideShadow { get; set; }

        /// <summary>
        /// Gets or Sets WaitForOngoingUpdatesInSecs
        /// </summary>
        [DataMember(Name = "waitForOngoingUpdatesInSecs", IsRequired = true, EmitDefaultValue = true)]
        public int WaitForOngoingUpdatesInSecs { get; set; }

        /// <summary>
        /// Gets or Sets RunInBackground
        /// </summary>
        [DataMember(Name = "runInBackground", EmitDefaultValue = true)]
        public bool RunInBackground { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class ShadowCopyBean {\n");
            sb.Append("  PrimaryAppName: ").Append(PrimaryAppName).Append("\n");
            sb.Append("  ShadowAppName: ").Append(ShadowAppName).Append("\n");
            sb.Append("  HideShadow: ").Append(HideShadow).Append("\n");
            sb.Append("  WaitForOngoingUpdatesInSecs: ").Append(WaitForOngoingUpdatesInSecs).Append("\n");
            sb.Append("  RunInBackground: ").Append(RunInBackground).Append("\n");
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
            return this.Equals(input as ShadowCopyBean);
        }

        /// <summary>
        /// Returns true if ShadowCopyBean instances are equal
        /// </summary>
        /// <param name="input">Instance of ShadowCopyBean to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ShadowCopyBean input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.PrimaryAppName == input.PrimaryAppName ||
                    (this.PrimaryAppName != null &&
                    this.PrimaryAppName.Equals(input.PrimaryAppName))
                ) && 
                (
                    this.ShadowAppName == input.ShadowAppName ||
                    (this.ShadowAppName != null &&
                    this.ShadowAppName.Equals(input.ShadowAppName))
                ) && 
                (
                    this.HideShadow == input.HideShadow ||
                    this.HideShadow.Equals(input.HideShadow)
                ) && 
                (
                    this.WaitForOngoingUpdatesInSecs == input.WaitForOngoingUpdatesInSecs ||
                    this.WaitForOngoingUpdatesInSecs.Equals(input.WaitForOngoingUpdatesInSecs)
                ) && 
                (
                    this.RunInBackground == input.RunInBackground ||
                    this.RunInBackground.Equals(input.RunInBackground)
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
                if (this.PrimaryAppName != null)
                {
                    hashCode = (hashCode * 59) + this.PrimaryAppName.GetHashCode();
                }
                if (this.ShadowAppName != null)
                {
                    hashCode = (hashCode * 59) + this.ShadowAppName.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.HideShadow.GetHashCode();
                hashCode = (hashCode * 59) + this.WaitForOngoingUpdatesInSecs.GetHashCode();
                hashCode = (hashCode * 59) + this.RunInBackground.GetHashCode();
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
