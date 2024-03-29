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
    //PROJECTIONS START
    //Sql datalarının uygulamamızdaki modele yansıtma işlemidir.(Entity, DTO/View Model, Anonymous Types) >> api'de DTO, MVC tarafında ise >> View Model olarak adlandırılır, her ikisi de aslında aynıdır. Entity'leri dış dünyaya açmamak adına yapılır.
    //context.products.toList(); >>> dendiğinde entity'ye yansıtırız.
    //
    //entity projections---------
    //var products = context.Products.ToList();
    //var products2 = context.Products.Include(x => x.Category).ToList();
    ////entity projections---------

    ////anonymous types start
    ////Eğer isimsiz bir tipe yansıtmak istiyorsak kullanılacak olan linq metodu >> Select
    //var products3 = await context.Products.Include(x => x.Category).Include(x => x.ProductFeature).Select(x => new
    //{
    //    CategoruName = x.Category.Name,
    //    ProductName = x.Name,
    //    ProductPrice = x.Price,
    //    Width = (int?)x.ProductFeature.Width

    //}).Where(x => x.Width > 10 && x.ProductName.StartsWith("k")).ToListAsync();//select ifadesi "IQueryable" döner geriye. Bu sayede select ifadesinden sonra koşul da yazabiliriz.
    ////koşul belirtirken belirtilen "x" ifadesi, select cümleciğinin içerisindekini belirtir.
    ////Where ifadesi de geriye "IQueryable dönmektedir. Yani bir şey "IQueryable" dönüyorsa geriye daha veri tabanına bir sql cümleciği oluşturmaz, burada bu datayı almak için mutlaka "tolist" ile bu datayı almamız gerekir.

    //var categories = await context.Categories.Include(x => x.Products).ThenInclude(x => x.ProductFeature).Select(x => new
    //{
    //    CategoryName = x.Name,
    //    Products = String.Join(",", x.Products.Select(z => z.Name)), //kalem1, kalem2, kalem3 şeklinde yazdırır.
    //    TotalPrice = x.Products.Sum(x => x.Price)
    //}).Where(y => y.TotalPrice >50).OrderBy(x => x.TotalPrice).ToListAsync();



    ////anonymous types2 start
    ////Eğerki select linq ifadesini kullanıyorsak include ifadelerini kullanmamıza gerek yok. Fakat bu özelliği kullanabilmek için entity'ler üzerinde navigation property'ler var ise bunu yapabiliriz.<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    //var products4 = await context.Products.Select(x => new
    //{
    //    categoryName = x.Category.Name,
    //    productName = x.Name,
    //    Width = (int?)x.ProductFeature.Width,
    //    TotalPrice = x.Category.Products.Sum(x => x.Price)
    //}).Where(x => x.Width>40).ToListAsync();

    //var categories2 = await context.Categories.Select(x => new
    //{
    //    categoryName = x.Name,
    //    Products = String.Join("", x.Products.Select(y => y.Name)),
    //    TotalPrice = x.Products.Sum(x => x.Price),
    //    TotalWidth = x.Products.Select(x => x.ProductFeature.Width).Sum()
    //}).Where(y => y.TotalPrice >100).OrderBy(x => x.TotalPrice).ToListAsync();
    ////anonymous types end
    ///--------------------------------------------------------------------------------------------
    //DTO/VIEW MODEL START
    //dto ve viewModeller aynı görevi üstlenirler; entity'leri dış dünyaya direkt olarak açmak yerine isimsiz bir tip ile veya dto/view model ile açarız.
    //isimsiz tipler o anda kullanılır, daha sonrasında bir başka yerde kullanamayız. Fakat DTO ya da View Model'leri auto mapper veya mapster gibi kütüpnaneleri kullanabiliriz. İsimsiz tiplerde ekstra sınıflar kullanmamıza gerek yoktur, direkt olarak new{} şeklinde tanımlanabilir. DTO ve view model'lerde ayrı class'lar oluşturmamız gerekmektedir. DTO ve view model daha güçlüdür. İhtiyaca göre seçim yapmakta fayda var.
    //var products5 = await context.Products.Select(x => new ProductDto
    //{
    //    CategoryName = x.Category.Name,
    //    ProductName = x.Name,
    //    ProductPrice = x.Price,
    //    Width = (int?)x.ProductFeature.Width
    //}).Where(x => x.Width > 30).ToListAsync();


    //var categories2 = await context.Categories.Select(x => new ProductDto2
    //{
    //    CategoryName = x.Name,
    //    Products = String.Join("", x.Products.Select(y => y.Name)),
    //    TotalPrice = x.Products.Sum(x => x.Price),
    //    TotalWidth = x.Products.Select(x => x.ProductFeature.Width).Sum()
    //}).Where(y => y.TotalPrice > 100).OrderBy(x => x.TotalPrice).ToListAsync();

    //Her seferinde yeni bir dto oluşturmak yerine AutoMapper kütüphanesi ekleyerek gerekli işlemleri yapmamız daha sağlıklıdır.

   // var products6Mapper = context.Products.ToList();
   // var productDto = ObjectMapper.Mapper.Map<List<ProductDto3AutoMapper>>(products6Mapper); //avantajı dezavantajı vardır. AutoMapper kütüphanesi ile yaptığımız zaman öncesinde tüm datalar çekilir, daha sonra mapping yapılınca gerekli alanlar kalır, istemediğimiz alanlar getirilmez.

    //Hem map'leme yapıp hem de sadece istediğimiz datalar sql'den çekilmesini istiyorsak aşağıdaki yol izlenir. Yani satır 86'daki tüm verileri başlangıç esnasında almak istemiyorsak.
    var productDto2 = context.Products.ProjectTo<ProductDto3AutoMapper>(ObjectMapper.Mapper.ConfigurationProvider).ToList();


    //DTO/VIEW MODEL END
    //PROJECTIONS END

    Console.WriteLine("İşlem Başarılı");

}




