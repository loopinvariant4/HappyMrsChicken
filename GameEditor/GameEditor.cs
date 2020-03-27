using HappyMrsChicken;
using HappyMrsChicken.Systems;
using HappyMrsChicken.Systems.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace GameEditor
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameEditor : Game
    {
        #region vars
        GraphicsDeviceManager graphics;
        SpriteBatch sb;
        public event EventHandler<InfoEventArgs> Info;
        Texture2D grass, transparent;
        TileManager tm;
        KeyboardExtended keyboard = new KeyboardExtended();
        Dictionary<char, Texture2D> textureMapper = new Dictionary<char, Texture2D>();
        #endregion

        #region properties
        public string SelectedTerrain { get; set; } = "B";
        public string SelectedTerrainObject { get; internal set; }
        public ActiveSpriteObject ActiveSprite { get; set; } = ActiveSpriteObject.Terrain;
        #endregion
        public GameEditor()
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
            // TODO: Add your initialization logic here
            this.graphics.PreferredBackBufferWidth = this.GraphicsDevice.Adapter.CurrentDisplayMode.Width;
            this.graphics.PreferredBackBufferHeight = this.GraphicsDevice.Adapter.CurrentDisplayMode.Height - 70;
            this.graphics.ApplyChanges();
            this.IsMouseVisible = true;


            grass = Content.Load<Texture2D>("terrain/grass");
            GridLines.InitLine(this.GraphicsDevice);
            createTransparentTexture();
            textureMapper.Add('G', Content.Load<Texture2D>("terrain/grass"));
            textureMapper.Add('W', Content.Load<Texture2D>("terrain/water"));
            textureMapper.Add('B', transparent);

            string filename = "map.txt";

            tm = new TileManager(textureMapper);
            tm.Init(this);
            if (!File.Exists(filename))
            {
                tm.LoadEmptyGrid((this.GraphicsDevice.Viewport.Height / Tile.SIZE) + 1, (this.GraphicsDevice.Viewport.Width / Tile.SIZE) + 1, grass);
            }
            else
            {
                tm.ReadFileHMC(filename);
            }
            base.Initialize();
        }

        private void createTransparentTexture()
        {
            Color[] data = new Color[Tile.SIZE * Tile.SIZE];
            for (int i = 0; i < Tile.SIZE * Tile.SIZE; i++)
            {
                data[i] = Color.Transparent;
                data[i].A = 0;
            }
            transparent = new Texture2D(this.GraphicsDevice, Tile.SIZE, Tile.SIZE);
            transparent.SetData<Color>(data);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            sb = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();
            keyboard.Update(gameTime);

            // TODO: Add your update logic here

            publishMouseCoordinates();

            var mouse = Mouse.GetState();
            if (mouse.LeftButton == ButtonState.Pressed)
            {
                applySelectedItem(mouse);

            }
            if (mouse.RightButton == ButtonState.Pressed)
            {
                clearItem(mouse);
            }

            if (keyboard.GetState().IsKeyUp(Keys.F5))
            {
                tm.SaveFileHMC("map.txt");
            }

            base.Update(gameTime);
        }

        private void applySelectedItem(MouseState mouse)
        {
            if (!IsWithinBounds(mouse))
            {
                return;
            }

            switch (ActiveSprite)
            {
                case ActiveSpriteObject.Terrain:
                    {
                        Debug.Assert(textureMapper.ContainsKey(SelectedTerrain[0]), "TextureMapper does not contain the key : " + SelectedTerrain[0]);
                        var tile = tm.GetTileUnderPoint(mouse.X, mouse.Y);
                        tm.SetTile(mouse.X, mouse.Y, new Tile(tile.X, tile.Y, SelectedTerrain[0], textureMapper[SelectedTerrain[0]]));
                        break;

                    }
                case ActiveSpriteObject.TerrainObject:
                    {
                        tm.AddTerrainObject("terrain_objects/" + SelectedTerrainObject, mouse.X, mouse.Y);
                        break;
                    }
            }
        }

        private void clearItem(MouseState mouse)
        {
            var item = tm.GetObjectUnderPoint(mouse.X, mouse.Y);
            if (item != null)
            {
                tm.RemoveItem(item);
            }
            else
            {
                clearTile(mouse);
            }
        }
        private void clearTile(MouseState mouse)
        {
            var tile = tm.GetTileUnderPoint(mouse.X, mouse.Y);
            tile.TileType = 'B';
            tile.Texture = transparent;
        }

        private bool IsWithinBounds(MouseState mouse)
        {
            return mouse.X >= 0 && mouse.X <= this.GraphicsDevice.Viewport.Width
                && mouse.Y >= 0 && mouse.Y <= this.GraphicsDevice.Viewport.Height;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // draw the map and all the items on it
            sb.Begin();
            tm.Draw(gameTime, sb);
            sb.End();

            // draw the object to be placed at the mouse cursor if one is selected
            if (!string.IsNullOrEmpty(SelectedTerrainObject) && IsWithinBounds(Mouse.GetState()))
            {
                var texture = Content.Load<Texture2D>("terrain_objects/" + SelectedTerrainObject);
                sb.Begin();
                sb.Draw(texture, 
                    new Rectangle(Mouse.GetState().X - texture.Width / 2, Mouse.GetState().Y - texture.Height / 2, texture.Width, texture.Height),
                    Color.White);
                sb.End();
            }

            sb.Begin(SpriteSortMode.Deferred, BlendState.Opaque);
            GridLines.DrawSpriteGrid(Vector2.Zero, this.GraphicsDevice.Viewport, 1.0f, sb);
            sb.End();

            base.Draw(gameTime);
        }


        private void publishMouseCoordinates()
        {
            var state = Mouse.GetState();
            publish("mouse", state.X, state.Y);
        }

        InfoEventArgs args = new InfoEventArgs(null, null);
        private void publish(string key, params object[] values)
        {
            args.Key = key;
            args.Values = values;
            Info(this, args);
        }
    }

    public class InfoEventArgs
    {
        public object[] Values { get; set; }
        public string Key { get; set; }

        public InfoEventArgs(string key, object[] args)
        {
            Key = key;
            Values = args;
        }
    }
}

