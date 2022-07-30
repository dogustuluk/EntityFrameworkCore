using EntityFrameworkCore.Query.DAL;
using EntityFrameworkCore.Query.Models;
using EntityFrameworkCore.Relationships.DAL;
using EntityFrameworkCore.StoredProcedureAndFunction.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EntityFrameworkCore.CodeFirst.DAL
{
    public class AppDbContext:DbContext
    {
       
        //public DbSet<Person> People { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> productFeatures { get; set; }
        public DbSet<ProductFull> productFulls { get; set; }

        public DbSet<ProductFull2> productFull2s { get; set; }
        public DbSet<ProductFullForFunction> productFullForFunctions { get; set; }

        //public DbSet<ProductEseential> productEseentials { get; set; }
        //public DbSet<ProductWithFeature> productWithFeatures { get; set; }

        //public DbSet<ProductFull> productFulls { get; set; }

        //public DbSet<Student> Students { get; set; }
        //public DbSet<Teacher> Teachers { get; set; }
        //

        //public DbSet<BasePerson> Persons { get; set; } 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            Initializer.Build();
             optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information).UseSqlServer(Initializer.Configuration.GetConnectionString("SqlCon"));
          

        }
        public override int SaveChanges()
        {

            return base.SaveChanges();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductFull>().HasNoKey();
            modelBuilder.Entity<ProductFull2>().HasNoKey();
            modelBuilder.Entity<ProductFullForFunction>().ToFunction("fc_product_full");
            base.OnModelCreating(modelBuilder);
        }

    }
   
}
