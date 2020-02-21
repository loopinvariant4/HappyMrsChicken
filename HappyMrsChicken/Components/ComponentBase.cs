using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyMrsChicken.Components
{
    public abstract class ComponentBase
    {
        public int EntityId { get; set; }

        public ComponentBase(int entityId)
        {
            EntityId = entityId;
        }
    }
}
