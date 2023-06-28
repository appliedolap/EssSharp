﻿using System;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using EssSharp.Api;
using EssSharp.Model;
using Newtonsoft.Json.Linq;

namespace EssSharp
{
    /// <summary />
    public class EssReportScript : EssScript, IEssReportScript
    {
        #region Private Data

        private readonly EssCube _cube;
        private readonly Script  _script;

        #endregion

        #region Constructors

        /// <summary />
        internal EssReportScript( Script script, EssCube cube ) : base(script, cube)
        {
            _script = script ??
                throw new ArgumentNullException(nameof(script), $"An API model {nameof(script)} is required to create an {nameof(EssScript)}.");

            _cube = cube ??
                throw new ArgumentNullException(nameof(cube), $"An API model {nameof(cube)} is required to create an {nameof(EssScript)}.");
        }

        #endregion

        #region IEssScript Members

        /// <inheritdoc />
        public override EssScriptType ScriptType => EssScriptType.Report;

        #endregion

        #region IEssScript Methods

        #endregion
    }
}