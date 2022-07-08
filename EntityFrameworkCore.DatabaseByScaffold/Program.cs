// See https://aka.ms/new-console-template for more information
using EntityFrameworkCore.DatabaseByScaffold.Models;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

using (var context = new EntityFrameworkCoreDatabaseFirstDbContext())
{
    var products = await context.Products.ToListAsync();
    products.ForEach(p =>
    {
        Console.WriteLine($"{p.Id}: {p.Name} - {p.Price} TL - {p.Stock}");
    });
}