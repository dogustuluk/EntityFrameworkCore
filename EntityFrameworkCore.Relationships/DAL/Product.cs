using EntityFrameworkCore.Relationships.DAL;
using System;
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
        public int Barcode { get; set; }

        //one to many start
        //EF Core'un default davranışını incelemek için alttaki iki ifadeyi siliyoruz fakat Category sınıfındaki navigation property'i silmiyoruz. Convension'un farklı çeşitlerini görmek amacıyla yapıyoruz bu örneği.
        //Bu sayede Category'den Productlara erişim olacak fakat Product'ın bağlı olduğu Category'e erişim olmayacaktır.
        //EF Core bu davranışta otomatik olarak bir foreign key ataması da yapıyor olacaktır.
        //
        //Shadow Property >>>>>>>> Eğer bir tabloda bir sütun var ama buna karşılık gelen bir propery yok ise; buna shadow property denmektedir. Örnek vermek gerekirse CategoryId bu sınıfta property olarak yok ama veri tabanında kayıtlı olarak duruyor.
        //Shadow Property >> domain driven design pattern'in best practise'lerinden birisidir. Herhangi bir nesne ekleneceği zaman parent'ı üzerinden child'larını ekleyebilirsin demektedir.
        //Eğer domain driven design kullanmıyorsak foreign key'i açık açık yazmak best practise olacaktır.

        //
        //public int Category_Id { get; set; } //Custom isim kullanırsak fluent api aracılığı ile foreign key olduğunu sisteme tanıtalım (2.yol)
        ////public int CategoryId { get; set; } //Convension yaklaşımına göre yani EF Core'un anlayacağı şekilde yazımıdır.
        //public Category Category { get; set; } //Navigation property
        //end

        //one to one start
        // public ProductFeature ProductFeature { get; set; }
        //end

        //one to many data add start
        public int CategoryId { get; set; }
        public Category Category { get; set; } // Product'tan Category'lere erişmemize imkan sağlayacak ve aynı zamanda Category tanımlamamıza da imkan sağlayacak.
        //end

        //one to one data add start
        public ProductFeature ProductFeature { get; set; }
        //end
    }
}
