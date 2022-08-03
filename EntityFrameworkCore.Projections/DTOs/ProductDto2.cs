using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Projections.DTOs
{
    public class ProductDto2
    {
        public string CategoryName { get; set; }
        public string Products { get; set; }
        public decimal TotalPrice { get; set; }
        public int? TotalWidth { get; set; }
    }
}
