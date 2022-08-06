using Microsoft.EntityFrameworkCore;

namespace Concurrency.Web.Models
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent API
            modelBuilder.Entity<Product>().Property(x => x.RowVersion).IsRowVersion(); //2.yol, best practise.
            modelBuilder.Entity<Product>().Property(x => x.Price).HasPrecision(18, 2);

            modelBuilder.Entity<User>().Property(x => x.RowVersion).IsRowVersion();

            base.OnModelCreating(modelBuilder);
        }
    }
}
