using EntityFrameworkCore.Query.DAL;
using EntityFrameworkCore.Query.Models;
using EntityFrameworkCore.Relationships.DAL;
using EntityFrameworkCore.StoredProcedureAndFunction.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data.Common;

namespace EntityFrameworkCore.CodeFirst.DAL
{
    public class AppDbContext:DbContext
    {
        private DbConnection _connection;

        public AppDbContext(DbConnection connection) //birden fazla instance var ise bu şekilde geçmemiz gerekiyor.
        {
            _connection = connection;
        }
        public AppDbContext()
        {

        }

        public DbSet<Product> Products { get; set; }
       public DbSet<ProductFeature> productFeatures { get; set; }
       public DbSet<Category>  Categories { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_connection == default(DbConnection)) //tanımlanmamış ise
            {
                 Initializer.Build();
                 optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information).UseSqlServer(Initializer.Configuration.GetConnectionString("SqlCon"));

            }
            else
            {
                 optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information).UseSqlServer(_connection);

            }
            //Initializer.Build();
            // optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information).UseSqlServer(Initializer.Configuration.GetConnectionString("SqlCon"));


        }
        public override int SaveChanges()
        {

            return base.SaveChanges();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            base.OnModelCreating(modelBuilder);
        }

    }
   
}
