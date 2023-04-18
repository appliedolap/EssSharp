using System.ComponentModel;

namespace EssSharp.Client
{
    internal static class Extensions
    {
        #region System.Enum

        /// <summary>
        /// Converts the value of this instance to its equivalent descriptive string representation.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static string ToDescription( this Enum value )
        {
            // Get the DescriptionAttribute value for the given enum value.
            var fieldInfo = value.GetType().GetField(value.ToString());
            var descriptions = fieldInfo?.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
            string description = null;

            // If we were able to get a description, use it.
            if ( (descriptions?.Length ?? 0) > 0 )
                description = descriptions[0]?.Description;

            // Return either the obtained description or the string representation.
            return !string.IsNullOrEmpty(description) ? description : value.ToString();
        }

        /// <summary>
        /// Converts the given description to the <typeparamref name="T"/> <see cref="Enum"/> value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="description">The <see cref="DescriptionAttribute.Description"/> for which to find a value.</param>
        /// <remarks>If an appropriate description cannot be found, the default value is returned.</remarks>
        internal static T ToValueFromDescription<T>( string description ) where T : Enum
        {
            foreach ( var field in typeof(T).GetFields() )
            {
                if ( Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute )
                {
                    if ( string.Equals(attribute.Description, description, StringComparison.OrdinalIgnoreCase) )
                        return (T)field.GetValue(null);
                }
            }

            if ( Enum.TryParse(typeof(T), description, true, out var value) )
                return (T)value;

            return default;
        }

        #endregion
    }
}
