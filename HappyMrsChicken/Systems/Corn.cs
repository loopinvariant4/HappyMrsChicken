using HappyMrsChicken.Components;
using HappyMrsChicken.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyMrsChicken.Systems
{
    /// <summary>
    /// Manages all events related to corn such as spawning a new one, tracking scrore and placement
    /// </summary>
    public class Corn : ISystem
    {
        ContentManager cm;
        Game game;
        Random r = new Random(2);
        public Entity Kernel { get; private set; }

        public Corn()
        {

        }

        public void Init(Game game)
        {
            this.game = game;
            cm = game.Content;
            createNewKernel();
        }

        public void OnCollide(int entityId)
        {
            var score = SystemManager.Instance.Get<Score>();
            score.Increment();
            EntityManager.Instance.RemoveEntity(Kernel.Id);
            createNewKernel();
            var collider = SystemManager.Instance.Get<Collider>();
            collider.Register(entityId, Kernel);
        }

        private void createNewKernel()
        {
            Kernel = new Entity();
            EntityManager.Instance.AddEntity(Kernel);

            AnimatedSprite sprite = new AnimatedSprite(Kernel.Id, "swaying_corn", cm, 1, 21, 11);
            Position p = new Position(Kernel.Id, getPosition(sprite.Size), sprite.Size);

            EntityManager.Instance.AddComponent<AnimatedSprite>(Kernel.Id, sprite);
            EntityManager.Instance.AddComponent<Position>(Kernel.Id, p);
            sprite.Play();
        }

        private Vector2 getPosition(Vector2 size)
        {
            var tm = SystemManager.Instance.Get<TileManager>();
            while (true)
            {
                int x = r.Next(0, game.GraphicsDevice.Viewport.Width);
                int y = r.Next(0, game.GraphicsDevice.Viewport.Height);
                var endX = x + size.X;
                var endY = y + size.Y;
                var tiles = tm.GetTilesUnderArea(x, y, endX, endY);
                var isBlocked = false;
                // check if the corn is not on a impassable tile
                foreach (var tile in tiles)
                {
                    if (!tile.IsPassable)
                    {
                        isBlocked = true;
                        break;
                    }
                }
                if(isBlocked)
                {
                    continue;
                }
                // check if the corn is not on another physical object
                var items = tm.GetTerrainObjectsUnderArea(x, y, size);
                if(items.Count == 0)
                {

                    return new Vector2(x, y);
                }
            }
        }
    }
}
