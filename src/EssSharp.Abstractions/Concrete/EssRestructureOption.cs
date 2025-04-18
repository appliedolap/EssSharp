namespace EssSharp
{
    /// <summary>
    /// Defines the restructure option for <see cref="EssJobType.Dimbuild" />.
    /// </summary>
    public enum EssRestructureOption
    {
        /// <summary>
        /// Enum PRESERVE_ALL_DATA for value: PRESERVE_ALL_DATA
        /// </summary>
        PRESERVE_ALL_DATA = 0,

        /// <summary>
        /// Enum RETAININPUTDATA for value: PRESERVE_NO_DATA
        /// </summary>
        PRESERVE_NO_DATA = 1,

        /// <summary>
        /// Enum RETAINLEAFDATA for value: PRESERVE_LEAFLEVEL_DATA
        /// </summary>
        PRESERVE_LEAFLEVEL_DATA = 2,

        /// <summary>
        /// Enum REMOVEALLDATA for value: PRESERVE_INPUT_DATA
        /// </summary>
        PRESERVE_INPUT_DATA = 3
    }
}
