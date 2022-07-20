using EntityFrameworkCore.CodeFirst;
using EntityFrameworkCore.CodeFirst.DAL;

Initializer.Build();

using (var context = new AppDbContext())
{
    //Query

    //context.People.Add(new() { Name = "Deniz", Phone = "05335465213" });
    //context.People.Add(new() { Name = "Sena", Phone = "05452367898" });
    //context.SaveChanges();
    
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //Client vs Server Evaluation Start
    //var persons = context.People.Where(x => FormatPhone (x.Phone) == "05335465213").ToList(); //>>Bu haliyle çalışmaz çünkü EF Core Server'a gönderdiği metotlarda custom bir metoda yer veremez. Bunu çözmek için aşağıdaki kod yazılmalıdır.
    var person1 = context.People.ToList().Where(x => FormatPhone(x.Phone) == "5335465213").ToList();// Bu kod ile ilk karşılaşılan "ToList" kısmında ef core tüm datayı memory'e alır ve devamında olan sorguyı client tarafta çalıştırır.
                                                                                                    //yukarıdaki kod düzenlenmemiş(formatlanmamış) dataları getirir. ikinci yolda ise gelen verileri düzenlenmiş olarak getirelim.

    var person2 = context.People.ToList().Select(x => new { PersonName = x.Name, PersonPhone = FormatPhone(x.Phone) }).ToList();


    //EF Core sorgularda iki şekilde davranır >>>>>>>>>> 1- Client, 2- Server.
    //Server, veri tabanına gönderilecek olan sql cümleciğini içermelidir, local fonksiyonlar barındıramaz.
    //Client tarafı ise memory'e gelmiş olan datayı sorgulamaya yaramaktadır. local fonksiyonlar barındırabilir.
    //Client vs Server Evaluation End
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    Console.WriteLine("İşlem Başarılı");



}

string FormatPhone(string phone)
{
    return phone.Substring(1, phone.Length - 1); //Telefon numaralarının başındaki "0"dan kurtuluruz.
    //Normalde bu metodu EF Core içerisindeki sorguya bunu yazamayız. EF Core bunu bir server değerlendirmesi olarak gerçekleştirmez.
}
