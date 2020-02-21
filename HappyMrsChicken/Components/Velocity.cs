using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyMrsChicken.Components
{
    /// <summary>
    /// Represent number of pixels to move per update and the direction of the movement(in radians)
    /// </summary>
    public class Velocity : ComponentBase
    {
        public int TopSpeed { get; set; }
        public int CurrentSpeed { get; set; }
        public float Direction { get; set; }
        public Velocity(int entityId, int topSpeed, float direction) : base(entityId)
        {
            TopSpeed = topSpeed;
            Direction = direction;
        }
    }
}
