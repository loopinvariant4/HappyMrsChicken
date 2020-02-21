using HappyMrsChicken.Components;
using HappyMrsChicken.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyMrsChicken.Systems
{
    public class EntityFSMUpdater : ISystem, IUpdatable
    {
        public void Init(Game game)
        {
        }

        public void Update(GameTime gameTime)
        {
            var fsms = EntityManager.Instance.GetComponentsByType<FiniteStateMachine>();
            foreach(FiniteStateMachine fsm in fsms)
            {
                fsm.Update();
            }
        }
    }
}
