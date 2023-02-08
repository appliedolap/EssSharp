using System;
using System.Collections.Generic;
using System.Linq;

using EssSharp.Model;

namespace EssSharp
{
    internal static class Extensions
    {
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
        /// Returns a <see cref="List{T}"/> of <see cref="IEssDimension"/> objects associated with the given <see cref="EssCube"/>.
        /// </summary>
        /// <param name="dimensionList" />
        /// <param name="cube" />
        internal static List<IEssDimension> ToEssSharpList( this DimensionList dimensionList, EssCube cube )
        {
            if (cube is null)
                throw new ArgumentNullException(nameof(cube), $"The given {nameof(cube)} is null.");

            return dimensionList
                .Items?
                .Where (dimension => dimension is not null)
                .Select(dimension => new EssDimension(dimension, cube) as IEssDimension)
                .ToList() ?? new List<IEssDimension>();
        }

        /// <summary>
        /// Returns a <see cref="List{T}"/> of <see cref="IEssDrillThroughReport"/> objects associated with the given <see cref="EssCube"/>.
        /// </summary>
        /// <param name="reportList" />
        /// <param name="cube" />
        internal static List<IEssDrillThroughReport> ToEssSharpList( this ReportList reportList, EssCube cube )
        {
            if (cube is null)
                throw new ArgumentNullException(nameof(cube), $"The given {nameof(cube)} is null.");

            return reportList
                .Items?
                .Where (report => report is not null)
                .Select(report => new EssDrillThroughReport(report, cube) as IEssDrillThroughReport)
                .ToList() ?? new List<IEssDrillThroughReport>();
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
                .Where (url => url is not null)
                .Select(url => new EssUrl(url, server) as IEssUrl)
                .ToList() ?? new List<IEssUrl>();
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
                    EssServer      server      => new EssServerVariable           (variable, server),
                    EssApplication application => new EssApplicationVariable(variable, application),
                    EssCube        cube        => new EssCubeVariable       (variable, cube),
                                   _           => null 
                } as T)
                .Where(variable => variable is not null)
                .ToList() ?? new List<T>();
        }
    }
}
