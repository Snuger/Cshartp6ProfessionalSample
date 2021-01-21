using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderDesigin
{
    public class DbContext
    {

        private readonly DbContextOptions _options;


        public DbContext(DbContextOptions options)
        {
            _options = options;
        }

        public DbContext()
        {
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="contextOptionsBuilder"></param>
        public virtual void OnConfiguring(DbContextOptionBuilder contextOptionsBuilder) {

         
        }
    }
}
