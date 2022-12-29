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
    /// AppGeneralSettings
    /// </summary>
    [DataContract(Name = "AppGeneralSettings")]
    public partial class AppGeneralSettings : IEquatable<AppGeneralSettings>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppGeneralSettings" /> class.
        /// </summary>
        /// <param name="description">description.</param>
        /// <param name="logLevelAsString">logLevelAsString.</param>
        /// <param name="easManagedApp">easManagedApp.</param>
        /// <param name="timeoutOnDataBlockLocks">timeoutOnDataBlockLocks.</param>
        /// <param name="maxAttachmentFileSizeInKbs">maxAttachmentFileSizeInKbs.</param>
        /// <param name="pendingCacheSizeLimitInMbs">pendingCacheSizeLimitInMbs.</param>
        public AppGeneralSettings(string description = default(string), string logLevelAsString = default(string), bool easManagedApp = default(bool), long timeoutOnDataBlockLocks = default(long), long maxAttachmentFileSizeInKbs = default(long), long pendingCacheSizeLimitInMbs = default(long))
        {
            this.Description = description;
            this.LogLevelAsString = logLevelAsString;
            this.EasManagedApp = easManagedApp;
            this.TimeoutOnDataBlockLocks = timeoutOnDataBlockLocks;
            this.MaxAttachmentFileSizeInKbs = maxAttachmentFileSizeInKbs;
            this.PendingCacheSizeLimitInMbs = pendingCacheSizeLimitInMbs;
        }

        /// <summary>
        /// Gets or Sets Description
        /// </summary>
        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets LogLevelAsString
        /// </summary>
        [DataMember(Name = "logLevelAsString", EmitDefaultValue = false)]
        public string LogLevelAsString { get; set; }

        /// <summary>
        /// Gets or Sets EasManagedApp
        /// </summary>
        [DataMember(Name = "easManagedApp", EmitDefaultValue = true)]
        public bool EasManagedApp { get; set; }

        /// <summary>
        /// Gets or Sets TimeoutOnDataBlockLocks
        /// </summary>
        [DataMember(Name = "timeoutOnDataBlockLocks", EmitDefaultValue = false)]
        public long TimeoutOnDataBlockLocks { get; set; }

        /// <summary>
        /// Gets or Sets MaxAttachmentFileSizeInKbs
        /// </summary>
        [DataMember(Name = "maxAttachmentFileSizeInKbs", EmitDefaultValue = false)]
        public long MaxAttachmentFileSizeInKbs { get; set; }

        /// <summary>
        /// Gets or Sets PendingCacheSizeLimitInMbs
        /// </summary>
        [DataMember(Name = "pendingCacheSizeLimitInMbs", EmitDefaultValue = false)]
        public long PendingCacheSizeLimitInMbs { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class AppGeneralSettings {\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  LogLevelAsString: ").Append(LogLevelAsString).Append("\n");
            sb.Append("  EasManagedApp: ").Append(EasManagedApp).Append("\n");
            sb.Append("  TimeoutOnDataBlockLocks: ").Append(TimeoutOnDataBlockLocks).Append("\n");
            sb.Append("  MaxAttachmentFileSizeInKbs: ").Append(MaxAttachmentFileSizeInKbs).Append("\n");
            sb.Append("  PendingCacheSizeLimitInMbs: ").Append(PendingCacheSizeLimitInMbs).Append("\n");
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
            return this.Equals(input as AppGeneralSettings);
        }

        /// <summary>
        /// Returns true if AppGeneralSettings instances are equal
        /// </summary>
        /// <param name="input">Instance of AppGeneralSettings to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(AppGeneralSettings input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.Description == input.Description ||
                    (this.Description != null &&
                    this.Description.Equals(input.Description))
                ) && 
                (
                    this.LogLevelAsString == input.LogLevelAsString ||
                    (this.LogLevelAsString != null &&
                    this.LogLevelAsString.Equals(input.LogLevelAsString))
                ) && 
                (
                    this.EasManagedApp == input.EasManagedApp ||
                    this.EasManagedApp.Equals(input.EasManagedApp)
                ) && 
                (
                    this.TimeoutOnDataBlockLocks == input.TimeoutOnDataBlockLocks ||
                    this.TimeoutOnDataBlockLocks.Equals(input.TimeoutOnDataBlockLocks)
                ) && 
                (
                    this.MaxAttachmentFileSizeInKbs == input.MaxAttachmentFileSizeInKbs ||
                    this.MaxAttachmentFileSizeInKbs.Equals(input.MaxAttachmentFileSizeInKbs)
                ) && 
                (
                    this.PendingCacheSizeLimitInMbs == input.PendingCacheSizeLimitInMbs ||
                    this.PendingCacheSizeLimitInMbs.Equals(input.PendingCacheSizeLimitInMbs)
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
                if (this.Description != null)
                {
                    hashCode = (hashCode * 59) + this.Description.GetHashCode();
                }
                if (this.LogLevelAsString != null)
                {
                    hashCode = (hashCode * 59) + this.LogLevelAsString.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.EasManagedApp.GetHashCode();
                hashCode = (hashCode * 59) + this.TimeoutOnDataBlockLocks.GetHashCode();
                hashCode = (hashCode * 59) + this.MaxAttachmentFileSizeInKbs.GetHashCode();
                hashCode = (hashCode * 59) + this.PendingCacheSizeLimitInMbs.GetHashCode();
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
