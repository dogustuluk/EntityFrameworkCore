using EntityFrameworkCore.CodeFirst;
using EntityFrameworkCore.CodeFirst.DAL;
using EntityFrameworkCore.Relationships.DAL;
using Microsoft.EntityFrameworkCore;
using System.Linq;

Initializer.Build();

using (var context = new AppDbContext())
{
    //Query

    //context.People.Add(new() { Name = "Deniz", Phone = "05335465213" });
    //context.People.Add(new() { Name = "Sena", Phone = "05452367898" });
    //context.SaveChanges();

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //Client vs Server Evaluation Start
    //var persons = context.People.Where(x => FormatPhone (x.Phone) == "05335465213").ToList(); //>>Bu haliyle çalışmaz çünkü EF Core Server'a gönderdiği metotlarda custom bir metoda yer veremez. Bunu çözmek için aşağıdaki kod yazılmalıdır.
    //  var person1 = context.People.ToList().Where(x => FormatPhone(x.Phone) == "5335465213").ToList();// Bu kod ile ilk karşılaşılan "ToList" kısmında ef core tüm datayı memory'e alır ve devamında olan sorguyı client tarafta çalıştırır.
    //yukarıdaki kod düzenlenmemiş(formatlanmamış) dataları getirir. ikinci yolda ise gelen verileri düzenlenmiş olarak getirelim.

    //  var person2 = context.People.ToList().Select(x => new { PersonName = x.Name, PersonPhone = FormatPhone(x.Phone) }).ToList();


    //EF Core sorgularda iki şekilde davranır >>>>>>>>>> 1- Client, 2- Server.
    //Server, veri tabanına gönderilecek olan sql cümleciğini içermelidir, local fonksiyonlar barındıramaz.
    //Client tarafı ise memory'e gelmiş olan datayı sorgulamaya yaramaktadır. local fonksiyonlar barındırabilir.
    //Client vs Server Evaluation End
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    //Joinlere ne zaman ihtiyaç duyarız >>>>>>>>> Eğer iki tablo arasında navigation property yok ise başvurabiliriz.
    //EF Core'da join yapısı yoktur ama linq ile yapabiliriz.
    //INNER JOIN START
    //İki tablo arasındaki ortak alanları almak istersek kullanabiliriz.


    //2'li join start
    //var result = context.Categories.Join(context.Products, x => x.Id, y => y.CategoryId,(c, p) => new
    //{
    //    CategoryName = c.Name,
    //    ProductName = p.Name,
    //    ProductPrice = p.Price
    //}).ToList();

    //var resultProduct = context.Categories.Join(context.Products, x => x.Id, y => y.CategoryId, (c, p) => p).ToList();
    //var resultCategory = context.Categories.Join(context.Products, x => x.Id, y => y.CategoryId, (c, p) => c).ToList();


    //result.ForEach(x =>
    //{
    //    Console.WriteLine($"{x.CategoryName} - {x.ProductName} - {x.ProductPrice}");
    //});


    //resultProduct.ForEach(x =>
    //{
    //    Console.WriteLine($"{x.Name}");
    //});
    //resultCategory.ForEach(x =>
    //{
    //    Console.WriteLine($"{x.Name}");
    //});

    ////2.yol olarak ise aşağıdaki gibi yazabiliriz.
    //var result2 = (from c in context.Categories
    //               join p in context.Products on c.Id equals p.CategoryId
    //               select new
    //               {
    //                   CategoryName = c.Name,
    //                   ProductName = p.Name,
    //                   ProductPrice = p.Price
    //               }).ToList();

    ////2'li join end
    ////3'lü join start
    //var resultTrilogy = context.Categories
    //    .Join(context.Products, c => c.Id, p => p.CategoryId, (c, p) => new { c, p })
    //    .Join(context.productFeatures, c => c.p.Id, pf => pf.Id, (c, pf) => new 
    //    {
    //        CategoryName = c.c.Name,
    //        ProductName = c.p.Name,
    //        ProductPrice = c.p.Price,
    //        ProductColor = pf.Color
    //    });

    ////2.yol >>>> Daha okunaklı olan, tavsiye edilen.
    //var resultTrilogy2 = (from c in context.Categories
    //                      join p in context.Products on c.Id equals p.CategoryId
    //                      join pf in context.productFeatures on p.Id equals pf.Id
    //                      select new
    //                      {
    //                          categoryName = c.Name,
    //                          productName = p.Name,
    //                          productPrice = p.Price,
    //                          productColor = pf.Color,
    //                          productHeight = pf.Height

    //                      }).ToList();
    //resultTrilogy2.ForEach(x =>
    //{
    //    Console.WriteLine($"{x.categoryName} - {x.productName} - {x.productPrice} - {x.productColor} - {x.productHeight}");
    //});
    //3'lü join end

    //INNER JOIN END

    //LEFT - RIGHT JOIN START
    //Left Join >>> Bu join tipinde hem kesişen alanlar hem de sol tarafta bulunan tablonun tüm alanları alınmaktadır.
    //Right Join >>> Bu join tipinde hem kesişen alanlar hem de sağ tarafta bulunan tablonun tüm alanları alınmaktadır.
    //EF Core tarafında left ya da right join yoktur ama custom linq sorguları yazılarak yapılabilir. Bunların adı left - right join olarak adlandırılmaz.

    //var leftJoinResult = (from p in context.Products
    //              join pf in context.productFeatures on p.Id equals pf.Id into pfList
    //              from pf in pfList.DefaultIfEmpty()
    //              select new 
    //              {
    //                  ProductName = p.Name,
    //                  ProductColor = pf.Color, //Nullable hatası almamamızın nedeni ise string değerlerin referans tipli olmasından kaynaklanmasıdır.
    //                  ProductHeight =(int?) pf.Height == null ? 0 : pf.Height //Eğer "(int?)" yazmazsak nullable hatası alırız.
    //              }
    //             ).ToList();


    //var rightJoinResuly = await (from pf in context.productFeatures
    //                             join p in context.Products on pf.Id equals p.Id into pList
    //                             from p in pList.DefaultIfEmpty()
    //                             select new
    //                             {
    //                                 productName = p.Name,
    //                                 productPrice = (decimal?)p.Price,
    //                                 productColor = pf.Color,
    //                                 productHeight =pf.Height
    //                             }).ToListAsync();

    //LEFT - RIGHT JOIN END

    //FULL OUTTER JOIN
    //EF Core tarafında yoktur, custom linq ile yapıyor olucaz.
    //her iki tablonun dataları ve kesişimlerinden oluşmaktadır.
    //left ve right join'de olduğu gibi outter join'de de query syntax ile yazılabilir, method syntax desteği yoktur.
    //var leftJoin = await (from p in context.Products
    //                      join pf in context.productFeatures on p.Id equals pf.Id into pfList
    //                      from pf in pfList.DefaultIfEmpty()
    //                      select new
    //                      {
    //                          pName = p.Name,
    //                          pfColor = pf.Color,
    //                          pfWidth = (int?)pf.Width
    //                      }).ToListAsync();

    //var rightJoin = await (from pf in context.productFeatures
    //                       join p in context.Products on pf.Id equals p.Id into pList
    //                       from p in pList.DefaultIfEmpty()
    //                       select new
    //                       {
    //                           pName = p.Name,
    //                           pfColor = pf.Color,
    //                           pfWidth = (int?)pf.Width
    //                       }).ToListAsync();

    //var outterJoin = leftJoin.Union(rightJoin); //union ifadesi birleştirmek için kullanılır.

    //outterJoin.ToList().ForEach (x =>
    //{
    //    Console.WriteLine($"{x.pName} - {x.pfColor} - {x.pfWidth}");
    //}) ;
    //FULL OUTTER JOİN END

    //RAW SQL QUERY START
    //var id = 3;
    //decimal price = 100;
    //var products = await context.Products.FromSqlRaw("select * from products").ToListAsync();


    ////parametre alarak sorgu yapma
    //var product = await co ntext.Products.FromSqlRaw("select * from products where price>{0}", id).FirstAsync();

    //var products2 = await context.Products.FromSqlRaw("select * from products where price > {0}", price).ToListAsync();

    //var products3 = await context.Products.FromSqlInterpolated($"select * from products where price > {price}").ToListAsync();

    ////-----------
    ////Custom Query

    //var products4 = await context.productEseentials.FromSqlRaw("select Name,Price from products").ToListAsync(); //Eğer id alanımız yok ise ya da almayacak isek context'e gidip hasNoKey ile işaretleme yapmamız gerekmektedir.

    //var products5 = await context.productWithFeatures.FromSqlRaw("select p.Id,p.Name, p.Price, pf.Color, pf.Height from Products p inner join productFeatures pf on p.Id = pf.Id ").ToListAsync();
    //------------------------------------------------------------------------------------------

    //ToSqlQuery
    //her seferinde "select *" yazmak yerine ön tanımlı sql cümlecikleri yazmak için.
    //Bunun için ToSqlQuery metodunu OnModelCreating metodu içerisinde kullanırız.
    // var productsToSqlQuery = context.Products.Where(x => x.Price > 100).ToList(); //istersek burda "where" ile şart ekleyebiliriz. Eklenen bu şart direkt olarak OnModelCreating'ten gelen hazır sql cümleciğine eklenir.
    //----------------------------------------------------


    //ToView Method Start
    //View'ler gerçek tablolar değildir, ön tanımlı sql cümlecikleri olarak düşünülebilir. Sanal tablolardır.
    //View'lerde insert, update, delete gibi metotlar uygulanması sağlıklı değildir. HasNoKey ile işaretlemek iyi olacaktır.
    //  var productToView = context.productFulls.ToList();

    //ToView Method End
    //RAW SQL QUERY END

    //Pagination Start
    //    GetProducts(2, 5).ForEach(x =>
    //    {
    //        Console.WriteLine($"{x.Id} - {x.Name}");
    //    });

    //    static List<Product> GetProducts(int page, int pageSize)
    //{
    //    using (var context = new AppDbContext())
    //    {
    //        return context.Products.OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
    //    } 
    //}

    //Pagination End

    //Global Query Filters Start
    //Soft Delete (IsDeleted),  Multi-tenancy (TenantId)
    // var products = context.Products.ToList(); ///Eğer AppDbContext(barcode numarası) yazarsak default int değerinden farklı olduğunu anladığından dolayı ilgili barcode numarasına sahip product gelir.
    // var productsIgnoreQueryFilter = context.Products.IgnoreQueryFilters().ToList(); //Ignore ederek query filter'larımızı iptal edebiliriz.

    //Global Query Filters End

    //Query Tags Start
    var productsWithFeatures = context.Products.TagWith("Bu sorgu product'ları ve product'lara bağlı feature'ları getirir.").Include(x => x.ProductFeature).Where(x => x.Price > 100).ToList();
    //Query Tags End
    #region data-insert
    //var category = new Category() { Name = "Defterler" };
    //category.Products.Add(new() { Name = "Defter1", Barcode = 123, DiscountPrice = 80, Price = 120, Stock = 210, URL = "asb", ProductFeature = new ProductFeature() { Color = "Red", Height = 35, Width = 54 } });
    //category.Products.Add(new() { Name = "Defter2", Barcode = 123, DiscountPrice = 80, Price = 120, Stock = 210, URL = "asb", ProductFeature = new ProductFeature() { Color = "Green", Height = 35, Width = 54 } });
    //category.Products.Add(new() { Name = "Defter3", Barcode = 123, DiscountPrice = 80, Price = 120, Stock = 210, URL = "asb", ProductFeature = new ProductFeature() { Color = "Blue", Height = 35, Width = 54 } });
    //context.Categories.Add(category);
    //context.SaveChanges();
    #endregion
    Console.WriteLine("İşlem Başarılı");

}



string FormatPhone(string phone)
{
    return phone.Substring(1, phone.Length - 1); //Telefon numaralarının başındaki "0"dan kurtuluruz.
    //Normalde bu metodu EF Core içerisindeki sorguya bunu yazamayız. EF Core bunu bir server değerlendirmesi olarak gerçekleştirmez.
}
