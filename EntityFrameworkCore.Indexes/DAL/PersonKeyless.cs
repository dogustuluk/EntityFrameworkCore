using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Model.DAL
{
    [Keyless]
    public class PersonKeyless
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
