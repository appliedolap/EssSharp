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
    /// MemberBean
    /// </summary>
    [DataContract(Name = "MemberBean")]
    public partial class MemberBean : IEquatable<MemberBean>, IValidatableObject
    {
        /// <summary>
        /// Defines Type
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum TypeEnum
        {
            /// <summary>
            /// Enum NONE for value: NONE
            /// </summary>
            [EnumMember(Value = "NONE")]
            NONE = 1,

            /// <summary>
            /// Enum NUMERIC for value: NUMERIC
            /// </summary>
            [EnumMember(Value = "NUMERIC")]
            NUMERIC = 2,

            /// <summary>
            /// Enum SMARTLIST for value: SMARTLIST
            /// </summary>
            [EnumMember(Value = "SMARTLIST")]
            SMARTLIST = 3,

            /// <summary>
            /// Enum DATE for value: DATE
            /// </summary>
            [EnumMember(Value = "DATE")]
            DATE = 4

        }


        /// <summary>
        /// Gets or Sets Type
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public TypeEnum? Type { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="MemberBean" /> class.
        /// </summary>
        /// <param name="name">name.</param>
        /// <param name="dimensionName">dimensionName.</param>
        /// <param name="numberOfChildren">numberOfChildren.</param>
        /// <param name="levelNumber">levelNumber.</param>
        /// <param name="generationNumber">generationNumber.</param>
        /// <param name="aliases">aliases.</param>
        /// <param name="activeAliasName">activeAliasName.</param>
        /// <param name="memberHasUniqueName">memberHasUniqueName.</param>
        /// <param name="uniqueName">uniqueName.</param>
        /// <param name="memberId">memberId.</param>
        /// <param name="uniqueId">uniqueId.</param>
        /// <param name="type">type.</param>
        /// <param name="memberSolveOrder">memberSolveOrder.</param>
        /// <param name="descendantsCount">descendantsCount.</param>
        /// <param name="previousSiblingsCount">previousSiblingsCount.</param>
        /// <param name="dimension">dimension.</param>
        /// <param name="attribute">attribute.</param>
        /// <param name="account">account.</param>
        /// <param name="links">links.</param>
        /// <param name="dimSolveOrder">dimSolveOrder.</param>
        /// <param name="dimensionType">dimensionType.</param>
        /// <param name="formatString">formatString.</param>
        /// <param name="dimStorageType">dimStorageType.</param>
        /// <param name="currencyConversionCategory">currencyConversionCategory.</param>
        /// <param name="uda">uda.</param>
        /// <param name="dataStorageType">dataStorageType.</param>
        /// <param name="parentName">parentName.</param>
        public MemberBean(string name = default(string), string dimensionName = default(string), int numberOfChildren = default(int), int levelNumber = default(int), int generationNumber = default(int), Dictionary<string, string> aliases = default(Dictionary<string, string>), string activeAliasName = default(string), bool memberHasUniqueName = default(bool), string uniqueName = default(string), string memberId = default(string), string uniqueId = default(string), TypeEnum? type = default(TypeEnum?), int memberSolveOrder = default(int), long descendantsCount = default(long), int previousSiblingsCount = default(int), bool dimension = default(bool), bool attribute = default(bool), bool account = default(bool), List<Link> links = default(List<Link>), int dimSolveOrder = default(int), string dimensionType = default(string), string formatString = default(string), string dimStorageType = default(string), string currencyConversionCategory = default(string), List<string> uda = default(List<string>), string dataStorageType = default(string), string parentName = default(string))
        {
            this.Name = name;
            this.DimensionName = dimensionName;
            this.NumberOfChildren = numberOfChildren;
            this.LevelNumber = levelNumber;
            this.GenerationNumber = generationNumber;
            this.Aliases = aliases;
            this.ActiveAliasName = activeAliasName;
            this.MemberHasUniqueName = memberHasUniqueName;
            this.UniqueName = uniqueName;
            this.MemberId = memberId;
            this.UniqueId = uniqueId;
            this.Type = type;
            this.MemberSolveOrder = memberSolveOrder;
            this.DescendantsCount = descendantsCount;
            this.PreviousSiblingsCount = previousSiblingsCount;
            this.Dimension = dimension;
            this.Attribute = attribute;
            this.Account = account;
            this.Links = links;
            this.DimSolveOrder = dimSolveOrder;
            this.DimensionType = dimensionType;
            this.FormatString = formatString;
            this.DimStorageType = dimStorageType;
            this.CurrencyConversionCategory = currencyConversionCategory;
            this.Uda = uda;
            this.DataStorageType = dataStorageType;
            this.ParentName = parentName;
        }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets DimensionName
        /// </summary>
        [DataMember(Name = "dimensionName", EmitDefaultValue = false)]
        public string DimensionName { get; set; }

        /// <summary>
        /// Gets or Sets NumberOfChildren
        /// </summary>
        [DataMember(Name = "numberOfChildren", EmitDefaultValue = false)]
        public int NumberOfChildren { get; set; }

        /// <summary>
        /// Gets or Sets LevelNumber
        /// </summary>
        [DataMember(Name = "levelNumber", EmitDefaultValue = false)]
        public int LevelNumber { get; set; }

        /// <summary>
        /// Gets or Sets GenerationNumber
        /// </summary>
        [DataMember(Name = "generationNumber", EmitDefaultValue = false)]
        public int GenerationNumber { get; set; }

        /// <summary>
        /// Gets or Sets Aliases
        /// </summary>
        [DataMember(Name = "aliases", EmitDefaultValue = false)]
        public Dictionary<string, string> Aliases { get; set; }

        /// <summary>
        /// Gets or Sets ActiveAliasName
        /// </summary>
        [DataMember(Name = "activeAliasName", EmitDefaultValue = false)]
        public string ActiveAliasName { get; set; }

        /// <summary>
        /// Gets or Sets MemberHasUniqueName
        /// </summary>
        [DataMember(Name = "memberHasUniqueName", EmitDefaultValue = true)]
        public bool MemberHasUniqueName { get; set; }

        /// <summary>
        /// Gets or Sets UniqueName
        /// </summary>
        [DataMember(Name = "uniqueName", EmitDefaultValue = false)]
        public string UniqueName { get; set; }

        /// <summary>
        /// Gets or Sets MemberId
        /// </summary>
        [DataMember(Name = "memberId", EmitDefaultValue = false)]
        public string MemberId { get; set; }

        /// <summary>
        /// Gets or Sets UniqueId
        /// </summary>
        [DataMember(Name = "uniqueId", EmitDefaultValue = false)]
        public string UniqueId { get; set; }

        /// <summary>
        /// Gets or Sets MemberSolveOrder
        /// </summary>
        [DataMember(Name = "memberSolveOrder", EmitDefaultValue = false)]
        public int MemberSolveOrder { get; set; }

        /// <summary>
        /// Gets or Sets DescendantsCount
        /// </summary>
        [DataMember(Name = "descendantsCount", EmitDefaultValue = false)]
        public long DescendantsCount { get; set; }

        /// <summary>
        /// Gets or Sets PreviousSiblingsCount
        /// </summary>
        [DataMember(Name = "previousSiblingsCount", EmitDefaultValue = false)]
        public int PreviousSiblingsCount { get; set; }

        /// <summary>
        /// Gets or Sets Dimension
        /// </summary>
        [DataMember(Name = "dimension", EmitDefaultValue = true)]
        public bool Dimension { get; set; }

        /// <summary>
        /// Gets or Sets Attribute
        /// </summary>
        [DataMember(Name = "attribute", EmitDefaultValue = true)]
        public bool Attribute { get; set; }

        /// <summary>
        /// Gets or Sets Account
        /// </summary>
        [DataMember(Name = "account", EmitDefaultValue = true)]
        public bool Account { get; set; }

        /// <summary>
        /// Gets or Sets Links
        /// </summary>
        [DataMember(Name = "links", EmitDefaultValue = false)]
        public List<Link> Links { get; set; }

        /// <summary>
        /// Gets or Sets DimSolveOrder
        /// </summary>
        [DataMember(Name = "dimSolveOrder", EmitDefaultValue = false)]
        public int DimSolveOrder { get; set; }

        /// <summary>
        /// Gets or Sets DimensionType
        /// </summary>
        [DataMember(Name = "dimensionType", EmitDefaultValue = false)]
        public string DimensionType { get; set; }

        /// <summary>
        /// Gets or Sets FormatString
        /// </summary>
        [DataMember(Name = "formatString", EmitDefaultValue = false)]
        public string FormatString { get; set; }

        /// <summary>
        /// Gets or Sets DimStorageType
        /// </summary>
        [DataMember(Name = "dimStorageType", EmitDefaultValue = false)]
        public string DimStorageType { get; set; }

        /// <summary>
        /// Gets or Sets CurrencyConversionCategory
        /// </summary>
        [DataMember(Name = "currencyConversionCategory", EmitDefaultValue = false)]
        public string CurrencyConversionCategory { get; set; }

        /// <summary>
        /// Gets or Sets Uda
        /// </summary>
        [DataMember(Name = "uda", EmitDefaultValue = false)]
        public List<string> Uda { get; set; }

        /// <summary>
        /// Gets or Sets DataStorageType
        /// </summary>
        [DataMember(Name = "dataStorageType", EmitDefaultValue = false)]
        public string DataStorageType { get; set; }

        /// <summary>
        /// Gets or Sets ParentName
        /// </summary>
        [DataMember(Name = "parentName", EmitDefaultValue = false)]
        public string ParentName { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class MemberBean {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  DimensionName: ").Append(DimensionName).Append("\n");
            sb.Append("  NumberOfChildren: ").Append(NumberOfChildren).Append("\n");
            sb.Append("  LevelNumber: ").Append(LevelNumber).Append("\n");
            sb.Append("  GenerationNumber: ").Append(GenerationNumber).Append("\n");
            sb.Append("  Aliases: ").Append(Aliases).Append("\n");
            sb.Append("  ActiveAliasName: ").Append(ActiveAliasName).Append("\n");
            sb.Append("  MemberHasUniqueName: ").Append(MemberHasUniqueName).Append("\n");
            sb.Append("  UniqueName: ").Append(UniqueName).Append("\n");
            sb.Append("  MemberId: ").Append(MemberId).Append("\n");
            sb.Append("  UniqueId: ").Append(UniqueId).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
            sb.Append("  MemberSolveOrder: ").Append(MemberSolveOrder).Append("\n");
            sb.Append("  DescendantsCount: ").Append(DescendantsCount).Append("\n");
            sb.Append("  PreviousSiblingsCount: ").Append(PreviousSiblingsCount).Append("\n");
            sb.Append("  Dimension: ").Append(Dimension).Append("\n");
            sb.Append("  Attribute: ").Append(Attribute).Append("\n");
            sb.Append("  Account: ").Append(Account).Append("\n");
            sb.Append("  Links: ").Append(Links).Append("\n");
            sb.Append("  DimSolveOrder: ").Append(DimSolveOrder).Append("\n");
            sb.Append("  DimensionType: ").Append(DimensionType).Append("\n");
            sb.Append("  FormatString: ").Append(FormatString).Append("\n");
            sb.Append("  DimStorageType: ").Append(DimStorageType).Append("\n");
            sb.Append("  CurrencyConversionCategory: ").Append(CurrencyConversionCategory).Append("\n");
            sb.Append("  Uda: ").Append(Uda).Append("\n");
            sb.Append("  DataStorageType: ").Append(DataStorageType).Append("\n");
            sb.Append("  ParentName: ").Append(ParentName).Append("\n");
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
            return this.Equals(input as MemberBean);
        }

        /// <summary>
        /// Returns true if MemberBean instances are equal
        /// </summary>
        /// <param name="input">Instance of MemberBean to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(MemberBean input)
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
                    this.DimensionName == input.DimensionName ||
                    (this.DimensionName != null &&
                    this.DimensionName.Equals(input.DimensionName))
                ) && 
                (
                    this.NumberOfChildren == input.NumberOfChildren ||
                    this.NumberOfChildren.Equals(input.NumberOfChildren)
                ) && 
                (
                    this.LevelNumber == input.LevelNumber ||
                    this.LevelNumber.Equals(input.LevelNumber)
                ) && 
                (
                    this.GenerationNumber == input.GenerationNumber ||
                    this.GenerationNumber.Equals(input.GenerationNumber)
                ) && 
                (
                    this.Aliases == input.Aliases ||
                    this.Aliases != null &&
                    input.Aliases != null &&
                    this.Aliases.SequenceEqual(input.Aliases)
                ) && 
                (
                    this.ActiveAliasName == input.ActiveAliasName ||
                    (this.ActiveAliasName != null &&
                    this.ActiveAliasName.Equals(input.ActiveAliasName))
                ) && 
                (
                    this.MemberHasUniqueName == input.MemberHasUniqueName ||
                    this.MemberHasUniqueName.Equals(input.MemberHasUniqueName)
                ) && 
                (
                    this.UniqueName == input.UniqueName ||
                    (this.UniqueName != null &&
                    this.UniqueName.Equals(input.UniqueName))
                ) && 
                (
                    this.MemberId == input.MemberId ||
                    (this.MemberId != null &&
                    this.MemberId.Equals(input.MemberId))
                ) && 
                (
                    this.UniqueId == input.UniqueId ||
                    (this.UniqueId != null &&
                    this.UniqueId.Equals(input.UniqueId))
                ) && 
                (
                    this.Type == input.Type ||
                    this.Type.Equals(input.Type)
                ) && 
                (
                    this.MemberSolveOrder == input.MemberSolveOrder ||
                    this.MemberSolveOrder.Equals(input.MemberSolveOrder)
                ) && 
                (
                    this.DescendantsCount == input.DescendantsCount ||
                    this.DescendantsCount.Equals(input.DescendantsCount)
                ) && 
                (
                    this.PreviousSiblingsCount == input.PreviousSiblingsCount ||
                    this.PreviousSiblingsCount.Equals(input.PreviousSiblingsCount)
                ) && 
                (
                    this.Dimension == input.Dimension ||
                    this.Dimension.Equals(input.Dimension)
                ) && 
                (
                    this.Attribute == input.Attribute ||
                    this.Attribute.Equals(input.Attribute)
                ) && 
                (
                    this.Account == input.Account ||
                    this.Account.Equals(input.Account)
                ) && 
                (
                    this.Links == input.Links ||
                    this.Links != null &&
                    input.Links != null &&
                    this.Links.SequenceEqual(input.Links)
                ) && 
                (
                    this.DimSolveOrder == input.DimSolveOrder ||
                    this.DimSolveOrder.Equals(input.DimSolveOrder)
                ) && 
                (
                    this.DimensionType == input.DimensionType ||
                    (this.DimensionType != null &&
                    this.DimensionType.Equals(input.DimensionType))
                ) && 
                (
                    this.FormatString == input.FormatString ||
                    (this.FormatString != null &&
                    this.FormatString.Equals(input.FormatString))
                ) && 
                (
                    this.DimStorageType == input.DimStorageType ||
                    (this.DimStorageType != null &&
                    this.DimStorageType.Equals(input.DimStorageType))
                ) && 
                (
                    this.CurrencyConversionCategory == input.CurrencyConversionCategory ||
                    (this.CurrencyConversionCategory != null &&
                    this.CurrencyConversionCategory.Equals(input.CurrencyConversionCategory))
                ) && 
                (
                    this.Uda == input.Uda ||
                    this.Uda != null &&
                    input.Uda != null &&
                    this.Uda.SequenceEqual(input.Uda)
                ) && 
                (
                    this.DataStorageType == input.DataStorageType ||
                    (this.DataStorageType != null &&
                    this.DataStorageType.Equals(input.DataStorageType))
                ) && 
                (
                    this.ParentName == input.ParentName ||
                    (this.ParentName != null &&
                    this.ParentName.Equals(input.ParentName))
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
                if (this.DimensionName != null)
                {
                    hashCode = (hashCode * 59) + this.DimensionName.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.NumberOfChildren.GetHashCode();
                hashCode = (hashCode * 59) + this.LevelNumber.GetHashCode();
                hashCode = (hashCode * 59) + this.GenerationNumber.GetHashCode();
                if (this.Aliases != null)
                {
                    hashCode = (hashCode * 59) + this.Aliases.GetHashCode();
                }
                if (this.ActiveAliasName != null)
                {
                    hashCode = (hashCode * 59) + this.ActiveAliasName.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.MemberHasUniqueName.GetHashCode();
                if (this.UniqueName != null)
                {
                    hashCode = (hashCode * 59) + this.UniqueName.GetHashCode();
                }
                if (this.MemberId != null)
                {
                    hashCode = (hashCode * 59) + this.MemberId.GetHashCode();
                }
                if (this.UniqueId != null)
                {
                    hashCode = (hashCode * 59) + this.UniqueId.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.Type.GetHashCode();
                hashCode = (hashCode * 59) + this.MemberSolveOrder.GetHashCode();
                hashCode = (hashCode * 59) + this.DescendantsCount.GetHashCode();
                hashCode = (hashCode * 59) + this.PreviousSiblingsCount.GetHashCode();
                hashCode = (hashCode * 59) + this.Dimension.GetHashCode();
                hashCode = (hashCode * 59) + this.Attribute.GetHashCode();
                hashCode = (hashCode * 59) + this.Account.GetHashCode();
                if (this.Links != null)
                {
                    hashCode = (hashCode * 59) + this.Links.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.DimSolveOrder.GetHashCode();
                if (this.DimensionType != null)
                {
                    hashCode = (hashCode * 59) + this.DimensionType.GetHashCode();
                }
                if (this.FormatString != null)
                {
                    hashCode = (hashCode * 59) + this.FormatString.GetHashCode();
                }
                if (this.DimStorageType != null)
                {
                    hashCode = (hashCode * 59) + this.DimStorageType.GetHashCode();
                }
                if (this.CurrencyConversionCategory != null)
                {
                    hashCode = (hashCode * 59) + this.CurrencyConversionCategory.GetHashCode();
                }
                if (this.Uda != null)
                {
                    hashCode = (hashCode * 59) + this.Uda.GetHashCode();
                }
                if (this.DataStorageType != null)
                {
                    hashCode = (hashCode * 59) + this.DataStorageType.GetHashCode();
                }
                if (this.ParentName != null)
                {
                    hashCode = (hashCode * 59) + this.ParentName.GetHashCode();
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
