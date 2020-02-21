using HappyMrsChicken.Components;
using HappyMrsChicken.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyMrsChicken.Systems
{
    public class AnimatedSpriteRenderer : ISystem, IRenderable, IUpdatable
    {
        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            var sprites = EntityManager.Instance.GetComponentsByType<AnimatedSprite>();
            if (sprites != null)
            {
                foreach (AnimatedSprite sprite in sprites)
                {
                    sprite.Draw(gameTime, sb);
                }
            }
        }

        public void Init(Game game)
        {
        }

        public void Update(GameTime gameTime)
        {
            var sprites = EntityManager.Instance.GetComponentsByType<AnimatedSprite>();
            if (sprites != null)
            {
                foreach (AnimatedSprite sprite in sprites)
                {
                    sprite.Update(gameTime);
                }
            }
        }
    }
}
