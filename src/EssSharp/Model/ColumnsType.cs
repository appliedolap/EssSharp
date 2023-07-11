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
    /// ColumnsType
    /// </summary>
    [DataContract(Name = "ColumnsType")]
    public partial class ColumnsType : IEquatable<ColumnsType>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnsType" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected ColumnsType() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnsType" /> class.
        /// </summary>
        /// <param name="column">column (required).</param>
        public ColumnsType(List<ColumnType> column = default(List<ColumnType>))
        {
            // to ensure "column" is required (not null)
            if (column == null)
            {
                throw new ArgumentNullException("column is a required property for ColumnsType and cannot be null");
            }
            this.Column = column;
        }

        /// <summary>
        /// Gets or Sets Column
        /// </summary>
        [DataMember(Name = "column", IsRequired = true, EmitDefaultValue = true)]
        public List<ColumnType> Column { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class ColumnsType {\n");
            sb.Append("  Column: ").Append(Column).Append("\n");
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
            return this.Equals(input as ColumnsType);
        }

        /// <summary>
        /// Returns true if ColumnsType instances are equal
        /// </summary>
        /// <param name="input">Instance of ColumnsType to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ColumnsType input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.Column == input.Column ||
                    this.Column != null &&
                    input.Column != null &&
                    this.Column.SequenceEqual(input.Column)
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
                if (this.Column != null)
                {
                    hashCode = (hashCode * 59) + this.Column.GetHashCode();
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
