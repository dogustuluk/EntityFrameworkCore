using EntityFrameworkCore.Query.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EntityFrameworkCore.CodeFirst.DAL
{
    public class AppDbContext:DbContext
    {
        public DbSet<Person> People { get; set; }
        // public DbSet<Category> Categories { get; set; }
        //public DbSet<Product> Products { get; set; }
        //   public DbSet<ProductFeature> productFeatures { get; set; }


        //public DbSet<Student> Students { get; set; }
        //public DbSet<Teacher> Teachers { get; set; }
        //

        //public DbSet<BasePerson> Persons { get; set; } 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            Initializer.Build(); 
            optionsBuilder.UseSqlServer(Initializer.Configuration.GetConnectionString("SqlCon"));
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
