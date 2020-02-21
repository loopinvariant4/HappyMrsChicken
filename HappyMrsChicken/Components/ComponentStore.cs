using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyMrsChicken.Components
{
    public abstract class ComponentStore
    {
    }
    public class ComponentStore<T> : ComponentStore
    {
        Dictionary<int, T> componentStore = new Dictionary<int, T>();

        public T GetComponent(int id)
        {
            return componentStore[id];
        }

        public bool Contains(int id)
        {
            return componentStore.ContainsKey(id);
        }

        public void Add(int id, T component)
        {
            if (!Contains(id))
            {
                componentStore.Add(id, component);
            }
        }
    }
}
