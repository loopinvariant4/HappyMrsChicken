using HappyMrsChicken.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyMrsChicken.Systems
{
    public class Score : ISystem, IRenderable
    {
        private int currentScore = 0;
        private GameSpriteFont font;
        Vector2 scorePosition;
        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            font.Draw(sb, scorePosition, currentScore.ToString());
        }

        public void Init(Game game)
        {
            font = new GameSpriteFont(game.Content);
            scorePosition = new Vector2(game.GraphicsDevice.Viewport.Width - 200, 70);
        }

        public void Increment()
        {
            currentScore++;
        }
    }
}
