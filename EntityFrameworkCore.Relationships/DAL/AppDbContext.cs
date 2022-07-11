using EntityFrameworkCore.Relationships.DAL;
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
        public DbSet<Category> Categories { get; set; } //Eğer veri tabanında AppDbContext aracılığıyla herhangi bir işlem yapılmasını istemiyorsak bu satırı silebiliriz.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Initializer.Build(); //Console uygulamasında bu kod alttaki koddan önce mutlaka yazılması gerekmektedir. Sebebi ise alttaki metottan önce bu metot çalışsın ki migration sırasında connectionString'i okuyabilsin.
            optionsBuilder.UseSqlServer(Initializer.Configuration.GetConnectionString("SqlCon"));
        }
        public override int SaveChanges()
        {
          
            return base.SaveChanges();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Hluent api relationship one to many start
            //Her zaman "has" ifadesiyle başlanacaktır.
            //hasMany(xx.Product) >>> Category'e ait birden fazla product olacağını ifade eder.
            //WithOne(xx.Category) >>> Product'ın sadece bir tane Category'si olduğunu ifade eder.
            //HasForeignKey(xx.ForeignKeyName) >>> ikincil anahtarın belirtildiği ifadeyi açıklar.
            
            //modelBuilder.Entity<Category>().HasMany(x => x.Products).WithOne(x => x.Category).HasForeignKey(x => x.Category_Id); 
            
            //end
            base.OnModelCreating(modelBuilder);
        }

    }
   
}
