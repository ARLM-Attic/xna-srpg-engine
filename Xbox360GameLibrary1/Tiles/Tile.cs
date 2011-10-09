using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StrategyRPG.TileEngine
{
    /// <summary>
    /// Represents a Tile.
    /// </summary>
    public static class Tile
    {
        /// <summary>
        /// Gets the odd row X offset.
        /// </summary>
        internal static int OddRowXOffset { get { return 32; } }

        /// <summary>
        /// Gets the height tile offset.
        /// </summary>
        internal static int HeightTileOffset { get { return 32; } }

        /// <summary>
        /// Gets the tile step X.
        /// </summary>
        internal static int TileStepX { get { return 64; } }

        /// <summary>
        /// Gets the tile step Y.
        /// </summary>
        internal static int TileStepY { get { return 16; } }

        /// <summary>
        /// Gets the tile height.
        /// </summary>
        internal static int TileHeight { get { return 64; } }

        /// <summary>
        /// Gets the tile width.
        /// </summary>
        internal static int TileWidth { get { return 64; } }

        /// <summary>
        /// Gets or Sets the TileSet Texture.
        /// </summary>
        public static Texture2D TileSetTexture { get; set; }

        /// <summary>
        /// Gets the source rectangle.
        /// </summary>
        /// <param name="tileIndex">Index of the tile.</param>
        /// <returns>Source rectangle.</returns>
        public static Rectangle GetSourceRectangle(int tileIndex)
        {
            int tileY = tileIndex / (TileSetTexture.Width / TileWidth);
            int tileX = tileIndex % (TileSetTexture.Width / TileWidth);

            return new Rectangle(tileX * TileWidth, tileY * TileHeight, TileWidth, TileHeight);
        }
    }
}
