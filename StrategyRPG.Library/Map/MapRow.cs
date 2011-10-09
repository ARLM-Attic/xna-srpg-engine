// -----------------------------------------------------------------------
// <copyright file="MapRow.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace StrategyRPG.TileEngine
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a MapRow.
    /// </summary>
    public class MapRow
    {
        /// <summary>
        /// Gets or sets the columns.
        /// </summary>
        public List<MapCell> Columns { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MapRow"/> class.
        /// </summary>
        public MapRow()
        {
            Columns = new List<MapCell>();
        }
    }
}
