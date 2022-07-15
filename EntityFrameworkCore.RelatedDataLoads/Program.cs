using EntityFrameworkCore.CodeFirst;
using EntityFrameworkCore.CodeFirst.DAL;
using EntityFrameworkCore.Relationships.DAL;
using Microsoft.EntityFrameworkCore;

Initializer.Build();

using (var context = new AppDbContext())
{
                                            //Related Data Loads

    //Eager loading,        Explicit loading,       Lazy loading
    //Eager Loading
        //Bir datayı çekerken, aynı anda ilgili datanın parent'ını da çekmek istediğimiz zaman buna "Eager Loading" denmektedir. Örnek::>> bir product'ı çekerken aynı anda product'a bağlı kategoriyi de çekmek denilebilir.


    
    Console.WriteLine("İşlem Başarılı");
}