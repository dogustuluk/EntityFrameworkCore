using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.StoredProcedureAndFunction.Models
{
    public class ProductFull2
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public int Stock { get; set; }
        [Precision(9, 2)]
        public decimal Price { get; set; }
        [Precision(9, 2)]
        public decimal DiscountPrice { get; set; }
        public int Barcode { get; set; }
        public int CategoryId { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public string? Color { get; set; }

    }
}
