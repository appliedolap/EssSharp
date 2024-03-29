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
    /// DrillthroughMetadataBean
    /// </summary>
    [DataContract(Name = "DrillthroughMetadataBean")]
    public partial class DrillthroughMetadataBean : IEquatable<DrillthroughMetadataBean>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DrillthroughMetadataBean" /> class.
        /// </summary>
        /// <param name="dtrContext">dtrContext.</param>
        /// <param name="aliasTable">aliasTable.</param>
        /// <param name="sessionId">sessionId.</param>
        public DrillthroughMetadataBean(List<DrillThroughRange> dtrContext = default(List<DrillThroughRange>), string aliasTable = default(string), string sessionId = default(string))
        {
            this.DtrContext = dtrContext;
            this.AliasTable = aliasTable;
            this.SessionId = sessionId;
        }

        /// <summary>
        /// Gets or Sets DtrContext
        /// </summary>
        [DataMember(Name = "dtrContext", EmitDefaultValue = false)]
        public List<DrillThroughRange> DtrContext { get; set; }

        /// <summary>
        /// Gets or Sets AliasTable
        /// </summary>
        [DataMember(Name = "aliasTable", EmitDefaultValue = false)]
        public string AliasTable { get; set; }

        /// <summary>
        /// Gets or Sets SessionId
        /// </summary>
        [DataMember(Name = "sessionId", EmitDefaultValue = false)]
        public string SessionId { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class DrillthroughMetadataBean {\n");
            sb.Append("  DtrContext: ").Append(DtrContext).Append("\n");
            sb.Append("  AliasTable: ").Append(AliasTable).Append("\n");
            sb.Append("  SessionId: ").Append(SessionId).Append("\n");
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
            return this.Equals(input as DrillthroughMetadataBean);
        }

        /// <summary>
        /// Returns true if DrillthroughMetadataBean instances are equal
        /// </summary>
        /// <param name="input">Instance of DrillthroughMetadataBean to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(DrillthroughMetadataBean input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.DtrContext == input.DtrContext ||
                    this.DtrContext != null &&
                    input.DtrContext != null &&
                    this.DtrContext.SequenceEqual(input.DtrContext)
                ) && 
                (
                    this.AliasTable == input.AliasTable ||
                    (this.AliasTable != null &&
                    this.AliasTable.Equals(input.AliasTable))
                ) && 
                (
                    this.SessionId == input.SessionId ||
                    (this.SessionId != null &&
                    this.SessionId.Equals(input.SessionId))
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
                if (this.DtrContext != null)
                {
                    hashCode = (hashCode * 59) + this.DtrContext.GetHashCode();
                }
                if (this.AliasTable != null)
                {
                    hashCode = (hashCode * 59) + this.AliasTable.GetHashCode();
                }
                if (this.SessionId != null)
                {
                    hashCode = (hashCode * 59) + this.SessionId.GetHashCode();
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
