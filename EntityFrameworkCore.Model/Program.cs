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
    //Key tanımlı değildir. Fakat ham sql cümleciklerinde id sütununu da içerebilir. Ama buradaki id sütununu primary key olarak değil, herhangi bir alan olarak düşün.
    //DbContext tarafından track edilmezler. Dolayısıyla veri tabanına insert, update ve delete işlemleri gerçekleştirilemez.
    //Raw Sql cümleciklerinden dönen datayı map'lemek istediğimizde kullanabiliriz.
    //Primary key içermeyen veri tabanlarındaki view'lerimizi map'lemek istediğimizde kullanabiliriz.
    //Primary key içermeyen tablolarımızı map'lemek istediğimizde kullanabiliriz.

    //EF Core'da linq sorgusu yazmak yerine ham sql sorgusu yazdığımızde geriye herhangi bir id'ye sahip olmayan bir tablo dönebilir ve böyle bir tabloyu bir entity ile karşılamak istediğimizde keyless entity type'ı kullanabiliriz.
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //var category = new Category() { Name = "Kalemler"};
    //category.Products.Add(new() { Name = "Kalem 1", Barcode = 111, Price = 100, Stock = 10, ProductFeature = new ProductFeature() {Color = "Red", Height =25, Width =40 } });
    //category.Products.Add(new() { Name = "Kalem 2", Barcode = 112, Price = 200, Stock = 20, ProductFeature = new ProductFeature() {Color = "Blue", Height =43, Width = 60 } });
    //category.Products.Add(new() { Name = "Kalem 3", Barcode = 113, Price = 300, Stock = 30, ProductFeature = new ProductFeature() { Color = "Green", Height = 24, Width = 38 } });
    //context.Categories.Add(category);
    //context.SaveChanges();

//    var productFull = context.ProductFulls.FromSqlRaw(@"select p.Id 'Product_Id',p.Name 'ProductName', pf.Color 'ProductColor', c.Name 'ProductCategory', p.Price 'ProductPrice'  from Products p
//join productFeatures pf on p.Id = pf.Id
//join Categories c on p.CategoryId = c.Id").ToList();
    //yukarıdaki sql sorgusunda tek tırnak içerisinde belirtilen isimler ile alınan entity'deki property isimleri aynı olmak zorundadır.
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //Keyless Entity Types End
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    //Entity Properties
    //[NotMapping],     [Column("Name")] : [Column(TypeName="Nvarchar(200)")],      [[Unicode(false)]] : varchar
    //[NotMapping]>>>>>>>>> Bir entity içerisinde bir property'miz vardır. Ama bu property'nin veri tabanında ilgili tabloda bir sütun olarak oluşmasını istemeyebiliriz. Kendimizin kod tarafında doldurmasını istediğimiz bir property olabilir. Bu gibi durumlarda NotMapping attribute'ünü kullanabiliriz. Fluen api tarafındaki karşılığı ise Ignore adlı metottur.
    //

    Console.WriteLine("İşlem Başarılı");



}