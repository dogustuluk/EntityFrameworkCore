﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Hierarchy.DAL
{
    public class Employee : BasePerson
    {
        [Precision(18,2)]
        public decimal Salary { get; set; }
    }
}
