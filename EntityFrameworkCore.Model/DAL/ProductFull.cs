using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Model.DAL
{
    public class ProductFull
    {
        public int Product_Id { get; set; }
        public string ProductCategory { get; set; }
        public string ProductName { get; set; }
        [Precision(9,2)]
        public decimal ProductPrice { get; set; }
        public string ProductColor { get; set; }
    }
}
