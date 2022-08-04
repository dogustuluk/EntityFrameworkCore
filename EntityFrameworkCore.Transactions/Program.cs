using AutoMapper.QueryableExtensions;
using EntityFrameworkCore.CodeFirst;
using EntityFrameworkCore.CodeFirst.DAL;
using EntityFrameworkCore.Projections.DTOs;
using EntityFrameworkCore.Projections.Mappers;
using EntityFrameworkCore.Relationships.DAL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;

Initializer.Build();

using (var context = new AppDbContext())
{
    //TRANSACTIONS START
    //Kullanıldığı scope'a(alana) göre farklı anlamlara gelebilmektedir.
    //Verinin bütünlüğünü sağlamaktadır. Ya hep ya hiç kuralına sahiptir. Eğer sıralı bir işlem yapılacak ise yani x kişisinin parası y kişisine transfer edilecek. X kişisinden paranın çekilmesinin sonucu olumlu ise ve y kişisine transfer işlemi başarılı olmadığında yapılmış olan tüm işlemler geri alınacaktır. Yani x kişisinden çekilen para da kişinin kendisine geri verilmiş olacaktır.
    //SaveChanges start
    var category = new Category() { Name = "Kılıflar" };
    context.Categories.Add(category);

    var product = context.Products.First();
    product.Name = "Defter 005";
    product.CategoryId = 45; //Bu noktada hata meydana geleceği için yeni bir kategori ekleme işlemi her ne kadar başarılı olarak gözükse de rollbag işlemi yapılır çünkü bu adımda bir hata meydana gelmektedir.
    context.SaveChanges(); //EF Core bu iki işlemi tek bir çağrı ile yapıyor şuanda. Eğer iki işlemden herhangi birinde hata meydana gelirse otomatik olarak rollbag olayı meydana gelip yapılan işlemler geri alınıyor. 

   //SaveChanges end
   //TRANSACTIONS END
    Console.WriteLine("İşlem Başarılı");

}




