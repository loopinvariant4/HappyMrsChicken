using HappyMrsChicken.Components;
using HappyMrsChicken.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyMrsChicken.Systems
{
    /// <summary>
    /// Maintains a list of objects and checks if they collide. The first object is called the source and the second object is called the target.
    /// We maintain a dictionary with a single source and multiple target objects. When we do a collision check we compare each source with all the targets to make a determination of a collision
    /// However, every target can also act like a source object
    /// </summary>
    public class Collider : ISystem
    {
        /// <summary>
        /// They key is the source entitiy's id. The value is a tuple with the target entitiy's id and the source and target entities' position object for comparison
        /// </summary>
        Dictionary<int, List<Tuple<Entity, Position, Position>>> collisionList = new Dictionary<int, List<Tuple<Entity, Position, Position>>>();
        public void Init(Game game)
        {
        }

        public void Register(int sourceId, Entity target)
        {
            var aPos = EntityManager.Instance.GetComponent<Position>(sourceId);
            var bPos = EntityManager.Instance.GetComponent<Position>(target.Id);

            List<Tuple<Entity, Position, Position>> list;

            if (collisionList.ContainsKey(sourceId))
            {
                list = collisionList[sourceId];
            }
            else
            {
                list = new List<Tuple<Entity, Position, Position>>();
                collisionList.Add(sourceId, list);
            }

            list.Add(new Tuple<Entity, Position, Position>(target, aPos, bPos));
        }

        public void UnregisterSource(Entity source)
        {
            Debug.Assert(collisionList.ContainsKey(source.Id), "You cannot unregister a source if it is not present or already removed");

            collisionList.Remove(source.Id);
        }
        public void UnregisterTarget(int sourceId, Entity target)
        {
            Debug.Assert(collisionList.ContainsKey(sourceId), "You cannot unregister a source if it is not present or already removed");
            Debug.Assert(containsTarget(collisionList[sourceId], target), "You cannot unregister a source if it is not present or already removed");

            var list = collisionList[sourceId];
            for(int i = 0; i < list.Count; i++)
            {
                if(list[i].Item1.Id == target.Id)
                {
                    list.RemoveAt(i);
                    break;
                }
            }
        }

        private bool containsTarget(List<Tuple<Entity, Position, Position>> list, Entity target)
        {
            foreach(var t in list)
            {
                if(t.Item1.Id == target.Id)
                {
                    return true;
                }
            }
            return false;
        }

        public List<Entity> GetCollisions(int sourceId)
        {
            Debug.Assert(collisionList.ContainsKey(sourceId), "You cannot check collision for a source if it is not present or already removed");
            var list = collisionList[sourceId];
            return list.Where(tuple => tuple.Item2.Rectangle.IntersectsWith(tuple.Item3.Rectangle)).Select(tuple => tuple.Item1).ToList();
        }
    }
}
