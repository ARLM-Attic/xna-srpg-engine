using Microsoft.Xna.Framework;

namespace StrategyRPG.TileEngine
{
    /// <summary>
    /// Camera class.
    /// </summary>
    public static class Camera
    {
        private static Vector2 _location;

        /// <summary>
        /// Gets or sets the width of the view.
        /// </summary>
        public static int ViewWidth { get; set; }

        /// <summary>
        /// Gets or sets the height of the view.
        /// </summary>
        public static int ViewHeight { get; set; }

        /// <summary>
        /// Gets or sets the width of the world.
        /// </summary>
        public static int WorldWidth { get; set; }

        /// <summary>
        /// Gets or sets the height of the world.
        /// </summary>
        public static int WorldHeight { get; set; }

        /// <summary>
        /// Gets or sets the display offset.
        /// </summary>
        public static Vector2 DisplayOffset { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        public static Vector2 Location
        {
            get
            {
                return _location;
            }
            set
            {
                _location = new Vector2(
                    MathHelper.Clamp(value.X, 0f, WorldWidth - ViewWidth),
                    MathHelper.Clamp(value.Y, 0f, WorldHeight - ViewHeight)
                );
            }
        }

        /// <summary>
        /// Initializes the <see cref="Camera"/> class.
        /// </summary>
        static Camera()
        {
            Location = Vector2.Zero;
        }

        /// <summary>
        /// Worlds to screen.
        /// </summary>
        /// <param name="worldPosition">The world position.</param>
        /// <returns>Vector.</returns>
        public static Vector2 WorldToScreen(Vector2 worldPosition)
        {
            return worldPosition - Location + DisplayOffset;
        }

        /// <summary>
        /// Screens to world.
        /// </summary>
        /// <param name="screenPosition">The screen position.</param>
        /// <returns>Vector.</returns>
        public static Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            return screenPosition + Location - DisplayOffset;
        }

        /// <summary>
        /// Moves the specified offset.
        /// </summary>
        /// <param name="offset">The offset.</param>
        public static void Move(Vector2 offset)
        {
            Location += offset;
        }
    }
}
