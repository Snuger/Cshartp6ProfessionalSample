using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderDesigin
{
     class DefaultDbContext:DbContext
    {
        public DefaultDbContext(DbContextOptions options) : base(options)
        {
        }

        public override void OnConfiguring(DbContextOptionBuilder contextOptionsBuilder)
        {           
            base.OnConfiguring(contextOptionsBuilder.UserMemoryCache());
        }
    }
}
