using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Hierarchy.DAL
{
    //[Owned] //Fluent ile de tanımlanabilir.
    public class Person
    {
        //public int Id { get; set; } //Owned ile işaretli olan entity'lerde id olamaz. Çünkü bu sınıf içerisindeki alanlar zaten bir başka tabloya aitler ve o tablonun bir id'si bulunmaktadır.
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}
