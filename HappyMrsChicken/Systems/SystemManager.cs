using HappyMrsChicken.Components;
using Microsoft.Xna.Framework;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyMrsChicken.Systems
{
    public class SystemManager
    {
        #region vars
        Dictionary<Type, ISystem> systems = new Dictionary<Type, ISystem>();
        List<IRenderable> renderableSystems = new List<IRenderable>();
        List<IUpdatable> updatableSystems = new List<IUpdatable>();
        private static SystemManager instance = new SystemManager();
        #endregion


        #region methods
        public void Add<T>(ISystem system) where T : class
        {
            Debug.Assert(systems.ContainsKey(typeof(T)) == false, "There can be only one instance of a system in the SystemManager");
            systems.Add(typeof(T), system);

            if (system is IRenderable ir) renderableSystems.Add(ir);
            if (system is IUpdatable iu) updatableSystems.Add(iu);
        }

        public T Get<T>() where T : class
        {
            return systems[typeof(T)] as T;
        }
        #endregion

        #region properties
        public List<IRenderable> Renderable => renderableSystems;
        public List<IUpdatable> Updatable => updatableSystems;
        public static SystemManager Instance => instance;

        #endregion
    }
}
