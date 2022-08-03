﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Projections.DTOs
{
    public class ProductDto
    {
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int? Width { get; set; }
    }
}
