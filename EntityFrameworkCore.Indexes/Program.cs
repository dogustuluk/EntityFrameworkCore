using EntityFrameworkCore.CodeFirst;
using EntityFrameworkCore.CodeFirst.DAL;
using EntityFrameworkCore.Hierarchy.DAL;
using EntityFrameworkCore.Relationships.DAL;
using Microsoft.EntityFrameworkCore;

Initializer.Build();

using (var context = new AppDbContext())
{
    //INDEXES
    //Index,    Included columns,   Check constraints,  Composite Index
    //Index'ler özellikle ilişkisel veri tabanlarında sorgulama performansını arttıran en önemli araçlardan bir tanesidir. Çok fazla sorgulanan sütunlarda indexleme yapmamız ciddi performans artışlarına neden olacaktır.
    //Sql Server tarafında primary key alanlarına direkt olarak clustered index atanmaktadır. Bunu oluşturmak için başka bir işlem yapmamıza gerek yoktur. Ek olarak foreign key alanlarına da default olarak clustered key atanmaktadır.

    Product product = new() { Name = "Kalem 1", URL = "abc", Price = 100, Barcode = 123, DiscountPrice = 190, Stock = 21 };
    if (product.DiscountPrice > product.Price)
    {
        //gerekli kontrol kodları yazılır
        Console.WriteLine("İndirimli fiyat ürünün normal fiyatından yüksek olamaz.");

    }

    context.Products.Add(new() { Name = "Kalem 1", URL = "abc", Price = 100, Barcode = 123, DiscountPrice = 90, Stock = 21 });
   // context.Products.Add(new() { Name = "Kalem 1", URL = "abc", Price = 100, Barcode = 123, DiscountPrice = 910, Stock = 21 });
    context.SaveChanges();
    Console.WriteLine("İşlem Başarılı");



}