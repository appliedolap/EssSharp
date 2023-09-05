using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Linq;

namespace EssSharp
{
    public class EssGridOperationsOptions
    {

        public EssGridOperationsOptions( IEssGrid grid, EssGridActionType action, string alias = default(string), List<int> coordinates = default(List<int>), List<List<int>> ranges = default(List<List<int>>) )
        {
            Grid = grid;
            Action = action;
            Alias = alias;
            Coordinates = coordinates;
            Ranges = ranges;
        }

        /// <summary>
        /// Gets or Sets Action
        /// </summary>
        public EssGridActionType Action { get; set; }

        /// <summary>
        /// Gets or Sets Grid
        /// </summary>
        public IEssGrid Grid { get; set; }

        /// <summary>
        /// Gets or Sets Alias
        /// </summary>
        [DataMember(Name = "alias", EmitDefaultValue = false)]
        public string Alias { get; set; }

        /// <summary>
        /// Gets or Sets Coordinates
        /// </summary>
        public List<int> Coordinates { get; set; }

        /// <summary>
        /// Gets or Sets Ranges
        /// </summary>
        public List<List<int>> Ranges { get; set; }
    }
}
