using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.CodeFirst.DAL
{
    public class AppDbContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Initializer.Build(); //Console uygulamasında bu kod alttaki koddan önce mutlaka yazılması gerekmektedir. Sebebi ise alttaki metottan önce bu metot çalışsın ki migration sırasında connectionString'i okuyabilsin.
            optionsBuilder.UseSqlServer(Initializer.Configuration.GetConnectionString("SqlCon"));
        }

    }
   
}
