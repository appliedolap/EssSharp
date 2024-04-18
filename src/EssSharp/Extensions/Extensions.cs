using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

using EssSharp.Client;
using EssSharp.Model;

using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;

namespace EssSharp
{
    internal static class Extensions
    {
        #region Migrations to EssSharp.Abstractions

        /// <summary>
        /// Returns an <see cref="EssDrillthroughDetails"/> from the given with the given <see cref="DrillthroughBean" />.
        /// </summary>
        /// <param name="dtb" />
        internal static EssDrillthroughDetails ToEssSharpDetails( this DrillthroughBean dtb ) => new EssDrillthroughDetails()
        {
            ColumnMappings = dtb.ColumnMapping.ToEssSharpDictionary(),
            Columns = dtb.Columns,
            DataSourceName = dtb.DataSourceName,
            DrillableRegions = dtb.DrillableRegions,
            Name = dtb.Name,
            Type = dtb.Type,
            Url = dtb.Url,
            UseTempTables = dtb.UseTempTables
        };

        /// <summary>
        /// Returns a <see cref="Dictionary{TKey, TValue}" /> of <see cref="EssDrillthroughDetails.ColumnMapping"/> objects with the given dictionary of <see cref="ColumnMappingInfo" /> objects.
        /// </summary>
        /// <param name="mappingInfo" />
        internal static Dictionary<string, EssDrillthroughDetails.ColumnMapping> ToEssSharpDictionary( this Dictionary<string, ColumnMappingInfo> mappingInfo )
        {
            var essDictionary = new Dictionary<string, EssDrillthroughDetails.ColumnMapping>();

            if ( mappingInfo is null )
                return essDictionary;

            foreach ( var key in mappingInfo.Keys )
            {
                essDictionary[key] = new EssDrillthroughDetails.ColumnMapping()
                {
                    Dimension = mappingInfo[key].Dimension,
                    Generation = mappingInfo[key].Generation,
                    Level = mappingInfo[key].Level,
                    GenerationNumber = mappingInfo[key].GenerationNumber
                };
            }

            return essDictionary;
        }

        /// <summary>
        /// Returns a <see cref="List{T}"/> of <see cref="IEssApplication"/> objects associated with the given <see cref="EssServer"/>.
        /// </summary>
        /// <param name="applicationList" />
        /// <param name="server" />
        internal static List<IEssApplication> ToEssSharpList( this ApplicationList applicationList, EssServer server )
        {
            if ( server is null )
                throw new ArgumentNullException(nameof(server), $"The given {nameof(server)} is null.");

            return applicationList
                .Items?
                .Where(application => application is not null)
                .Select(application => new EssApplication(application, server) as IEssApplication)
                .ToList() ?? new List<IEssApplication>();
        }

        /// <summary>
        /// Returns a <see cref="List{T}"/> of <see cref="IEssApplicationConfiguration"/> objects associated with the given <see cref="EssApplication"/>.
        /// </summary>
        /// <param name="configList"></param>
        /// <param name="application"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static List<IEssApplicationConfiguration> ToEssSharpList( this ApplicationConfigList configList, EssApplication application )
        {
            if ( application is null )
                throw new ArgumentNullException(nameof(application), $"The given {nameof(application)} is null.");

            return configList
                .Items?
                .Where(config => config is not null)
                .Select(config => new EssApplicationConfiguration(application, config) as IEssApplicationConfiguration)
                .ToList() ?? new List<IEssApplicationConfiguration>();
        }

        /// <summary>
        /// Returns a <see cref="List{T}"/> of <see cref="IEssCube"/> objects associated with the given <see cref="EssApplication"/>.
        /// </summary>
        /// <param name="cubeList" />
        /// <param name="application" />
        internal static List<IEssCube> ToEssSharpList( this CubeList cubeList, EssApplication application )
        {
            if ( application is null )
                throw new ArgumentNullException(nameof(application), $"The given {nameof(application)} is null.");

            return cubeList
                .Items?
                .Where(cube => cube is not null)
                .Select(cube => new EssCube(cube, application) as IEssCube)
                .ToList() ?? new List<IEssCube>();
        }

        /// <summary>
        /// Returns a <see cref="List{T}"/> of <see cref="IEssCube"/> objects associated with the given <see cref="EssApplication"/>.
        /// </summary>
        /// <param name="cubeList" />
        /// <param name="application" />
        internal static List<IEssGeneration> ToEssSharpList( this GenerationLevelList generationLevelList ) =>
            generationLevelList
                .Items?
                .Where(generationLevel => generationLevel is not null)
                .Select(generationLevel => new EssGeneration(generationLevel) as IEssGeneration)
                .ToList() ?? new List<IEssGeneration>();



        /// <summary>
        /// Returns a <see cref="List{T}"/> of <see cref="IEssDatasource"/> objects associated with the given <see cref="EssServer"/>.
        /// </summary>
        /// <param name="datasourcesList" />
        /// <param name="server" />
        internal static List<IEssDatasource> ToEssSharpList( this DatasourcesList datasourcesList, EssServer server )
        {
            if ( server is null )
                throw new ArgumentNullException(nameof(server), $"The given {nameof(server)} is null.");

            return datasourcesList
                .Items?
                .Where(datasource => datasource is not null)
                .Select(datasource => new EssDatasource(datasource, server) as IEssDatasource)
                .ToList() ?? new List<IEssDatasource>();
        }

        /// <summary>
        /// Returns a <see cref="List{T}"/> of <see cref="IEssFile"/> or <see cref="IEssFolder"/> objects associated with the given <see cref="EssServer"/>.
        /// </summary>
        /// <param name="datasourcesList" />
        /// <param name="application" />
        internal static List<IEssApplicationDatasource> ToEssSharpList( this DatasourcesList datasourcesList, EssApplication application )
        {
            if ( application is null )
                throw new ArgumentNullException(nameof(application), $"The given {nameof(application)} is null.");

            return datasourcesList
                .Items?
                .Where(datasource => datasource is not null)
                .Select(datasource => new EssApplicationDatasource(datasource, application) as IEssApplicationDatasource)
                .ToList() ?? new List<IEssApplicationDatasource>();
        }

        /// <summary>
        /// Returns a <see cref="List{T}"/> of <see cref="IEssDimension"/> objects associated with the given <see cref="EssCube"/>.
        /// </summary>
        /// <param name="dimensionList" />
        /// <param name="cube" />
        internal static List<IEssDimension> ToEssSharpList( this DimensionList dimensionList, EssCube cube )
        {
            if ( cube is null )
                throw new ArgumentNullException(nameof(cube), $"The given {nameof(cube)} is null.");

            return dimensionList
                .Items?
                .Where(dimension => dimension is not null)
                .Select((dimension, index) => new EssDimension(dimension, cube) { DimensionNumber = index } as IEssDimension)
                .ToList() ?? new List<IEssDimension>();
        }

