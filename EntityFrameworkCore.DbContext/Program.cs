using EntityFrameworkCore.CodeFirst;
using EntityFrameworkCore.CodeFirst.DAL;
using Microsoft.EntityFrameworkCore;

Initializer.Build();

using (var context = new AppDbContext())
{
    //detached state start
    var product = await context.Products.FirstAsync();
    Console.WriteLine($"ilk state : {context.Entry(product).State}");
    context.Entry(product).State = EntityState.Detached; //Burada Ef Core'a ilgili product'ı track etmemesini söylüyoruz. Yani bundan sonra yapılacak olan işlemler veri tabanına yansıtılmayacaktır.
    Console.WriteLine($"son state : {context.Entry(product).State}");
    product.Price = 5000;
    await context.SaveChangesAsync();
    Console.WriteLine($"state after save changes : {context.Entry(product).State}");
    //detached state end



    ////delete scenario start
    ////Ef Core'da bir nesneyi sildikten sonra save changes ile state'ine bakarsak, ilgili nesnenin state'i detached olarak işaretlenmektedir. Çünkü silinmiş olan bir nesne track edilmez.
    //var product = await context.Products.FirstAsync();
    //Console.WriteLine($"ilk state : {context.Entry(product).State}");
    //context.Remove(product);
    //Console.WriteLine($"son state : {context.Entry(product).State}");
    //await context.SaveChangesAsync();
    //Console.WriteLine($"state after save changes : {context.Entry(product).State}");
    ////delete scenario end



    ////update scenario start
    //var product = await context.Products.FirstAsync();
    //Console.WriteLine($"ilk state : {context.Entry(product).State}");
    //product.Stock = 100;
    //Console.WriteLine($"son state : {context.Entry(product).State}");
    //await context.SaveChangesAsync();
    //Console.WriteLine($"state after save changes : {context.Entry(product).State}");
    ////update scenario end



    ////added scenario start
    ////Detached yapısı >>>> Ef Core tarafından tracj edilmediği anlamına gelmektedir. Yani ilgili nesneye save changes çağırırsak herhangi bir şey olmayacak ta ki Add metodunu çağırıncaya değin. Böylelikle state'i Added olarak değişecektir. Burada Save changes denilmesi bir sonuç kazandıracaktır. Save changes sonrası state'i Unchanged olacaktır. 
    //var newProduct = new Product { Name = "Rotring 600", Price = 500, Stock = 112, Barcode = 666 };
    //Console.WriteLine($"İlk state: {context.Entry(newProduct).State}");

    //context.Entry(newProduct).State = EntityState.Added;// Bu kod ile aşağıda yazılmış olan kodun sonucu aynı olmaktadır.
    ////await context.AddAsync(newProduct);

    //Console.WriteLine($"Son state: {context.Entry(newProduct).State}");

    //await context.SaveChangesAsync();

    //Console.WriteLine($"State after save changes: {context.Entry(newProduct).State}");
    //added scenario end


    //var products = await context.Products.ToListAsync();
    //products.ForEach(p =>
    //{
    //    //state yapısı
    //    //Unchanged >>>> Veri tabanından veriler çekildiğinde memory'de state'i unchanged olarak belirtilir. Herhangi bir değişiklik yapılmadığı anlamına gelmektedir.
    //    var state = context.Entry(p).State;

    //    Console.WriteLine($"{p.Id}: {p.Name} - {p.Price} - {p.Stock} - state:{state}");
    //});
}