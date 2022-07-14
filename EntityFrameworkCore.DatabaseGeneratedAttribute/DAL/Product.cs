using EntityFrameworkCore.Relationships.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int Kdv { get; set; }
        public int Barcode { get; set; }

       // [DatabaseGenerated(DatabaseGeneratedOption.Computed)] //Fluent api'de ise ValueGeneratedOnAddOrUpdate
        public decimal PriceKdv { get; set; }

        //örnek 1
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)] //Eğer veri tabanından veya EF Core ile default bir değer giriyor isek, bu kodu yazmamız performans açısından oldukça olumlu sonuç verecektir. Aşağıdaki property'i güncelleme sorgularında dahil etmemiş oluruz.
        //public DateTime CreatedDate { get; set; } = DateTime.Now; //default değer olarak şimdiki tarihi atadık.
        //-
        //örnek 2
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)] //Burada ise; 
        //public DateTime LAstAccessDate { get; set; } //İlgili sisteme en son ne zaman erişildiyse bu alanın güncellenmesini istiyorsak veri tabanından yaptığımızda (trigger ile yapabiliriz ya da kendi yazdığımız başka bir fonksiyon olabilir.) EF Core add ve update sorgularına bunu dahil etmemelidir. Hem eklemeyi hem de güncellemeyi veri tabanımda yapıyor olucam.
        //-
        //public int CategoryId { get; set; }
        //public Category Category { get; set; }
        // public ProductFeature ProductFeature { get; set; }
    }
}
