using AutoMapper.QueryableExtensions;
using EntityFrameworkCore.CodeFirst;
using EntityFrameworkCore.CodeFirst.DAL;
using EntityFrameworkCore.Projections.DTOs;
using EntityFrameworkCore.Projections.Mappers;
using EntityFrameworkCore.Relationships.DAL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.Linq;

using (var context = new AppDbContext())
{
    //ISOLATION LEVELS START
    //transaction'ların birbirleriyle olan ilişkilerini tanımlar
    //read uncommitted > read commited > repeatable read > snapshot > serializable ________________________ sağ tarafa doğru gidildikçe; 1)tutarlılık artar 2)Kaynak tüketimi artar 3)bir transaction'ın diğer bir transaction'ı bloklaması artar 4)eş zamanlı okuma sayısı azalır 5) concurrency effect azalır.
    //Concurrency start
    //4 adet etki vardır:: lost updated 2)dirt read 3)nonrepeatable reads 4)phantom reads
    //lost updates >>>  Eğer sıralı bir işimiz var ise ve bunlar ayrı birer transaction içerisinde ele alınıyorsa ve bu transaction'ların sıralı olması gerekirken işlemi yapma süreleri bu sırayı bozuyor ise buna lost updates denir. 
    //dirty read >>> Bir transaction'ın bir diğer transaction'ındaki commit edilmemiş dataları okumasına denmektedir.  
    //nonrepeatable reads >>> eğer a transaction'ında bir okuma yapıp arada farklı bir transaction'da burada okunmuş olan dataları güncellersek ve tekrar a transaction'ında bir okuma yaparsak tutarsız bir okuma olacaktır. A transaction'ında ilk okunan datalar ile son okunan datalar bir olmaz böylece.
    //phantom read >>> nonrepetable ile aynıdır, farklı bir mantığı yoktur. Burada ekleme(add) varken nonrepeatable reads'de ise güncelleme vardır.
    //Concurrency end

    //Read Uncommitted start
    //using (var transaction = context.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted)) //data güncellerken buradaki gibi isolation level belirtmeye gerek yoktur. Burada okuma yapan yer kritik noktadır.
    //    //Nerede okuma(read) yapılıyor ise orada isolation level belirtmemiz gerekmektedir. 
    //{
    //    var categories = context.Categories.ToList(); //Eğer commit edilmemiş dataları da çekmek istersek isolation level'i yukarıda belirtmemiz gerekmektedir.
    //    var product = context.Products.First();
    //    product.Price = 653;
    //    context.SaveChanges();

    //    transaction.Commit();
    //}

    //using (var transaction2 = context.Database.BeginTransaction())
    //{
    //    var product2 = context.Products.First();
    //    product2.Price = 6000;
    //    context.Products.Add(new Product() { Name = "Uncommit Kalem 1", Barcode = 000232, Price = 239, CategoryId = 1, DiscountPrice = 200, Stock = 586, URL = "uncomPen2231" });
    //    context.SaveChanges();

    //    transaction2.Commit();
    //}


    //başka bir transaction ekleme, okuma, silme ve güncelleme yapabilir, herhangi bir lag olmaz. Fakat aynı satırı uncommit'teki bir transaction güncellerken başka bir transaction aynı satırı güncelleme yapamaz. Aynı satırı başka bir transaction güncelleme esnasında ve daha commit etmediğinde bir başka transaction aynı id'ye sahip alanı güncellerken lag olur.

    //>>>>>>>>>>
    //önemli>>>>>>>>>>>>>>>>>>> phantom ve repeatable problemlerine sebep olmaktadır.
    //>>>>>>>>>>

    //Read Uncommitted end

    //READ COMMITTED START
    //bu isolation level, sql server'ın default seviyesidir.
    //dirty read meydana gelmez. commit edilmemiş datayı okumaz, güncel datayı okur.
    //phantom ve unrepeatable problemleri meydana gelmektedir.

    //using (var transaction = context.Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted)) //data güncellerken buradaki gibi isolation level belirtmeye gerek yoktur. Burada okuma yapan yer kritik noktadır.
    //                                                                                                        //Nerede okuma(read) yapılıyor ise orada isolation level belirtmemiz gerekmektedir. 
    //{
    //    var product = context.Products.First();
    //    product.Price = 5000;
    //    context.SaveChanges();

    //    transaction.Commit();
    //}
    //READ COMMITTED END

    //REPEATABLE READ START
    //güncel transaction içerisinde okuma yapılırken bir başka transaction update işlemi yapamamaktadır. Dolayısıyla unrepeatable problemi de oluşmamış olur.
    //update ve delete olmaz.
    //pahntom problemi oluşturmaktadır.

    //using (var transaction = context.Database.BeginTransaction(System.Data.IsolationLevel.RepeatableRead)) //data güncellerken buradaki gibi isolation level belirtmeye gerek yoktur. Burada okuma yapan yer kritik noktadır.
    //                                                                                                      //Nerede okuma(read) yapılıyor ise orada isolation level belirtmemiz gerekmektedir. 
    //{
    //  var product = context.Products.Take(2).ToList();

    //    transaction.Commit();
    //}

    //REPEATABLE READ END

    //SERIALIZABLE START
    //Repeatable ile yakın bir ilişki içerisindedir. hiç bir farkı yokttur. Tek bir artısı vardır ona göre, repeatable read'da update yapılamazken insert yapılıyordu bu durum da phantom problemine neden olmaktaydı. Bunda ise ne update ne de insert yapılabiliyor. Dolayısıyla phantom(insert olmadığı için) ve unrepeatable(update olmadığı için) problemleri oluşmuyor.
    //sadece commit edilen datayı okur, uncommit olanı okumaz.

    //using (var transaction = context.Database.BeginTransaction(System.Data.IsolationLevel.Serializable)) //data güncellerken buradaki gibi isolation level belirtmeye gerek yoktur. Burada okuma yapan yer kritik noktadır.
    //                                                                                                       //Nerede okuma(read) yapılıyor ise orada isolation level belirtmemiz gerekmektedir. 
    //{
    //    var product = context.Products.ToList();

    //    transaction.Commit();
    //}


    //SERIALIZABLE END

    //SNAPSHOT START
    //Snapshot isolation level'da bir transaction başladıktan sonra bir başka transaction bu başlayan transaction içerisindeki dataları günceller,siler veya eklerse ilgili transaction bunu gösteremez. dolayısıyla herhangi bir lag'lama olmaz.

    //İlk olarak veri tabanında snapshot özelliğini açmamız lazım. >>>> alter database EntityFrameworkCoreCodeFirstDb set ALLOW_SNAPSHOT_ISOLATION on >> yazılır

    using (var transaction = context.Database.BeginTransaction(System.Data.IsolationLevel.Snapshot))
    {
        var product = context.Products.AsNoTracking().ToList();
        
        //Arada farklı business kodlarının olduğunu varsay.
        
        
        var product2 = context.Products.AsNoTracking().ToList();

        //Arada farklı bir transaction'dan gelen data ekleme vs gibi olaylar olsa dahi product ile product2'deki datalar ilk hali gibi aynı olacaktır.

        transaction.Commit();
    }


    //SNAPSHOT END

    //ISOLATION LEVELS END

    //Eğer tutarlılık önemli ise >>>>>>>>>>>> Isolation level arttır
    //Eğer hız önemli ise >>>>>>>>>>>>>>>>>>> Isolation level düşür


    Console.WriteLine("İşlem Başarılı");

}
