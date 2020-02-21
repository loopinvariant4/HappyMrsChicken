using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyMrsChicken.Utils
{
    public class GameSpriteFont
    {
        Texture2D numberSprite;
        Dictionary<char, Rectangle> numbers;
        int digitWidth;
        public GameSpriteFont(ContentManager cm)
        {
            numberSprite = cm.Load<Texture2D>("font_numbers");
            digitWidth = numberSprite.Width / 10;
            numbers = new Dictionary<char, Rectangle>();
            for (int i = 0; i < 10; i++)
            {
                numbers.Add(i.ToString()[0], new Rectangle(digitWidth * i, 0, digitWidth, numberSprite.Height));
            }
        }

        public void Draw(SpriteBatch sb, Vector2 position, string num)
        {
            var scale = 2f;
            foreach (var n in num)
            {
                sb.Draw(numberSprite, position, numbers[n], Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0f);
                position.X += (digitWidth * scale);
            }
        }
    }
}
