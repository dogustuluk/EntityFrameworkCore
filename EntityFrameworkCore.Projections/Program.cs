using EntityFrameworkCore.CodeFirst;
using EntityFrameworkCore.CodeFirst.DAL;
using EntityFrameworkCore.Relationships.DAL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;

Initializer.Build();

using (var context = new AppDbContext())
{
    //PROJECTIONS START
    //Sql datalarının uygulamamızdaki modele yansıtma işlemidir.(Entity, DTO/View Model, Anonymous Types) >> api'de DTO, MVC tarafında ise >> View Model olarak adlandırılır, her ikisi de aslında aynıdır. Entity'leri dış dünyaya açmamak adına yapılır.
    //context.products.toList(); >>> dendiğinde entity'ye yansıtırız.
    //
    //entity projections---------
    var products = context.Products.ToList();
    var products2 = context.Products.Include(x => x.Category).ToList();
    //entity projections---------

    //anonymous types start
    //Eğer isimsiz bir tipe yansıtmak istiyorsak kullanılacak olan linq metodu >> Select
    var products3 = await context.Products.Include(x => x.Category).Include(x => x.ProductFeature).Select(x => new
    {
        CategoruName = x.Category.Name,
        ProductName = x.Name,
        ProductPrice = x.Price,
        Width = (int?)x.ProductFeature.Width

    }).Where(x => x.Width > 10 && x.ProductName.StartsWith("k")).ToListAsync();//select ifadesi "IQueryable" döner geriye. Bu sayede select ifadesinden sonra koşul da yazabiliriz.
    //koşul belirtirken belirtilen "x" ifadesi, select cümleciğinin içerisindekini belirtir.
    //Where ifadesi de geriye "IQueryable dönmektedir. Yani bir şey "IQueryable" dönüyorsa geriye daha veri tabanına bir sql cümleciği oluşturmaz, burada bu datayı almak için mutlaka "tolist" ile bu datayı almamız gerekir.

    var categories = await context.Categories.Include(x => x.Products).ThenInclude(x => x.ProductFeature).Select(x => new
    {
        CategoryName = x.Name,
        Products = String.Join(",", x.Products.Select(z => z.Name)), //kalem1, kalem2, kalem3 şeklinde yazdırır.
        TotalPrice = x.Products.Sum(x => x.Price)
    }).Where(y => y.TotalPrice >50).OrderBy(x => x.TotalPrice).ToListAsync();

    //anonymous types end
    //PROJECTIONS END


    Console.WriteLine("İşlem Başarılı");

}




