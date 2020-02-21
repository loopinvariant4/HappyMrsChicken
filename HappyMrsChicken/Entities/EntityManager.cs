using HappyMrsChicken.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyMrsChicken.Entities
{
    public class EntityManager
    {
        Dictionary<int, Entity> entities = new Dictionary<int, Entity>();
        Dictionary<Type, Dictionary<int, ComponentBase>> compStores = new Dictionary<Type, Dictionary<int, ComponentBase>>();
        private static EntityManager instance = new EntityManager();
        public static EntityManager Instance
        {
            get
            {
                return instance;
            }
        }
        private EntityManager()
        {

        }

        public void AddEntity(Entity e)
        {
            if (entities.ContainsKey(e.Id) == false)
            {
                entities.Add(e.Id, e);
            }
        }

        public Entity GetEntity(int id)
        {
            return entities[id];
        }

        public void RemoveEntity(int id)
        {
            if (entities.ContainsKey(id))
            {
                entities.Remove(id);
            }
            foreach(var compDict in compStores.Values)
            {
                if (compDict.ContainsKey(id))
                {
                    compDict.Remove(id);
                }
            }
        }

        private void AddComponentStore(Type t)
        {
            if (compStores.ContainsKey(t) == false)
            {
                compStores.Add(t, new Dictionary<int, ComponentBase>());
            }
        }

        public T GetComponent<T>(int id) where T : class
        {
            var store = compStores[typeof(T)];
            return store[id] as T;
        }

        public void AddComponent<T>(int id, ComponentBase component)
        {
            if (compStores.ContainsKey(typeof(T)) == false)
            {
                AddComponentStore(typeof(T));
            }
            compStores[typeof(T)].Add(id, component);
        }

        public void RemoveComponent<T>(int id)
        {
            var store = compStores[typeof(T)];
            store.Remove(id);
        }

        public IEnumerable<ComponentBase> GetComponentsByType<T>() where T : ComponentBase
        {
            Dictionary<int, ComponentBase> store;
            if (compStores.TryGetValue(typeof(T), out store))
            {
                return store.Values as IEnumerable<ComponentBase>;
            }
            return null;
        }
    }
}
