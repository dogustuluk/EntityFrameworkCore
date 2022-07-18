using EntityFrameworkCore.CodeFirst;
using EntityFrameworkCore.CodeFirst.DAL;
using EntityFrameworkCore.Hierarchy.DAL;
using EntityFrameworkCore.Relationships.DAL;
using Microsoft.EntityFrameworkCore;

Initializer.Build();

using (var context = new AppDbContext())
{
                                                              //MODEL
    //Owned Entity Types Start[Owned]
                //Hiyerarşi yapısına ek olarak EF Core'un ek olarak 3.bir yaklaşımı vardır; Owned entity type.
                //Owned entity type'ta miras alma gibi bir durumdan söz edemeyiz.
                //Ortak olarak almamız gereken alanlar var ise; bu alanları herhangi bir id almayan bir sınıfta tanımlıyoruz ve "[Owned]" attribute'ü ile işaretliyoruz. Daha sonrasında bu ortak kullanılacak alanları isteyen entity'de property olarak geçiyoruz (örn: public Person person {get; set;})
    //Owned Entity Types End
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    //Keyless Entity Types Start[Keyless]
        //Key tanımlı değildir.
        //DbContext tarafından track edilmezler. Dolayısıyla veri tabanına insert, update ve delete işlemleri gerçekleştirilemez.
        //Raw Sql cümleciklerinden dönen datayı map'lemek istediğimizde kullanabiliriz.
        //Primary key içermeyen veri tabanlarındaki view'lerimizi map'lemek istediğimizde kullanabiliriz.
        //Primary key içermeyen tablolarımızı map'lemek istediğimizde kullanabiliriz.

        //EF Core'da linq sorgusu yazmak yerine ham sql sorgusu yazdığımızde geriye herhangi bir id'ye sahip olmayan bir tablo dönebilir ve böyle bir tabloyu bir entity ile karşılamak istediğimizde keyless entity type'ı kullanabiliriz.
    //Keyless Entity Types End
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    Console.WriteLine("İşlem Başarılı");



}