using EntityFrameworkCore.CodeFirst;
using EntityFrameworkCore.CodeFirst.DAL;
using EntityFrameworkCore.Hierarchy.DAL;
using EntityFrameworkCore.Relationships.DAL;
using Microsoft.EntityFrameworkCore;

Initializer.Build();

using (var context = new AppDbContext())
{
    //Hierarchy
    //TPH (table per hierarchy)
    //EF Core default davranış olarak bunu yapar. Hiyearşi başına tek bir tablo oluşturulur. Context'e miras alınan sınıf tanıtılmaz.
    //Eğer EF Core'un default davranışını istemiyorsak base sınıfı da context'e geçeriz. Bu sayede EF Core veri tabanına kaydederken base sınıfı kayıt eder ve miras alan sınıfların sağladığı ek özellikleri de bu sınıfın içerisine ekler. Fakat buradaki farklı olan kısım EF Core'un alt sınıftan gelen özelliği "Discriminant" olarak ayırmasıyla başlar.
   //>>>>>>>>>>>>>>>>>>> Genellikle base sınıfları context'e geçip EF Core'un default davranışını kırmak doğru değildir. Fakat bazı senaryolarda, özellikle çok fazla tablo oluşacağı zaman işimize yarayabilir. Bu durumun tek dezavantajı ise; atama yapılmayan alanlara null değer atamasıdır.
    
    //tpt (table per type)
        //Her bir entity için bir tablo oluşmasını istiyorsak kullanmalıyız.
        //Bunun için her bir entity'i context'te "OnModelCreating'te" geçmeliyiz.
   


    // context.Persons.Add(new Manager() { FirstName = "Manager 1", LastName = "M", Age = 24, Grade = 1 });
    // context.Persons.Add(new Employee() { FirstName = "Employee 1", LastName = "E", Age = 24, Salary = 5000 });
    //context.SaveChanges();
    var managers = context.Managers.ToList();
    var employees = context.Employees.ToList();
    var persons = context.Persons.ToList();

    persons.ForEach(person =>
    {
        switch (person)
        {
            case Manager manager:
                Console.WriteLine($"Manager Sınıfı : {manager.FirstName} {manager.LastName} - {manager.Age} - {manager.Grade}");
                break;

            case Employee employee:
                Console.WriteLine($"Employee sınıfı : {employee.FirstName} {employee.LastName} - {employee.Age} - {employee.Salary}");
                break;

            default:
                break;
        }
    });

    Console.WriteLine("İşlem Başarılı");
}