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
    /// UploadFilePartResponse
    /// </summary>
    [DataContract(Name = "UploadFilePartResponse")]
    public partial class UploadFilePartResponse : IEquatable<UploadFilePartResponse>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UploadFilePartResponse" /> class.
        /// </summary>
        /// <param name="partNum">partNum.</param>
        /// <param name="response">response.</param>
        public UploadFilePartResponse(int partNum = default(int), Response response = default(Response))
        {
            this.PartNum = partNum;
            this.Response = response;
        }

        /// <summary>
        /// Gets or Sets PartNum
        /// </summary>
        [DataMember(Name = "partNum", EmitDefaultValue = false)]
        public int PartNum { get; set; }

        /// <summary>
        /// Gets or Sets Response
        /// </summary>
        [DataMember(Name = "response", EmitDefaultValue = false)]
        public Response Response { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class UploadFilePartResponse {\n");
            sb.Append("  PartNum: ").Append(PartNum).Append("\n");
            sb.Append("  Response: ").Append(Response).Append("\n");
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
            return this.Equals(input as UploadFilePartResponse);
        }

        /// <summary>
        /// Returns true if UploadFilePartResponse instances are equal
        /// </summary>
        /// <param name="input">Instance of UploadFilePartResponse to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(UploadFilePartResponse input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.PartNum == input.PartNum ||
                    this.PartNum.Equals(input.PartNum)
                ) && 
                (
                    this.Response == input.Response ||
                    (this.Response != null &&
                    this.Response.Equals(input.Response))
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
                hashCode = (hashCode * 59) + this.PartNum.GetHashCode();
                if (this.Response != null)
                {
                    hashCode = (hashCode * 59) + this.Response.GetHashCode();
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
