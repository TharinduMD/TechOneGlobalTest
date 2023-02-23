using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Core;

namespace Weather.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options) 
        {

        }
        public DbSet<City> Cities { get; set; }
    }
}
