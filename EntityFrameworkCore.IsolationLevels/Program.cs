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

    //ISOLATION LEVELS END


    Console.WriteLine("İşlem Başarılı");

}
