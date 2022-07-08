using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.DatabaseFirst.DAL
{
    public class AppDbContext: DbContext
    {
        //Parametre alan bir constructor tanımlarsak, default constructor'ını da yazmalıyız.
        public DbSet<Product> Products { get; set; }

        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
