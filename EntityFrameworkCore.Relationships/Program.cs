using EntityFrameworkCore.CodeFirst;
using EntityFrameworkCore.CodeFirst.DAL;
using Microsoft.EntityFrameworkCore;

Initializer.Build();

using (var context = new AppDbContext())
{
    //EF Core Relationships >>>>>>>>>>>>>>>>>>>>>> one to many
    //one to many (bire çok) ilişkide (örnekte category - bir, product - çok) category'den bir satır silinirse o kategoriye sahip tüm product'lar da silinir.>>default davranışı<<<<<


    //context.Products.Add(new Product { Name = "Lamy Safari Blue", Price = 550, Stock = 25, Barcode = 563 });
    //context.Products.Add(new Product { Name = "Lamy Safari Red", Price = 550, Stock = 15, Barcode = 564 });
    //context.Products.Add(new Product { Name = "Lamy Safari Green", Price = 550, Stock = 5, Barcode = 565 });
   
    //var products = await context.Products.ToListAsync();

    
    context.SaveChanges();







}