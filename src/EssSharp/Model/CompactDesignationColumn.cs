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
    /// CompactDesignationColumn
    /// </summary>
    [DataContract(Name = "CompactDesignationColumn")]
    public partial class CompactDesignationColumn : IEquatable<CompactDesignationColumn>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompactDesignationColumn" /> class.
        /// </summary>
        /// <param name="originalHeaderName">originalHeaderName.</param>
        /// <param name="objectName">objectName.</param>
        /// <param name="referenceObjectName">referenceObjectName.</param>
        /// <param name="objectType">objectType.</param>
        /// <param name="formula">formula.</param>
        /// <param name="dimensionAlias">dimensionAlias.</param>
        /// <param name="externalDimName">externalDimName.</param>
        /// <param name="solveOrder">solveOrder.</param>
        public CompactDesignationColumn(string originalHeaderName = default(string), string objectName = default(string), string referenceObjectName = default(string), string objectType = default(string), string formula = default(string), string dimensionAlias = default(string), string externalDimName = default(string), int solveOrder = default(int))
        {
            this.OriginalHeaderName = originalHeaderName;
            this.ObjectName = objectName;
            this.ReferenceObjectName = referenceObjectName;
            this.ObjectType = objectType;
            this.Formula = formula;
            this.DimensionAlias = dimensionAlias;
            this.ExternalDimName = externalDimName;
            this.SolveOrder = solveOrder;
        }

        /// <summary>
        /// Gets or Sets OriginalHeaderName
        /// </summary>
        [DataMember(Name = "originalHeaderName", EmitDefaultValue = false)]
        public string OriginalHeaderName { get; set; }

        /// <summary>
        /// Gets or Sets ObjectName
        /// </summary>
        [DataMember(Name = "objectName", EmitDefaultValue = false)]
        public string ObjectName { get; set; }

        /// <summary>
        /// Gets or Sets ReferenceObjectName
        /// </summary>
        [DataMember(Name = "referenceObjectName", EmitDefaultValue = false)]
        public string ReferenceObjectName { get; set; }

        /// <summary>
        /// Gets or Sets ObjectType
        /// </summary>
        [DataMember(Name = "objectType", EmitDefaultValue = false)]
        public string ObjectType { get; set; }

        /// <summary>
        /// Gets or Sets Formula
        /// </summary>
        [DataMember(Name = "formula", EmitDefaultValue = false)]
        public string Formula { get; set; }

        /// <summary>
        /// Gets or Sets DimensionAlias
        /// </summary>
        [DataMember(Name = "dimensionAlias", EmitDefaultValue = false)]
        public string DimensionAlias { get; set; }

        /// <summary>
        /// Gets or Sets ExternalDimName
        /// </summary>
        [DataMember(Name = "externalDimName", EmitDefaultValue = false)]
        public string ExternalDimName { get; set; }

        /// <summary>
        /// Gets or Sets SolveOrder
        /// </summary>
        [DataMember(Name = "solveOrder", EmitDefaultValue = false)]
        public int SolveOrder { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class CompactDesignationColumn {\n");
            sb.Append("  OriginalHeaderName: ").Append(OriginalHeaderName).Append("\n");
            sb.Append("  ObjectName: ").Append(ObjectName).Append("\n");
            sb.Append("  ReferenceObjectName: ").Append(ReferenceObjectName).Append("\n");
            sb.Append("  ObjectType: ").Append(ObjectType).Append("\n");
            sb.Append("  Formula: ").Append(Formula).Append("\n");
            sb.Append("  DimensionAlias: ").Append(DimensionAlias).Append("\n");
            sb.Append("  ExternalDimName: ").Append(ExternalDimName).Append("\n");
            sb.Append("  SolveOrder: ").Append(SolveOrder).Append("\n");
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
            return this.Equals(input as CompactDesignationColumn);
        }

        /// <summary>
        /// Returns true if CompactDesignationColumn instances are equal
        /// </summary>
        /// <param name="input">Instance of CompactDesignationColumn to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(CompactDesignationColumn input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.OriginalHeaderName == input.OriginalHeaderName ||
                    (this.OriginalHeaderName != null &&
                    this.OriginalHeaderName.Equals(input.OriginalHeaderName))
                ) && 
                (
                    this.ObjectName == input.ObjectName ||
                    (this.ObjectName != null &&
                    this.ObjectName.Equals(input.ObjectName))
                ) && 
                (
                    this.ReferenceObjectName == input.ReferenceObjectName ||
                    (this.ReferenceObjectName != null &&
                    this.ReferenceObjectName.Equals(input.ReferenceObjectName))
                ) && 
                (
                    this.ObjectType == input.ObjectType ||
                    (this.ObjectType != null &&
                    this.ObjectType.Equals(input.ObjectType))
                ) && 
                (
                    this.Formula == input.Formula ||
                    (this.Formula != null &&
                    this.Formula.Equals(input.Formula))
                ) && 
                (
                    this.DimensionAlias == input.DimensionAlias ||
                    (this.DimensionAlias != null &&
                    this.DimensionAlias.Equals(input.DimensionAlias))
                ) && 
                (
                    this.ExternalDimName == input.ExternalDimName ||
                    (this.ExternalDimName != null &&
                    this.ExternalDimName.Equals(input.ExternalDimName))
                ) && 
                (
                    this.SolveOrder == input.SolveOrder ||
                    this.SolveOrder.Equals(input.SolveOrder)
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
                if (this.OriginalHeaderName != null)
                {
                    hashCode = (hashCode * 59) + this.OriginalHeaderName.GetHashCode();
                }
                if (this.ObjectName != null)
                {
                    hashCode = (hashCode * 59) + this.ObjectName.GetHashCode();
                }
                if (this.ReferenceObjectName != null)
                {
                    hashCode = (hashCode * 59) + this.ReferenceObjectName.GetHashCode();
                }
                if (this.ObjectType != null)
                {
                    hashCode = (hashCode * 59) + this.ObjectType.GetHashCode();
                }
                if (this.Formula != null)
                {
                    hashCode = (hashCode * 59) + this.Formula.GetHashCode();
                }
                if (this.DimensionAlias != null)
                {
                    hashCode = (hashCode * 59) + this.DimensionAlias.GetHashCode();
                }
                if (this.ExternalDimName != null)
                {
                    hashCode = (hashCode * 59) + this.ExternalDimName.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.SolveOrder.GetHashCode();
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
