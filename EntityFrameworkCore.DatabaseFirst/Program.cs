// See https://aka.ms/new-console-template for more information
using EntityFrameworkCore.DatabaseFirst.DAL;
using Microsoft.EntityFrameworkCore;

DbContextInıtıalizer.Build();

//using (var _context = new AppDbContext(DbContextInıtıalizer.OptionsBuilder.Options)) //Buradaki daha kullanışlı
using (var _context = new AppDbContext()) //Eğer uygulama boyunca tek bir veri tabanı ile çalışılacak ise farklı bir yöntem olarak kullanılabilir. (2.yöntem)
{
    var products = await _context.Products.ToListAsync();

    products.ForEach(p =>
    {
        Console.WriteLine($"{p.Id} : {p.Name}");
    });
}