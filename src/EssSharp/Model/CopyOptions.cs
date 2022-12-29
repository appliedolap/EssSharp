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
    /// CopyOptions
    /// </summary>
    [DataContract(Name = "CopyOptions")]
    public partial class CopyOptions : IEquatable<CopyOptions>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CopyOptions" /> class.
        /// </summary>
        /// <param name="properties">properties.</param>
        /// <param name="comments">comments.</param>
        /// <param name="data">data.</param>
        /// <param name="tasks">tasks.</param>
        /// <param name="keyMetrics">keyMetrics.</param>
        /// <param name="slice">slice.</param>
        /// <param name="layouts">layouts.</param>
        /// <param name="approvers">approvers.</param>
        /// <param name="participants">participants.</param>
        public CopyOptions(bool properties = default(bool), bool comments = default(bool), bool data = default(bool), bool tasks = default(bool), bool keyMetrics = default(bool), bool slice = default(bool), bool layouts = default(bool), bool approvers = default(bool), bool participants = default(bool))
        {
            this.Properties = properties;
            this.Comments = comments;
            this.Data = data;
            this.Tasks = tasks;
            this.KeyMetrics = keyMetrics;
            this.Slice = slice;
            this.Layouts = layouts;
            this.Approvers = approvers;
            this.Participants = participants;
        }

        /// <summary>
        /// Gets or Sets Properties
        /// </summary>
        [DataMember(Name = "properties", EmitDefaultValue = true)]
        public bool Properties { get; set; }

        /// <summary>
        /// Gets or Sets Comments
        /// </summary>
        [DataMember(Name = "comments", EmitDefaultValue = true)]
        public bool Comments { get; set; }

        /// <summary>
        /// Gets or Sets Data
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = true)]
        public bool Data { get; set; }

        /// <summary>
        /// Gets or Sets Tasks
        /// </summary>
        [DataMember(Name = "tasks", EmitDefaultValue = true)]
        public bool Tasks { get; set; }

        /// <summary>
        /// Gets or Sets KeyMetrics
        /// </summary>
        [DataMember(Name = "keyMetrics", EmitDefaultValue = true)]
        public bool KeyMetrics { get; set; }

        /// <summary>
        /// Gets or Sets Slice
        /// </summary>
        [DataMember(Name = "slice", EmitDefaultValue = true)]
        public bool Slice { get; set; }

        /// <summary>
        /// Gets or Sets Layouts
        /// </summary>
        [DataMember(Name = "layouts", EmitDefaultValue = true)]
        public bool Layouts { get; set; }

        /// <summary>
        /// Gets or Sets Approvers
        /// </summary>
        [DataMember(Name = "approvers", EmitDefaultValue = true)]
        public bool Approvers { get; set; }

        /// <summary>
        /// Gets or Sets Participants
        /// </summary>
        [DataMember(Name = "participants", EmitDefaultValue = true)]
        public bool Participants { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class CopyOptions {\n");
            sb.Append("  Properties: ").Append(Properties).Append("\n");
            sb.Append("  Comments: ").Append(Comments).Append("\n");
            sb.Append("  Data: ").Append(Data).Append("\n");
            sb.Append("  Tasks: ").Append(Tasks).Append("\n");
            sb.Append("  KeyMetrics: ").Append(KeyMetrics).Append("\n");
            sb.Append("  Slice: ").Append(Slice).Append("\n");
            sb.Append("  Layouts: ").Append(Layouts).Append("\n");
            sb.Append("  Approvers: ").Append(Approvers).Append("\n");
            sb.Append("  Participants: ").Append(Participants).Append("\n");
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
            return this.Equals(input as CopyOptions);
        }

        /// <summary>
        /// Returns true if CopyOptions instances are equal
        /// </summary>
        /// <param name="input">Instance of CopyOptions to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(CopyOptions input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.Properties == input.Properties ||
                    this.Properties.Equals(input.Properties)
                ) && 
                (
                    this.Comments == input.Comments ||
                    this.Comments.Equals(input.Comments)
                ) && 
                (
                    this.Data == input.Data ||
                    this.Data.Equals(input.Data)
                ) && 
                (
                    this.Tasks == input.Tasks ||
                    this.Tasks.Equals(input.Tasks)
                ) && 
                (
                    this.KeyMetrics == input.KeyMetrics ||
                    this.KeyMetrics.Equals(input.KeyMetrics)
                ) && 
                (
                    this.Slice == input.Slice ||
                    this.Slice.Equals(input.Slice)
                ) && 
                (
                    this.Layouts == input.Layouts ||
                    this.Layouts.Equals(input.Layouts)
                ) && 
                (
                    this.Approvers == input.Approvers ||
                    this.Approvers.Equals(input.Approvers)
                ) && 
                (
                    this.Participants == input.Participants ||
                    this.Participants.Equals(input.Participants)
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
                hashCode = (hashCode * 59) + this.Properties.GetHashCode();
                hashCode = (hashCode * 59) + this.Comments.GetHashCode();
                hashCode = (hashCode * 59) + this.Data.GetHashCode();
                hashCode = (hashCode * 59) + this.Tasks.GetHashCode();
                hashCode = (hashCode * 59) + this.KeyMetrics.GetHashCode();
                hashCode = (hashCode * 59) + this.Slice.GetHashCode();
                hashCode = (hashCode * 59) + this.Layouts.GetHashCode();
                hashCode = (hashCode * 59) + this.Approvers.GetHashCode();
                hashCode = (hashCode * 59) + this.Participants.GetHashCode();
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
