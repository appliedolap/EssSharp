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
    /// ApplicationConfigEntry
    /// </summary>
    [DataContract(Name = "ApplicationConfigEntry")]
    public partial class ApplicationConfigEntry : IEquatable<ApplicationConfigEntry>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationConfigEntry" /> class.
        /// </summary>
        /// <param name="key">key.</param>
        /// <param name="description">description.</param>
        /// <param name="syntax">syntax.</param>
        /// <param name="example">example.</param>
        /// <param name="value">value.</param>
        /// <param name="configured">configured.</param>
        /// <param name="links">links.</param>
        public ApplicationConfigEntry(string key = default(string), string description = default(string), string syntax = default(string), string example = default(string), string value = default(string), bool configured = default(bool), List<Link> links = default(List<Link>))
        {
            this.Key = key;
            this.Description = description;
            this.Syntax = syntax;
            this.Example = example;
            this.Value = value;
            this.Configured = configured;
            this.Links = links;
        }

        /// <summary>
        /// Gets or Sets Key
        /// </summary>
        [DataMember(Name = "key", EmitDefaultValue = false)]
        public string Key { get; set; }

        /// <summary>
        /// Gets or Sets Description
        /// </summary>
        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets Syntax
        /// </summary>
        [DataMember(Name = "syntax", EmitDefaultValue = false)]
        public string Syntax { get; set; }

        /// <summary>
        /// Gets or Sets Example
        /// </summary>
        [DataMember(Name = "example", EmitDefaultValue = false)]
        public string Example { get; set; }

        /// <summary>
        /// Gets or Sets Value
        /// </summary>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }

        /// <summary>
        /// Gets or Sets Configured
        /// </summary>
        [DataMember(Name = "configured", EmitDefaultValue = true)]
        public bool Configured { get; set; }

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
            sb.Append("class ApplicationConfigEntry {\n");
            sb.Append("  Key: ").Append(Key).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  Syntax: ").Append(Syntax).Append("\n");
            sb.Append("  Example: ").Append(Example).Append("\n");
            sb.Append("  Value: ").Append(Value).Append("\n");
            sb.Append("  Configured: ").Append(Configured).Append("\n");
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
            return this.Equals(input as ApplicationConfigEntry);
        }

        /// <summary>
        /// Returns true if ApplicationConfigEntry instances are equal
        /// </summary>
        /// <param name="input">Instance of ApplicationConfigEntry to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ApplicationConfigEntry input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.Key == input.Key ||
                    (this.Key != null &&
                    this.Key.Equals(input.Key))
                ) && 
                (
                    this.Description == input.Description ||
                    (this.Description != null &&
                    this.Description.Equals(input.Description))
                ) && 
                (
                    this.Syntax == input.Syntax ||
                    (this.Syntax != null &&
                    this.Syntax.Equals(input.Syntax))
                ) && 
                (
                    this.Example == input.Example ||
                    (this.Example != null &&
                    this.Example.Equals(input.Example))
                ) && 
                (
                    this.Value == input.Value ||
                    (this.Value != null &&
                    this.Value.Equals(input.Value))
                ) && 
                (
                    this.Configured == input.Configured ||
                    this.Configured.Equals(input.Configured)
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
                if (this.Key != null)
                {
                    hashCode = (hashCode * 59) + this.Key.GetHashCode();
                }
                if (this.Description != null)
                {
                    hashCode = (hashCode * 59) + this.Description.GetHashCode();
                }
                if (this.Syntax != null)
                {
                    hashCode = (hashCode * 59) + this.Syntax.GetHashCode();
                }
                if (this.Example != null)
                {
                    hashCode = (hashCode * 59) + this.Example.GetHashCode();
                }
                if (this.Value != null)
                {
                    hashCode = (hashCode * 59) + this.Value.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.Configured.GetHashCode();
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
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}
