using System;
using System.Linq;

namespace EssSharp
{
    /// <summary />
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

    /// <summary />
    public class EssJobScriptOptions : EssJobOptions, IEssJobOptions
    {

        /// <summary />
        /// <param name="scriptName">The name of script to execute. Maps to <see cref="File" />.</param>
        /// <param name="applicationName" />
        /// <param name="cubeName" />
        public EssJobScriptOptions( string fileName, string applicationName = null, string cubeName = null ) : base(EssJobType.Unknown)
        {
            if ( string.IsNullOrEmpty(fileName) )
                throw new ArgumentNullException($@"The name of a script is required to create an {nameof(EssJobScriptOptions)} with this constructor.", nameof(fileName));

            var extension = fileName?.Split('.').Last();
            if ( !(string.Equals(extension, "csc") || string.Equals(extension, "mdx")) )
                throw new ArgumentException($@"{nameof(EssJobScriptOptions)} requires file extension for parameter {nameof(fileName)}.");

            JobType = extension switch
            {
                "csc" => EssJobType.Calc,
                "mdx" => EssJobType.MdxScript,
                _     => throw new NotImplementedException()
            };

            ApplicationName = applicationName;
            CubeName        = cubeName;

            Script          = fileName;
        }

        /// <summary />
        /// <param name="scriptName">The name of script to execute. Maps to <see cref="File" />.</param>
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
                EssScriptType.Calc => EssJobType.Calc,
                EssScriptType.MDX  => EssJobType.MdxScript,
                _                  => throw new NotImplementedException()
            };

            ApplicationName = applicationName;
            CubeName        = cubeName;

            Script = $@"{essScript.Name}.{(JobType is EssJobType.Calc ? "csc" : "mdx")}";
        }

        #region IEssJobOptions EssJobType.Calc Members

        /// <inheritdoc />
        public string Script { get; set; }

        #endregion
    }
}
