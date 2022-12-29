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
    /// CreateFilePartUploadResponse
    /// </summary>
    [DataContract(Name = "CreateFilePartUploadResponse")]
    public partial class CreateFilePartUploadResponse : IEquatable<CreateFilePartUploadResponse>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateFilePartUploadResponse" /> class.
        /// </summary>
        /// <param name="uploadId">uploadId.</param>
        /// <param name="_namespace">_namespace.</param>
        /// <param name="_object">_object.</param>
        /// <param name="timeCreated">timeCreated.</param>
        /// <param name="bucket">bucket.</param>
        public CreateFilePartUploadResponse(string uploadId = default(string), string _namespace = default(string), string _object = default(string), string timeCreated = default(string), string bucket = default(string))
        {
            this.UploadId = uploadId;
            this.Namespace = _namespace;
            this.Object = _object;
            this.TimeCreated = timeCreated;
            this.Bucket = bucket;
        }

        /// <summary>
        /// Gets or Sets UploadId
        /// </summary>
        [DataMember(Name = "uploadId", EmitDefaultValue = false)]
        public string UploadId { get; set; }

        /// <summary>
        /// Gets or Sets Namespace
        /// </summary>
        [DataMember(Name = "namespace", EmitDefaultValue = false)]
        public string Namespace { get; set; }

        /// <summary>
        /// Gets or Sets Object
        /// </summary>
        [DataMember(Name = "object", EmitDefaultValue = false)]
        public string Object { get; set; }

        /// <summary>
        /// Gets or Sets TimeCreated
        /// </summary>
        [DataMember(Name = "timeCreated", EmitDefaultValue = false)]
        public string TimeCreated { get; set; }

        /// <summary>
        /// Gets or Sets Bucket
        /// </summary>
        [DataMember(Name = "bucket", EmitDefaultValue = false)]
        public string Bucket { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class CreateFilePartUploadResponse {\n");
            sb.Append("  UploadId: ").Append(UploadId).Append("\n");
            sb.Append("  Namespace: ").Append(Namespace).Append("\n");
            sb.Append("  Object: ").Append(Object).Append("\n");
            sb.Append("  TimeCreated: ").Append(TimeCreated).Append("\n");
            sb.Append("  Bucket: ").Append(Bucket).Append("\n");
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
            return this.Equals(input as CreateFilePartUploadResponse);
        }

        /// <summary>
        /// Returns true if CreateFilePartUploadResponse instances are equal
        /// </summary>
        /// <param name="input">Instance of CreateFilePartUploadResponse to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(CreateFilePartUploadResponse input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.UploadId == input.UploadId ||
                    (this.UploadId != null &&
                    this.UploadId.Equals(input.UploadId))
                ) && 
                (
                    this.Namespace == input.Namespace ||
                    (this.Namespace != null &&
                    this.Namespace.Equals(input.Namespace))
                ) && 
                (
                    this.Object == input.Object ||
                    (this.Object != null &&
                    this.Object.Equals(input.Object))
                ) && 
                (
                    this.TimeCreated == input.TimeCreated ||
                    (this.TimeCreated != null &&
                    this.TimeCreated.Equals(input.TimeCreated))
                ) && 
                (
                    this.Bucket == input.Bucket ||
                    (this.Bucket != null &&
                    this.Bucket.Equals(input.Bucket))
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
                if (this.UploadId != null)
                {
                    hashCode = (hashCode * 59) + this.UploadId.GetHashCode();
                }
                if (this.Namespace != null)
                {
                    hashCode = (hashCode * 59) + this.Namespace.GetHashCode();
                }
                if (this.Object != null)
                {
                    hashCode = (hashCode * 59) + this.Object.GetHashCode();
                }
                if (this.TimeCreated != null)
                {
                    hashCode = (hashCode * 59) + this.TimeCreated.GetHashCode();
                }
                if (this.Bucket != null)
                {
                    hashCode = (hashCode * 59) + this.Bucket.GetHashCode();
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
