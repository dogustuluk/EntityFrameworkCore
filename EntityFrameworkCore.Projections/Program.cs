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
    //PROJECTIONS END


    Console.WriteLine("İşlem Başarılı");

}




