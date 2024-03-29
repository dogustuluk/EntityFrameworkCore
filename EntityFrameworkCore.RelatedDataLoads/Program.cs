﻿using EntityFrameworkCore.CodeFirst;
using EntityFrameworkCore.CodeFirst.DAL;
using EntityFrameworkCore.Relationships.DAL;
using Microsoft.EntityFrameworkCore;

Initializer.Build();

using (var context = new AppDbContext())
{
    //Related Data Loads

    //Eager loading,        Explicit loading,       Lazy loading
    //Eager Loading
    //Bir datayı çekerken, aynı anda ilgili datanın parent'ını da çekmek istediğimiz zaman buna "Eager Loading" denmektedir. Örnek::>> bir product'ı çekerken aynı anda product'a bağlı kategoriyi de çekmek denilebilir.

    //var category = new Category() { Name = "Kalemler" };

    //category.Products.Add(new() { Name = "Kalem 1", Price = 100, Stock = 10, Barcode = 101, ProductFeature = new() { Color = "Red", Height = 100, Width = 90 } });
    //category.Products.Add(new() { Name = "Kalem 2", Price = 200, Stock = 20, Barcode = 102, ProductFeature = new() { Color = "Blue", Height = 80, Width = 50 } });

    //await context.AddAsync(category);
    //await context.SaveChangesAsync();

    //Eager Loading start
    //var categoryWithProducts = context.Categories.Include(x => x.Products).ThenInclude(x => x.ProductFeature).First(); //İlk include'daki x; Category'e karşılık gelirken ikinci include'daki x; Product'a karşılık gelmektedir.
    //var product = context.Products.Include(x => x.ProductFeature).Include(x => x.Category).First(); //Burada iki tane "Include" kullanmamızın sebebi; Product nesnesinin sahip olduğu iki tane navigation propert'si olmasıdır.

    //categoryWithProducts.Products.ForEach(product =>
    //{
    //    Console.WriteLine($"{categoryWithProducts.Name} {product.Name} {product.ProductFeature.Color}");
    //});
    //End

    //Explicit Loading Start(Açık Yükleme)

    //Eager loading'teki gibi datanın çekilmesi esnasında ilgili diğer dataları da getirmez, istenildiği takdirde bağlı olan sınıfların datalarını çekmemize yarar.
    ////var product = context.Products.First();

    ////var category = context.Categories.First();
    ////if (true)
    ////{
    ////    //context.productFeatures.Where(x => x.Id == product.Id).First(); //Bu kodu yazmak yerine aşağıdaki gibi explicit loading ile yazmak best practise açısından önemlidir.
    ////    context.Entry(product).Reference(x => x.ProductFeature).Load(); //Burada product ile productFeature arasında bire bir ilişki olduğundan dolayı Collection olarak değil, Reference olarak explicit loading yapılır.
    ////    context.Entry(category).Collection(x => x.Products).Load(); //bire çok bir ilişki olduğundan Collection ile alındı. İşlem buraya gelene kadar category'de Product nesnesi tutulmaz, ne zaman bu satır okunur o zaman category'de atanacak olan nesneler oluşturulur.
    //}
    //End

    //Lazy Loading start
        //Geç yüklenme olarak akılda tut.
        //Default olarak kapalı gelmektedir, açmamız gerekmektedir. Açmak için gerekli olan >>>>> Proxies kütüphanesini yükle.
        //Bu noktada sadece kütüphane yüklenmesi de yeterli olmamaktadır. Aynı zamanda Lazy loading özelliğini açmamız gerekmektedir. Bunun için appDbContext'e gidip veri tabanını tanıttığımız yere >>> "UseLazyLoadingProxies()" yazmamız gerekmektedir. Loglama yapmak için de aynı yere "LogTo(NereyeLoglamaYapılacağı,LogLevel.istenenLoglamaTürü") yazılır.
        //lazy loading için kütüphane eklemek ve veri tabanının tanıtıldığı yerden sonra 3.adım olarak ise navigation property'leri virtual yapmamız gerekmektedir.
       //Lazy loading kullandığımızda ilk anda dataları getirmez, siz navigation property'i ya da navigation property'leri ne zaman çağırırsanız o zaman dataları yükler.


    var category = await context.Categories.FirstAsync();
    Console.WriteLine("category çekildi");
    var products = category.Products;
    foreach (var item in products)
    {
        var productFeature = item.ProductFeature;
    }
    //Lazy loading'i ForEach ile kullanmak ciddi performans sorunlarına yol açmaktadır. Çünkü döngünün içerisine her girdiğinde EF Core ile veri tabanına tekrar bir istek atıp tüm sorguyu başlatacak. Bu probleme >>> N+1 <<< problemi denmektedir. Domain Driven Design Pattern'de foreach ile kullanılabilinir. Her mimari yaklaşımın kendine özgü bir çözümü vardır.
    //End
    Console.WriteLine("İşlem Başarılı");
}