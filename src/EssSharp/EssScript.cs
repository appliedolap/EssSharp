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
    public class EssScript : EssObject, IEssScript
    {
        #region Private Data

        private readonly EssCube _cube;
        private readonly Script  _script;

        #endregion

        #region Constructors

        /// <summary />
        internal EssScript( Script script, EssCube cube ) : base(cube?.Configuration, cube?.Client)
        {
            _script = script ??
                throw new ArgumentNullException(nameof(script), $"An API model {nameof(script)} is required to create an {nameof(EssScript)}.");

            _cube = cube ??
                throw new ArgumentNullException(nameof(cube), $"An API model {nameof(cube)} is required to create an {nameof(EssScript)}.");
        }

        #endregion

        #region IEssObject Members

        /// <inheritdoc />
        public override string Name => _script?.Name;

        /// <inheritdoc />
        public override EssType Type => EssType.Script;

        #endregion

        #region IEssScript Members

        /// <inheritdoc />
        public string Content => _script?.Content;

        /// <inheritdoc />
        public IEssCube Cube => _cube;

        /// <inheritdoc />
        public long ModifiedTime => _script.ModifiedTime;

        /// <inheritdoc />
        public EssScriptType ScriptType => EssScriptType.Unknown;

        /// <inheritdoc />
        public long Size => _script.SizeInBytes;

        #endregion

        #region IEssScript Methods

        /// <inheritdoc />
        public void Delete() => DeleteAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task DeleteAsync ( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<ScriptsApi>();
                await api.ScriptsDeleteScriptAsync( Cube.Application.Name, Cube.Name, Name, null, 0, cancellationToken).ConfigureAwait(false);
            }
            catch ( Exception )
            {
                throw;
            }
        }

        /// <inheritdoc />
        public void Execute() => ExecuteAsync( ).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <remarks>TODO: FIGURE OUT HOW TO MAP SCRIPT TYPE TO JOBTYPE.</remarks>
        public async Task ExecuteAsync( CancellationToken cancellationToken = default)
        {
            try
            {
                var options = new EssJobScriptOptions(essScript: this)
                {
                    ApplicationName = Cube.Application.Name,
                    CubeName = Cube.Name
                };
                
                await Cube.Application.Server.CreateJob(options).ExecuteAsync(cancellationToken).ThrowIfFailed().ConfigureAwait(false);
                
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception )
            {
                throw;
            }
            
        }

        /// <inheritdoc />
        /// <returns></returns>
        public string GetScriptContent() => GetScriptContentAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns></returns>
        public async Task<string> GetScriptContentAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<ScriptsApi>();

                if ( await api.ScriptsGetScriptContentAsync(applicationName: Cube.Application.Name, databaseName: Cube.Name, scriptName: Name, file: ScriptType.ToString(), cancellationToken: cancellationToken).ConfigureAwait(false) is not { } content )
                    throw new Exception("Could not get script content.");

                return content.Content;
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e)
            {
                throw new Exception($@"Unable to get script content for {Name}. {e.Message}");
            }
        }
        #endregion
    }
}
