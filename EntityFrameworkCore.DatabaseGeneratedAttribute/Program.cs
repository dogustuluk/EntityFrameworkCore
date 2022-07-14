using EntityFrameworkCore.CodeFirst;
using EntityFrameworkCore.CodeFirst.DAL;
using EntityFrameworkCore.Relationships.DAL;
using Microsoft.EntityFrameworkCore;

Initializer.Build();

using (var context = new AppDbContext())
{
                                            //DATABASEGENERATED ATTRIBUTE


    //Computed,     Identity,       None
    //Computed
    //EF Core "Add" ve "Upddate" işlemlerinde bu alanı sorgulara dahil etmez.
    //Eğer bir tablodaki sütunların değerlerini kodsal olarak değil de veri tabanından belirlememiz gerekecekse, bu noktada computed kullanılır.

    //Identity
    //Insert işlemlerinde etkili olmaktadır.
    //Veri tabanından veya kodsal olarak bir alanı sadece insert edildiği anda doldurmak istersek Identity attribute'ünü ekleriz.
    //EF Core sadece Update işlemlerinde bu alanı dahil etmez.

    //None
    //Veri tabanı tarafından otomatik değer üretmeyi kapatır.
   
    Console.WriteLine("İşlem Başarılı");
}