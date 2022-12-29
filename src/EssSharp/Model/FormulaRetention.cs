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
    /// FormulaRetention
    /// </summary>
    [DataContract(Name = "FormulaRetention")]
    public partial class FormulaRetention : IEquatable<FormulaRetention>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormulaRetention" /> class.
        /// </summary>
        /// <param name="comments">comments.</param>
        /// <param name="zoom">zoom.</param>
        /// <param name="focus">focus.</param>
        /// <param name="retrive">retrive.</param>
        /// <param name="fill">fill.</param>
        public FormulaRetention(bool comments = default(bool), bool zoom = default(bool), bool focus = default(bool), bool retrive = default(bool), bool fill = default(bool))
        {
            this.Comments = comments;
            this.Zoom = zoom;
            this.Focus = focus;
            this.Retrive = retrive;
            this.Fill = fill;
        }

        /// <summary>
        /// Gets or Sets Comments
        /// </summary>
        [DataMember(Name = "comments", EmitDefaultValue = true)]
        public bool Comments { get; set; }

        /// <summary>
        /// Gets or Sets Zoom
        /// </summary>
        [DataMember(Name = "zoom", EmitDefaultValue = true)]
        public bool Zoom { get; set; }

        /// <summary>
        /// Gets or Sets Focus
        /// </summary>
        [DataMember(Name = "focus", EmitDefaultValue = true)]
        public bool Focus { get; set; }

        /// <summary>
        /// Gets or Sets Retrive
        /// </summary>
        [DataMember(Name = "retrive", EmitDefaultValue = true)]
        public bool Retrive { get; set; }

        /// <summary>
        /// Gets or Sets Fill
        /// </summary>
        [DataMember(Name = "fill", EmitDefaultValue = true)]
        public bool Fill { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class FormulaRetention {\n");
            sb.Append("  Comments: ").Append(Comments).Append("\n");
            sb.Append("  Zoom: ").Append(Zoom).Append("\n");
            sb.Append("  Focus: ").Append(Focus).Append("\n");
            sb.Append("  Retrive: ").Append(Retrive).Append("\n");
            sb.Append("  Fill: ").Append(Fill).Append("\n");
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
            return this.Equals(input as FormulaRetention);
        }

        /// <summary>
        /// Returns true if FormulaRetention instances are equal
        /// </summary>
        /// <param name="input">Instance of FormulaRetention to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(FormulaRetention input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.Comments == input.Comments ||
                    this.Comments.Equals(input.Comments)
                ) && 
                (
                    this.Zoom == input.Zoom ||
                    this.Zoom.Equals(input.Zoom)
                ) && 
                (
                    this.Focus == input.Focus ||
                    this.Focus.Equals(input.Focus)
                ) && 
                (
                    this.Retrive == input.Retrive ||
                    this.Retrive.Equals(input.Retrive)
                ) && 
                (
                    this.Fill == input.Fill ||
                    this.Fill.Equals(input.Fill)
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
                hashCode = (hashCode * 59) + this.Comments.GetHashCode();
                hashCode = (hashCode * 59) + this.Zoom.GetHashCode();
                hashCode = (hashCode * 59) + this.Focus.GetHashCode();
                hashCode = (hashCode * 59) + this.Retrive.GetHashCode();
                hashCode = (hashCode * 59) + this.Fill.GetHashCode();
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
