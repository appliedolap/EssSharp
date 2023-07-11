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
    /// DrillthroughBean
    /// </summary>
    [DataContract(Name = "DrillthroughBean")]
    public partial class DrillthroughBean : IEquatable<DrillthroughBean>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DrillthroughBean" /> class.
        /// </summary>
        /// <param name="links">links.</param>
        /// <param name="useTempTables">useTempTables.</param>
        /// <param name="parameterMapping">parameterMapping.</param>
        /// <param name="columnMapping">columnMapping.</param>
        /// <param name="drillableRegions">drillableRegions.</param>
        /// <param name="columns">columns.</param>
        /// <param name="dataSourceName">dataSourceName.</param>
        /// <param name="url">url.</param>
        /// <param name="type">type.</param>
        /// <param name="name">name.</param>
        public DrillthroughBean(List<Link> links = default(List<Link>), bool useTempTables = default(bool), Dictionary<string, RunTimeParametersInfo> parameterMapping = default(Dictionary<string, RunTimeParametersInfo>), Dictionary<string, ColumnMappingInfo> columnMapping = default(Dictionary<string, ColumnMappingInfo>), List<string> drillableRegions = default(List<string>), List<string> columns = default(List<string>), string dataSourceName = default(string), string url = default(string), string type = default(string), string name = default(string))
        {
            this.Links = links;
            this.UseTempTables = useTempTables;
            this.ParameterMapping = parameterMapping;
            this.ColumnMapping = columnMapping;
            this.DrillableRegions = drillableRegions;
            this.Columns = columns;
            this.DataSourceName = dataSourceName;
            this.Url = url;
            this.Type = type;
            this.Name = name;
        }

        /// <summary>
        /// Gets or Sets Links
        /// </summary>
        [DataMember(Name = "links", EmitDefaultValue = false)]
        public List<Link> Links { get; set; }

        /// <summary>
        /// Gets or Sets UseTempTables
        /// </summary>
        [DataMember(Name = "useTempTables", EmitDefaultValue = true)]
        public bool UseTempTables { get; set; }

        /// <summary>
        /// Gets or Sets ParameterMapping
        /// </summary>
        [DataMember(Name = "parameterMapping", EmitDefaultValue = false)]
        public Dictionary<string, RunTimeParametersInfo> ParameterMapping { get; set; }

        /// <summary>
        /// Gets or Sets ColumnMapping
        /// </summary>
        [DataMember(Name = "columnMapping", EmitDefaultValue = false)]
        public Dictionary<string, ColumnMappingInfo> ColumnMapping { get; set; }

        /// <summary>
        /// Gets or Sets DrillableRegions
        /// </summary>
        [DataMember(Name = "drillableRegions", EmitDefaultValue = false)]
        public List<string> DrillableRegions { get; set; }

        /// <summary>
        /// Gets or Sets Columns
        /// </summary>
        [DataMember(Name = "columns", EmitDefaultValue = false)]
        public List<string> Columns { get; set; }

        /// <summary>
        /// Gets or Sets DataSourceName
        /// </summary>
        [DataMember(Name = "dataSourceName", EmitDefaultValue = false)]
        public string DataSourceName { get; set; }

        /// <summary>
        /// Gets or Sets Url
        /// </summary>
        [DataMember(Name = "url", EmitDefaultValue = false)]
        public string Url { get; set; }

        /// <summary>
        /// Gets or Sets Type
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class DrillthroughBean {\n");
            sb.Append("  Links: ").Append(Links).Append("\n");
            sb.Append("  UseTempTables: ").Append(UseTempTables).Append("\n");
            sb.Append("  ParameterMapping: ").Append(ParameterMapping).Append("\n");
            sb.Append("  ColumnMapping: ").Append(ColumnMapping).Append("\n");
            sb.Append("  DrillableRegions: ").Append(DrillableRegions).Append("\n");
            sb.Append("  Columns: ").Append(Columns).Append("\n");
            sb.Append("  DataSourceName: ").Append(DataSourceName).Append("\n");
            sb.Append("  Url: ").Append(Url).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
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
            return this.Equals(input as DrillthroughBean);
        }

        /// <summary>
        /// Returns true if DrillthroughBean instances are equal
        /// </summary>
        /// <param name="input">Instance of DrillthroughBean to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(DrillthroughBean input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.Links == input.Links ||
                    this.Links != null &&
                    input.Links != null &&
                    this.Links.SequenceEqual(input.Links)
                ) && 
                (
                    this.UseTempTables == input.UseTempTables ||
                    this.UseTempTables.Equals(input.UseTempTables)
                ) && 
                (
                    this.ParameterMapping == input.ParameterMapping ||
                    this.ParameterMapping != null &&
                    input.ParameterMapping != null &&
                    this.ParameterMapping.SequenceEqual(input.ParameterMapping)
                ) && 
                (
                    this.ColumnMapping == input.ColumnMapping ||
                    this.ColumnMapping != null &&
                    input.ColumnMapping != null &&
                    this.ColumnMapping.SequenceEqual(input.ColumnMapping)
                ) && 
                (
                    this.DrillableRegions == input.DrillableRegions ||
                    this.DrillableRegions != null &&
                    input.DrillableRegions != null &&
                    this.DrillableRegions.SequenceEqual(input.DrillableRegions)
                ) && 
                (
                    this.Columns == input.Columns ||
                    this.Columns != null &&
                    input.Columns != null &&
                    this.Columns.SequenceEqual(input.Columns)
                ) && 
                (
                    this.DataSourceName == input.DataSourceName ||
                    (this.DataSourceName != null &&
                    this.DataSourceName.Equals(input.DataSourceName))
                ) && 
                (
                    this.Url == input.Url ||
                    (this.Url != null &&
                    this.Url.Equals(input.Url))
                ) && 
                (
                    this.Type == input.Type ||
                    (this.Type != null &&
                    this.Type.Equals(input.Type))
                ) && 
                (
                    this.Name == input.Name ||
                    (this.Name != null &&
                    this.Name.Equals(input.Name))
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
                if (this.Links != null)
                {
                    hashCode = (hashCode * 59) + this.Links.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.UseTempTables.GetHashCode();
                if (this.ParameterMapping != null)
                {
                    hashCode = (hashCode * 59) + this.ParameterMapping.GetHashCode();
                }
                if (this.ColumnMapping != null)
                {
                    hashCode = (hashCode * 59) + this.ColumnMapping.GetHashCode();
                }
                if (this.DrillableRegions != null)
                {
                    hashCode = (hashCode * 59) + this.DrillableRegions.GetHashCode();
                }
                if (this.Columns != null)
                {
                    hashCode = (hashCode * 59) + this.Columns.GetHashCode();
                }
                if (this.DataSourceName != null)
                {
                    hashCode = (hashCode * 59) + this.DataSourceName.GetHashCode();
                }
                if (this.Url != null)
                {
                    hashCode = (hashCode * 59) + this.Url.GetHashCode();
                }
                if (this.Type != null)
                {
                    hashCode = (hashCode * 59) + this.Type.GetHashCode();
                }
                if (this.Name != null)
                {
                    hashCode = (hashCode * 59) + this.Name.GetHashCode();
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
