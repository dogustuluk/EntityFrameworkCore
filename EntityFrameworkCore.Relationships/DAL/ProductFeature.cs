using EntityFrameworkCore.CodeFirst.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Relationships.DAL
{
    public class ProductFeature
    {
        [ForeignKey("Product")]
        public int Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Color { get; set; }
      //  public int ProductRefId { get; set; } //Fluent API ile sisteme tanıtmak istersek. Best practise için bunu kullanmamalıyız.
        //public int ProductId { get; set; } //EF Core one to one'da default olarak parent child ilişkisini anlamıyor. Bu noktada foreign key'i child'a vermemiz gerekiyor. (Convension)
        public Product Product { get; set; }
    }
}
