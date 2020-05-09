using HappyMrsChicken.Components;
using HappyMrsChicken.Entities;
using HappyMrsChicken.Globals;
using HappyMrsChicken.Systems;
using HappyMrsChicken.Systems.Input;
using HappyMrsChicken.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace HappyMrsChicken
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MainGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch sb;
        Texture2D garden;
        //SystemManager sm = new SystemManager();

        public MainGame()
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

            base.Initialize();
            this.graphics.PreferredBackBufferWidth = this.GraphicsDevice.Adapter.CurrentDisplayMode.Width;
            this.graphics.PreferredBackBufferHeight = this.GraphicsDevice.Adapter.CurrentDisplayMode.Height - 70;
            this.graphics.ApplyChanges();
            this.IsMouseVisible = true;
            initialiseSystems();
            initialiseComponents();
            garden = Content.Load<Texture2D>("cut_tree");
        }

        private void initialiseComponents()
        {
            Entity e = new Entity();
            EntityManager.Instance.AddEntity(e);
            ChickenAnimation anim = new ChickenAnimation(e.Id, getChickenSprites(e.Id), getChickenSounds());
            Position p = new Position(e.Id, new Vector2(125, 125), anim.Size);
            Velocity v = new Velocity(e.Id, 10, 0);
            v.CurrentSpeed = 4;

            InputActionReactor iar = new InputActionReactor(e.Id, SystemManager.Instance.Get<GameInputHandler>());
            FiniteStateMachine fsm = new FiniteStateMachine(e.Id, null);
            ChickenStateAction csa = new ChickenStateAction(e.Id, fsm);
            csa.SetState(ChickenState.Idle);
            EntityManager.Instance.AddComponent<FiniteStateMachine>(e.Id, fsm);
            EntityManager.Instance.AddComponent<ChickenStateAction>(e.Id, csa);
            EntityManager.Instance.AddComponent<ChickenAnimation>(e.Id, anim);
            EntityManager.Instance.AddComponent<Position>(e.Id, p);
            EntityManager.Instance.AddComponent<Velocity>(e.Id, v);
            EntityManager.Instance.AddComponent<InputActionReactor>(e.Id, iar);

            // register chicken
            var collider = SystemManager.Instance.Get<Collider>();
            var corn = SystemManager.Instance.Get<Corn>();
            collider.Register(e.Id, corn.Kernel);

            // register gropochek
            var npcs = SystemManager.Instance.Get<NPCManager>().NPCs;
            foreach(var npc in npcs)
            {
                collider.Register(npc, corn.Kernel);
            }
        }

        private Dictionary<ChickenState, SoundEffectInstance> getChickenSounds()
        {
            var cluck = Content.Load<SoundEffect>("chicken_cluck").CreateInstance();
            var eat = Content.Load<SoundEffect>("chicken_eat").CreateInstance();

            var dict = new Dictionary<ChickenState, SoundEffectInstance>();
            dict.Add(ChickenState.MoveLeft, cluck);
            dict.Add(ChickenState.MoveRight, cluck);
            dict.Add(ChickenState.MoveUp, cluck);
            dict.Add(ChickenState.MoveDown, cluck);
            dict.Add(ChickenState.Eat, eat);

            return dict;
        }

        private Dictionary<GropochekState, SoundEffectInstance> getGropochekSounds()
        {
            var cackle = Content.Load<SoundEffect>("cackle").CreateInstance();

            var dict = new Dictionary<GropochekState, SoundEffectInstance>();
            dict.Add(GropochekState.Eating, cackle);
            return dict;
        }

        private Dictionary<ChickenState, AnimatedSprite> getChickenSprites(int id)
        {
            AnimatedSprite chickenRight = new AnimatedSprite(id, "chicken", this.Content, 1, 12, 12);
            var texture = Content.Load<Texture2D>("chicken");
            RenderTarget2D chickenRenderTarget = new RenderTarget2D(this.GraphicsDevice, texture.Width, texture.Height);
            sb.GraphicsDevice.SetRenderTarget(chickenRenderTarget);
            sb.GraphicsDevice.Clear(Color.Transparent);
            sb.Begin();
            sb.Draw(texture, destinationRectangle: new Rectangle(0, 0, texture.Width, texture.Height), effects: SpriteEffects.FlipHorizontally, color: Color.White);
            sb.End();
            sb.GraphicsDevice.Reset();
            AnimatedSprite chickenLeft = new AnimatedSprite(id, chickenRenderTarget, 1, 12, 12, true);
            var dict = new Dictionary<ChickenState, AnimatedSprite>();
            dict.Add(ChickenState.MoveLeft, chickenLeft);
            dict.Add(ChickenState.MoveRight, chickenRight);
            dict.Add(ChickenState.MoveUp, chickenLeft);
            dict.Add(ChickenState.MoveDown, chickenLeft);
            dict.Add(ChickenState.Idle, chickenRight);

            return dict;
        }

        private void initialiseSystems()
        {
            GridLines.InitLine(this.GraphicsDevice);
            Dictionary<char, Texture2D> textureMapper = new Dictionary<char, Texture2D>();
            textureMapper.Add('G', Content.Load<Texture2D>("terrain/grass"));
            textureMapper.Add('W', Content.Load<Texture2D>("terrain/water"));
            textureMapper.Add('B', createTransparentTexture());

            TileManager tm = new TileManager(textureMapper);
            tm.Init(this);
            //tm.LoadFromFile("Content\\simple_enclosure.txt");
            tm.ReadFileHMC(@"C:\code\repos\HappyMrsChicken\GameEditor\bin\Windows\x86\Debug\Content\map.txt");
            SystemManager.Instance.Add<TileManager>(tm);

            KeyboardExtended keyboard = new KeyboardExtended();
            keyboard.Init(this);
            SystemManager.Instance.Add<KeyboardExtended>(keyboard);

            GameInputHandler gih = new GameInputHandler(keyboard);
            gih.Init(this);
            SystemManager.Instance.Add<GameInputHandler>(gih);

            AnimatedSpriteRenderer asr = new AnimatedSpriteRenderer();
            asr.Init(this);
            SystemManager.Instance.Add<AnimatedSpriteRenderer>(asr);

            AnimationRenderer ar = new AnimationRenderer();
            ar.Init(this);
            SystemManager.Instance.Add<AnimationRenderer>(ar);

            EntityFSMUpdater efu = new EntityFSMUpdater();
            efu.Init(this);
            SystemManager.Instance.Add<EntityFSMUpdater>(efu);

            Corn corn = new Corn();
            corn.Init(this);
            SystemManager.Instance.Add<Corn>(corn);

            Collider collider = new Collider();
            collider.Init(this);
            SystemManager.Instance.Add<Collider>(collider);

            Score score = new Score();
            score.Init(this);
            SystemManager.Instance.Add<Score>(score);

            Gropochek chek = new Gropochek(this.Content, GraphicsDevice.Viewport, getGropochekSounds());
            NPCManager npcManager = new NPCManager();
            npcManager.RegisterNPC(chek.Instance.Id, chek);

            SystemManager.Instance.Add<NPCManager>(npcManager);
        }

        private Texture2D createTransparentTexture()
        {
            Color[] data = new Color[Tile.SIZE * Tile.SIZE];
            for (int i = 0; i < Tile.SIZE * Tile.SIZE; i++)
            {
                data[i] = Color.Transparent;
                data[i].A = 0;
            }
            var transparent = new Texture2D(this.GraphicsDevice, Tile.SIZE, Tile.SIZE);
            transparent.SetData<Color>(data);
            return transparent;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            sb = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            foreach (var iu in SystemManager.Instance.Updatable)
            {
                iu.Update(gameTime);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            foreach (var ir in SystemManager.Instance.Renderable)
            {
                ir.Draw(gameTime, sb);
            }
            sb.Draw(garden, new Vector2(1000, 300), Color.White);
            sb.End();


            if (DebugSettings.ShowGrid)
            {
                sb.Begin(SpriteSortMode.Deferred, BlendState.Opaque);
                GridLines.DrawSpriteGrid(Vector2.Zero, this.GraphicsDevice.Viewport, 1.0f, sb);
                sb.End();
            }
            base.Draw(gameTime);
        }
    }
}
