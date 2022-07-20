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
       // public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
     //   public DbSet<ProductFeature> productFeatures { get; set; }
        //public DbSet<ProductFull> ProductFulls { get; set; }
        //public DbSet<PersonKeyless> PersonKeylesses { get; set; }


        //public DbSet<Student> Students { get; set; }
        //public DbSet<Teacher> Teachers { get; set; }
        //

       
      //  public DbSet<Employee> Employees { get; set; }
      //  public DbSet<Manager> Managers { get; set; }
     
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
            //modelBuilder.Entity<Product>().HasIndex(x => x.Name);
            modelBuilder.Entity<Product>().HasIndex(x => new { x.Name, x.URL });
            modelBuilder.Entity<Product>().HasIndex(x => x.Name).IncludeProperties(x => new { x.Price, x.URL }); //included properties kullanırsak eğer biz Name'e bağlı bir sorgu yapıp çıkan sonuçların name'i ile beraber hem price'ı hem de url'i index tablosundan çok hızlı bir şekilde gelsin istersek bu kodu yazarız.
                        //>sorgusu: context.products.where(x => x.name =="kalem1").select(x => new {name = x.name, price = x.price, url = x.url})
            base.OnModelCreating(modelBuilder);
        }

    }
   
}
