using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssDrillThroughReport : IEssDrillThroughReport
    {
        #region Private Data

        private readonly EssCube    _cube;
        private readonly ReportBean _report;

        #endregion

        #region Constructors

        /// <summary />
        internal EssDrillThroughReport( ReportBean report, EssCube cube )
        {
            _report = report ??
                throw new ArgumentNullException(nameof(report), $"An API model {nameof(report)} is required to create an {nameof(EssDrillThroughReport)}.");

            _cube = cube ??
                throw new ArgumentNullException(nameof(cube), $"An {nameof(EssCube)} {nameof(cube)} is required to create an {nameof(EssDrillThroughReport)}.");
        }

        #endregion

        #region IEssDrillThroughReport Members

        /// <inheritdoc />
        public IEssCube Cube => _cube;

        /// <inheritdoc />
        public string Name => _report?.Name;

        /// <inheritdoc />
        public EssType Type => EssType.DrillThroughReport;

        /// <inheritdoc />
        public void Execute(IEssDrillThroughRange context, string aliasTable = null)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public void Execute(IEnumerable<IEssDrillThroughRange> context, string aliasTable = null)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public Task ExecuteAsync(IEssDrillThroughRange context, string aliasTable = null, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public Task ExecuteAsync(IEnumerable<IEssDrillThroughRange> context, string aliasTable = null, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}