using HappyMrsChicken;
using HappyMrsChicken.Components;
using HappyMrsChicken.Systems;
using HappyMrsChicken.Systems.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MA.Systems.Input
{
    /// <summary>
    /// This interface must be implemented by all classes that want to receive input events from keyboard, mouse, etc
    /// </summary>
    public abstract class InputHandlerBase : IUpdatable, ISystem
    {
        protected KeyboardExtended keyboard;
        public InputHandlerBase(KeyboardExtended keyboard)
        {
            this.keyboard = keyboard;
        }

        public void Update(GameTime gameTime)
        {
            HandleInput(); 
        }

        public abstract void HandleInput();

        public void Init(Game game)
        {
        }
    }
}
