using System;
using System.IO;
using System.Linq;

namespace EssSharp
{
    /// <summary>
    /// Supported script types.
    /// </summary> />
    public enum EssScriptType
    {
        /// <summary />
        Unknown = 0,
        /// <summary />
        Calc = 1,
        /// <summary />
        MaxL = 2,
        /// <summary />
        MDX = 3,
        /// <summary />
        Report = 4
    };

    /// <summary>
    /// Options required to execute <see cref="IEssCalcScript"/>, <see cref="IEssMdxScript"/>, <see cref="IEssMaxlScript"/>, and <see cref="IEssReportScript"/> scripts.
    /// </summary>
    public class EssJobScriptOptions : EssJobOptions, IEssJobOptions
    {
        /// <summary/>
        /// <param name="fileName">The name of script to execute. Maps to <see cref="File" />.</param>
        /// <param name="applicationName">Name of application.</param>
        /// <param name="cubeName">Name of cube</param>
        public EssJobScriptOptions( string fileName, string applicationName = null, string cubeName = null ) : base(EssJobType.Unknown)
        {
            if ( string.IsNullOrEmpty(fileName) )
                throw new ArgumentNullException($@"The name of a script is required to create an {nameof(EssJobScriptOptions)} with this constructor.", nameof(fileName));

            // Throw if a filename without an extension was given.
            if ( !Path.HasExtension(fileName) )
                throw new ArgumentException($@"{nameof(EssJobScriptOptions)} requires the {nameof(fileName)} parameter to include a file extension.");
            
            // Determine the JobType on the basis of the given filename's extension.
            JobType = Path.GetExtension(fileName)?.ToLowerInvariant() switch
            {
                ".csc" => EssJobType.Calc,
                ".mdx" => EssJobType.MdxScript,
                ".msh" => EssJobType.Maxl,
                ".rep" => EssJobType.ExecuteReport,
                _      => throw new ArgumentException($@"{nameof(EssJobScriptOptions)} requires the {nameof(fileName)} parameter to include a known script file extension.")
            };

            // Strip the extension from the given script filename.
            var name = Path.GetFileNameWithoutExtension(fileName);

            ApplicationName = applicationName;
            CubeName        = cubeName;

            switch ( JobType )
            {
                case EssJobType.ExecuteReport:
                    ReportScriptFilename = fileName;
                    LockForUpdate = LockForUpdate;
                    break;
                default:
                    Script = fileName;
                    break;
            }
        }

        /// <summary />
        /// <param name="essScript">The name of script to execute. Maps to <see cref="IEssScript" />.</param>
        /// <param name="applicationName" />
        /// <param name="cubeName" />
        public EssJobScriptOptions( IEssScript essScript, string applicationName = null, string cubeName = null ) : base(EssJobType.Unknown)
        {
            if ( essScript is null )
                throw new ArgumentNullException($@"An {nameof(IEssScript)} object is required to create an {nameof(EssJobScriptOptions)} with this constructor.", nameof(essScript));

            if ( essScript.ScriptType is EssScriptType.Unknown )
                throw new ArgumentException($@"A {nameof(essScript.ScriptType)} must be set on the {nameof(essScript)} given to this constructor.", nameof(essScript));

            JobType = essScript.ScriptType switch
            {
                EssScriptType.Calc   => EssJobType.Calc,
                EssScriptType.MDX    => EssJobType.MdxScript,
                EssScriptType.Report => EssJobType.ExecuteReport,
                EssScriptType.MaxL   => EssJobType.Maxl,
                _                    => throw new NotImplementedException()
            };

            ApplicationName = applicationName;
            CubeName        = cubeName;

            switch ( JobType )
            {
                case EssJobType.Calc:
                    Script = $@"{essScript.Name}.csc";
                    break;
                case EssJobType.MdxScript:
                    Script = $@"{essScript.Name}.mdx";
                    break;
                case EssJobType.ExecuteReport:
                    ReportScriptFilename = essScript.Name;
                    LockForUpdate = LockForUpdate;
                    break;
                case EssJobType.Maxl:
                    Script = $"{essScript.Name}.msh";
                    break;
            }
        }

        #region IEssJobOptions EssJobType.Calc Members

        /// <inheritdoc />
        public bool? LockForUpdate { get; set; }

        /// <inheritdoc />
        public string ReportScriptFilename { get; set; }

        /// <inheritdoc />
        public string Script { get; set; }

        #endregion
    }
}
