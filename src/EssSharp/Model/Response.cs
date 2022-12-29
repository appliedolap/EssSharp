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
    /// Response
    /// </summary>
    [DataContract(Name = "Response")]
    public partial class Response : IEquatable<Response>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Response" /> class.
        /// </summary>
        /// <param name="opRequestId">opRequestId.</param>
        /// <param name="opcContentMd5">opcContentMd5.</param>
        /// <param name="etag">etag.</param>
        public Response(string opRequestId = default(string), string opcContentMd5 = default(string), string etag = default(string))
        {
            this.OpRequestId = opRequestId;
            this.OpcContentMd5 = opcContentMd5;
            this.Etag = etag;
        }

        /// <summary>
        /// Gets or Sets OpRequestId
        /// </summary>
        [DataMember(Name = "opRequestId", EmitDefaultValue = false)]
        public string OpRequestId { get; set; }

        /// <summary>
        /// Gets or Sets OpcContentMd5
        /// </summary>
        [DataMember(Name = "opcContentMd5", EmitDefaultValue = false)]
        public string OpcContentMd5 { get; set; }

        /// <summary>
        /// Gets or Sets Etag
        /// </summary>
        [DataMember(Name = "etag", EmitDefaultValue = false)]
        public string Etag { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class Response {\n");
            sb.Append("  OpRequestId: ").Append(OpRequestId).Append("\n");
            sb.Append("  OpcContentMd5: ").Append(OpcContentMd5).Append("\n");
            sb.Append("  Etag: ").Append(Etag).Append("\n");
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
            return this.Equals(input as Response);
        }

        /// <summary>
        /// Returns true if Response instances are equal
        /// </summary>
        /// <param name="input">Instance of Response to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Response input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.OpRequestId == input.OpRequestId ||
                    (this.OpRequestId != null &&
                    this.OpRequestId.Equals(input.OpRequestId))
                ) && 
                (
                    this.OpcContentMd5 == input.OpcContentMd5 ||
                    (this.OpcContentMd5 != null &&
                    this.OpcContentMd5.Equals(input.OpcContentMd5))
                ) && 
                (
                    this.Etag == input.Etag ||
                    (this.Etag != null &&
                    this.Etag.Equals(input.Etag))
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
                if (this.OpRequestId != null)
                {
                    hashCode = (hashCode * 59) + this.OpRequestId.GetHashCode();
                }
                if (this.OpcContentMd5 != null)
                {
                    hashCode = (hashCode * 59) + this.OpcContentMd5.GetHashCode();
                }
                if (this.Etag != null)
                {
                    hashCode = (hashCode * 59) + this.Etag.GetHashCode();
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
