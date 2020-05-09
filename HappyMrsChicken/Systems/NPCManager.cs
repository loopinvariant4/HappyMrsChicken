using HappyMrsChicken.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyMrsChicken.Systems
{
    public class NPCManager : ISystem, IUpdatable
    {
        List<IUpdatable> npcs = new List<IUpdatable>();
        List<int> npcEntity = new List<int>();
        public List<int> NPCs => npcEntity; 
        public void Init(Game game)
        {

        }

        public void RegisterNPC(int id, IUpdatable npc)
        {
            npcs.Add(npc);
            npcEntity.Add(id);
        }

        public void Update(GameTime gameTime)
        {
            foreach(var npc in npcs)
            {
                npc.Update(gameTime);
            }
        }
    }
}
