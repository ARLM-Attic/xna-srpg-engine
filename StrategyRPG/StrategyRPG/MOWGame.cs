using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StrategyRPG.TileEngine;

namespace StrategyRPG
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MOWGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Engine engine;
        SpriteFont pericles6;

        public MOWGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            this.IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            var width = this.graphics.PreferredBackBufferWidth;
            var height = this.graphics.PreferredBackBufferHeight;

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            pericles6 = Content.Load<SpriteFont>(@"Fonts\Pericles6");

            Texture2D tileSet = Content.Load<Texture2D>(@"Textures\TileSets\part4_tileset");
            Texture2D mouseMap = Content.Load<Texture2D>(@"Textures\Utility\mousemap");
            Texture2D mouseIcon = Content.Load<Texture2D>(@"Textures\Utility\hilight");
            Texture2D mainCharacter = Content.Load<Texture2D>(@"Textures\Characters\vlad_sword");

            MapRow[] mapData = null; // Content.Load<MapRow[]>(@"MapData\sampleMap");

            engine = new Engine(
                tileSet,
                height, width,
                mouseMap,
                mouseIcon,
                mainCharacter,
                mapData
            );
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                this.Exit();
            }

            engine.HandleInput(GamePad.GetState(PlayerIndex.One), gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            engine.Draw(spriteBatch, pericles6);

            base.Draw(gameTime);
        }
    }
}
