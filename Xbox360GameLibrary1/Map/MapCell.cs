using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StrategyRPG.TileEngine
{
    /// <summary>
    /// Represents a map cell.
    /// </summary>
    public class MapCell
    {
        /// <summary>
        /// Gets or sets the tile ID.
        /// </summary>
        public int TileID
        {
            get
            {
                return BaseTiles.FirstOrDefault();
            }
            set
            {
                if (BaseTiles.Count > 0)
                {
                    BaseTiles[0] = value;
                }
                else
                {
                    AddBaseTile(value);
                }
            }
        }

        /// <summary>
        /// Gets the base tiles.
        /// </summary>
        public List<int> BaseTiles { get; private set; }

        /// <summary>
        /// Gets or sets the height tiles.
        /// </summary>
        public int[] HeightTiles { get; set; }

        /// <summary>
        /// Gets or sets the topper tiles.
        /// </summary>
        public int[] TopperTiles { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MapCell"/> class.
        /// </summary>
        public MapCell()
        {
            BaseTiles = new List<int>();
            HeightTiles = new List<int>().ToArray();
            TopperTiles = new List<int>().ToArray();
        }

        /// <summary>
        /// Adds the base tile.
        /// </summary>
        /// <param name="tileID">The tile ID.</param>
        public void AddBaseTile(int tileID)
        {
            BaseTiles.Add(tileID);
        }

        /// <summary>
        /// Adds the height tile.
        /// </summary>
        /// <param name="tileID">The tile ID.</param>
        public void AddHeightTile(int tileID)
        {
            var newTiles = HeightTiles.ToList();
            newTiles.Add(tileID);
            HeightTiles = newTiles.ToArray();
        }

        /// <summary>
        /// Adds the topper tile.
        /// </summary>
        /// <param name="tileID">The tile ID.</param>
        public void AddTopperTile(int tileID)
        {
            var newTiles = TopperTiles.ToList();
            newTiles.Add(tileID);
            TopperTiles = newTiles.ToArray();
        }
    }
}
