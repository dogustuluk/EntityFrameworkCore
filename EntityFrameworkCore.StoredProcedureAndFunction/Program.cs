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

    //level 1>
    //Eğer geriye bir tablo dönüyorsak iş basit bir şekilde çözülür. Tabii bu tablo bizim entity'lerimizde de birebir karşılığı var ise
    //Sql Server'da aşağıdaki kodu yazıp basit bir procedure oluşturmamız gereklidir.
//    create procedure sp_get_products
//as
//begin
//select* from Products
//end
    var products = await context.Products.FromSqlRaw("exec sp_get_products").ToListAsync();
    products.ToList().ForEach(p =>
    {
        Console.WriteLine($"{p.Name} - {p.Price} - {p.Barcode}");
    });

    //>level 1


    //STORED PROCEDURE END

    Console.WriteLine("İşlem Başarılı");

}




