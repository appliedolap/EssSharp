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
    /// JAXBElement
    /// </summary>
    [DataContract(Name = "JAXBElement")]
    public partial class JAXBElement : IEquatable<JAXBElement>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JAXBElement" /> class.
        /// </summary>
        /// <param name="name">name.</param>
        /// <param name="value">value.</param>
        /// <param name="nil">nil.</param>
        /// <param name="globalScope">globalScope.</param>
        /// <param name="typeSubstituted">typeSubstituted.</param>
        public JAXBElement(QName name = default(QName), Object value = default(Object), bool nil = default(bool), bool globalScope = default(bool), bool typeSubstituted = default(bool))
        {
            this.Name = name;
            this.Value = value;
            this.Nil = nil;
            this.GlobalScope = globalScope;
            this.TypeSubstituted = typeSubstituted;
        }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public QName Name { get; set; }

        /// <summary>
        /// Gets or Sets Value
        /// </summary>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public Object Value { get; set; }

        /// <summary>
        /// Gets or Sets Nil
        /// </summary>
        [DataMember(Name = "nil", EmitDefaultValue = true)]
        public bool Nil { get; set; }

        /// <summary>
        /// Gets or Sets GlobalScope
        /// </summary>
        [DataMember(Name = "globalScope", EmitDefaultValue = true)]
        public bool GlobalScope { get; set; }

        /// <summary>
        /// Gets or Sets TypeSubstituted
        /// </summary>
        [DataMember(Name = "typeSubstituted", EmitDefaultValue = true)]
        public bool TypeSubstituted { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class JAXBElement {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Value: ").Append(Value).Append("\n");
            sb.Append("  Nil: ").Append(Nil).Append("\n");
            sb.Append("  GlobalScope: ").Append(GlobalScope).Append("\n");
            sb.Append("  TypeSubstituted: ").Append(TypeSubstituted).Append("\n");
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
            return this.Equals(input as JAXBElement);
        }

        /// <summary>
        /// Returns true if JAXBElement instances are equal
        /// </summary>
        /// <param name="input">Instance of JAXBElement to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(JAXBElement input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.Name == input.Name ||
                    (this.Name != null &&
                    this.Name.Equals(input.Name))
                ) && 
                (
                    this.Value == input.Value ||
                    (this.Value != null &&
                    this.Value.Equals(input.Value))
                ) && 
                (
                    this.Nil == input.Nil ||
                    this.Nil.Equals(input.Nil)
                ) && 
                (
                    this.GlobalScope == input.GlobalScope ||
                    this.GlobalScope.Equals(input.GlobalScope)
                ) && 
                (
                    this.TypeSubstituted == input.TypeSubstituted ||
                    this.TypeSubstituted.Equals(input.TypeSubstituted)
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
                if (this.Name != null)
                {
                    hashCode = (hashCode * 59) + this.Name.GetHashCode();
                }
                if (this.Value != null)
                {
                    hashCode = (hashCode * 59) + this.Value.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.Nil.GetHashCode();
                hashCode = (hashCode * 59) + this.GlobalScope.GetHashCode();
                hashCode = (hashCode * 59) + this.TypeSubstituted.GetHashCode();
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
