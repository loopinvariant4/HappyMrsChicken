using HappyMrsChicken.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyMrsChicken.Systems.Input
{
    public interface IInputReceiver
    {
        void Receive(InputAction action);
    }
}
