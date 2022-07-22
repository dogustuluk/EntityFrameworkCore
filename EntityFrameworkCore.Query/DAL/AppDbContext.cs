using EntityFrameworkCore.Query.DAL;
using EntityFrameworkCore.Query.Models;
using EntityFrameworkCore.Relationships.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EntityFrameworkCore.CodeFirst.DAL
{
    public class AppDbContext:DbContext
    {
        private readonly int Barcode;

        public AppDbContext(int barcode)
        {
            Barcode = barcode;
        }

        public AppDbContext()
        {
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> productFeatures { get; set; }

        public DbSet<ProductEseential> productEseentials { get; set; }
        public DbSet<ProductWithFeature> productWithFeatures { get; set; }

        public DbSet<ProductFull> productFulls { get; set; }

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
            modelBuilder.Entity<ProductEseential>().HasNoKey();
            modelBuilder.Entity<ProductWithFeature>().HasNoKey();
            modelBuilder.Entity<ProductEseential>().ToSqlQuery("select Name, Price from Products"); //ToSqlQuery > hazır sql cümleciği oluşturuldu.
            modelBuilder.Entity<ProductFull>().HasNoKey().ToView("productwithfeature");
            modelBuilder.Entity<Product>().Property(x => x.IsDeleted).HasDefaultValue(false);//Global Query Filters Start
            if (Barcode != default(int))
            {
                modelBuilder.Entity<Product>().HasQueryFilter(x => !x.IsDeleted && x.Barcode == Barcode);//Global Query Filters End

            }
            else
                modelBuilder.Entity<Product>().HasQueryFilter(x => !x.IsDeleted);

            //Bir nesne örneği üretildiğinde integer değerlerin default değeri vardır.(sadece int?). Int default değeri "0"
            base.OnModelCreating(modelBuilder);
        }

    }
   
}
