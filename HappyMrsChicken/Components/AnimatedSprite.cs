using HappyMrsChicken.Entities;
using HappyMrsChicken.Globals;
using HappyMrsChicken.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyMrsChicken.Components
{
    public class AnimatedSprite : ComponentBase, IRenderable, IUpdatable
    {
        #region vars
        private int tpf;
        private Rectangle[] frames;
        private Texture2D texture;
        private int currFrame = 0;
        private int elapsedTicks = 0;
        private bool isPaused = true;
        private Vector2 size;
        #endregion
        public AnimatedSprite(int entityId, string spriteName, ContentManager cm, int rows, int cols, int fps) : base(entityId)
        {
            frames = new Rectangle[rows * cols];
            texture = cm.Load<Texture2D>(spriteName);
            var width = texture.Width / cols;
            var height = texture.Height / rows;
            size = new Vector2(width, height);
            int x = 0, y = 0;
            int l = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    frames[l++] = new Rectangle(j * width, i * height, width, height);
                }
            }
            TPF = fps;
            Position = Vector2.Zero;
            Origin = Vector2.Zero;
        }
        public AnimatedSprite(int entityId, Texture2D texture, int rows, int cols, int fps, bool shouldReverseAnimation) : base(entityId)
        {
            frames = new Rectangle[rows * cols];
            this.texture = texture;
            int width = texture.Width / cols;
            int height = texture.Height / rows;
            int x = 0, y = 0;
            int l = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    frames[l++] = new Rectangle(j * width, i * height, width, height);
                }
            }
            if (shouldReverseAnimation)
            {
                Array.Reverse(frames);
            }
            TPF = fps;
            Position = Vector2.Zero;
            Origin = Vector2.Zero;
        }

        #region properties
        /// <summary>
        /// TPF is ticks per frame. We tie the animation to the game updates rather than real time.
        /// Example: If the animation is 10 frames per sec and we have update expectation at 60 fps then each frame should stay for 60/10 = 6 updates. 
        /// When the game slows down then the animation should also slow down and speed up</param>
        /// </summary>
        private int TPF
        {
            get => tpf;
            set
            {
                tpf = 60 / value;
            }
        }

        private int FPS
        {
            get => 60 / tpf;
            set => TPF = value;
        }
        public Vector2 Position { get; set; }

        public Vector2 Origin { get; set; }

        public Vector2 Size => size;
        #endregion
        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            var pos = EntityManager.Instance.GetComponent<Position>(EntityId);
 
            sb.Draw(texture, pos.XY, frames[currFrame], Color.White, 0f, Origin, 1.0f, SpriteEffects.None, 0f);
            if(DebugSettings.ShowSpriteBorders)
            {
                GridLines.DrawRectangle(sb, pos.XY, frames[currFrame].Width, frames[currFrame].Height, Origin, Color.Red);
            }
        }

        public void Update(GameTime gameTime)
        {
            if (isPaused)
            {
                return;
            }
            if (++elapsedTicks > TPF)
            {
                elapsedTicks = 0;
                currFrame = (currFrame + 1) % frames.Length;
            }
        }

        public void Play()
        {
            isPaused = false;
        }

        public void Pause()
        {
            isPaused = true;
        }

        public void Reset()
        {
            Pause();
            currFrame = 0;
        }
    }
}
