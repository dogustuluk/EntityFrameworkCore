using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.StoredProcedureAndFunction.Models
{
    public class ProductWithFeaturesFunctionMethodColorParameter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Color { get; set; }
        public int? Height { get; set; }
        public int? Width { get; set; }
    }
}
