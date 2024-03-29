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
    /// BufferSettings
    /// </summary>
    [DataContract(Name = "BufferSettings")]
    public partial class BufferSettings : IEquatable<BufferSettings>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BufferSettings" /> class.
        /// </summary>
        /// <param name="dataRetrievalBufferSize">dataRetrievalBufferSize.</param>
        /// <param name="dataRetrievalSortBufferSize">dataRetrievalSortBufferSize.</param>
        public BufferSettings(long dataRetrievalBufferSize = default(long), long dataRetrievalSortBufferSize = default(long))
        {
            this.DataRetrievalBufferSize = dataRetrievalBufferSize;
            this.DataRetrievalSortBufferSize = dataRetrievalSortBufferSize;
        }

        /// <summary>
        /// Gets or Sets DataRetrievalBufferSize
        /// </summary>
        [DataMember(Name = "dataRetrievalBufferSize", EmitDefaultValue = false)]
        public long DataRetrievalBufferSize { get; set; }

        /// <summary>
        /// Gets or Sets DataRetrievalSortBufferSize
        /// </summary>
        [DataMember(Name = "dataRetrievalSortBufferSize", EmitDefaultValue = false)]
        public long DataRetrievalSortBufferSize { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class BufferSettings {\n");
            sb.Append("  DataRetrievalBufferSize: ").Append(DataRetrievalBufferSize).Append("\n");
            sb.Append("  DataRetrievalSortBufferSize: ").Append(DataRetrievalSortBufferSize).Append("\n");
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
            return this.Equals(input as BufferSettings);
        }

        /// <summary>
        /// Returns true if BufferSettings instances are equal
        /// </summary>
        /// <param name="input">Instance of BufferSettings to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(BufferSettings input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.DataRetrievalBufferSize == input.DataRetrievalBufferSize ||
                    this.DataRetrievalBufferSize.Equals(input.DataRetrievalBufferSize)
                ) && 
                (
                    this.DataRetrievalSortBufferSize == input.DataRetrievalSortBufferSize ||
                    this.DataRetrievalSortBufferSize.Equals(input.DataRetrievalSortBufferSize)
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
                hashCode = (hashCode * 59) + this.DataRetrievalBufferSize.GetHashCode();
                hashCode = (hashCode * 59) + this.DataRetrievalSortBufferSize.GetHashCode();
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
