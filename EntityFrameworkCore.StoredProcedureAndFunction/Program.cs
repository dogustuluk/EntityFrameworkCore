using EntityFrameworkCore.CodeFirst;
using EntityFrameworkCore.CodeFirst.DAL;
using EntityFrameworkCore.Relationships.DAL;
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
    var products = await context.productFulls.FromSqlRaw("exec sp_get_productsfull2").ToListAsync();
    products.ToList().ForEach(p =>
    {
        Console.WriteLine($"{p.Id} - {p.Name} - {p.Price} - {p.CategoryName} - {p.Color} - {p.Width} - {p.Height}");
    });

    //<level 2

    //STORED PROCEDURE END

    Console.WriteLine("İşlem Başarılı");

}




