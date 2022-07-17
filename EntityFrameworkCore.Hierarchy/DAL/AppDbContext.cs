using EntityFrameworkCore.Hierarchy.DAL;
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
        //public DbSet<Category> Categories { get; set; }
        //public DbSet<Product> Products { get; set; }
        //public DbSet<ProductFeature> productFeatures { get; set; }

        //public DbSet<Student> Students { get; set; }
        //public DbSet<Teacher> Teachers { get; set; }
        //

        //  default hiyerarşi 
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Manager> Managers { get; set; }
        //  /default hiyerarşi
        public DbSet<BasePerson> Persons { get; set; } //Eğer EF Core'un default hiyerarşi yapısını istemiyorsak miras alınan sınıfı da burada tanıtırız.

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
            //TPT (table per type)
            //modelBuilder.Entity<BasePerson>().ToTable("Persons");
            //modelBuilder.Entity<Employee>().ToTable("Employees");
            //modelBuilder.Entity<Manager>().ToTable("Managers");
            //modelBuilder.Entity<Product>().Property(x => x.Price).HasPrecision(9, 2);
            base.OnModelCreating(modelBuilder);
        }

    }
   
}
