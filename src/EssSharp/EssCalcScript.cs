using EssSharp.Model;

namespace EssSharp
{
    /// <summary>
    /// Represents an MDX script that is specific to a cube.
    /// </summary>
    public class EssCalcScript : EssScript, IEssCalcScript
    {
        #region Constructors

        /// <summary />
        internal EssCalcScript( Script script, EssCube cube ) : base(script, cube) { }

        #endregion

        #region IEssScript Members

        /// <inheritdoc />
        public override EssScriptType ScriptType => EssScriptType.Calc;

        #endregion
    }
}
