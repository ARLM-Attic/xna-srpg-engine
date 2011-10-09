// -----------------------------------------------------------------------
// <copyright file="TileEngine.cs">
// MIT License, Copyright (c) 2011 Sidney Andrews
// </copyright>
// -----------------------------------------------------------------------

namespace StrategyRPG.TileEngine
{
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using StrategyRPG.TileEngine.Sprite;
    using System.Collections.Generic;

    /// <summary>
    /// Statically handle TileEngine and Camera Logic
    /// </summary>
    public class Engine
    {
        #region Private Properties

        private TileMap myMap;
        private int squaresAcross = 17;
        private int squaresDown = 35;
        private int baseOffsetX = -32;
        private int baseOffsetY = -64;
        private float heightRowDepthMod = 0.0000001f;
        private float characterMargin = 100f;

        private SpriteAnimation _mainCharacter;
        private ButtonState _previousRightShoulder;
        private Texture2D _mouseIcon;
        private bool _showHelp;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="TileEngine&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="textureToLoad">The texture to load.</param>
        /// <param name="height">The height.</param>
        /// <param name="width">The width.</param>
        /// <param name="mouseMap">The mouse map.</param>
        /// <param name="mouseIcon">The mouse icon.</param>
        /// <param name="mainCharacter">The main character.</param>
        public Engine(Texture2D textureToLoad, int height, int width, Texture2D mouseMap, Texture2D mouseIcon, Texture2D mainCharacter) : this(textureToLoad, height, width, mouseMap, mouseIcon, mainCharacter, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Engine&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="textureToLoad">The texture to load.</param>
        /// <param name="height">The height.</param>
        /// <param name="width">The width.</param>
        /// <param name="mouseMap">The mouse map.</param>
        /// <param name="mouseIcon">The mouse icon.</param>
        /// <param name="mainCharacter">The main character.</param>
        /// <param name="mapData">The map data.</param>
        public Engine(Texture2D textureToLoad, int height, int width, Texture2D mouseMap, Texture2D mouseIcon, Texture2D mainCharacter, MapRow[] mapData)
        {
            if (textureToLoad == null)
            {
                // throw new ArgumentNullException(GetParamName(() => textureToLoad)); }
            }

            _showHelp = false;
            _mouseIcon = mouseIcon;

            _mainCharacter = mainCharacter.ToSprite();

            Tile.TileSetTexture = textureToLoad;

            squaresAcross = (width / Tile.TileStepX) + 5;
            squaresDown = (height / Tile.TileStepY) + 5;

            myMap = new TileMap(squaresAcross + 5, squaresDown + 10, mapData, mouseMap);

            Camera.ViewWidth = width;
            Camera.ViewHeight = height;
            Camera.WorldWidth = ((myMap.MapWidth - 2) * Tile.TileStepX);
            Camera.WorldHeight = ((myMap.MapHeight - 2) * Tile.TileStepY);
            Camera.DisplayOffset = new Vector2(baseOffsetX, baseOffsetY);
        }

        /// <summary>
        /// Handles the keyboard input.
        /// </summary>
        /// <param name="ks">The ks.</param>
        public void HandleInput(GamePadState gs, GameTime gt)
        {
            if (gs.Buttons.RightShoulder == ButtonState.Pressed && _previousRightShoulder == ButtonState.Released)
            {
                _showHelp = !_showHelp;
            }
            _previousRightShoulder = gs.Buttons.RightShoulder;

            Vector2 moveVector = Vector2.Zero;
            Vector2 moveDir = Vector2.Zero;
            string animation = "";

            // Move Camera
            {
                if (gs.ThumbSticks.Right.X < 0.0f) // Left
                {
                    Camera.Move(new Vector2(-2, 0));
                }

                if (gs.ThumbSticks.Right.X > 0.0f) // Right
                {
                    Camera.Move(new Vector2(2, 0));
                }

                if (gs.ThumbSticks.Right.Y > 0.0f) // Up
                {
                    Camera.Move(new Vector2(0, -2));
                }

                if (gs.ThumbSticks.Right.Y < 0.0f) // Down
                {
                    Camera.Move(new Vector2(0, 2));
                }
            }
        }

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="font">The font.</param>
        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            Vector2 firstSquare = new Vector2(Camera.Location.X / Tile.TileStepX, Camera.Location.Y / Tile.TileStepY);
            int firstX = (int)firstSquare.X;
            int firstY = (int)firstSquare.Y;

            Vector2 squareOffset = new Vector2(Camera.Location.X % Tile.TileStepX, Camera.Location.Y % Tile.TileStepY);
            int offsetX = (int)squareOffset.X;
            int offsetY = (int)squareOffset.Y;

            float maxdepth = ((myMap.MapWidth + 1) + ((myMap.MapHeight + 1) * Tile.TileWidth)) * 10;
            float depthOffset;

            for (int y = 0; y < squaresDown; y++)
            {
                int rowOffset = 0;
                if ((firstY + y) % 2 == 1) { rowOffset = Tile.OddRowXOffset; }

                for (int x = 0; x < squaresAcross; x++)
                {
                    int mapx = (firstX + x);
                    int mapy = (firstY + y);
                    depthOffset = 0.7f - ((mapx + (mapy * Tile.TileWidth)) / maxdepth);

                    if ((mapx >= myMap.MapWidth) || (mapy >= myMap.MapHeight))
                        continue;
                    foreach (int tileID in myMap.Rows[mapy].Columns[mapx].BaseTiles)
                    {
                        spriteBatch.Draw(

                            Tile.TileSetTexture,
                            Camera.WorldToScreen(
                                new Vector2((mapx * Tile.TileStepX) + rowOffset, mapy * Tile.TileStepY)),
                            Tile.GetSourceRectangle(tileID),
                            Color.White,
                            0.0f,
                            Vector2.Zero,
                            1.0f,
                            SpriteEffects.None,
                            1.0f);
                    }
                    int heightRow = 0;

                    foreach (int tileID in myMap.Rows[mapy].Columns[mapx].HeightTiles)
                    {
                        spriteBatch.Draw(
                            Tile.TileSetTexture,
                            Camera.WorldToScreen(
                                new Vector2(
                                    (mapx * Tile.TileStepX) + rowOffset,
                                    mapy * Tile.TileStepY - (heightRow * Tile.HeightTileOffset))),
                            Tile.GetSourceRectangle(tileID),
                            Color.White,
                            0.0f,
                            Vector2.Zero,
                            1.0f,
                            SpriteEffects.None,
                            depthOffset - ((float)heightRow * heightRowDepthMod));
                        heightRow++;
                    }

                    foreach (int tileID in myMap.Rows[y + firstY].Columns[x + firstX].TopperTiles)
                    {
                        spriteBatch.Draw(
                            Tile.TileSetTexture,
                            Camera.WorldToScreen(
                                new Vector2((mapx * Tile.TileStepX) + rowOffset, mapy * Tile.TileStepY)),
                            Tile.GetSourceRectangle(tileID),
                            Color.White,
                            0.0f,
                            Vector2.Zero,
                            1.0f,
                            SpriteEffects.None,
                            depthOffset - ((float)heightRow * heightRowDepthMod));
                    }

                    if (_showHelp)
                    {
                        spriteBatch.DrawString(font,
                            (x + firstX).ToString() + ", " + (y + firstY).ToString(),
                            new Vector2((x * Tile.TileStepX) - offsetX + rowOffset + baseOffsetX + 24,
                            (y * Tile.TileStepY) - offsetY + baseOffsetY + 48), Color.White, 0f, Vector2.Zero,
                            1.0f,
                            SpriteEffects.None,
                            0.0f
                        );
                    }
                }
            }

            // Draw character
            Point mainCharacterStandingOn = myMap.WorldToMapCell(new Point((int)_mainCharacter.Position.X, (int)_mainCharacter.Position.Y));
            int mainCharacterHeight = myMap.Rows[mainCharacterStandingOn.Y].Columns[mainCharacterStandingOn.X].HeightTiles.Count() * Tile.HeightTileOffset;
            _mainCharacter.Draw(spriteBatch, 0, -mainCharacterHeight);

            // Draw mouse highlight
            Vector2 hilightLoc = Camera.ScreenToWorld(new Vector2(Mouse.GetState().X, Mouse.GetState().Y));
            Point hilightPoint = myMap.WorldToMapCell(new Point((int)hilightLoc.X, (int)hilightLoc.Y));

            int hilightrowOffset = 0;
            if ((hilightPoint.Y) % 2 == 1)
            {
                hilightrowOffset = Tile.OddRowXOffset;
            }

            float originX = (hilightPoint.X * Tile.TileStepX) + hilightrowOffset;
            float originY = (hilightPoint.Y + 2) * Tile.TileStepY;

            List<Transformation> transformations = new List<Transformation>
            {
                new Transformation { X = 0, Y = 0 },                            // Origin

                new Transformation { X = -32, Y = 16 },                         // Left one
                new Transformation { X = -64, Y = 32 },                         // Left two
                new Transformation { X = -96, Y = 48 },                         // Left three

                new Transformation { X = 32, Y = 16 },                          // Right one
                new Transformation { X = 64, Y = 32 },                          // Right one
                new Transformation { X = 96, Y = 48},                           // Right one

                new Transformation { X = 32, Y = -16 },                         // Up one
                new Transformation { X = 64, Y = -32 },                         // Up two
                new Transformation { X = 96, Y = -48 },                         // Up three
                
                new Transformation { X = 0, Y = 32 },                           // Up one, Right one
                new Transformation { X = -32, Y = 48 },                         // Up one, Right two
                new Transformation { X = 32, Y = 48 },                          // Up two, Right one
                
                new Transformation { X = 64, Y = 0 },                           // Up one, Left one   
                new Transformation { X = 96, Y = -16 },                         // Up one, Left two   
                new Transformation { X = 96, Y = 16 },                          // Up one, Left two             
            };

            foreach (var trans in transformations)
            {

                spriteBatch.Draw(
                    _mouseIcon,
                    Camera.WorldToScreen(
                        new Vector2(
                            originX + trans.X, 
                            originY + trans.Y
                        )
                    ),
                    new Rectangle(0, 0, 64, 32),
                    trans.IsHighlighted ? Color.CornflowerBlue * 0.3f : Color.White * 0.3f,
                    0.0f,
                    Vector2.Zero,
                    1.0f,
                    SpriteEffects.None,
                    0.0f
                );
            }

            spriteBatch.End();
        }
    }

    public class Transformation
    {
        public float X { get; set; }
        public float Y { get; set; }
        public bool IsHighlighted { get; set; }
    }
}
