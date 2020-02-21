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
    public class AnimationRenderer : ISystem, IRenderable, IUpdatable
    {
        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            var anims = EntityManager.Instance.GetComponentsByType<ChickenAnimation>();
            if (anims != null)
            {
                foreach (ChickenAnimation anim in anims)
                {
                    anim.Draw(gameTime, sb);
                }
            }
        }

        public void Init(Game game)
        {
        }

        public void Update(GameTime gameTime)
        {
            var anims = EntityManager.Instance.GetComponentsByType<ChickenAnimation>();
            if (anims != null)
            {
                foreach (ChickenAnimation anim in anims)
                {
                    anim.Update(gameTime);
                }
            }
        }
    }
}