        /// <summary>
        /// Returns a <see cref="List{T}"/> of <see cref="IEssMember"/> objects associated with the given <see cref="EssCube"/>.
        /// </summary>
        /// <param name="memberList" />
        /// <param name="cube" />
        internal static List<IEssMember> ToEssSharpList( this MembersList memberList, EssCube cube )
        {
            if ( cube is null )
                throw new ArgumentNullException(nameof(cube), $"The given {nameof(cube)} is null.");

            return memberList
                .Items?
                .Where(member => member is not null)
                .Select(member => new EssMember(member, cube) as IEssMember)
                .ToList() ?? new List<IEssMember>();
        }

        /// <summary>
        /// Returns a <see cref="List{T}"/> of <see cref="IEssMember"/> objects associated with the given <see cref="EssCube"/>.
        /// </summary>
        /// <param name="memberList" />
        /// <param name="cube" />
        internal static List<IEssMember> ToEssSharpList( this List<MemberBean> memberList, EssCube cube )
        {
            if ( cube is null )
                throw new ArgumentNullException(nameof(cube), $"The given {nameof(cube)} is null.");

            return memberList
                .Where(member => member is not null)
                .Select(member => new EssMember(member, cube) as IEssMember)
                .ToList() ?? new List<IEssMember>();
        }

        /// <summary>
        /// Returns a <see cref="List{T}"/> of <see cref="IEssFile"/> or <see cref="IEssFolder"/> objects associated with the given <see cref="EssServer"/>.
        /// </summary>
        /// <param name="fileCollectionResponse" />
        /// <param name="server" />
        internal static List<T> ToEssSharpList<T>( this FileCollectionResponse fileCollectionResponse, EssServer server ) where T : class, IEssFile
        {
            if ( server is null )
                throw new ArgumentNullException(nameof(server), $"The given {nameof(server)} is null.");

            return fileCollectionResponse
                .Items?
                .Where(file => file is not null)
                .Select(file => (string.Equals(file.Type, "folder", StringComparison.OrdinalIgnoreCase) ? new EssFolder(file, server) : new EssFile(file, server)) as T)
                .Where(file => file is not null && (typeof(T) == typeof(IEssFolder) || file is not EssFolder))
                .ToList() ?? new List<T>();
        }

        /// <summary>
        /// Returns a <see cref="List{T}"/> of <see cref="IEssGroup"/> objects associated with the given <see cref="EssServer"/>.
        /// </summary>
        /// <param name="groups" />
        /// <param name="server" />
        internal static List<IEssGroup> ToEssSharpList( this Groups groups, EssServer server )
        {
            if ( server is null )
                throw new ArgumentNullException(nameof(server), $"The given {nameof(server)} is null.");

            return groups
                .Items?
                .Where(group => group is not null)
                .Select(group => new EssGroup(group, server) as IEssGroup)
                .ToList() ?? new List<IEssGroup>();
        }

        /// <summary>
        /// Returns a <see cref="List{T}"/> of <see cref="IEssJob"/> objects associated with the given <see cref="EssServer"/>.
        /// </summary>
        /// <param name="jobWrapper" />
        /// <param name="server" />
        internal static List<IEssJob> ToEssSharpList( this JobRecordPaginatedResultWrapper jobWrapper, EssServer server )
        {
            if ( server is null )
                throw new ArgumentNullException(nameof(server), $"The given {nameof(server)} is null.");

            return jobWrapper
                .Items?
                .Where(job => job is not null)
                .Select(job => new EssJob(job, server) as IEssJob)
                .ToList() ?? new List<IEssJob>();
        }

        /// <summary>
        /// Returns a <see cref="List{T}"/> of <see cref="IEssLockObject"/> objects associated with the given <see cref="EssCube"/>.
        /// </summary>
        /// <param name="lockObjectList"></param>
        /// <param name="cube"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static List<IEssLockObject> ToEssSharpList( this LockObjectList lockObjectList, EssCube cube )
        {
            if ( cube is null )
                throw new ArgumentNullException(nameof(cube), $"The given {nameof(cube)} is null.");

            return lockObjectList
                .Items?
                .Where(lockObject => lockObject is not null)
                .Select(lockObject => new EssLockObject(lockObject, cube) as IEssLockObject)
                .ToList() ?? new List<IEssLockObject>();
        }

        /// <summary>
        /// Returns a <see cref="List{T}"/> of <see cref="IEssLockBlock"/> objects associated with the given <see cref="EssCube"/>.
        /// </summary>
        /// <param name="lockBlockList"></param>
        /// <param name="cube"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static List<IEssLockBlock> ToEssSharpList( this LockBlockList lockBlockList, EssCube cube )
        {
            if ( cube is null )
                throw new ArgumentNullException(nameof(cube), $"The given {nameof(cube)} is null.");

            return lockBlockList
                .Items?
                .Where(lockBlock => lockBlock is not null)
                .Select(lockBlock => new EssLockBlock(lockBlock, cube) as IEssLockBlock)
                .ToList() ?? new List<IEssLockBlock>();
        }


        /// <summary>
        /// Returns a <see cref="List{T}"/> of <see cref="IEssDrillthroughReport"/> objects associated with the given <see cref="EssCube"/>.
        /// </summary>
        /// <param name="reportList" />
        /// <param name="cube" />
        internal static List<IEssDrillthroughReport> ToEssSharpList( this ReportList reportList, EssCube cube )
        {
            if ( cube is null )
                throw new ArgumentNullException(nameof(cube), $"The given {nameof(cube)} is null.");

            return reportList
                .Items?
                .Where(report => report is not null)
                .Select(report => new EssDrillthroughReport(report, cube) as IEssDrillthroughReport)
                .ToList() ?? new List<IEssDrillthroughReport>();
        }

        /// <summary>
        /// Returns a <see cref="List{T}"/> of <see cref="IEssUtility"/> objects associated with the given <see cref="EssServer"/>.
        /// </summary>
        /// <param name="resourceList" />
        /// <param name="server" />
        internal static List<IEssUtility> ToEssSharpList( this ResourceList resourceList, EssServer server )
        {
            if ( server is null )
                throw new ArgumentNullException(nameof(server), $"The given {nameof(server)} is null.");

            return resourceList
                .Items?
                .Where(resource => resource is not null)
                .Select(resource => new EssUtility(resource, server) as IEssUtility)
                .ToList() ?? new List<IEssUtility>();
        }

