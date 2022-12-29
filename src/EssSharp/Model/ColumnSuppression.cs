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
    /// ColumnSuppression
    /// </summary>
    [DataContract(Name = "ColumnSuppression")]
    public partial class ColumnSuppression : IEquatable<ColumnSuppression>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnSuppression" /> class.
        /// </summary>
        /// <param name="zero">zero.</param>
        /// <param name="missing">missing.</param>
        /// <param name="underScore">underScore.</param>
        /// <param name="derived">derived.</param>
        /// <param name="noAccess">noAccess.</param>
        /// <param name="emptyBlocks">emptyBlocks.</param>
        /// <param name="invalid">invalid.</param>
        /// <param name="error">error.</param>
        public ColumnSuppression(bool zero = default(bool), bool missing = default(bool), bool underScore = default(bool), bool derived = default(bool), bool noAccess = default(bool), bool emptyBlocks = default(bool), bool invalid = default(bool), bool error = default(bool))
        {
            this.Zero = zero;
            this.Missing = missing;
            this.UnderScore = underScore;
            this.Derived = derived;
            this.NoAccess = noAccess;
            this.EmptyBlocks = emptyBlocks;
            this.Invalid = invalid;
            this.Error = error;
        }

        /// <summary>
        /// Gets or Sets Zero
        /// </summary>
        [DataMember(Name = "zero", EmitDefaultValue = true)]
        public bool Zero { get; set; }

        /// <summary>
        /// Gets or Sets Missing
        /// </summary>
        [DataMember(Name = "missing", EmitDefaultValue = true)]
        public bool Missing { get; set; }

        /// <summary>
        /// Gets or Sets UnderScore
        /// </summary>
        [DataMember(Name = "underScore", EmitDefaultValue = true)]
        public bool UnderScore { get; set; }

        /// <summary>
        /// Gets or Sets Derived
        /// </summary>
        [DataMember(Name = "derived", EmitDefaultValue = true)]
        public bool Derived { get; set; }

        /// <summary>
        /// Gets or Sets NoAccess
        /// </summary>
        [DataMember(Name = "noAccess", EmitDefaultValue = true)]
        public bool NoAccess { get; set; }

        /// <summary>
        /// Gets or Sets EmptyBlocks
        /// </summary>
        [DataMember(Name = "emptyBlocks", EmitDefaultValue = true)]
        public bool EmptyBlocks { get; set; }

        /// <summary>
        /// Gets or Sets Invalid
        /// </summary>
        [DataMember(Name = "invalid", EmitDefaultValue = true)]
        public bool Invalid { get; set; }

        /// <summary>
        /// Gets or Sets Error
        /// </summary>
        [DataMember(Name = "error", EmitDefaultValue = true)]
        public bool Error { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class ColumnSuppression {\n");
            sb.Append("  Zero: ").Append(Zero).Append("\n");
            sb.Append("  Missing: ").Append(Missing).Append("\n");
            sb.Append("  UnderScore: ").Append(UnderScore).Append("\n");
            sb.Append("  Derived: ").Append(Derived).Append("\n");
            sb.Append("  NoAccess: ").Append(NoAccess).Append("\n");
            sb.Append("  EmptyBlocks: ").Append(EmptyBlocks).Append("\n");
            sb.Append("  Invalid: ").Append(Invalid).Append("\n");
            sb.Append("  Error: ").Append(Error).Append("\n");
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
            return this.Equals(input as ColumnSuppression);
        }

        /// <summary>
        /// Returns true if ColumnSuppression instances are equal
        /// </summary>
        /// <param name="input">Instance of ColumnSuppression to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ColumnSuppression input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.Zero == input.Zero ||
                    this.Zero.Equals(input.Zero)
                ) && 
                (
                    this.Missing == input.Missing ||
                    this.Missing.Equals(input.Missing)
                ) && 
                (
                    this.UnderScore == input.UnderScore ||
                    this.UnderScore.Equals(input.UnderScore)
                ) && 
                (
                    this.Derived == input.Derived ||
                    this.Derived.Equals(input.Derived)
                ) && 
                (
                    this.NoAccess == input.NoAccess ||
                    this.NoAccess.Equals(input.NoAccess)
                ) && 
                (
                    this.EmptyBlocks == input.EmptyBlocks ||
                    this.EmptyBlocks.Equals(input.EmptyBlocks)
                ) && 
                (
                    this.Invalid == input.Invalid ||
                    this.Invalid.Equals(input.Invalid)
                ) && 
                (
                    this.Error == input.Error ||
                    this.Error.Equals(input.Error)
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
                hashCode = (hashCode * 59) + this.Zero.GetHashCode();
                hashCode = (hashCode * 59) + this.Missing.GetHashCode();
                hashCode = (hashCode * 59) + this.UnderScore.GetHashCode();
                hashCode = (hashCode * 59) + this.Derived.GetHashCode();
                hashCode = (hashCode * 59) + this.NoAccess.GetHashCode();
                hashCode = (hashCode * 59) + this.EmptyBlocks.GetHashCode();
                hashCode = (hashCode * 59) + this.Invalid.GetHashCode();
                hashCode = (hashCode * 59) + this.Error.GetHashCode();
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
