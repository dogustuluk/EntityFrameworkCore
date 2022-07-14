using EntityFrameworkCore.CodeFirst.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Relationships.DAL
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public List<Product> Products { get; set; } //navigation property //one to many//
        public List<Product> Products { get; set; } = new List<Product>(); //Null exception hatası almamak için Prduct nesnesi üretiyoruz.
    }
}
