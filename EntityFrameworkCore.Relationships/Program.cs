using EntityFrameworkCore.CodeFirst;
using EntityFrameworkCore.CodeFirst.DAL;
using EntityFrameworkCore.Relationships.DAL;
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

    //one to many data add start
    //#1 start
    //var category = new Category() { Name = "Kalemler" };
    //var product = new Product() { Name = "Conklin herringbone", Price = 1350, Barcode = 156, Stock = 10, Category = category }; //navigation property olan Category'i burada belirtmek gerekiyor. Navigation property kullandığımız zaman sisteme parent olan nesneyi kaydetmemize gerek kalmaz, child üzerinden bir nesne eklerken parent nesnesi de sisteme kayıt edilmektedir.
    //var product2 = new Product() { Name = "Conklin herringbone", Price = 1350, Barcode = 156, Stock = 10, Category = category }; //Bu şekilde ekleme işleminde parent'ı tekrardan ekleyeceği için sistem bir başka kategorininmiş gibi hareket etmektedir.
    //context.Products.AddRange(product, product2);
    //#1 end
    //#2 start
    //var category = new Category() { Name = "Defterler" };
    //category.Products.Add(new() { Name = "Fabio Ricci Defter", Price = 100, Stock = 53, Barcode = 201 });
    //category.Products.Add(new() { Name = "Moleskine Defter", Price = 230, Barcode = 202, Stock = 40 });
    //context.Add(category);
    //category.Products.Add(new() { Name = "Eternals Bullet Journal", Price = 90, Barcode = 203, Stock = 80 });
    //context.Add(category);
    //#2 end
    //#3 start
    var category = context.Categories.First(x => x.Name == "Kalemler");
    var product = new Product() { Name = "Montblanc Homage to Grimms Brother", Barcode = 301, Price = 410, Stock = 7, CategoryId = category.Id };
    context.Add(product);
    //#3 end
    context.SaveChanges();
    Console.WriteLine("İşlem Başarılı");
    //end





}