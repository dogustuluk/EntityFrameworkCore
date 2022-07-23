using EntityFrameworkCore.CodeFirst;
using EntityFrameworkCore.CodeFirst.DAL;
using EntityFrameworkCore.Relationships.DAL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;

Initializer.Build();

using (var context = new AppDbContext())
{
    //STORED PROCEDURE START
    //Store procedure'ler daha hızlı, daha performanslı ve network tarafını trafiğini azaltma tarafında bize kolaylıklar sağlayan ilişkisel veri tabanı ile gelen özelliklerden bir tanesidir.
    //Sql sorgusunu her yaptığımızda sorgu derlenir ve execution plan çıkarılır ardından geriye bir sonuç döner ve bu uzun süren bir işlemdir. Eğer ilgili sql cümleciklerini ilgili store procedure'lerde yazar isek sorgu ilk çalıştığında derlenir ve execution plan çıkarılır. Daha sonra tekrardan aynı store procedure çalıştırılır ise derleme ve execution plan yapılmaz ve ham sql cümlecikleri daha hızlı yanıt verme yeteneği kazanmış olur.
    //Her yerde kullanmak yerine hibrit bir modelde kullanmak daha mantıklı olur; join'lerin fazla olduğu yerlerde.
    //Store procedure'lerde where koşulu kullanılamaz server tarafında. >>>>>>>>>>>>>>>>  Ama illa bir koşul belirtecek ise client tarafında istenen şartı yazabiliriz.
    //    //level 1>
    //    //Eğer geriye bir tablo dönüyorsak iş basit bir şekilde çözülür. Tabii bu tablo bizim entity'lerimizde de birebir karşılığı var ise
    //    //Sql Server'da aşağıdaki kodu yazıp basit bir procedure oluşturmamız gereklidir.
    ////    create procedure sp_get_products
    ////as
    ////begin
    ////select* from Products
    ////end
    //    var products = await context.Products.FromSqlRaw("exec sp_get_products").ToListAsync();
    //    products.ToList().ForEach(p =>
    //    {
    //        Console.WriteLine($"{p.Name} - {p.Price} - {p.Barcode}");
    //    });

    //    //<level 1

    //level 2 -> Custom table>
    //Eğer custom bir table alacak isek bu dataları property olarak karşılayabilecek bir class'a ihtiyacımız vardır.
    //Bu tablolarda herhangi bir insert,delete,update yapılmayacağı için hasNoKey ile işaretlememiz lazım. Bunlar bir entity değildir; bir model olarak geçmektedirler.
    //Migration yapıldığında da model'daki sınıfın database'e yansımadan kod ekranında silmemiz gerekmektedir. Daha sonrasında veri tabanına yansıtmamız gerekecektir.
    //sql'deki procedure kodu aşağıdadır
    //    create procedure sp_get_productsfull2
    //as
    //begin
    //select p.Id, p.Name, p.Price, c.Name 'CategoryName', pf.Color, pf.Height, pf.Width from Products p
    //join Categories c on p.CategoryId = c.Id
    //join productFeatures pf on p.Id = pf.Id
    //end

    //exec sp_get_productsfull2

    //var products = await context.productFulls.FromSqlRaw("exec sp_get_productsfull2").ToListAsync();
    //products.ToList().ForEach(p =>
    //{
    //    Console.WriteLine($"{p.Id} - {p.Name} - {p.Price} - {p.CategoryName} - {p.Color} - {p.Width} - {p.Height}");
    //});

    ////<level 2

    //level 3 -> parametreli store procedure>
    //Custom bir tabloyu maplemek istediğimizde mutlaka bir model olmalı(ProductFull gibi) ve nullable alanlar var ise model'de bunu belirtmemiz gerekmektedir.

    //sql kodu aşağıdadır
    //    create procedure sp_get_products_full_parameters
    //@categoryId int,
    //@price decimal(9, 2)
    //as
    //begin
    //select p.Id, p.Name, p.Price, c.Name 'CategoryName', pf.Color, pf.Width, pf.Height from Categories c
    //join Products p on p.CategoryId = c.Id
    //left join productFeatures pf on pf.Id = p.Id
    //where p.CategoryId = @categoryId and p.Price >= @price
    //end

    //int categoryId = 1;
    //decimal price = 50;
    //var productsWithParameters = await context.productFulls.FromSqlInterpolated($"exec sp_get_products_full_parameters2 {categoryId},{price}").ToListAsync();
    //productsWithParameters.ToList().ForEach(p =>
    //{
    //    Console.WriteLine($"{p.Id}-{p.CategoryName}-{p.Name}-{p.Price}-{p.Color}-{p.Width}-{p.Height}");
    //});
    //Null olabilecek alanların mutlaka nullable olarak kendi model sınıfı içerisinde işaretleme yapılması gerekmektedir.

    //<level 3


    //level 4 -> insert, update yapan store procedure
    //Geriye bir şey dönmeyen ya da tek bir data dönen sorgularda executeSql kullanılabilir.
    //bu gibi işlemlerde geriye bir atanmış olan id'yi döndürmek istersek model üzerinden gidilmez, direkt olarak context'den gidilir.


    //store procedure sql kodu aşağıdadır 
    //    create procedure sp_insert_product
    //@name nvarchar(max),
    //@url nvarchar(max),
    //@stock int,
    //@price decimal(9, 2),
    //@discountPrice decimal(9, 2),
    //@barcode int,
    //@categoryId int,
    //@newId int output --yukarıdakiler birer input'tur, bu ise bir output olacaktır. Yeni bir Id geri dönmek için gereklidir.
    //as
    //begin
    //insert into Products(Name, URL, Stock, Price, DiscountPrice, Barcode, CategoryId) values(@name, @url, @stock, @price, @discountPrice, @barcode, @categoryId)
    //set @newId = SCOPE_IDENTITY();
    //    return @newId;
    //    end
    var product = new Product()
    {
    Name = "Kalem code store procedure 4",
    URL = "store.exe.ww",
        Stock = 53,
        Price = 530,
        DiscountPrice = 340,

        Barcode = 1230,
    CategoryId = 1,

    };

    var newProductIdParameter = new SqlParameter("@newId", System.Data.SqlDbType.Int);
    newProductIdParameter.Direction = System.Data.ParameterDirection.Output;
    context.Database.ExecuteSqlInterpolated($"exec sp_insert_product {product.Name},{product.URL},{product.Stock},{product.Price},{product.DiscountPrice},{product.Barcode},{product.CategoryId},{newProductIdParameter} out");

    var productId = newProductIdParameter.Value;

    //<level 4

    //STORED PROCEDURE END

    Console.WriteLine("İşlem Başarılı");

}




