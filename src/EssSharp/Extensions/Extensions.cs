﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;

using EssSharp.Model;

namespace EssSharp
{
    internal static class Extensions
    {
        # region Migrations to EssSharp.Abstractions

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
                .Where (application => application is not null)
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
        internal static List<IEssApplicationConfiguration> ToEssSharpList(this ApplicationConfigList configList, EssApplication application)
        {
            if (application is null)
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
                .Where (cube => cube is not null)
                .Select(cube => new EssCube(cube, application) as IEssCube)
                .ToList() ?? new List<IEssCube>();
        }


        /// <summary>
        /// Returns a <see cref="List{T}"/> of <see cref="IEssDatasource"/> objects associated with the given <see cref="EssServer"/>.
        /// </summary>
        /// <param name="datasourcesList" />
        /// <param name="server" />
        internal static List<IEssDatasource> ToEssSharpList(this DatasourcesList datasourcesList, EssServer server)
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
        internal static List<IEssDimension> ToEssSharpList(this DimensionList dimensionList, EssCube cube)
        {
            if (cube is null)
                throw new ArgumentNullException(nameof(cube), $"The given {nameof(cube)} is null.");

            return dimensionList
                .Items?
                .Where(dimension => dimension is not null)
                .Select(dimension => new EssDimension(dimension, cube) as IEssDimension)
                .ToList() ?? new List<IEssDimension>();
        }

        /// <summary>
        /// Returns a <see cref="List{T}"/> of <see cref="IEssFile"/> or <see cref="IEssFolder"/> objects associated with the given <see cref="EssServer"/>.
        /// </summary>
        /// <param name="fileCollectionResponse" />
        /// <param name="server" />
        internal static List<T> ToEssSharpList<T>(this FileCollectionResponse fileCollectionResponse, EssServer server) where T : class, IEssFile
        {
            if (server is null)
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
        /// Returns a <see cref="List{T}"/> of <see cref="IEssLock"/> objects associated with the given <see cref="EssCube"/>.
        /// </summary>
        /// <param name="lockObjectList"></param>
        /// <param name="cube"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static List<IEssLock> ToEssSharpList(this LockObjectList lockObjectList, EssCube cube)
        {
            if (cube is null)
                throw new ArgumentNullException(nameof(cube), $"The given {nameof(cube)} is null.");
            
            return lockObjectList
                .Items?
                .Where(lockObject => lockObject is not null)
                .Select(lockObject => new EssLock(lockObject, cube) as IEssLock)
                .ToList() ?? new List<IEssLock>();
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
        internal static List<IEssUtility> ToEssSharpList(this ResourceList resourceList, EssServer server)
        {
            if (server is null)
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
                .Select(session => new EssSession(session) as IEssSession)
                .ToList() ?? new List<IEssSession>();
        }

        /// <summary>
        /// Returns a <see cref="List{T}"/> of <see cref="IEssScript"/> objects associated with the given <see cref="EssCube"/>.
        /// </summary>
        /// <param name="scriptList" />
        /// <param name="cube" />
        internal static List<IEssScript> ToEssSharpList(this ScriptList scriptList, EssCube cube)
        {
            if (cube is null)
                throw new ArgumentNullException(nameof(cube), $"The given {nameof(cube)} is null.");

            return scriptList
                .Items?
                .Where(script => script is not null)
                .Select(script => new EssScript(script, cube) as IEssScript)
                .ToList() ?? new List<IEssScript>();
        }

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
                .Where (variable => variable is not null)
                .Select(variable => parent switch 
                {
                    EssServer      server      => new EssServerVariable     (variable, server),
                    EssApplication application => new EssApplicationVariable(variable, application),
                    EssCube        cube        => new EssCubeVariable       (variable, cube),
                                   _           => null 
                } as T)
                .Where(variable => variable is not null)
                .ToList() ?? new List<T>();
        }

        #endregion

        #region Migrations from EssSharp.Abstractions

        /// <summary>
        /// Returns a <see cref="DrillthroughMetadataBean"/> from the given <see cref="IEssDrillthroughRange"/> object
        /// and optionally, an <see cref="IEssDrillthroughOptions"/>..
        /// </summary>
        /// <param name="context" />
        /// <param name="options" />
        internal static DrillthroughMetadataBean ToModelBean( this IEssDrillthroughRange context, IEssDrillthroughOptions options = null )
            => ToModelBean(new List<IEssDrillthroughRange>() { context }, options);

        /// <summary>
        /// Returns a <see cref="DrillthroughMetadataBean"/> from the collection of <see cref="IEssDrillthroughRange"/> objects
        /// and optionally, an <see cref="IEssDrillthroughOptions"/>..
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
        /// Returns a <see cref="ParametersBean"/> from the given <see cref="IEssJobOptions"/> object.
        /// </summary>
        /// <param name="options" />
        internal static ParametersBean ToModelBean( this IEssJobOptions options ) => new ParametersBean()
        {
            // EssJobType.exportExcel
            BuildMethod          = options.BuildMethod.HasValue && Enum.IsDefined(typeof(ParametersBean.BuildMethodEnum), (int)options.BuildMethod) ? (ParametersBean.BuildMethodEnum)options.BuildMethod : null,
            Calc                 = options.Calc?.ToString().ToLowerInvariant(),
            Data                 = options.Data?.ToString().ToLowerInvariant(),
            MemberIds            = options.MemberIds?.ToString().ToLowerInvariant(),
            // EssJobType.importExcel
            BuildOption          = options.BuildOption.HasValue && Enum.IsDefined(typeof(ParametersBean.BuildOptionEnum), (int)options.BuildOption) ? (ParametersBean.BuildOptionEnum)options.BuildOption : null,
            CatalogExcelPath     = options.CatalogExcelPath,
            CreateFiles          = options.CreateFiles?.ToString().ToLowerInvariant(),
            DeleteExcelOnSuccess = options.DeleteExcelOnSuccess?.ToString().ToLowerInvariant(),
            ExecuteScript        = options.ExecuteScripts?.ToString().ToLowerInvariant(),
            ImportExcelFileName  = options.ImportExcelFilename,
            Loaddata             = options.LoadData?.ToString().ToLowerInvariant(),
            Overwrite            = options.Overwrite?.ToString().ToLowerInvariant(),
            RecreateApplication  = options.RecreateApp?.ToString().ToLowerInvariant(),
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

        #endregion

        #region System.Enum

        /// <summary>
        /// Converts the value of this instance to its equivalent descriptive string representation.
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

        #endregion
    }
}