        /// <summary>
        /// Returns a <see cref="List{T}"/> of <see cref="IEssSession"/> objects associated with the given <see cref="EssServer"/>.
        /// </summary>
        /// <param name="sessionList" />
        /// <param name="server" />
        internal static List<IEssSession> ToEssSharpList( this List<SessionAttributes> sessionList, EssServer server )
        {
            if ( server is null )
                throw new ArgumentNullException(nameof(server), $"The given {nameof(server)} is null.");

            return sessionList
                .Where(session => session is not null)
                .Select(session => new EssSession(session, server) as IEssSession)
                .ToList() ?? new List<IEssSession>();
        }

        /// <summary>
        /// Returns a <see cref="List{T}"/> of <see cref="IEssScript"/> objects associated with the given <see cref="EssCube"/>.
        /// </summary>
        /// <param name="scriptList" />
        /// <param name="cube" />
        internal static List<T> ToEssSharpList<T>( this ScriptList scriptList, EssCube cube ) where T : class, IEssScript
        {
            if ( cube is null )
                throw new ArgumentNullException(nameof(cube), $"The given {nameof(cube)} is null.");

            // Throw if a specific type of IEssScript is not given.
            if ( typeof(T) == typeof(IEssScript) )
                throw new ArgumentException($"A specific type of {nameof(IEssScript)} must be requested.", "T");

            // Identify the specific script type.
            var scriptType = GetScriptType<T>();

            return scriptList
                .Items?
                .Where(script => script is not null)
                .Select(script => scriptType.CreateScript<T>(script, cube))
                .ToList() ?? new List<T>();
        }

        internal static T CreateScript<T>( this EssScriptType type, Script script, EssCube cube ) where T : class, IEssScript => type switch
        {
            EssScriptType.Calc => new EssCalcScript(script, cube) as T,
            EssScriptType.MaxL => new EssMaxlScript(script, cube) as T,
            EssScriptType.MDX => new EssMdxScript(script, cube) as T,
            EssScriptType.Report => new EssReportScript(script, cube) as T,
            _ => new EssScript(script, cube) as T
        };

        /// <summary>
        /// Returns a <see cref="List{T}"/> of <see cref="IEssUrl"/> objects associated with the given <see cref="EssServer"/>.
        /// </summary>
        /// <param name="urlList" />
        /// <param name="server" />
        internal static List<IEssUrl> ToEssSharpList( this EssbaseURLList urlList, EssServer server )
        {
            if ( server is null )
                throw new ArgumentNullException(nameof(server), $"The given {nameof(server)} is null.");

            return urlList
                .Items?
                .Where(url => url is not null)
                .Select(url => new EssUrl(url, server) as IEssUrl)
                .ToList() ?? new List<IEssUrl>();
        }

        /// <summary>
        /// Returns a <see cref="List{T}"/> of <see cref="IEssUser"/> objects associated with the given <see cref="EssServer"/>.
        /// </summary>
        /// <param name="userList" />
        /// <param name="server" />
        internal static List<IEssUser> ToEssSharpList( this Users userList, EssServer server )
        {
            if ( server is null )
                throw new ArgumentNullException(nameof(server), $"The given {nameof(server)} is null.");

            return userList
                .Items?
                .Where(user => user is not null)
                .Select(user => new EssUser(user, server) as IEssUser)
                .ToList() ?? new List<IEssUser>();
        }

        internal static List<IEssApplicationPermission> ToEssSharpList( this UserGroupProvisionInfoList permissionsList, EssApplication application )
        {
            if ( application is null )
                throw new ArgumentNullException(nameof(application), $"The given {nameof(application)} is null.");

            return permissionsList
                .Items?
                .Where(permissions => permissions is not null)
                .Select(permissions => new EssApplicationPermission(permissions, application) as IEssApplicationPermission)
                .ToList() ?? new List<IEssApplicationPermission>();
        }

        /// <summary>
        /// Returns a <see cref="List{T}"/> of <see cref="IEssServerVariable"/> objects associated with the given <paramref name="variableList"/>.
        /// </summary>
        /// <param name="variableList" />
        /// <param name="parent" />
        internal static List<T> ToEssSharpList<T>( this VariableList variableList, EssObject parent ) where T : class, IEssServerVariable
        {
            if ( parent is null )
                throw new ArgumentNullException(nameof(parent), $"The given {nameof(parent)} is null.");

            return variableList
                .Items?
                .Where(variable => variable is not null)
                .Select(variable => parent switch
                {
                    EssServer server => new EssServerVariable(variable, server),
                    EssApplication application => new EssApplicationVariable(variable, application),
                    EssCube cube => new EssCubeVariable(variable, cube),
                    _ => null
                } as T)
                .Where(variable => variable is not null)
                .ToList() ?? new List<T>();
        }

        #endregion

        #region Migrations from EssSharp.Abstractions

        /// <summary>
        /// Returns a <see cref="DrillthroughMetadataBean"/> from the given <see cref="IEssDrillthroughRange"/> object
        /// and optionally, an <see cref="IEssDrillthroughOptions"/>.
        /// </summary>
        /// <param name="context" />
        /// <param name="options" />
        internal static DrillthroughMetadataBean ToModelBean( this IEssDrillthroughRange context, IEssDrillthroughOptions options = null )
            => ToModelBean(new List<IEssDrillthroughRange>() { context }, options);

        /// <summary>
        /// Returns a <see cref="DrillthroughMetadataBean"/> from the collection of <see cref="IEssDrillthroughRange"/> objects
        /// and optionally, an <see cref="IEssDrillthroughOptions"/>.
        /// </summary>
        /// <param name="context" />
        /// <param name="options" />
        internal static DrillthroughMetadataBean ToModelBean( this IEnumerable<IEssDrillthroughRange> context, IEssDrillthroughOptions options = null )
        {
            if ( context?.Any(dtr => dtr is not null) is not true )
                throw new ArgumentException($"At least one {nameof(IEssDrillthroughRange)} is required to produce a {nameof(DrillthroughMetadataBean)}.", nameof(context));

            return new DrillthroughMetadataBean(context?
                .Where(dtr => dtr is not null)
                .Select(dtr => new DrillThroughRange(dtr.DimensionMemberSets))
                .ToList(), aliasTable: options?.AliasTable);
        }

