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
    /// StorageStatistics
    /// </summary>
    [DataContract(Name = "StorageStatistics")]
    public partial class StorageStatistics : IEquatable<StorageStatistics>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StorageStatistics" /> class.
        /// </summary>
        /// <param name="dimensions">dimensions.</param>
        /// <param name="maxKeyLengthBits">maxKeyLengthBits.</param>
        /// <param name="maxKeyLengthBytes">maxKeyLengthBytes.</param>
        /// <param name="numberOfInputLevelCells">numberOfInputLevelCells.</param>
        /// <param name="numberOfIncrementalDataSlices">numberOfIncrementalDataSlices.</param>
        /// <param name="numberOfIncrementalInputCells">numberOfIncrementalInputCells.</param>
        /// <param name="numberOfAggregateViews">numberOfAggregateViews.</param>
        /// <param name="numberOfAggregateCells">numberOfAggregateCells.</param>
        /// <param name="numberOfIncrementalAggregateCells">numberOfIncrementalAggregateCells.</param>
        /// <param name="costOfQueryingIncrementalData">costOfQueryingIncrementalData.</param>
        /// <param name="inputLevelDataSize">inputLevelDataSize.</param>
        /// <param name="aggregateDataSize">aggregateDataSize.</param>
        /// <param name="numberOfExistingBlocks">numberOfExistingBlocks.</param>
        /// <param name="blockSize">blockSize.</param>
        /// <param name="potentialNumberOfBlocks">potentialNumberOfBlocks.</param>
        /// <param name="existingLevelZeroBlocks">existingLevelZeroBlocks.</param>
        /// <param name="existingUpperLevelBlocks">existingUpperLevelBlocks.</param>
        /// <param name="blockDensity">blockDensity.</param>
        /// <param name="percentageOfMaximumBlocksExisting">percentageOfMaximumBlocksExisting.</param>
        /// <param name="compressionRatio">compressionRatio.</param>
        /// <param name="averageClusteringRatio">averageClusteringRatio.</param>
        /// <param name="pageFileSize">pageFileSize.</param>
        /// <param name="indexFileSize">indexFileSize.</param>
        public StorageStatistics(List<StatisticsDimensions> dimensions = default(List<StatisticsDimensions>), int maxKeyLengthBits = default(int), int maxKeyLengthBytes = default(int), int numberOfInputLevelCells = default(int), int numberOfIncrementalDataSlices = default(int), int numberOfIncrementalInputCells = default(int), int numberOfAggregateViews = default(int), int numberOfAggregateCells = default(int), int numberOfIncrementalAggregateCells = default(int), double costOfQueryingIncrementalData = default(double), int inputLevelDataSize = default(int), int aggregateDataSize = default(int), double numberOfExistingBlocks = default(double), int blockSize = default(int), double potentialNumberOfBlocks = default(double), double existingLevelZeroBlocks = default(double), double existingUpperLevelBlocks = default(double), double blockDensity = default(double), double percentageOfMaximumBlocksExisting = default(double), double compressionRatio = default(double), double averageClusteringRatio = default(double), long pageFileSize = default(long), long indexFileSize = default(long))
        {
            this.Dimensions = dimensions;
            this.MaxKeyLengthBits = maxKeyLengthBits;
            this.MaxKeyLengthBytes = maxKeyLengthBytes;
            this.NumberOfInputLevelCells = numberOfInputLevelCells;
            this.NumberOfIncrementalDataSlices = numberOfIncrementalDataSlices;
            this.NumberOfIncrementalInputCells = numberOfIncrementalInputCells;
            this.NumberOfAggregateViews = numberOfAggregateViews;
            this.NumberOfAggregateCells = numberOfAggregateCells;
            this.NumberOfIncrementalAggregateCells = numberOfIncrementalAggregateCells;
            this.CostOfQueryingIncrementalData = costOfQueryingIncrementalData;
            this.InputLevelDataSize = inputLevelDataSize;
            this.AggregateDataSize = aggregateDataSize;
            this.NumberOfExistingBlocks = numberOfExistingBlocks;
            this.BlockSize = blockSize;
            this.PotentialNumberOfBlocks = potentialNumberOfBlocks;
            this.ExistingLevelZeroBlocks = existingLevelZeroBlocks;
            this.ExistingUpperLevelBlocks = existingUpperLevelBlocks;
            this.BlockDensity = blockDensity;
            this.PercentageOfMaximumBlocksExisting = percentageOfMaximumBlocksExisting;
            this.CompressionRatio = compressionRatio;
            this.AverageClusteringRatio = averageClusteringRatio;
            this.PageFileSize = pageFileSize;
            this.IndexFileSize = indexFileSize;
        }

        /// <summary>
        /// Gets or Sets Dimensions
        /// </summary>
        [DataMember(Name = "dimensions", EmitDefaultValue = false)]
        public List<StatisticsDimensions> Dimensions { get; set; }

        /// <summary>
        /// Gets or Sets MaxKeyLengthBits
        /// </summary>
        [DataMember(Name = "maxKeyLengthBits", EmitDefaultValue = false)]
        public int MaxKeyLengthBits { get; set; }

        /// <summary>
        /// Gets or Sets MaxKeyLengthBytes
        /// </summary>
        [DataMember(Name = "maxKeyLengthBytes", EmitDefaultValue = false)]
        public int MaxKeyLengthBytes { get; set; }

        /// <summary>
        /// Gets or Sets NumberOfInputLevelCells
        /// </summary>
        [DataMember(Name = "numberOfInputLevelCells", EmitDefaultValue = false)]
        public int NumberOfInputLevelCells { get; set; }

        /// <summary>
        /// Gets or Sets NumberOfIncrementalDataSlices
        /// </summary>
        [DataMember(Name = "numberOfIncrementalDataSlices", EmitDefaultValue = false)]
        public int NumberOfIncrementalDataSlices { get; set; }

        /// <summary>
        /// Gets or Sets NumberOfIncrementalInputCells
        /// </summary>
        [DataMember(Name = "numberOfIncrementalInputCells", EmitDefaultValue = false)]
        public int NumberOfIncrementalInputCells { get; set; }

        /// <summary>
        /// Gets or Sets NumberOfAggregateViews
        /// </summary>
        [DataMember(Name = "numberOfAggregateViews", EmitDefaultValue = false)]
        public int NumberOfAggregateViews { get; set; }

        /// <summary>
        /// Gets or Sets NumberOfAggregateCells
        /// </summary>
        [DataMember(Name = "numberOfAggregateCells", EmitDefaultValue = false)]
        public int NumberOfAggregateCells { get; set; }

        /// <summary>
        /// Gets or Sets NumberOfIncrementalAggregateCells
        /// </summary>
        [DataMember(Name = "numberOfIncrementalAggregateCells", EmitDefaultValue = false)]
        public int NumberOfIncrementalAggregateCells { get; set; }

        /// <summary>
        /// Gets or Sets CostOfQueryingIncrementalData
        /// </summary>
        [DataMember(Name = "costOfQueryingIncrementalData", EmitDefaultValue = false)]
        public double CostOfQueryingIncrementalData { get; set; }

        /// <summary>
        /// Gets or Sets InputLevelDataSize
        /// </summary>
        [DataMember(Name = "inputLevelDataSize", EmitDefaultValue = false)]
        public int InputLevelDataSize { get; set; }

        /// <summary>
        /// Gets or Sets AggregateDataSize
        /// </summary>
        [DataMember(Name = "aggregateDataSize", EmitDefaultValue = false)]
        public int AggregateDataSize { get; set; }

        /// <summary>
        /// Gets or Sets NumberOfExistingBlocks
        /// </summary>
        [DataMember(Name = "numberOfExistingBlocks", EmitDefaultValue = false)]
        public double NumberOfExistingBlocks { get; set; }

        /// <summary>
        /// Gets or Sets BlockSize
        /// </summary>
        [DataMember(Name = "blockSize", EmitDefaultValue = false)]
        public int BlockSize { get; set; }

        /// <summary>
        /// Gets or Sets PotentialNumberOfBlocks
        /// </summary>
        [DataMember(Name = "potentialNumberOfBlocks", EmitDefaultValue = false)]
        public double PotentialNumberOfBlocks { get; set; }

        /// <summary>
        /// Gets or Sets ExistingLevelZeroBlocks
        /// </summary>
        [DataMember(Name = "existingLevelZeroBlocks", EmitDefaultValue = false)]
        public double ExistingLevelZeroBlocks { get; set; }

        /// <summary>
        /// Gets or Sets ExistingUpperLevelBlocks
        /// </summary>
        [DataMember(Name = "existingUpperLevelBlocks", EmitDefaultValue = false)]
        public double ExistingUpperLevelBlocks { get; set; }

        /// <summary>
        /// Gets or Sets BlockDensity
        /// </summary>
        [DataMember(Name = "blockDensity", EmitDefaultValue = false)]
        public double BlockDensity { get; set; }

        /// <summary>
        /// Gets or Sets PercentageOfMaximumBlocksExisting
        /// </summary>
        [DataMember(Name = "percentageOfMaximumBlocksExisting", EmitDefaultValue = false)]
        public double PercentageOfMaximumBlocksExisting { get; set; }

        /// <summary>
        /// Gets or Sets CompressionRatio
        /// </summary>
        [DataMember(Name = "compressionRatio", EmitDefaultValue = false)]
        public double CompressionRatio { get; set; }

        /// <summary>
        /// Gets or Sets AverageClusteringRatio
        /// </summary>
        [DataMember(Name = "averageClusteringRatio", EmitDefaultValue = false)]
        public double AverageClusteringRatio { get; set; }

        /// <summary>
        /// Gets or Sets PageFileSize
        /// </summary>
        [DataMember(Name = "pageFileSize", EmitDefaultValue = false)]
        public long PageFileSize { get; set; }

        /// <summary>
        /// Gets or Sets IndexFileSize
        /// </summary>
        [DataMember(Name = "indexFileSize", EmitDefaultValue = false)]
        public long IndexFileSize { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class StorageStatistics {\n");
            sb.Append("  Dimensions: ").Append(Dimensions).Append("\n");
            sb.Append("  MaxKeyLengthBits: ").Append(MaxKeyLengthBits).Append("\n");
            sb.Append("  MaxKeyLengthBytes: ").Append(MaxKeyLengthBytes).Append("\n");
            sb.Append("  NumberOfInputLevelCells: ").Append(NumberOfInputLevelCells).Append("\n");
            sb.Append("  NumberOfIncrementalDataSlices: ").Append(NumberOfIncrementalDataSlices).Append("\n");
            sb.Append("  NumberOfIncrementalInputCells: ").Append(NumberOfIncrementalInputCells).Append("\n");
            sb.Append("  NumberOfAggregateViews: ").Append(NumberOfAggregateViews).Append("\n");
            sb.Append("  NumberOfAggregateCells: ").Append(NumberOfAggregateCells).Append("\n");
            sb.Append("  NumberOfIncrementalAggregateCells: ").Append(NumberOfIncrementalAggregateCells).Append("\n");
            sb.Append("  CostOfQueryingIncrementalData: ").Append(CostOfQueryingIncrementalData).Append("\n");
            sb.Append("  InputLevelDataSize: ").Append(InputLevelDataSize).Append("\n");
            sb.Append("  AggregateDataSize: ").Append(AggregateDataSize).Append("\n");
            sb.Append("  NumberOfExistingBlocks: ").Append(NumberOfExistingBlocks).Append("\n");
            sb.Append("  BlockSize: ").Append(BlockSize).Append("\n");
            sb.Append("  PotentialNumberOfBlocks: ").Append(PotentialNumberOfBlocks).Append("\n");
            sb.Append("  ExistingLevelZeroBlocks: ").Append(ExistingLevelZeroBlocks).Append("\n");
            sb.Append("  ExistingUpperLevelBlocks: ").Append(ExistingUpperLevelBlocks).Append("\n");
            sb.Append("  BlockDensity: ").Append(BlockDensity).Append("\n");
            sb.Append("  PercentageOfMaximumBlocksExisting: ").Append(PercentageOfMaximumBlocksExisting).Append("\n");
            sb.Append("  CompressionRatio: ").Append(CompressionRatio).Append("\n");
            sb.Append("  AverageClusteringRatio: ").Append(AverageClusteringRatio).Append("\n");
            sb.Append("  PageFileSize: ").Append(PageFileSize).Append("\n");
            sb.Append("  IndexFileSize: ").Append(IndexFileSize).Append("\n");
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
            return this.Equals(input as StorageStatistics);
        }

        /// <summary>
        /// Returns true if StorageStatistics instances are equal
        /// </summary>
        /// <param name="input">Instance of StorageStatistics to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(StorageStatistics input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.Dimensions == input.Dimensions ||
                    this.Dimensions != null &&
                    input.Dimensions != null &&
                    this.Dimensions.SequenceEqual(input.Dimensions)
                ) && 
                (
                    this.MaxKeyLengthBits == input.MaxKeyLengthBits ||
                    this.MaxKeyLengthBits.Equals(input.MaxKeyLengthBits)
                ) && 
                (
                    this.MaxKeyLengthBytes == input.MaxKeyLengthBytes ||
                    this.MaxKeyLengthBytes.Equals(input.MaxKeyLengthBytes)
                ) && 
                (
                    this.NumberOfInputLevelCells == input.NumberOfInputLevelCells ||
                    this.NumberOfInputLevelCells.Equals(input.NumberOfInputLevelCells)
                ) && 
                (
                    this.NumberOfIncrementalDataSlices == input.NumberOfIncrementalDataSlices ||
                    this.NumberOfIncrementalDataSlices.Equals(input.NumberOfIncrementalDataSlices)
                ) && 
                (
                    this.NumberOfIncrementalInputCells == input.NumberOfIncrementalInputCells ||
                    this.NumberOfIncrementalInputCells.Equals(input.NumberOfIncrementalInputCells)
                ) && 
                (
                    this.NumberOfAggregateViews == input.NumberOfAggregateViews ||
                    this.NumberOfAggregateViews.Equals(input.NumberOfAggregateViews)
                ) && 
                (
                    this.NumberOfAggregateCells == input.NumberOfAggregateCells ||
                    this.NumberOfAggregateCells.Equals(input.NumberOfAggregateCells)
                ) && 
                (
                    this.NumberOfIncrementalAggregateCells == input.NumberOfIncrementalAggregateCells ||
                    this.NumberOfIncrementalAggregateCells.Equals(input.NumberOfIncrementalAggregateCells)
                ) && 
                (
                    this.CostOfQueryingIncrementalData == input.CostOfQueryingIncrementalData ||
                    this.CostOfQueryingIncrementalData.Equals(input.CostOfQueryingIncrementalData)
                ) && 
                (
                    this.InputLevelDataSize == input.InputLevelDataSize ||
                    this.InputLevelDataSize.Equals(input.InputLevelDataSize)
                ) && 
                (
                    this.AggregateDataSize == input.AggregateDataSize ||
                    this.AggregateDataSize.Equals(input.AggregateDataSize)
                ) && 
                (
                    this.NumberOfExistingBlocks == input.NumberOfExistingBlocks ||
                    this.NumberOfExistingBlocks.Equals(input.NumberOfExistingBlocks)
                ) && 
                (
                    this.BlockSize == input.BlockSize ||
                    this.BlockSize.Equals(input.BlockSize)
                ) && 
                (
                    this.PotentialNumberOfBlocks == input.PotentialNumberOfBlocks ||
                    this.PotentialNumberOfBlocks.Equals(input.PotentialNumberOfBlocks)
                ) && 
                (
                    this.ExistingLevelZeroBlocks == input.ExistingLevelZeroBlocks ||
                    this.ExistingLevelZeroBlocks.Equals(input.ExistingLevelZeroBlocks)
                ) && 
                (
                    this.ExistingUpperLevelBlocks == input.ExistingUpperLevelBlocks ||
                    this.ExistingUpperLevelBlocks.Equals(input.ExistingUpperLevelBlocks)
                ) && 
                (
                    this.BlockDensity == input.BlockDensity ||
                    this.BlockDensity.Equals(input.BlockDensity)
                ) && 
                (
                    this.PercentageOfMaximumBlocksExisting == input.PercentageOfMaximumBlocksExisting ||
                    this.PercentageOfMaximumBlocksExisting.Equals(input.PercentageOfMaximumBlocksExisting)
                ) && 
                (
                    this.CompressionRatio == input.CompressionRatio ||
                    this.CompressionRatio.Equals(input.CompressionRatio)
                ) && 
                (
                    this.AverageClusteringRatio == input.AverageClusteringRatio ||
                    this.AverageClusteringRatio.Equals(input.AverageClusteringRatio)
                ) && 
                (
                    this.PageFileSize == input.PageFileSize ||
                    this.PageFileSize.Equals(input.PageFileSize)
                ) && 
                (
                    this.IndexFileSize == input.IndexFileSize ||
                    this.IndexFileSize.Equals(input.IndexFileSize)
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
                if (this.Dimensions != null)
                {
                    hashCode = (hashCode * 59) + this.Dimensions.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.MaxKeyLengthBits.GetHashCode();
                hashCode = (hashCode * 59) + this.MaxKeyLengthBytes.GetHashCode();
                hashCode = (hashCode * 59) + this.NumberOfInputLevelCells.GetHashCode();
                hashCode = (hashCode * 59) + this.NumberOfIncrementalDataSlices.GetHashCode();
                hashCode = (hashCode * 59) + this.NumberOfIncrementalInputCells.GetHashCode();
                hashCode = (hashCode * 59) + this.NumberOfAggregateViews.GetHashCode();
                hashCode = (hashCode * 59) + this.NumberOfAggregateCells.GetHashCode();
                hashCode = (hashCode * 59) + this.NumberOfIncrementalAggregateCells.GetHashCode();
                hashCode = (hashCode * 59) + this.CostOfQueryingIncrementalData.GetHashCode();
                hashCode = (hashCode * 59) + this.InputLevelDataSize.GetHashCode();
                hashCode = (hashCode * 59) + this.AggregateDataSize.GetHashCode();
                hashCode = (hashCode * 59) + this.NumberOfExistingBlocks.GetHashCode();
                hashCode = (hashCode * 59) + this.BlockSize.GetHashCode();
                hashCode = (hashCode * 59) + this.PotentialNumberOfBlocks.GetHashCode();
                hashCode = (hashCode * 59) + this.ExistingLevelZeroBlocks.GetHashCode();
                hashCode = (hashCode * 59) + this.ExistingUpperLevelBlocks.GetHashCode();
                hashCode = (hashCode * 59) + this.BlockDensity.GetHashCode();
                hashCode = (hashCode * 59) + this.PercentageOfMaximumBlocksExisting.GetHashCode();
                hashCode = (hashCode * 59) + this.CompressionRatio.GetHashCode();
                hashCode = (hashCode * 59) + this.AverageClusteringRatio.GetHashCode();
                hashCode = (hashCode * 59) + this.PageFileSize.GetHashCode();
                hashCode = (hashCode * 59) + this.IndexFileSize.GetHashCode();
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
