using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.StoredProcedureAndFunction.Models
{
    public class ProductFullForFunction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Precision(9,2)]
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public string? Color { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
    }
}
