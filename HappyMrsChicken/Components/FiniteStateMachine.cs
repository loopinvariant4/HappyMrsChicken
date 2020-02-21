using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyMrsChicken.Components
{
    public class FiniteStateMachine : ComponentBase
    {
        private Action activeState;

        public FiniteStateMachine(int entityId, Action activeState) : base(entityId)
        {
            this.activeState = activeState;
        }

        public void SetState(Action act)
        {
            activeState = act;
        }

        public void Update()
        {
            if(activeState != null)
            {
                Console.WriteLine(activeState.Method.Name.ToString());
                activeState();
            }
        }

    }
}
