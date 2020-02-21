using HappyMrsChicken.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  HappyMrsChicken.Entities
{
    public class EntityBuilder
    {
        private ContentManager cm;
        public EntityBuilder(Game game)
        {
            cm = game.Content;
        }
        public void BuildSkeleton()
        {
            Entity e = new Entity();
            EntityManager.Instance.AddEntity(e);
            Position p = new Position(e.Id, new Vector2(100, 100));
            Sprite s = new Sprite(e.Id, "skeleton", cm, Point.Zero);
            EntityManager.Instance.AddComponent<Position>(e.Id, p);
            EntityManager.Instance.AddComponent<Sprite>(e.Id, s);
        }

        public void BuildAnimatedSkeleton()
        {
            Entity e = new Entity();
            EntityManager.Instance.AddEntity(e);
            Position p = new Position(e.Id, new Vector2(300, 300));
            AnimatedSprite s = new AnimatedSprite(e.Id, "skeleton_walk", cm, 1, 10, 10);
            EntityManager.Instance.AddComponent<Position>(e.Id, p);
            EntityManager.Instance.AddComponent<AnimatedSprite>(e.Id, s);
        }
    }
}
