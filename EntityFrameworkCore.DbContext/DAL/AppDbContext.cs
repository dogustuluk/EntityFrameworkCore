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
        public override int SaveChanges()
        {
            ChangeTracker.Entries().ToList().ForEach(e =>
            {
                if (e.Entity is Product p) //Is keyword'ü >>>>> Herhangi bir nesnenin diğer bir nesneye dönüştürülüp dönüştürülemeyeceğini true-false olarak döner. True ise dönüştürme işlemi başarılı olur ve ilgili nesneye atamasını da yapar.
                {
                    if (e.State == EntityState.Added)
                    {
                        p.CreatedDate = DateTime.Now;
                    }
                    //Console.WriteLine($"{p.Id} - {p.Name} - {p.Price} - {p.Stock} - {p.Barcode}");
                }
                //Bu kod her daim save changes öncesinde çalışacağı için >>>> save changes metodunu override edip içersine yazmak best practise olarak en iyisidir.
            });
            return base.SaveChanges();
        }

    }
   
}
