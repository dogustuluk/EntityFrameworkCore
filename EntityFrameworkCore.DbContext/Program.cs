using EntityFrameworkCore.CodeFirst;
using EntityFrameworkCore.CodeFirst.DAL;
using Microsoft.EntityFrameworkCore;

Initializer.Build();

using (var context = new AppDbContext())
{
    context.Products.Add(new Product { Name = "Lamy Safari Blue", Price = 550, Stock = 25, Barcode = 563 });
    context.Products.Add(new Product { Name = "Lamy Safari Red", Price = 550, Stock = 15, Barcode = 564 });
    context.Products.Add(new Product { Name = "Lamy Safari Green", Price = 550, Stock = 5, Barcode = 565 });
    //var products = await context.Products.ToListAsync();
    //products.ForEach(p =>
    //{
    //    //state yapısı
    //    //Unchanged >>>> Veri tabanından veriler çekildiğinde memory'de state'i unchanged olarak belirtilir. Herhangi bir değişiklik yapılmadığı anlamına gelmektedir.
    //    var state = context.Entry(p).State;

    //    Console.WriteLine($"{p.Id}: {p.Name} - {p.Price} - {p.Stock} - state:{state}");
    //});


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




    ////update scenario start
    //var product = await context.Products.FirstAsync();
    //Console.WriteLine($"ilk state : {context.Entry(product).State}");
    //product.Stock = 100;
    //Console.WriteLine($"son state : {context.Entry(product).State}");
    //await context.SaveChangesAsync();
    //Console.WriteLine($"state after save changes : {context.Entry(product).State}");
    ////update scenario end



    ////delete scenario start
    ////Ef Core'da bir nesneyi sildikten sonra save changes ile state'ine bakarsak, ilgili nesnenin state'i detached olarak işaretlenmektedir. Çünkü silinmiş olan bir nesne track edilmez.
    //var product = await context.Products.FirstAsync();
    //Console.WriteLine($"ilk state : {context.Entry(product).State}");
    //context.Remove(product);
    //Console.WriteLine($"son state : {context.Entry(product).State}");
    //await context.SaveChangesAsync();
    //Console.WriteLine($"state after save changes : {context.Entry(product).State}");
    ////delete scenario end



    ////detached state start
    //var product = await context.Products.FirstAsync();
    //Console.WriteLine($"ilk state : {context.Entry(product).State}");
    //context.Entry(product).State = EntityState.Detached; //Burada Ef Core'a ilgili product'ı track etmemesini söylüyoruz. Yani bundan sonra yapılacak olan işlemler veri tabanına yansıtılmayacaktır.
    //Console.WriteLine($"son state : {context.Entry(product).State}");
    //product.Price = 5000;
    //await context.SaveChangesAsync();
    //Console.WriteLine($"state after save changes : {context.Entry(product).State}");
    ////detached state end

    //Change Tracking start
    //Veri tabanından gelen tüm datalar Ef Core tarafından track edilir. Bu Faydalı bir durum değildir. Update veya Delete durumlarında problem olmayabilir fakat diğer koşullarda track etmemesi gerekmektedir. Bu durumdan AsNoTracking() metodu ile aşılabilir. 
    //AsNoTracking avantajları ise > 1) Memory'de tutulmuyor. 2) RAM'i daha düzgün kullanmış oluruz.
    //NOT >>>>>>>>>> Güncelleme yapacağımız koşulda da track etmememiz daha doğru olacaktır. Where ile filtreleyip kullanmalıyız ya da sayfalama yaparak parça parça SQL Server'a yansıtmak daha doğru olacaktır.
    //ChangeTracker >>>> Track edilen datalar ile ilgili modifikasyona izin verir. Ortak olan yapıları her zaman save changes'dan önce çağırıp atamalıyız. Bu duruma örnek vermek gerekirse; Eğer Product tablomuza veri eklemek istersek oluşturulma tarihlerini ortak olarak merkezi bir yerden atayabiliriz. Bunun için Ef Core'un sahip olduğu ChangeTracker metodu vardır. Bu metodu save changes metodunu override ederek(AppDbContext içerisinde) ilgili olan ortak alanları merkezi bir yerden atama yapabilmemize olanak sağlar. Bu durum best practise açısından en iyi durumdur. Sadece oluşturulma tarihi değil, güncelleme tarihleri için de aynısını yapabiliriz. Fakat bu iki durumda da güncelleme yaparken state'inin "Added" ya da "Modified" olmasının kontrolünü yapmalıyız. 


    //  var products = await context.Products.AsNoTracking().ToListAsync(); //Bu kod ile memory'de takip edilmemesini sağlarız ve save changes öncesinde yapmış olduğumuz hiç bir değişiklik veri tabanına kayıt edilmez.

    //products.ForEach(p =>
    //{
    //    p.Stock += 100;
    //    Console.WriteLine($"{p.Id} - {p.Name} - {p.Price} - {p.Stock} - {p.Barcode}");
    //});

    //context.SaveChanges();
    var products = await context.Products.ToListAsync();

    
    context.SaveChanges();
    //Change Tracking end







}