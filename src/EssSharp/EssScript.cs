using System;
using System.Threading;
using System.Threading.Tasks;
using EssSharp.Api;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary>
    /// Represents a script that is specific to a cube.
    /// </summary>
    public class EssScript : EssObject, IEssScript
    {
        #region Private Data

        private readonly EssCube _cube;
        private Script  _script;

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
        public string Content
        {
            get => _script.Content;
            set => _script.Content = value;
        }

        /// <inheritdoc />
        public IEssCube Cube => _cube;

        /// <inheritdoc />
        public long ModifiedTime => _script.ModifiedTime;

        /// <inheritdoc />
        public virtual EssScriptType ScriptType => throw new NotImplementedException();

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
                await api.ScriptsDeleteScriptAsync( Cube.Application.Name, Cube.Name, Name, ScriptType.ToString(), 0, cancellationToken).ConfigureAwait(false);
            }
            catch ( Exception )
            {
                throw;
            }
        }

        /// <inheritdoc />
        /// <returns><see cref="IEssJob"/></returns>
        public IEssJob Execute() => ExecuteAsync( ).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns><see cref="IEssJob"/></returns>
        public async Task<IEssJob> ExecuteAsync( CancellationToken cancellationToken = default)
        {
            try
            {
                var options = new EssJobScriptOptions(essScript: this)
                {
                    ApplicationName = Cube.Application.Name,
                    CubeName = Cube.Name
                };
                
                var job = await Cube.Application.Server.CreateJob(options).ExecuteAsync(cancellationToken).ThrowIfFailed().ConfigureAwait(false);

                return job;
                
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception )
            {
                throw;
            }
            
        }

        /// <inheritdoc />
        /// <returns><see cref="string"/> containing script content</returns>
        public string GetContent() => GetContentAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns><see cref="string"/> containing script content</returns>
        public async Task<string> GetContentAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<ScriptsApi>();

                if ( await api.ScriptsGetScriptContentAsync(applicationName: Cube.Application.Name, databaseName: Cube.Name, scriptName: Name, file: ScriptType.ToString(), cancellationToken: cancellationToken).ConfigureAwait(false) is not { } content )
                    throw new Exception("Could not get script content.");

                return _script.Content = content.Content;
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e)
            {
                throw new Exception($@"Unable to get script content for {Name}. {e.Message}");
            }
        }

        /// <inheritdoc />
        /// <returns>T where T is <see cref="IEssScript"/></returns>
        public T Save<T>() where T: class, IEssScript => SaveAsync<T>().GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>T where T is <see cref="IEssScript"/></returns>
        public async Task<T> SaveAsync<T>( CancellationToken cancellationToken = default ) where T : class, IEssScript
        {
            try
            {
                var api = GetApi<ScriptsApi>();

                if ( await api.ScriptsCreateScriptAsync(applicationName: Cube.Application.Name, databaseName: Cube.Name, body: _script, file: ScriptType.ToString().ToLowerInvariant(), cancellationToken: cancellationToken).ConfigureAwait(false) is not { } script )
                    throw new Exception("Unable to save script");

                _script = script;

                return this as T;
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to save script cube ""{Name}"". {e.Message}", e);
            }
        }
        #endregion
    }
}
