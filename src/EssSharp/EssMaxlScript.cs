using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssMaxlScript : EssScript, IEssMaxlScript
    {
        #region Constructors

        /// <summary />
        internal EssMaxlScript( Script script, EssCube cube ) : base(script, cube) { }

        #endregion

        #region IEssScript Members

        /// <inheritdoc />
        public override EssScriptType ScriptType => EssScriptType.MaxL;

        #endregion
    }
}
