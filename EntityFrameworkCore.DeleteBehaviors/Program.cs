using EntityFrameworkCore.CodeFirst;
using EntityFrameworkCore.CodeFirst.DAL;
using EntityFrameworkCore.Relationships.DAL;
using Microsoft.EntityFrameworkCore;

Initializer.Build();

using (var context = new AppDbContext())
{
    //DELETE BEHAVIORS
    //Cascade,      Restrict,       SetNull,        NoAction
    //CASCADE
    //Parent tablodan silinen nesnenin sahip olduğu tüm child nesneleri de silinmektedir. Bu davranış EF Core'un default davranışıdır.

    //Restrict
    //Parent sınıfından bir nesne silmeye çalıştığımızda eğer child sınıfına bağlı nesneleri ilgili nesnenin altında ise silme işlemini engellemeye yaramaktadır. Örneğin A kategorisine ait alt nesneler var ise ilgili kategorinin silinmesini engellemektedir.

    //SetNull
    //Eğer child tablomuzdaki foreign key'lerimiz nullable ise ve setnull olarak bunu belirlediysek; parent sınıftan ilgili bir nesneyi silersek, ilgili nesnenin id'sinin olduğu foreignKey'e null atamasını yapar.

    //NoAction
    //EF Core hiçbir şey yapmaz, tamamen veri tabanında hangi ayarlamaları yaptıysak öyle bırakır.

    //Eğer davranışı değiştirirsek mutlaka migration yapıp veri tabanını güncellememiz gerekir

    //#1 start

    var category = context.Categories.First();
    //Eğer restrict ile çalışıyorsak ve ilgili parent'ı silmek istersek child içerisindeki tüm dataları silmemiz gerekir.
    var products = context.Products.Where(x => x.CategoryId == category.Id).ToList();
    context.RemoveRange(products);
    context.Categories.Remove(category);
    //var category = new Category()
    //{
    //    Name = "Kalemler",
    //    Products = new List<Product>()
    //    {
    //        new(){Name = "Kalem 1", Barcode = 123, Price = 100, Stock = 12},
    //        new(){Name = "Kalem 2", Barcode = 123, Price = 100, Stock = 12},
    //        new(){Name = "Kalem 3", Barcode = 123, Price = 100, Stock = 12}
    //    }
    //};
    //context.Categories.Add(category);

    //#1 end
    context.SaveChanges();


    Console.WriteLine("İşlem Başarılı");
}