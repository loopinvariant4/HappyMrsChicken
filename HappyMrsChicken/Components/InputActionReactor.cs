using HappyMrsChicken.Entities;
using HappyMrsChicken.Systems;
using HappyMrsChicken.Systems.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyMrsChicken.Components
{
    public class InputActionReactor : ComponentBase, IInputReceiver
    {
        public InputActionReactor(int entityId, GameInputHandler inputHandler) : base(entityId)
        {
            inputHandler.Register(this);
        }

        public void Receive(InputAction action)
        {
            var componentState = EntityManager.Instance.GetComponent<ChickenStateAction>(EntityId);

            switch (action)
            {
                case InputAction.MoveUp:
                    {
                        componentState.SetState(ChickenState.MoveUp);
                        break;
                    }
                case InputAction.MoveDown:
                    {
                        componentState.SetState(ChickenState.MoveDown);
                        break;
                    }
                case InputAction.MoveLeft:
                    {
                        componentState.SetState(ChickenState.MoveLeft);
                        break;
                    }
                case InputAction.MoveRight:
                    {
                        componentState.SetState(ChickenState.MoveRight);
                        break;
                    }
                default:
                    {
                        componentState.SetState(ChickenState.Idle);
                        break;
                    }
            }
        }
    }
}
