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
    //[Index(nameof(Name))] //Name alanı için bir index oluşturduk. Normal index denir.
    //[Index(nameof(Name), nameof(Price))] //Birden fazla index ataması yaparsam bunlara >>> Composite index denmektedir.
                //yukarıdaki indexlemede her iki şartı da kontrol edersek oldukça performanslı çalışır. Keza sadece ilk alan olarak verdiğimiz "Name" alanını da sorgular isek hızlı çalışır fakat "Price" alanı biraz daha yavaş çalışacaktır. Eğer "Price" alanının da hızlı çalışmasını istersek onu normal index olarak oluşturmamız gerekmektedir.
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string URL { get; set; }
        public int Stock { get; set; }
        [Precision(9,2)]
        public decimal Price { get; set; }
        [Precision(9, 2)]
        public decimal DiscountPrice { get; set; }
        public int Barcode { get; set; }

        //public virtual Category Category { get; set; }
        //public virtual ProductFeature ProductFeature { get; set; }
        //public int CategoryId { get; set; }
    }
}
