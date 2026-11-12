namespace EssSharp
{
    /// <summary>
    /// An entry of a smart list (text list): a numeric value and the text displayed for it.
    /// </summary>
    public class EssSmartListEntry
    {
        /// <summary />
        /// <param name="id">The numeric value stored in a cell.</param>
        /// <param name="text">The text displayed for the value.</param>
        public EssSmartListEntry( int id, string text )
        {
            Id   = id;
            Text = text;
        }

        /// <summary>
        /// The numeric value stored in a cell.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// The text displayed for the value.
        /// </summary>
        public string Text { get; }

        /// <inheritdoc />
        public override string ToString() => $"{nameof(EssSmartListEntry)} {{ {nameof(Id)} = {Id}, {nameof(Text)} = {Text} }}";
    }
}
