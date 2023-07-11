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
    /// ScenarioBean
    /// </summary>
    [DataContract(Name = "ScenarioBean")]
    public partial class ScenarioBean : IEquatable<ScenarioBean>, IValidatableObject
    {
        /// <summary>
        /// Defines State
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StateEnum
        {
            /// <summary>
            /// Enum NEW for value: NEW
            /// </summary>
            [EnumMember(Value = "NEW")]
            NEW = 1,

            /// <summary>
            /// Enum SUBMITTED for value: SUBMITTED
            /// </summary>
            [EnumMember(Value = "SUBMITTED")]
            SUBMITTED = 2,

            /// <summary>
            /// Enum APPROVED for value: APPROVED
            /// </summary>
            [EnumMember(Value = "APPROVED")]
            APPROVED = 3,

            /// <summary>
            /// Enum REJECTED for value: REJECTED
            /// </summary>
            [EnumMember(Value = "REJECTED")]
            REJECTED = 4,

            /// <summary>
            /// Enum APPLIED for value: APPLIED
            /// </summary>
            [EnumMember(Value = "APPLIED")]
            APPLIED = 5

        }


        /// <summary>
        /// Gets or Sets State
        /// </summary>
        [DataMember(Name = "state", EmitDefaultValue = false)]
        public StateEnum? State { get; set; }
        /// <summary>
        /// Defines Priority
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum PriorityEnum
        {
            /// <summary>
            /// Enum LOW for value: LOW
            /// </summary>
            [EnumMember(Value = "LOW")]
            LOW = 1,

            /// <summary>
            /// Enum MEDIUM for value: MEDIUM
            /// </summary>
            [EnumMember(Value = "MEDIUM")]
            MEDIUM = 2,

            /// <summary>
            /// Enum HIGH for value: HIGH
            /// </summary>
            [EnumMember(Value = "HIGH")]
            HIGH = 3

        }


        /// <summary>
        /// Gets or Sets Priority
        /// </summary>
        [DataMember(Name = "priority", EmitDefaultValue = false)]
        public PriorityEnum? Priority { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="ScenarioBean" /> class.
        /// </summary>
        /// <param name="links">links.</param>
        /// <param name="scenarioUser">scenarioUser.</param>
        /// <param name="scripts">scripts.</param>
        /// <param name="approvers">approvers.</param>
        /// <param name="sandbox">sandbox.</param>
        /// <param name="dueDate">dueDate.</param>
        /// <param name="overdue">overdue.</param>
        /// <param name="createdTime">createdTime.</param>
        /// <param name="submittedTime">submittedTime.</param>
        /// <param name="appliedTime">appliedTime.</param>
        /// <param name="refreshedTime">refreshedTime.</param>
        /// <param name="useCalculatedValues">useCalculatedValues.</param>
        /// <param name="commentsCount">commentsCount.</param>
        /// <param name="database">database.</param>
        /// <param name="application">application.</param>
        /// <param name="description">description.</param>
        /// <param name="participants">participants.</param>
        /// <param name="owner">owner.</param>
        /// <param name="state">state.</param>
        /// <param name="id">id.</param>
        /// <param name="name">name.</param>
        /// <param name="priority">priority.</param>
        public ScenarioBean(List<Link> links = default(List<Link>), bool scenarioUser = default(bool), List<ScriptBean> scripts = default(List<ScriptBean>), List<ApproverBean> approvers = default(List<ApproverBean>), string sandbox = default(string), long dueDate = default(long), bool overdue = default(bool), long createdTime = default(long), long submittedTime = default(long), long appliedTime = default(long), long refreshedTime = default(long), bool useCalculatedValues = default(bool), int commentsCount = default(int), string database = default(string), string application = default(string), string description = default(string), List<ParticipantBean> participants = default(List<ParticipantBean>), string owner = default(string), StateEnum? state = default(StateEnum?), long id = default(long), string name = default(string), PriorityEnum? priority = default(PriorityEnum?))
        {
            this.Links = links;
            this.ScenarioUser = scenarioUser;
            this.Scripts = scripts;
            this.Approvers = approvers;
            this.Sandbox = sandbox;
            this.DueDate = dueDate;
            this.Overdue = overdue;
            this.CreatedTime = createdTime;
            this.SubmittedTime = submittedTime;
            this.AppliedTime = appliedTime;
            this.RefreshedTime = refreshedTime;
            this.UseCalculatedValues = useCalculatedValues;
            this.CommentsCount = commentsCount;
            this.Database = database;
            this.Application = application;
            this.Description = description;
            this.Participants = participants;
            this.Owner = owner;
            this.State = state;
            this.Id = id;
            this.Name = name;
            this.Priority = priority;
        }

        /// <summary>
        /// Gets or Sets Links
        /// </summary>
        [DataMember(Name = "links", EmitDefaultValue = false)]
        public List<Link> Links { get; set; }

        /// <summary>
        /// Gets or Sets ScenarioUser
        /// </summary>
        [DataMember(Name = "scenarioUser", EmitDefaultValue = true)]
        public bool ScenarioUser { get; set; }

        /// <summary>
        /// Gets or Sets Scripts
        /// </summary>
        [DataMember(Name = "scripts", EmitDefaultValue = false)]
        public List<ScriptBean> Scripts { get; set; }

        /// <summary>
        /// Gets or Sets Approvers
        /// </summary>
        [DataMember(Name = "approvers", EmitDefaultValue = false)]
        public List<ApproverBean> Approvers { get; set; }

        /// <summary>
        /// Gets or Sets Sandbox
        /// </summary>
        [DataMember(Name = "sandbox", EmitDefaultValue = false)]
        public string Sandbox { get; set; }

        /// <summary>
        /// Gets or Sets DueDate
        /// </summary>
        [DataMember(Name = "dueDate", EmitDefaultValue = false)]
        public long DueDate { get; set; }

        /// <summary>
        /// Gets or Sets Overdue
        /// </summary>
        [DataMember(Name = "overdue", EmitDefaultValue = true)]
        public bool Overdue { get; set; }

        /// <summary>
        /// Gets or Sets CreatedTime
        /// </summary>
        [DataMember(Name = "createdTime", EmitDefaultValue = false)]
        public long CreatedTime { get; set; }

        /// <summary>
        /// Gets or Sets SubmittedTime
        /// </summary>
        [DataMember(Name = "submittedTime", EmitDefaultValue = false)]
        public long SubmittedTime { get; set; }

        /// <summary>
        /// Gets or Sets AppliedTime
        /// </summary>
        [DataMember(Name = "appliedTime", EmitDefaultValue = false)]
        public long AppliedTime { get; set; }

        /// <summary>
        /// Gets or Sets RefreshedTime
        /// </summary>
        [DataMember(Name = "refreshedTime", EmitDefaultValue = false)]
        public long RefreshedTime { get; set; }

        /// <summary>
        /// Gets or Sets UseCalculatedValues
        /// </summary>
        [DataMember(Name = "useCalculatedValues", EmitDefaultValue = true)]
        public bool UseCalculatedValues { get; set; }

        /// <summary>
        /// Gets or Sets CommentsCount
        /// </summary>
        [DataMember(Name = "commentsCount", EmitDefaultValue = false)]
        public int CommentsCount { get; set; }

        /// <summary>
        /// Gets or Sets Database
        /// </summary>
        [DataMember(Name = "database", EmitDefaultValue = false)]
        public string Database { get; set; }

        /// <summary>
        /// Gets or Sets Application
        /// </summary>
        [DataMember(Name = "application", EmitDefaultValue = false)]
        public string Application { get; set; }

        /// <summary>
        /// Gets or Sets Description
        /// </summary>
        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets Participants
        /// </summary>
        [DataMember(Name = "participants", EmitDefaultValue = false)]
        public List<ParticipantBean> Participants { get; set; }

        /// <summary>
        /// Gets or Sets Owner
        /// </summary>
        [DataMember(Name = "owner", EmitDefaultValue = false)]
        public string Owner { get; set; }

        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public long Id { get; set; }

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
            sb.Append("class ScenarioBean {\n");
            sb.Append("  Links: ").Append(Links).Append("\n");
            sb.Append("  ScenarioUser: ").Append(ScenarioUser).Append("\n");
            sb.Append("  Scripts: ").Append(Scripts).Append("\n");
            sb.Append("  Approvers: ").Append(Approvers).Append("\n");
            sb.Append("  Sandbox: ").Append(Sandbox).Append("\n");
            sb.Append("  DueDate: ").Append(DueDate).Append("\n");
            sb.Append("  Overdue: ").Append(Overdue).Append("\n");
            sb.Append("  CreatedTime: ").Append(CreatedTime).Append("\n");
            sb.Append("  SubmittedTime: ").Append(SubmittedTime).Append("\n");
            sb.Append("  AppliedTime: ").Append(AppliedTime).Append("\n");
            sb.Append("  RefreshedTime: ").Append(RefreshedTime).Append("\n");
            sb.Append("  UseCalculatedValues: ").Append(UseCalculatedValues).Append("\n");
            sb.Append("  CommentsCount: ").Append(CommentsCount).Append("\n");
            sb.Append("  Database: ").Append(Database).Append("\n");
            sb.Append("  Application: ").Append(Application).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  Participants: ").Append(Participants).Append("\n");
            sb.Append("  Owner: ").Append(Owner).Append("\n");
            sb.Append("  State: ").Append(State).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Priority: ").Append(Priority).Append("\n");
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
            return this.Equals(input as ScenarioBean);
        }

        /// <summary>
        /// Returns true if ScenarioBean instances are equal
        /// </summary>
        /// <param name="input">Instance of ScenarioBean to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ScenarioBean input)
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
                    this.ScenarioUser == input.ScenarioUser ||
                    this.ScenarioUser.Equals(input.ScenarioUser)
                ) && 
                (
                    this.Scripts == input.Scripts ||
                    this.Scripts != null &&
                    input.Scripts != null &&
                    this.Scripts.SequenceEqual(input.Scripts)
                ) && 
                (
                    this.Approvers == input.Approvers ||
                    this.Approvers != null &&
                    input.Approvers != null &&
                    this.Approvers.SequenceEqual(input.Approvers)
                ) && 
                (
                    this.Sandbox == input.Sandbox ||
                    (this.Sandbox != null &&
                    this.Sandbox.Equals(input.Sandbox))
                ) && 
                (
                    this.DueDate == input.DueDate ||
                    this.DueDate.Equals(input.DueDate)
                ) && 
                (
                    this.Overdue == input.Overdue ||
                    this.Overdue.Equals(input.Overdue)
                ) && 
                (
                    this.CreatedTime == input.CreatedTime ||
                    this.CreatedTime.Equals(input.CreatedTime)
                ) && 
                (
                    this.SubmittedTime == input.SubmittedTime ||
                    this.SubmittedTime.Equals(input.SubmittedTime)
                ) && 
                (
                    this.AppliedTime == input.AppliedTime ||
                    this.AppliedTime.Equals(input.AppliedTime)
                ) && 
                (
                    this.RefreshedTime == input.RefreshedTime ||
                    this.RefreshedTime.Equals(input.RefreshedTime)
                ) && 
                (
                    this.UseCalculatedValues == input.UseCalculatedValues ||
                    this.UseCalculatedValues.Equals(input.UseCalculatedValues)
                ) && 
                (
                    this.CommentsCount == input.CommentsCount ||
                    this.CommentsCount.Equals(input.CommentsCount)
                ) && 
                (
                    this.Database == input.Database ||
                    (this.Database != null &&
                    this.Database.Equals(input.Database))
                ) && 
                (
                    this.Application == input.Application ||
                    (this.Application != null &&
                    this.Application.Equals(input.Application))
                ) && 
                (
                    this.Description == input.Description ||
                    (this.Description != null &&
                    this.Description.Equals(input.Description))
                ) && 
                (
                    this.Participants == input.Participants ||
                    this.Participants != null &&
                    input.Participants != null &&
                    this.Participants.SequenceEqual(input.Participants)
                ) && 
                (
                    this.Owner == input.Owner ||
                    (this.Owner != null &&
                    this.Owner.Equals(input.Owner))
                ) && 
                (
                    this.State == input.State ||
                    this.State.Equals(input.State)
                ) && 
                (
                    this.Id == input.Id ||
                    this.Id.Equals(input.Id)
                ) && 
                (
                    this.Name == input.Name ||
                    (this.Name != null &&
                    this.Name.Equals(input.Name))
                ) && 
                (
                    this.Priority == input.Priority ||
                    this.Priority.Equals(input.Priority)
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
                hashCode = (hashCode * 59) + this.ScenarioUser.GetHashCode();
                if (this.Scripts != null)
                {
                    hashCode = (hashCode * 59) + this.Scripts.GetHashCode();
                }
                if (this.Approvers != null)
                {
                    hashCode = (hashCode * 59) + this.Approvers.GetHashCode();
                }
                if (this.Sandbox != null)
                {
                    hashCode = (hashCode * 59) + this.Sandbox.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.DueDate.GetHashCode();
                hashCode = (hashCode * 59) + this.Overdue.GetHashCode();
                hashCode = (hashCode * 59) + this.CreatedTime.GetHashCode();
                hashCode = (hashCode * 59) + this.SubmittedTime.GetHashCode();
                hashCode = (hashCode * 59) + this.AppliedTime.GetHashCode();
                hashCode = (hashCode * 59) + this.RefreshedTime.GetHashCode();
                hashCode = (hashCode * 59) + this.UseCalculatedValues.GetHashCode();
                hashCode = (hashCode * 59) + this.CommentsCount.GetHashCode();
                if (this.Database != null)
                {
                    hashCode = (hashCode * 59) + this.Database.GetHashCode();
                }
                if (this.Application != null)
                {
                    hashCode = (hashCode * 59) + this.Application.GetHashCode();
                }
                if (this.Description != null)
                {
                    hashCode = (hashCode * 59) + this.Description.GetHashCode();
                }
                if (this.Participants != null)
                {
                    hashCode = (hashCode * 59) + this.Participants.GetHashCode();
                }
                if (this.Owner != null)
                {
                    hashCode = (hashCode * 59) + this.Owner.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.State.GetHashCode();
                hashCode = (hashCode * 59) + this.Id.GetHashCode();
                if (this.Name != null)
                {
                    hashCode = (hashCode * 59) + this.Name.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.Priority.GetHashCode();
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
