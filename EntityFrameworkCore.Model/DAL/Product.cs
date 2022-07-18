using EntityFrameworkCore.Relationships.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.CodeFirst.DAL
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public int Stock { get; set; }
        [Precision(9,2)]
        public decimal Price { get; set; }
        public int Barcode { get; set; }

        public virtual Category Category { get; set; }
        public virtual ProductFeature ProductFeature { get; set; }
        public int CategoryId { get; set; }
    }
}
