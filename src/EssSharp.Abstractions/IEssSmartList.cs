using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EssSharp
{
    /// <summary>
    /// A smart list (text list) of a cube's outline: a named list of numeric values and the text each one stands for,
    /// which a text measure displays in place of the number stored in its cells.
    /// </summary>
    public interface IEssSmartList : IEssObject
    {
        /// <summary>
        /// Returns the cube whose outline holds this smart list.
        /// </summary>
        public IEssCube Cube { get; }

        /// <summary>
        /// The entries of the list, in the order the server lists them.
        /// </summary>
        public IReadOnlyList<EssSmartListEntry> Entries { get; }

        /// <summary>
        /// The text displayed for a cell that holds no value.
        /// </summary>
        public string MissingName { get; }

        /// <summary>
        /// The text displayed for a cell whose value is not an entry of the list.
        /// </summary>
        public string OutOfRangeName { get; }

        /// <summary>
        /// Deletes the smart list from the cube's outline.
        /// </summary>
        public void Delete();

        /// <summary>
        /// Asynchronously deletes the smart list from the cube's outline.
        /// </summary>
        /// <param name="cancellationToken" />
        public Task DeleteAsync( CancellationToken cancellationToken = default );
    }
}
