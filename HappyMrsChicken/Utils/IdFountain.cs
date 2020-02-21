using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyMrsChicken.Utils
{
    public class IdFountain
    {
        static int id = 0;
        private static object _lock = new object();
        static IdFountain()
        {
            // init id from a file
          id = 0;
        }

        public static int Next
        {
            get
            {
                lock(_lock)
                {
                    id += 1;
                    return id;
                }
            }
        }
    }
}
