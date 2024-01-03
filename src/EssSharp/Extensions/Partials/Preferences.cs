using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace EssSharp.Model
{
    /// <summary />
    public partial class Preferences : IEquatable<Preferences>, IValidatableObject
    {
        /// <summary>
        /// A serialization-only <see cref="RowSupression"/> property.
        /// </summary>
        /// <remarks>
        /// The server returns "rowSupression" when getting preferences but requires that "rowSuppression" is serialized when setting preferences.
        /// </remarks>
        [JsonProperty]
        [DataMember(Name = "rowSuppression", EmitDefaultValue = false)]
        private RowSuppression RowSuppression => RowSupression;

        ///// <summary>
        ///// Uncomment this method to serialize only "rowSuppression" rather than both "rowSuppression" and "rowSupression".
        ///// </summary>
        //public bool ShouldSerializeRowSupression() => false;
    }
}
