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
    /// LayoutDimension
    /// </summary>
    [DataContract(Name = "LayoutDimension")]
    public partial class LayoutDimension : IEquatable<LayoutDimension>, IValidatableObject
    {
        /// <summary>
        /// Defines Axis
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum AxisEnum
        {
            /// <summary>
            /// Enum COLUMN for value: COLUMN
            /// </summary>
            [EnumMember(Value = "COLUMN")]
            COLUMN = 1,

            /// <summary>
            /// Enum ROW for value: ROW
            /// </summary>
            [EnumMember(Value = "ROW")]
            ROW = 2,

            /// <summary>
            /// Enum POV for value: POV
            /// </summary>
            [EnumMember(Value = "POV")]
            POV = 3

        }


        /// <summary>
        /// Gets or Sets Axis
        /// </summary>
        [DataMember(Name = "axis", EmitDefaultValue = false)]
        public AxisEnum? Axis { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="LayoutDimension" /> class.
        /// </summary>
        /// <param name="name">name.</param>
        /// <param name="displayName">displayName.</param>
        /// <param name="page">page.</param>
        /// <param name="hidden">hidden.</param>
        /// <param name="expanded">expanded.</param>
        /// <param name="axis">axis.</param>
        /// <param name="position">position.</param>
        public LayoutDimension(string name = default(string), string displayName = default(string), int page = default(int), bool hidden = default(bool), bool expanded = default(bool), AxisEnum? axis = default(AxisEnum?), int position = default(int))
        {
            this.Name = name;
            this.DisplayName = displayName;
            this.Page = page;
            this.Hidden = hidden;
            this.Expanded = expanded;
            this.Axis = axis;
            this.Position = position;
        }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets DisplayName
        /// </summary>
        [DataMember(Name = "displayName", EmitDefaultValue = false)]
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or Sets Page
        /// </summary>
        [DataMember(Name = "page", EmitDefaultValue = false)]
        public int Page { get; set; }

        /// <summary>
        /// Gets or Sets Hidden
        /// </summary>
        [DataMember(Name = "hidden", EmitDefaultValue = true)]
        public bool Hidden { get; set; }

        /// <summary>
        /// Gets or Sets Expanded
        /// </summary>
        [DataMember(Name = "expanded", EmitDefaultValue = true)]
        public bool Expanded { get; set; }

        /// <summary>
        /// Gets or Sets Position
        /// </summary>
        [DataMember(Name = "position", EmitDefaultValue = false)]
        public int Position { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class LayoutDimension {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  DisplayName: ").Append(DisplayName).Append("\n");
            sb.Append("  Page: ").Append(Page).Append("\n");
            sb.Append("  Hidden: ").Append(Hidden).Append("\n");
            sb.Append("  Expanded: ").Append(Expanded).Append("\n");
            sb.Append("  Axis: ").Append(Axis).Append("\n");
            sb.Append("  Position: ").Append(Position).Append("\n");
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
            return this.Equals(input as LayoutDimension);
        }

        /// <summary>
        /// Returns true if LayoutDimension instances are equal
        /// </summary>
        /// <param name="input">Instance of LayoutDimension to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(LayoutDimension input)
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
                    this.DisplayName == input.DisplayName ||
                    (this.DisplayName != null &&
                    this.DisplayName.Equals(input.DisplayName))
                ) && 
                (
                    this.Page == input.Page ||
                    this.Page.Equals(input.Page)
                ) && 
                (
                    this.Hidden == input.Hidden ||
                    this.Hidden.Equals(input.Hidden)
                ) && 
                (
                    this.Expanded == input.Expanded ||
                    this.Expanded.Equals(input.Expanded)
                ) && 
                (
                    this.Axis == input.Axis ||
                    this.Axis.Equals(input.Axis)
                ) && 
                (
                    this.Position == input.Position ||
                    this.Position.Equals(input.Position)
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
                if (this.DisplayName != null)
                {
                    hashCode = (hashCode * 59) + this.DisplayName.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.Page.GetHashCode();
                hashCode = (hashCode * 59) + this.Hidden.GetHashCode();
                hashCode = (hashCode * 59) + this.Expanded.GetHashCode();
                hashCode = (hashCode * 59) + this.Axis.GetHashCode();
                hashCode = (hashCode * 59) + this.Position.GetHashCode();
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
