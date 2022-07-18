using EntityFrameworkCore.Hierarchy.DAL;
using EntityFrameworkCore.Model.DAL;
using EntityFrameworkCore.Relationships.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.CodeFirst.DAL
{
    public class AppDbContext:DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> productFeatures { get; set; }
        public DbSet<ProductFull> ProductFulls { get; set; }
        public DbSet<PersonKeyless> PersonKeylesses { get; set; }


        //public DbSet<Student> Students { get; set; }
        //public DbSet<Teacher> Teachers { get; set; }
        //

       
      //  public DbSet<Employee> Employees { get; set; }
      //  public DbSet<Manager> Managers { get; set; }
     
        //public DbSet<BasePerson> Persons { get; set; } //Owned olarak kullanacağımız için buradan kaldırıyoruz.
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
            ////Owned entity type belirleme
            //modelBuilder.Entity<Employee>().OwnsOne(x => x.Person, p =>
            //{//Eğer veri tabanında oluşacak olan ortak alanların isimlerini kendimiz belirlemek istersek p lambda ile içerisine girip property isimlerini değiştirmemiz gerekmektedir. Yukarıda "p" ile belirtilen kısım bizim Person nesnemize denk gelmektedir.
            //    p.Property(x => x.FirstName).HasColumnName("Adı");
            //    p.Property(x => x.LastName).HasColumnName("Soyadı");
            //    p.Property(x => x.Age).HasColumnName("Yaşı");
            //});
            //modelBuilder.Entity<Manager>().OwnsOne(x => x.Person, p =>
            //{
            //    p.Property(x => x.FirstName).HasColumnName("Adı");
            //    p.Property(x => x.LastName).HasColumnName("Soyadı");
            //    p.Property(x => x.Age).HasColumnName("Yaşı");
            //});
            ////end
            ///-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

            //Keyless entity type start
            modelBuilder.Entity<ProductFull>().HasNoKey();
            //End
            base.OnModelCreating(modelBuilder);
        }

    }
   
}
