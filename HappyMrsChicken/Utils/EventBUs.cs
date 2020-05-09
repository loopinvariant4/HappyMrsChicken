using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyMrsChicken.Utils
{
    public static class EventBus
    {
        public static event EventHandler CornSpawned;
        public static void FireCornSpawned(object sender) => CornSpawned?.Invoke(sender, null);
    }
}
