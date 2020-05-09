using HappyMrsChicken.Components;
using HappyMrsChicken.Systems;
using HappyMrsChicken.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyMrsChicken.Entities
{
    public class Gropochek : IUpdatable, IRenderable
    {
        public Entity Instance { get; set; }
        ContentManager cm;
        AnimatedSprite sprite;
        Position position;
        Viewport viewport;
        bool hasSpawned = false;
        Dictionary<GropochekState, SoundEffectInstance> sounds;
        MoveDirection direction = MoveDirection.Horizontal;
        Random r = new Random(13);
        Random rWait = new Random(5);

        int maxWaitTurns = 3;
        bool isWaiting = true;

        public Gropochek(ContentManager contentManager, Viewport viewport, Dictionary<GropochekState, SoundEffectInstance> sounds)
        {
            cm = contentManager;
            this.viewport = viewport;
            this.sounds = sounds;
            createNewInstance();
            //sprite.IsHidden = true;
            EventBus.CornSpawned += EventBus_CornSpawned;
        }

        private void EventBus_CornSpawned(object sender, EventArgs e)
        {
            var shouldSpawn = rWait.Next(0, 4) == 0 ? true : false;
            if (shouldSpawn == false)
            {
                maxWaitTurns -= 1;
                if (maxWaitTurns == 0)
                {
                    position.XY = getNewStartPosition(sprite.Size);
                    maxWaitTurns = 3;
                    isWaiting = false;

                }
            }
            else
            {
                position.XY = getNewStartPosition(sprite.Size);
                isWaiting = false;
                maxWaitTurns = 3;
            }

        }

        private void createNewInstance()
        {
            Instance = new Entity();
            EntityManager.Instance.AddEntity(Instance);

            sprite = new AnimatedSprite(Instance.Id, "gropochek", cm, 1, 3, 2);
            sprite.ShouldDrawShadow = true;
            position = new Position(Instance.Id, getNewStartPosition(sprite.Size), sprite.Size);

            EntityManager.Instance.AddComponent<AnimatedSprite>(Instance.Id, sprite);
            EntityManager.Instance.AddComponent<Position>(Instance.Id, position);
            sprite.Play();
        }

        private Vector2 getNewStartPosition(Vector2 size)
        {
            direction = r.Next(0, 2) == 0 ? MoveDirection.Horizontal : MoveDirection.Vertical;
            var kernel = SystemManager.Instance.Get<Corn>().Kernel;
            var position = EntityManager.Instance.GetComponent<Position>(kernel.Id);
            if (direction == MoveDirection.Horizontal)
            {
                return new Vector2(-300, position.Y - sprite.Size.Y / 2);
            }
            else
            {
                return new Vector2(position.X - sprite.Size.X / 2, -300);
            }
        }

        public void Update(GameTime gameTime)
        {
            updateGropochek();
        }

        private void updateGropochek()
        {
            if (isWaiting)
            {
                return;
            }

            int moveSpeed = 2; 
            if (direction == MoveDirection.Horizontal)
            {
                if (position.X > viewport.Width)
                {
                    isWaiting = true;
                    return;
                }
                position.X += moveSpeed;
            }
            else
            {
                if (position.Y > viewport.Height)
                {
                    isWaiting = true;
                    return;
                }
                position.Y += moveSpeed;
            }

            checkCollision();

        }

        private void checkCollision()
        {
            var collider = SystemManager.Instance.Get<Collider>();
            var list = collider.GetCollisions(Instance.Id);
            foreach (var item in list)
            {
                //collider.UnregisterTarget(Instance.Id, item);
                var corn = SystemManager.Instance.Get<Corn>();
                corn.OnCollide(Instance.Id);
                playSound(GropochekState.Eating);
            }
        }

        private void playSound(GropochekState eating)
        {
            sounds[eating].Play();
        }

        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            sprite.Draw(gameTime, sb);
        }
    }

    public enum MoveDirection
    {
        Vertical,
        Horizontal
    }
}
