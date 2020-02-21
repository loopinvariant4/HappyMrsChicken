using HappyMrsChicken.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyMrsChicken.Systems.Input
{
    public class KeyboardExtended : ISystem, IUpdatable
    {
        private KeyboardStateExtended state;
        public KeyboardExtended()
        {
            KeyboardState kbs = new KeyboardState();
            state = new KeyboardStateExtended(kbs, kbs);
        }
        public void Init(Game game)
        {
        }

        public void Update(GameTime gameTime)
        {
            state.PreviousKeyboardState = state.CurrentKeyboardState;
            state.CurrentKeyboardState = Keyboard.GetState();
        }

        public KeyboardStateExtended GetState()
        {
            return state;
        }
    }
}
