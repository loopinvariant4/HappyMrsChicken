using HappyMrsChicken.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  HappyMrsChicken.Entities
{
    public class Entity
    {
        public int Id { get; private set; }

        public Entity()
        {
            Id = IdFountain.Next;
        }
    }
}
