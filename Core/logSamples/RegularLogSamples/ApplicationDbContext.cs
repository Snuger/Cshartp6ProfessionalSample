using Microsoft.EntityFrameworkCore;
using RegularLogSamples.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Runtime.InteropServices.ComTypes;

namespace RegularLogSamples
{
    public  class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContext):base(dbContext)
        {

        }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Post> Posts { get; set; }
     
    }
}