        /// <summary>
        /// Returns a <see cref="Grid" /> object from the given <see cref="EssQueryReport" /> object.
        /// </summary>
        /// <param name="report" />
        internal static Grid ToModelGrid( this EssQueryReport report )
        {
            if ( report.Data is null )
                throw new ArgumentNullException(nameof(report.Data), "The report contains no data.");

            int rowCount = report.Data.GetLength(0);
            int colCount = report.Data.GetLength(1);

            return new Grid(
                slice: new Slice(
                    rows: rowCount,
                    columns: colCount,
                    data: new Data(new List<GridRange>()
                    {
                        new GridRange(
                            values: report.Data.OfType<string>().ToList(),
                            types:  report.Types.OfType<int>().Select(t => t.ToString()).ToList(),
                            end:    rowCount * colCount - 1)
                    })),
                dimensions: report.Metadata?.ToModelGridDimensions());
        }

        /// <summary>
        /// Returns a list of <see cref="GridDimension" /> objects from the given <see cref="EssQueryReport.ReportMetadata" /> object.
        /// </summary>
        /// <param name="metadata" />
        internal static List<GridDimension> ToModelGridDimensions( this EssQueryReport.ReportMetadata metadata )
        {
            var gridDimensions = new List<GridDimension>();

            gridDimensions.AddRange(metadata.RowDimensionMembers.Select(( rdm, i ) => new GridDimension(row: -1, column: i, name: rdm)));
            gridDimensions.AddRange(metadata.ColumnDimensionMembers.Select(( cdm, i ) => new GridDimension(row: i, column: -1, name: cdm)));
            gridDimensions.AddRange(metadata.PageDimensionMembers.Select(pdm => new GridDimension(row: -1, column: -1, name: pdm))); ;

            return gridDimensions;
        }

