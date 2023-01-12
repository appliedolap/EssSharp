﻿using System;
using System.Collections.Generic;
using System.Linq;

using EssSharp.Model;

namespace EssSharp
{
    internal static class Extensions
    {
        /// <summary>
        /// Returns an <see cref="List{T}"/> of <see cref="IEssApplication"/> objects associated with the given <see cref="EssServer"/>.
        /// </summary>
        /// <param name="applicationList" />
        /// <param name="server" />
        internal static List<IEssApplication> ToEssSharpList( this ApplicationList applicationList, EssServer server )
        {
            if ( server is null )
                throw new ArgumentNullException(nameof(server), "The given server is null.");

            return applicationList?
                .Items?
                .Where (application => application is not null)
                .Select(application => new EssApplication(application, server) as IEssApplication)?
                .ToList() ?? new List<IEssApplication>();
        }

        /// <summary>
        /// Returns an <see cref="List{T}"/> of <see cref="IEssCube"/> objects associated with the given <see cref="EssApplication"/>.
        /// </summary>
        /// <param name="cubeList" />
        /// <param name="application" />
        internal static List<IEssCube> ToEssSharpList( this CubeList cubeList, EssApplication application )
        {
            if ( application is null )
                throw new ArgumentNullException(nameof(application), "The given application is null.");

            return cubeList?
                .Items?
                .Where (cube => cube is not null)
                .Select(cube => new EssCube(application, cube) as IEssCube)?
                .ToList() ?? new List<IEssCube>();
        }

        /// <summary>
        /// Returns an <see cref="List{T}"/> of <see cref="IEssUrl"/> objects associated with the given <see cref="EssServer"/>.
        /// </summary>
        /// <param name="urlList" />
        /// <param name="server" />
        internal static List<IEssUrl> ToEssSharpList( this EssbaseURLList urlList, EssServer server )
        {
            if ( server is null )
                throw new ArgumentNullException(nameof(server), @"The given {nameof(server)} is null.");

            return urlList?
                .Items?
                .Where( url => url is not null)
                .Select(url => new EssUrl(url, server) as IEssUrl)?
                .ToList() ?? new List<IEssUrl>();
        }


    }
}
