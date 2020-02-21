using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyMrsChicken.Components
{
    public class ChickenAnimation : ComponentBase, IRenderable, IUpdatable
    {
        public ChickenState CurrentState { get; private set; }
        public AnimatedSprite CurrentSprite { get; set; }
        public SoundEffectInstance CurrentSound { get; set; }
        public Vector2 Size => CurrentSprite.Size;

        Dictionary<ChickenState, AnimatedSprite> sprites;
        Dictionary<ChickenState, SoundEffectInstance> sounds;
        
        public ChickenAnimation(int entityId, Dictionary<ChickenState, AnimatedSprite>  sprites, Dictionary<ChickenState, SoundEffectInstance> sounds) : base(entityId)
        {
            this.sprites = sprites;
            this.sounds = sounds;
            SetAnimation(ChickenState.Idle);
        }

        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            CurrentSprite.Draw(gameTime, sb);
            if(CurrentSound != null && CurrentSound.State != SoundState.Playing)
            {
                CurrentSound.Play();
            }
        }

        public void Update(GameTime gameTime)
        {
            CurrentSprite.Update(gameTime);
        }

        public void SetAnimation(ChickenState state)
        {
            CurrentState = state;
            if (sprites.ContainsKey(state))
            {
                CurrentSprite = sprites[state];
            }
            if (sounds.ContainsKey(state))
            {
                CurrentSound = sounds[state];
            } 
            else
            {
                if (CurrentSound != null)
                {
                    CurrentSound.Stop(true);
                    CurrentSound = null;
                }
            }
        }

        public void Play()
        {
            CurrentSprite.Play();
        }

        public void Pause()
        {
            CurrentSprite.Pause();
        }

    }
}
