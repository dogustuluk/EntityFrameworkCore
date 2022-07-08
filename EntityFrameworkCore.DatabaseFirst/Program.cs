// See https://aka.ms/new-console-template for more information
using EntityFrameworkCore.DatabaseFirst.DAL;
using Microsoft.EntityFrameworkCore;

DbContextInıtıalizer.Build();

using (var _context = new AppDbContext(DbContextInıtıalizer.OptionsBuilder.Options))
{
    var products = await _context.Products.ToListAsync();

    products.ForEach(p =>
    {
        Console.WriteLine($"{p.Id} : {p.Name}");
    });
}