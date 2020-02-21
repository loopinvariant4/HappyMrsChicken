using HappyMrsChicken.Entities;
using HappyMrsChicken.Systems.Input;
using MA.Systems.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyMrsChicken.Systems
{
    public class GameInputHandler : InputHandlerBase
    {
        Dictionary<Keys, InputAction> keyDownMapper;
        List<IInputReceiver> receivers = new List<IInputReceiver>();
        public GameInputHandler(KeyboardExtended keyboard) : base(keyboard)
        {
            mapKeyActions();
        }

        private void mapKeyActions()
        {
            keyDownMapper = new Dictionary<Keys, InputAction>();
            keyDownMapper.Add(Keys.Up, InputAction.MoveUp);
            keyDownMapper.Add(Keys.Down, InputAction.MoveDown);
            keyDownMapper.Add(Keys.Left, InputAction.MoveLeft);
            keyDownMapper.Add(Keys.Right, InputAction.MoveRight);
        }

        public override void HandleInput()
        {
            var state = keyboard.GetState();
            var keys = state.GetDownKeys();
            if (keys.Length > 0)
            {
                InputAction act;
                if (keyDownMapper.TryGetValue(keys[0], out act))
                {
                    foreach (var recv in receivers)
                    {
                        recv.Receive(act);
                    }
                }
            }
            else
            {
                foreach (var recv in receivers)
                {
                    recv.Receive(InputAction.Idle);
                }
            }
        }

        public void Register(IInputReceiver receiver)
        {
            Debug.Assert(receivers.Contains(receiver) == false, "You cannot register the same receiver multiple times :" + receiver.ToString());
            receivers.Add(receiver);
        }
    }
}
