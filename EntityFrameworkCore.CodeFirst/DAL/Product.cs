﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.CodeFirst.DAL
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }
}
