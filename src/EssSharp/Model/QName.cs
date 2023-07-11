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
    /// QName
    /// </summary>
    [DataContract(Name = "QName")]
    public partial class QName : IEquatable<QName>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QName" /> class.
        /// </summary>
        /// <param name="namespaceURI">namespaceURI.</param>
        /// <param name="localPart">localPart.</param>
        /// <param name="prefix">prefix.</param>
        public QName(string namespaceURI = default(string), string localPart = default(string), string prefix = default(string))
        {
            this.NamespaceURI = namespaceURI;
            this.LocalPart = localPart;
            this.Prefix = prefix;
        }

        /// <summary>
        /// Gets or Sets NamespaceURI
        /// </summary>
        [DataMember(Name = "namespaceURI", EmitDefaultValue = false)]
        public string NamespaceURI { get; set; }

        /// <summary>
        /// Gets or Sets LocalPart
        /// </summary>
        [DataMember(Name = "localPart", EmitDefaultValue = false)]
        public string LocalPart { get; set; }

        /// <summary>
        /// Gets or Sets Prefix
        /// </summary>
        [DataMember(Name = "prefix", EmitDefaultValue = false)]
        public string Prefix { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class QName {\n");
            sb.Append("  NamespaceURI: ").Append(NamespaceURI).Append("\n");
            sb.Append("  LocalPart: ").Append(LocalPart).Append("\n");
            sb.Append("  Prefix: ").Append(Prefix).Append("\n");
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
            return this.Equals(input as QName);
        }

        /// <summary>
        /// Returns true if QName instances are equal
        /// </summary>
        /// <param name="input">Instance of QName to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(QName input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.NamespaceURI == input.NamespaceURI ||
                    (this.NamespaceURI != null &&
                    this.NamespaceURI.Equals(input.NamespaceURI))
                ) && 
                (
                    this.LocalPart == input.LocalPart ||
                    (this.LocalPart != null &&
                    this.LocalPart.Equals(input.LocalPart))
                ) && 
                (
                    this.Prefix == input.Prefix ||
                    (this.Prefix != null &&
                    this.Prefix.Equals(input.Prefix))
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
                if (this.NamespaceURI != null)
                {
                    hashCode = (hashCode * 59) + this.NamespaceURI.GetHashCode();
                }
                if (this.LocalPart != null)
                {
                    hashCode = (hashCode * 59) + this.LocalPart.GetHashCode();
                }
                if (this.Prefix != null)
                {
                    hashCode = (hashCode * 59) + this.Prefix.GetHashCode();
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
