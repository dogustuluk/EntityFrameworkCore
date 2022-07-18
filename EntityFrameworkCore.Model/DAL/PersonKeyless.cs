using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Model.DAL
{
    [Keyless]
    public class PersonKeyless
    {//Keyless için 2.senaryomuz olan içerisinde herhangi bir foreign key olmayan bir sınıf üzerinden örnek yapılması.
        //Migration işlemi yapmak istersek eğer EF Core hata verir çünkü içerisinde bir key olmasını istemektedir. Fakat Keyless olarak işaretleme yaparsak böyle bir hata almamış oluruz.
        //EF Core tarafından okuma haricindeki crud işlemleri yapılmaz.
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
