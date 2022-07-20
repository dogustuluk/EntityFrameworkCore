using EntityFrameworkCore.CodeFirst;
using EntityFrameworkCore.CodeFirst.DAL;
using EntityFrameworkCore.Hierarchy.DAL;
using EntityFrameworkCore.Relationships.DAL;
using Microsoft.EntityFrameworkCore;

Initializer.Build();

using (var context = new AppDbContext())
{
                            //INDEXES
    //Index,    Included columns,   Check constraints,  Composite Index
    //Index'ler özellikle ilişkisel veri tabanlarında sorgulama performansını arttıran en önemli araçlardan bir tanesidir. Çok fazla sorgulanan sütunlarda indexleme yapmamız ciddi performans artışlarına neden olacaktır.
    //Sql Server tarafında primary key alanlarına direkt olarak clustered index atanmaktadır. Bunu oluşturmak için başka bir işlem yapmamıza gerek yoktur. Ek olarak foreign key alanlarına da default olarak clustered key atanmaktadır.
    Console.WriteLine("İşlem Başarılı");



}