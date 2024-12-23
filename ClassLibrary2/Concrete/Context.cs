using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Concrete
{
    public class Context:DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Eğer harici bir yapılandırma kullanılmazsa bu yedek ayarı uygular.
                optionsBuilder.UseSqlServer("server=DESKTOP-I79O76V;database=Category;integrated security=true;TrustServerCertificate=True");
            }
        }
        public DbSet<Category> Categories { get; set; }


    }
}
