using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EssSharp.Api;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary>
    /// Represents a script that is specific to a cube.
    /// </summary>
    public class EssScript : EssObject, ICloneable, IEssScript
    {
        #region Private Data

        private readonly EssCube _cube;
        private          Script  _script;

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

        #region ICloneable Members

        /// <inheritdoc />
        public object Clone()
        {
            if ( MemberwiseClone() is not EssScript clone )
                throw new Exception("Failed to clone the script.");

            // Deep copy the model Script object.
            clone._script = new Script()
            {
                Content = _script.Content,
                Links = _script.Links,
                Locked = _script.Locked,
                LockedBy = _script.LockedBy,
                LockedTime = _script.LockedTime,
                ModifiedTime = _script.ModifiedTime,
                Name = _script.Name,
                SizeInBytes = _script.SizeInBytes
            };

            return clone;
        }

        #endregion

        #region IEssObject Members

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
        public DateTime ModifiedTime => DateTimeOffset.FromUnixTimeMilliseconds(_script.ModifiedTime).DateTime;

        /// <inheritdoc cref="IEssScript" />
        public new string Name
        {
            get => _script.Name;
            set => _script.Name = value;
        }

        /// <inheritdoc />
        public virtual EssScriptType ScriptType => EssScriptType.Unknown;

        /// <inheritdoc />
        public long Size => _script.SizeInBytes;

        #endregion

        #region IEssScript Methods

        /// <inheritdoc />
        public T Copy<T>( string newName ) where T : class, IEssScript => CopyAsync<T>(newName).GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task<T> CopyAsync<T>( string newName, CancellationToken cancellationToken = default ) where T : class, IEssScript
        {
            if ( string.IsNullOrWhiteSpace(newName) || string.Equals(Name, newName, StringComparison.OrdinalIgnoreCase) )
                throw new ArgumentException("A new name different than the current name must be given in order to copy this script.", nameof(newName));

            // Copy the original script.
            if ( Clone() is not EssScript copiedScript )
                throw new ArgumentException("Unable to clone the current script in order to copy it.");

            // Update the name of the copied script.
            copiedScript.Name = newName;

            // Save the copied script (with a different name), which creates a new script.
            await copiedScript.SaveAsync(cancellationToken).ConfigureAwait(false);

            // Return the copied script.
            return copiedScript as T;
        }

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
        public bool Exists() => ExistsAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task<bool> ExistsAsync( CancellationToken cancellationToken = default )
        {
            var api = GetApi<ScriptsApi>();

            // Return whether this script exists under the cube.
            return (await api.ScriptsListScriptsAsync(applicationName: Cube.Application.Name, databaseName: Cube.Name, file: ScriptType.ToString().ToLowerInvariant(), cancellationToken: cancellationToken).ConfigureAwait(false))?
                .Items?.Any(script => string.Equals(Name, script?.Name, StringComparison.OrdinalIgnoreCase)) ?? false;
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

                // We don't need to verify that the script exists on the server at the expense of another API call.
                // The server gives a good error message when the script doesn't exist.
                //if ( !await ExistsAsync(cancellationToken).ConfigureAwait(false) )
                    //throw new Exception("The script must exist on the cube in order to get its content.");

                if ( await api.ScriptsGetScriptContentAsync(applicationName: Cube.Application.Name, databaseName: Cube.Name, scriptName: Name, file: ScriptType.ToString(), cancellationToken: cancellationToken).ConfigureAwait(false) is not { } content )
                    throw new Exception("Could not get script content.");

                return _script.Content = content.Content;
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e)
            {
                throw new Exception($@"Unable to get script content for {Name}. {e.Message}", e);
            }
        }

        /// <inheritdoc />
        public void Rename( string newName ) => RenameAsync(newName).GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task RenameAsync( string newName, CancellationToken cancellationToken = default )
        {
            if ( string.IsNullOrWhiteSpace(newName) || string.Equals(Name, newName, StringComparison.OrdinalIgnoreCase) )
                throw new ArgumentException("A new name different than the current name must be given in order to rename this script.", nameof(newName));

            // Copy and retain the original script.
            if ( Clone() is not EssScript originalScript )
                throw new ArgumentException("Unable to clone the current script prior to renaming it.");

            // Update the name of this script.
            _script.Name = newName;

            // Save the renamed script, which creates a new script.
            await SaveAsync(cancellationToken).ConfigureAwait(false);

            // If the original script exists on the server, delete it.
            if ( await originalScript.ExistsAsync(cancellationToken).ConfigureAwait(false) )
                await originalScript.DeleteAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public void Save() => SaveAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task SaveAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                if ( Content is null )
                    throw new ArgumentNullException(nameof(Content), "The content of this script must set before it can be saved.");

                var api = GetApi<ScriptsApi>();

                // If this script already exists on the cube, edit it.
                if ( await ExistsAsync(cancellationToken).ConfigureAwait(false) )
                {
                    try
                    {
                        if ( await api.ScriptsEditScriptAsync(applicationName: Cube.Application.Name, databaseName: Cube.Name, scriptName: Name, body: _script, file: ScriptType.ToString().ToLowerInvariant(), cancellationToken: cancellationToken).ConfigureAwait(false) is not { } updatedScript )
                            throw new Exception("Received an empty or invalid response.");

                        // Update the backing script.
                        _script = updatedScript;
                    }
                    catch ( Exception e )
                    {
                        throw new Exception("Could not update the existing script.", e);
                    }
                }
                // Otherwise, save it as a new script.
                else
                {
                    try
                    {
                        if ( await api.ScriptsCreateScriptAsync(applicationName: Cube.Application.Name, databaseName: Cube.Name, body: _script, file: ScriptType.ToString().ToLowerInvariant(), cancellationToken: cancellationToken).ConfigureAwait(false) is not { } createdScript )
                            throw new Exception("Received an empty or invalid response.");

                        // Update the backing script.
                        _script = createdScript;
                    }
                    catch ( Exception e )
                    {
                        throw new Exception("Could not create the new script.", e);
                    }
                }

                // Get the content.
                await GetContentAsync(cancellationToken).ConfigureAwait(false);
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to save script ""{Name}"". {e.Message}", e);
            }
        }

        #endregion
    }
}
