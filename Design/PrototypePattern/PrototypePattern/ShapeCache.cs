using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypePattern
{
    public  class ShapeCache
    {
        private static Dictionary<string, Shape> shaps = new Dictionary<string, Shape>(); 

        /// <summary>
        /// Indexer
        /// </summary>
        public Shape this[string key]
        {
            get { return shaps[key]; }
            set { 
                if(!shaps.Any(u=>u.Key==key))
                    shaps.Add(key, value);
            }
        }       
    }
}