        /// <summary>
        /// Returns a <see cref="ParametersBean"/> from the given <see cref="IEssJobOptions"/> object.
        /// </summary>
        /// <param name="options" />
        internal static ParametersBean ToModelBean( this IEssJobOptions options ) => new ParametersBean()
        {
            // EssJobType.Unknown (Multiple)
            Script = options.Script,

            // EssJobType.Clear
            Option = options.Option?.ToString(),
            PartialDataExpression = options.PartialDataExpression,

            // EssJobType.ExportExcel
            BuildMethod = options.BuildMethod.HasValue && Enum.IsDefined(typeof(ParametersBean.BuildMethodEnum), (int)options.BuildMethod) ? (ParametersBean.BuildMethodEnum)options.BuildMethod : null,
            Calc = options.Calc?.ToString().ToLowerInvariant(),
            Data = options.Data?.ToString().ToLowerInvariant(),
            MemberIds = options.MemberIds?.ToString().ToLowerInvariant(),

            // EssJobType.ImportExcel
            BuildOption = options.BuildOption.HasValue && Enum.IsDefined(typeof(ParametersBean.BuildOptionEnum), (int)options.BuildOption) ? (ParametersBean.BuildOptionEnum)options.BuildOption : null,
            CatalogExcelPath = options.CatalogExcelPath,
            CreateFiles = options.CreateFiles?.ToString().ToLowerInvariant(),
            DeleteExcelOnSuccess = options.DeleteExcelOnSuccess?.ToString().ToLowerInvariant(),
            ExecuteScript = options.ExecuteScripts?.ToString().ToLowerInvariant(),
            ImportExcelFileName = options.ImportExcelFilename,
            Loaddata = options.LoadData?.ToString().ToLowerInvariant(),
            Overwrite = options.Overwrite?.ToString().ToLowerInvariant(),
            RecreateApplication = options.RecreateApp?.ToString().ToLowerInvariant(),

            // EssJobType.LoadData
            File = options is EssJobLoadDataOptions ?
                                        $@"[""{string.Join(@""",""", options.File ?? new List<string>())}""]" :
                                        options.File?.FirstOrDefault(),
            AbortOnError = options.AbortOnError?.ToString().ToLowerInvariant(),
            Rule = options is EssJobLoadDataOptions ?
                                        $@"[""{string.Join(@""",""", options.Rule ?? new List<string>())}""]" :
                                        options.Rule?.FirstOrDefault(),

            // EssJobType.ExecuteReport
            IsScriptContent       = options.IsScriptContent ?? false,
            LockForUpdate         = options.LockForUpdate   ?? false,
            ReportScriptFilename  = options.ReportScriptFilename,

            // EssJobType.LCMExport
            AllApp = options.AllApp.ToString(),
            GenerateArtifactList = options.Generateartifactlist.ToString(),
            IncludeServerLevel = options.IncludeServerLevel.ToString(),
            ZipFileName = options.ZipFileName,
            Skipdata = options.SkipData.ToString(),

            // EssJobType.LCMImport
            TargetApplicationName = options.TargetApplicationName
        };

        internal static List<EssGridDimension> ToEssGridDimension( this List<GridDimension> gridDimensions )
        {
            var dimensionList = new List<EssGridDimension>();

            foreach ( var dimension in gridDimensions )
            {
                dimensionList?.Add(
                    new EssGridDimension()
                    {
                        Column = dimension.Column,
                        Row = dimension.Row,
                        DisplayName = dimension.DisplayName,
                        Name = dimension.Name,
                        Pov = dimension.Pov,
                        Expanded = dimension.Expanded,
                        Hidden = dimension.Hidden
                    }
                );
            }
            return dimensionList;
        }

        internal static GridOperation.ActionEnum ToEssGridActionType( this EssGridZoomType actionEnum )
        {
            if ( Enum.IsDefined(typeof(GridOperation.ActionEnum), (int)actionEnum) )
                return (GridOperation.ActionEnum)actionEnum;

            throw new ArgumentException($@"{nameof(GridOperation.ActionEnum)}.{actionEnum} does not map to a model job type.");
        }

        internal static List<GridDimension> ToModelBean( this List<EssGridDimension> dimensionList )
        {
            var dimList = new List<GridDimension>();
            foreach ( var dimension in dimensionList )
            {
                dimList.Add(
                    new GridDimension()
                    {
                        Name = dimension.Name,
                        Row = dimension.Row,
                        Column = dimension.Column,
                        Pov = dimension.Pov,
                        Hidden = dimension.Hidden,
                        Expanded = dimension.Expanded
                    }
                );
            }
            return dimList;
        }

        internal static List<GridRange> ToModelBean( this List<EssGridRange> essGridRangeList )
        {
            var gridRangeList = new List<GridRange>();

            foreach ( var range in essGridRangeList )
            {
                gridRangeList.Add(
                    new GridRange()
                    {
                        Start = range.Start,
                        End = range.End,
                        Values = range.Values,
                        Types = range.Types,
                        Texts = range.Texts,
                        DataFormats = range.DataFormats,
                        Statuses = range.Statuses,
                        Filters = range.Filters,
                        EnumIds = range.EnumIds
                    }
                );
            }
            return gridRangeList;
        }

        internal static Data ToModelBean( this EssGridSliceData essGridSliceData ) => new Data()
        {
            Ranges = essGridSliceData.Ranges.ToModelBean()
        };

        internal static Slice ToModelBean( this EssGridSlice essGridSlice ) => new Slice()
        {
            Columns = essGridSlice.Columns,
            Rows = essGridSlice.Rows,
            DirtyTexts = essGridSlice.DirtyTexts,
            Data = essGridSlice.Data.ToModelBean(),
            DirtyCells = essGridSlice.DirtyCells,
        };

        internal static Grid ToModelBean( this EssGrid grid ) => new Grid()
        {
            Dimensions = grid.Dimensions.ToModelBean(),
            Slice = grid.Slice.ToModelBean(),
            Alias = grid.Alias,

        };

        internal static EssGridSliceData ToEssGridSliceData( this Data data )
        {
            var sliceData = new EssGridSliceData();

            foreach ( var range in data?.Ranges )
            {
                sliceData.Ranges.Add(
                    new EssGridRange() {
                        DataFormats = range.DataFormats,
                        End = range.End,
                        EnumIds = range.EnumIds,
                        Filters = range.Filters,
                        Start = range.Start,
                        Statuses = range.Statuses,
                        Texts = range.Texts,
                        Types = range.Types,
                        Values = range.Values
                    }
                );
            };
            return sliceData;
        }

        internal static EssGridSlice ToEssGridSlice( this Slice slice ) => new EssGridSlice()
        {
            Columns = slice.Columns,
            Data = slice.Data.ToEssGridSliceData(),
            DirtyCells = slice.DirtyCells ?? new List<int>(),
            DirtyTexts = slice.DirtyTexts,
            Rows = slice.Rows
        };

        internal static EssGridLayoutData ToEssSharpObject( this LayoutData layoutData ) => new EssGridLayoutData()
        {
            Statuses = layoutData.Statuses,
            Texts = layoutData.Texts,
            EnumIds = layoutData.EnumIds,
            DataFormats = layoutData.DataFormats,
            Types = layoutData.Types,
            Filters = layoutData.Filters,
            Values = layoutData.Values
        };

        internal static EssGridPreferencesAxisSuppression ToEssSharpObject( this ColumnSuppression columnSuppression ) => new EssGridPreferencesAxisSuppression()
        {
            Derived = columnSuppression.Derived,
            EmptyBlocks = columnSuppression.EmptyBlocks,
            Error = columnSuppression.Error,
            Invalid = columnSuppression.Invalid,
            Missing = columnSuppression.Missing,
            NoAccess = columnSuppression.NoAccess,
            UnderScore = columnSuppression.UnderScore,
            Zero = columnSuppression.Zero
        };

        internal static EssGridPreferencesAxisSuppression ToEssSharpObject( this RowSuppression rowSuppression ) => new EssGridPreferencesAxisSuppression()
        {
            Derived = rowSuppression.Derived,
            EmptyBlocks = rowSuppression.EmptyBlocks,
            Error = rowSuppression.Error,
            Invalid = rowSuppression.Invalid,
            Missing = rowSuppression.Missing,
            NoAccess = rowSuppression.NoAccess,
            UnderScore = rowSuppression.UnderScore,
            Zero = rowSuppression.Zero
        };

        internal static EssGridPreferencesZoomIn ToEssSharpObject( this ZoomIn zoomIn ) => new EssGridPreferencesZoomIn()
        {
            Ancestor = Enum.IsDefined(typeof(ZoomInAncestor), (ZoomInAncestor)zoomIn.Ancestor) ? (ZoomInAncestor)zoomIn.Ancestor : ZoomInAncestor.UNKNOWN,
            Mode = Enum.IsDefined(typeof(ZoomInMode), (ZoomInMode)zoomIn.Mode) ? (ZoomInMode)zoomIn.Mode : ZoomInMode.UNKNOWN
        };

        internal static EssGridPreferencesFormulaRetention ToEssSharpObject( this FormulaRetention formulaRetention ) => new EssGridPreferencesFormulaRetention()
        {
            Comments = formulaRetention.Comments,
            Fill = formulaRetention.Fill,
            Focus = formulaRetention.Focus,
            Retrieve = formulaRetention.Retrive,
            Zoom = formulaRetention.Zoom
        };

        internal static EssGridPreferences ToEssGridPreferences( this Preferences preferences ) => new EssGridPreferences()
        {
            CellText = preferences.CellText,
            ColumnSupression = preferences.ColumnSupression.ToEssSharpObject(),
            FormulaRetention = preferences.FormulaRetention.ToEssSharpObject(),
            IncludeDescriptionLabel = preferences.IncludeDescriptionLabel,
            IncludeSelection = preferences.IncludeSelection,
            Indentation = Enum.IsDefined(typeof(IndentationType), (IndentationType)preferences.Indentation) ? (IndentationType)preferences.Indentation : IndentationType.UNKNOWN,
            MaxColumns = preferences.MaxColumns,
            MaxRows = preferences.MaxRows,
            MissingText = preferences.MissingText,
            Navigate = preferences.Navigate,
            NoAccessText = preferences.NoAccessText,
            RemoveUnSelectedGroup = preferences.RemoveUnSelectedGroup,
            RepeatMemberLabels = preferences.RepeatMemberLabels,
            RowSupression = preferences.RowSupression.ToEssSharpObject(),
            WithinSelectedGroup = preferences.WithinSelectedGroup,
            ZoomIn = preferences.ZoomIn.ToEssSharpObject()
        };

        internal static ColumnSuppression ToColumnSuppressionObject( this EssGridPreferencesAxisSuppression columnSuppression ) => new ColumnSuppression()
        {
            Derived = columnSuppression.Derived,
            EmptyBlocks = columnSuppression.EmptyBlocks,
            Error = columnSuppression.Error,
            Invalid = columnSuppression.Invalid,
            Missing = columnSuppression.Missing,
            NoAccess = columnSuppression.NoAccess,
            UnderScore = columnSuppression.UnderScore,
            Zero = columnSuppression.Zero
        };

        internal static RowSuppression ToRowSuppressionObject( this EssGridPreferencesAxisSuppression rowSuppression ) => new RowSuppression()
        {
            Derived = rowSuppression.Derived,
            EmptyBlocks = rowSuppression.EmptyBlocks,
            Error = rowSuppression.Error,
            Invalid = rowSuppression.Invalid,
            Missing = rowSuppression.Missing,
            NoAccess = rowSuppression.NoAccess,
            UnderScore = rowSuppression.UnderScore,
            Zero = rowSuppression.Zero
        };

        internal static ZoomIn ToZoomInObject( this EssGridPreferencesZoomIn zoomIn ) => new ZoomIn()
        {
            Ancestor = Enum.IsDefined(typeof(ZoomIn.AncestorEnum), (ZoomIn.AncestorEnum)zoomIn.Ancestor) ? (ZoomIn.AncestorEnum)zoomIn.Ancestor : throw new NotSupportedException(),
            Mode = Enum.IsDefined(typeof(ZoomIn.ModeEnum), (ZoomIn.ModeEnum)zoomIn.Mode) ? (ZoomIn.ModeEnum)zoomIn.Mode : throw new NotSupportedException()
        };

        internal static FormulaRetention ToFormulaRetentionObject( this EssGridPreferencesFormulaRetention formulaRetention ) => new FormulaRetention()
        {
            Comments = formulaRetention.Comments,
            Fill = formulaRetention.Fill,
            Focus = formulaRetention.Focus,
            Retrive = formulaRetention.Retrieve,
            Zoom = formulaRetention.Zoom
        };

        internal static Preferences ToModelObject( this EssGridPreferences preferences ) => new Preferences()
        {
            CellText = preferences.CellText,
            ColumnSupression = preferences.ColumnSupression.ToColumnSuppressionObject(),
            FormulaRetention = preferences.FormulaRetention.ToFormulaRetentionObject(),
            IncludeDescriptionLabel = preferences.IncludeDescriptionLabel,
            IncludeSelection = preferences.IncludeSelection,
            Indentation = Enum.IsDefined(typeof(Preferences.IndentationEnum), (Preferences.IndentationEnum)preferences.Indentation) ? (Preferences.IndentationEnum)preferences.Indentation : throw new NotSupportedException(),
            MaxColumns = preferences.MaxColumns,
            MaxRows = preferences.MaxRows,
            MissingText = preferences.MissingText,
            Navigate = preferences.Navigate,
            NoAccessText = preferences.NoAccessText,
            RemoveUnSelectedGroup = preferences.RemoveUnSelectedGroup,
            RepeatMemberLabels = preferences.RepeatMemberLabels,
            RowSupression = preferences.RowSupression.ToRowSuppressionObject(),
            WithinSelectedGroup = preferences.WithinSelectedGroup,
            ZoomIn = preferences.ZoomIn.ToZoomInObject()
        };

        internal static LockObject ToLockObject( this EssLockOptions lockOptions ) => new LockObject()
        {
            Name = lockOptions.LockObjectName,
            Type = lockOptions.LockedFileType.HasValue && Enum.IsDefined(typeof(LockObject.TypeEnum), (LockObject.TypeEnum)lockOptions.LockedFileType) ? (LockObject.TypeEnum)lockOptions.LockedFileType : throw new Exception($@"{nameof(EssLockedFileType)} is required.")
        };

        internal static NamedQueriesPreferences ToNamedQueriesPreferences( this EssQueryPreferences queryPreferences ) => new NamedQueriesPreferences()
        {
            Dataless = queryPreferences.Dataless,
            HideRestrictedData = queryPreferences.HideRestrictedData,
            CellAttributes = queryPreferences.CellAttributes,
            FormatString = queryPreferences.FormatString,
            FormatValues = queryPreferences.FormatValues,
            MeaninglessCells = queryPreferences.MeaninglessCells,
            TextList = queryPreferences.TextList,
            UrlDrillThrough = queryPreferences.UrlDrillThrough,
            MemberIdentifierType = Enum.IsDefined(typeof(NamedQueriesPreferences.MemberIdentifierTypeEnum), (NamedQueriesPreferences.MemberIdentifierTypeEnum)queryPreferences.MemberIdentifier) ? (NamedQueriesPreferences.MemberIdentifierTypeEnum)queryPreferences.MemberIdentifier : null
        };

        /// <summary>
        /// Returns an <see cref="EssJobType" /> from the given <see cref="JobsInputBean.JobtypeEnum" />.
        /// </summary>
        internal static JobsInputBean.JobtypeEnum ToModelEnum( this EssJobType jobType )
        {
            if ( Enum.IsDefined(typeof(JobsInputBean.JobtypeEnum), (int)jobType) )
                return (JobsInputBean.JobtypeEnum)jobType;

            throw new ArgumentException($@"{nameof(EssJobType)}.{jobType} does not map to a model job type.");
        }

        internal static UserBean ToUserBean( this EssUserCreationOptions options ) => new UserBean()
        {
            Id = options.ID,
            Password = options.Password,
            Groups = options.Groups,
            Role = options.Role switch {
                EssServerRole.ServiceAdministrator => "Service Administrator",
                EssServerRole.PowerUser => "Power User",
                EssServerRole.User => "User",
                _ => throw new NotSupportedException($@"The role {options.Role} is not supported.")
            }
        };

        internal static string ToDelimitedString( this EssMemberFields filterOption )
        {
            var values = new List<string>();

            foreach ( EssMemberFields value in Enum.GetValues(typeof(EssMemberFields)) )
            {
                if ( filterOption.HasFlag(value) )
                {
                    values.Add(value.ToString());
                }
            }

            return string.Join(",", values) ?? string.Empty;
        }

        #endregion

        #region EssScript/EssScriptType Extensions

        /// <summary />
        internal static T CreateScript<T>( Script script, EssCube cube ) where T : class, IEssScript => GetScriptType<T>().CreateScript<T>(script, cube);

        /// <summary />
        internal static EssScriptType GetScriptType<T>() where T : class, IEssScript => typeof(T)?.Name switch
        {
            nameof(IEssCalcScript) => EssScriptType.Calc,
            nameof(IEssMdxScript) => EssScriptType.MDX,
            nameof(IEssMaxlScript) => EssScriptType.MaxL,
            nameof(IEssReportScript) => EssScriptType.Report,
            _ => EssScriptType.Unknown
        };

        #endregion

        #region System.Enum

        /// <summary>
        /// Converts the value of this instance to its equivalent <see cref="DescriptionAttribute" /> value..
        /// </summary>
        /// <param name="value"></param>
        internal static string ToDescription( this Enum value )
        {
            // Get the DescriptionAttribute value for the given enum value.
            var fieldInfo = value.GetType().GetField(value.ToString());
            var descriptions = fieldInfo?.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
            string description = null;

            // If we were able to get a description, use it.
            if ( (descriptions?.Length ?? 0) > 0 )
                description = descriptions[0]?.Description;

            // Return either the obtained description or the string representation.
            return !string.IsNullOrEmpty(description) ? description : value.ToString();
        }

        /// <summary>
        /// Converts the value of this instance to its equivalent <see cref="EnumMemberAttribute" /> value.
        /// </summary>
        /// <param name="value"></param>
        internal static string ToMemberValue( this Enum value )
        {
            // Get the EnumMemberAttribute value for the given enum value.
            var fieldInfo = value.GetType().GetField(value.ToString());
            var memberValues = fieldInfo?.GetCustomAttributes(typeof(EnumMemberAttribute), false) as EnumMemberAttribute[];
            string memberValue = null;

            // If we were able to get a member value, use it.
            if ( (memberValues?.Length ?? 0) > 0 )
                memberValue = memberValues[0]?.Value;

            // Return either the obtained member value or the string representation.
            return !string.IsNullOrEmpty(memberValue) ? memberValue : value.ToString();
        }

        /// <summary>
        /// Converts the given description to the <typeparamref name="T"/> <see cref="Enum"/> value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="description">The <see cref="DescriptionAttribute.Description"/> for which to find a value.</param>
        /// <remarks>If an appropriate description cannot be found, the default value is returned.</remarks>
        internal static T ToValueFromDescription<T>( string description ) where T : struct, Enum
        {
            foreach ( var field in typeof(T).GetFields() )
            {
                if ( Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute )
                {
                    if ( string.Equals(attribute.Description, description, StringComparison.OrdinalIgnoreCase) )
                        return (T)field.GetValue(null);
                }
            }

            if ( Enum.TryParse<T>(description, true, out var value) )
                return value;

            return default;
        }

        internal static EssApplicationRole ToEssApplicationRole( this string role ) => role switch
        {
            "db_access" => EssApplicationRole.db_access,
            "db_update" => EssApplicationRole.db_update,
            "db_manager" => EssApplicationRole.db_manager,
            "app_manager" => EssApplicationRole.app_manager,
            "all" => EssApplicationRole.All,
            _ => throw new NotSupportedException()
        };

        internal static EssServerRole ToEssServerRole( this string role ) => role.ToLowerInvariant() switch
        {
            "user" => EssServerRole.User,
            "power user" => EssServerRole.PowerUser,
            "service admininistrator" => EssServerRole.ServiceAdministrator,
            _ => EssServerRole.Unkown
        };

        internal static string EssServerRoleToString( this EssServerRole role ) => role switch
        {
            EssServerRole.User => "user",
            EssServerRole.PowerUser => "power user",
            EssServerRole.ServiceAdministrator => "service administration",
            _ => throw new NotSupportedException("Unsupported user role.")
        };

        internal static EssDimStorageType ToEssEnum( this MemberBean.DimStorageTypeEnum? dim ) => dim switch
        {
            MemberBean.DimStorageTypeEnum.DENSE => EssDimStorageType.Dense,
            MemberBean.DimStorageTypeEnum.SPARSE => EssDimStorageType.Sparse,
            _ => EssDimStorageType.Unknown
        };

        internal static EssDimensionType ToEssEnum( this MemberBean.DimensionTypeEnum? type ) => type switch
        {
            MemberBean.DimensionTypeEnum.TIME => EssDimensionType.TIME,
            MemberBean.DimensionTypeEnum.REGULAR => EssDimensionType.REGULAR,
            MemberBean.DimensionTypeEnum.ACCOUNTS => EssDimensionType.ACCOUNTS,
            MemberBean.DimensionTypeEnum.ATTRIBUTE => EssDimensionType.ATTRIBUTE,
            MemberBean.DimensionTypeEnum.ATTRIBUTECALC => EssDimensionType.ATTRIBUTECALC,
            _ => EssDimensionType.Unknown
        };

        #endregion

        #region RestSharp Extensions

        /// <summary>
        /// Log the <see cref="RestRequest" /> to any configured <see cref="ILogger" />.
        /// </summary>
        /// <param name="request" />
        /// <param name="configuration" />
        internal static void WriteLogMessage( this RestRequest request, IReadableConfiguration configuration ) =>
            configuration?.Logger?.Log(logLevel: LogLevel.Information, eventId: request.GetEventId(configuration), message: request.GetFormattedRequestMessage(configuration));

        /// <summary />
        /// <param name="request" />
        /// <param name="configuration" />
        internal static string GetFormattedRequestMessage( this RestRequest request, IReadableConfiguration configuration )
        {
            var builder = new StringBuilder();

            try
            {
                var requestUrl = new RestClient(configuration.BasePath).BuildUri(request).AbsoluteUri;

                builder.AppendLine($@"# {request.Method.ToString().ToUpperInvariant()} {requestUrl} HTTP/1.1");

                if ( request.GetFormattedHeaders() is { Length: > 0 } headers )
                    builder.AppendLine($@"# {headers}");

                if ( request.GetFormattedContent() is { Length: > 0 } content )
                    builder.AppendLine(content);
            }
            catch
            {
                // Swallow any exception here.
            }

            return builder.ToString().TrimEnd();
        }

        /// <summary />
        private static EventId GetEventId( this RestRequest request, IReadableConfiguration configuration )
        {
            // Construct a new event identifier with the request event type.
            var identifier = new EventId(id: (int)EssSharpLogEventType.Request);

            try
            {
                // Attempt to fetch the path from the request Uri and rebuild the event identifier.
                var requestPath = new RestClient(configuration.BasePath).BuildUri(request).GetLeftPart(UriPartial.Path).Trim('/');
                identifier = new EventId(id: identifier.Id, name: JsonConvert.SerializeObject(new EssSharpLogEventContext() { Path = requestPath }));
            }
            catch
            {
                // Swallow any exception here.
            }

            return identifier;
        }

        /// <summary />
        private static string GetFormattedContent( this RestRequest request )
        {
            return request?.Parameters?.OfType<BodyParameter>().FirstOrDefault() is { Value: { } content } bodyParameter
                ? GetFormattedContent(bodyParameter.ContentType, content)
                : null;
        }

        /// <summary />
        private static string GetFormattedHeaders( this RestRequest request, bool excludeSensitiveHeaders = true, string[] headersToExclude = null, string delimiter = null )
        {
            return request?.Parameters?.OfType<HeaderParameter>().ToList() is { Count: > 0 } headers
                ? GetFormattedHeaders(headers, excludeSensitiveHeaders, headersToExclude, delimiter)
                : null;
        }

        /// <summary>
        /// Log the <see cref="RestResponse" /> to any configured <see cref="ILogger" />.
        /// </summary>
        /// <param name="response" />
        /// <param name="configuration" />
        internal static void WriteLogMessage( this RestResponse response, IReadableConfiguration configuration ) =>
            configuration?.Logger?.Log(logLevel: LogLevel.Information, eventId: response.GetEventId(), message: response.GetFormattedResponseMessage());

        /// <summary />
        /// <param name="response" />
        internal static string GetFormattedResponseMessage( this RestResponse response )
        {
            var builder = new StringBuilder();

            try
            {
                var responseUrl = response.ResponseUri.AbsoluteUri;

                builder.AppendLine($@"# HTTP/1.1 {(int)response.StatusCode} {response.StatusDescription}");

                if ( response.GetFormattedHeaders() is { Length: > 0 } headers )
                    builder.AppendLine($@"# {headers}");

                if ( response.GetFormattedContent() is { Length: > 0 } content )
                    builder.AppendLine(content);
            }
            catch
            {
                // Swallow any exception here.
            }

            return builder.ToString().TrimEnd();
        }

        /// <summary />
        private static EventId GetEventId( this RestResponse response )
        {
            // Construct a new event identifier with the request event type.
            var identifier = new EventId(id: (int)EssSharpLogEventType.Response);

            try
            {
                // Attempt to fetch the path from the response Uri and rebuild the event identifier.
                var responsePath = response.ResponseUri.GetLeftPart(UriPartial.Path).Trim('/');
                identifier = new EventId(id: identifier.Id, name: JsonConvert.SerializeObject(new EssSharpLogEventContext() { Path = responsePath }));
            }
            catch
            {
                // Swallow any exception here.
            }

            return identifier;
        }

        /// <summary />
        private static string GetFormattedContent( this RestResponse response )
        {
            return response?.Content is { Length: > 0 } content 
                ? GetFormattedContent(response.ContentType, content) 
                : null;
        }

        /// <summary />
        private static string GetFormattedHeaders( this RestResponse response, bool excludeSensitiveHeaders = false, string[] headersToExclude = null, string delimiter = null )
        {
            return (response?.Headers?.AsEnumerable() ?? Enumerable.Empty<HeaderParameter>()).Concat(response?.ContentHeaders?.AsEnumerable() ?? Enumerable.Empty<HeaderParameter>()).ToList() is { Count: > 0 } headers
                ? GetFormattedHeaders(headers, excludeSensitiveHeaders, headersToExclude, delimiter)
                : null;
        }

        /// <summary />
        private static string GetFormattedContent( ContentType contentType, object content )
        {
            string formattedContent = null;

            try
            {
                switch ( contentType )
                {
                    case "application/json": // ContentType.Json:
                    {
                        if ( (content is string cs ? cs : JsonConvert.SerializeObject(content)) is { Length: > 0 } jString )
                        {
                            using var sReader = new StringReader  (jString);
                            using var sWriter = new StringWriter  ();
                            using var jReader = new JsonTextReader(sReader);
                            using var jWriter = new JsonTextWriter(sWriter) { Formatting = Newtonsoft.Json.Formatting.Indented };
                            jWriter.WriteToken(jReader);

                            formattedContent = sWriter.ToString();
                        }
                        break;
                    }

                    case "application/octet-stream": // ContentType.Binary
                    {
                        if ( (content is string cs ? cs : Encoding.UTF8.GetString((byte[])content)) is { Length: > 0 } bString )
                        {
                            formattedContent = bString;
                        }
                        break;
                    }

                    case "application/xml": // ContentType.Xml
                    {
                        if ( content.ToString() is { Length: > 0 } xString )
                        {
                            using var xWriter = XmlWriter.Create(new StringBuilder(), new XmlWriterSettings() { Encoding = new UTF8Encoding(false), Indent = true });
                            var xmlDocument = new XmlDocument();

                            xmlDocument.LoadXml(xString);
                            xmlDocument.Save(xWriter);

                            formattedContent = xWriter.ToString();
                        }
                        break;
                    }

                    default:
                    {
                        formattedContent = content?.ToString();
                        break;
                    }
                }
            }
            catch { }
            finally
            {
                if ( string.IsNullOrEmpty(formattedContent) )
                    formattedContent = content?.ToString();
            }

            return formattedContent;
        }

        /// <summary />
        private static string GetFormattedHeaders( List<HeaderParameter> headers, bool excludeSensitiveHeaders = true, string[] headersToExclude = null, string delimiter = null )
        {
            // Initialize the standard array of sensitive headers, based on the excludeSensitiveHeaders flag.
            var sensitiveHeadersArray = excludeSensitiveHeaders ? new string[] { "authorization", "username", "password" } : new string[0];

            // Join the standard set of headers to exclude with the specific set of headers to exclude.
            var prohibitedHeaders = sensitiveHeadersArray.Union(headersToExclude ?? new string[0], StringComparer.OrdinalIgnoreCase);

            // Initialize the headers dictionary.
            var headerDictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            // Add each header exposed by the standard headers collection to the headers dictionary.
            foreach ( var header in headers )
            {
                if ( !string.IsNullOrEmpty(header.Name) )
                {
                    if ( headerDictionary.ContainsKey(header.Name) )
                        headerDictionary[header.Name] = $@"{headerDictionary[header.Name]}; {header.Value?.ToString()}";
                    else
                        headerDictionary[header.Name] = header.Value?.ToString();
                }
            }

            // If no headers could be obtained, return an empty string. 
            if ( headers.Count is 0 )
                return string.Empty;

            // Set the delimiter.
            delimiter ??= $@"{Environment.NewLine}# ";

            // Get the length of the longest header key plus one for the separator character.
            var keyLength = headerDictionary.Keys
                .Where(key => !prohibitedHeaders.Any(header => string.Equals(header, key, StringComparison.OrdinalIgnoreCase)))
                .Max  (key => key.Length) + 1;

            // Return the formatted header string.
            return string.Join(delimiter, headerDictionary.Keys
                    .Where  (key => !prohibitedHeaders.Any(header => string.Equals(header, key, StringComparison.OrdinalIgnoreCase)))
                    .OrderBy(key => key)
                    .Select (key => $@"{$@"{key}:".PadRight(keyLength)} {headerDictionary[key]}"));
        }

        #endregion
    }
}
