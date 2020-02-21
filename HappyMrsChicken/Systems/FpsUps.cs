using HappyMrsChicken.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyMrsChicken.Systems
{
    public class FpsUps : ISystem, IRenderable, IUpdatable
    {
        private float fps = 0f, ups = 0f;
        private TimeSpan elapsedFps = TimeSpan.Zero, elapsedUps = TimeSpan.Zero;
        private int fpsCounter = 0, upsCounter = 0;
        TimeSpan ONESECOND = TimeSpan.FromSeconds(1);
        SpriteFont arialFont;
        Vector2 stringPosition;
        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            fpsCounter += 1;
            elapsedFps+= gameTime.ElapsedGameTime;
            if (elapsedFps> ONESECOND)

            {
                elapsedFps-= ONESECOND;
                fps = fpsCounter;
                fpsCounter = 0;
            }
            sb.DrawString(arialFont, string.Format("FPS/UPS : {0}/{1}", fps, ups), stringPosition, Color.White);
        }

        public void Init(Game game)
        {
            arialFont = game.Content.Load<SpriteFont>("arial");
            var size = arialFont.MeasureString("FPS/UPS : 00/00");
            stringPosition = new Vector2(game.GraphicsDevice.Viewport.Width - (20f + size.Length()), 10f);
        }

        public void Update(GameTime gameTime)
        {
            upsCounter += 1;
            elapsedUps+= gameTime.ElapsedGameTime;
            if (elapsedUps> ONESECOND)
            {
                elapsedUps-= ONESECOND;
                ups = upsCounter;
                upsCounter = 0;
            }
        }
    }
}
