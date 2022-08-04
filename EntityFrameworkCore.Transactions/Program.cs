﻿using AutoMapper.QueryableExtensions;
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
    ////SaveChanges start
    //var category = new Category() { Name = "Kılıflar" };
    //context.Categories.Add(category);

    //var product = context.Products.First();
    //product.Name = "Defter 005";
    //product.CategoryId = 45; //Bu noktada hata meydana geleceği için yeni bir kategori ekleme işlemi her ne kadar başarılı olarak gözükse de rollbag işlemi yapılır çünkü bu adımda bir hata meydana gelmektedir.
    //context.SaveChanges(); //EF Core bu iki işlemi tek bir çağrı ile yapıyor şuanda. Eğer iki işlemden herhangi birinde hata meydana gelirse otomatik olarak rollbag olayı meydana gelip yapılan işlemler geri alınıyor. 

    //transaction 2 start

    using (var transaction = context.Database.BeginTransaction())
    {
        var category = new Category() { Name = "Kılıflar" };
        context.Categories.Add(category);
        context.SaveChanges();
        Product product = new()
        {
            Name = "Kılıf 1",
            Price = 485,
            Stock = 63,
            Barcode = 47035,
            DiscountPrice = 365,
            CategoryId = category.Id //buradaki id alanı çalışmaz, ilk önce eklenmiş olması lazım ama şuan aynı anda ekleme işlemi yapılacak. Eğer bunu kullanmak istiyorsak kategori eklendikten sonra da SaveChanges dememiz lazım. Fakat ilk saveChanges sonrasında ikinci saveChanges ifadesinde bir hata olursa ilk veri ya da sonrasındaki veride hata oluşması durumunda veri tabanında bir tutarsızlık olacaktır. Bu durumda açık açık transaction belirtmemiz gerekecektir.
              //Önemli >>>>>>>>>>>>> Birden fazla saveChanges kullanırsak transaction yapısını, scope'unu kullanmamız gerekir.
             
            //CategoryId = category.Id  şeklinde kullanmak doğru olmamaktadır. Bu yapıda transaction yapılmasına gerek yoktur. İlla id alınması gereken bir yer var ise yapılır. Ama bu yapıyı transaction kullanmadan da çözebiliriz. Not "#nonTransAction" isimli region'da belirtilmektedir.
            //Transaction açıldığında her zaman trt-catch bloğu açılmamalı, ihtiyacımız varsa açılması. Eğer trt-catch açar isek catch bloğu içerisinde rollbag açık açık yazılmalıdır.

        };

        context.Products.Add(product);
        context.SaveChanges();
        throw new Exception();
        transaction.Commit(); //En sonda commit ettiğimiz için istediğimiz kadar saveChanges kullanabiliriz. Eğer 10 savechanges kullanıp sadece birinde hata çıksa dahi veri tabanında tutarsızlık olmayacak, transaction bunun yönetimini sağlayıp tüm işlemler geri alınacaktır.
                              //Burada eğer bu transaction yapısını loglama yapmayacak isek try-catch bloğuna almamıza gerek yoktur.

        #region nonTransAction
        //var category1 = new Category() { Name = "Kılıflar" };
        //context.Categories.Add(category1);
        //Product product1 = new()
        //{
        //    Name = "Kılıf 1",
        //    Price = 485,
        //    Stock = 63,
        //    Barcode = 47035,
        //    DiscountPrice = 365,
        //    Category = category1
        //};
        //context.Products.Add(product1);
        //context.SaveChanges();

        #endregion




        //transaction 2 end

        //SaveChanges end
        //TRANSACTIONS END
        Console.WriteLine("İşlem Başarılı");

}




