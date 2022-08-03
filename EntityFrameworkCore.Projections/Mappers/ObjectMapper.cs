using AutoMapper;
using EntityFrameworkCore.CodeFirst.DAL;
using EntityFrameworkCore.Projections.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Projections.Mappers
{
    internal class ObjectMapper
    {
        //Eğer bu bir mvc veya api projesi olsaydı bu class'a ihtiyaç yoktu, DI Container'a direkt olarak ekleyebiliyoruz.
        //lazy loading olarak gerçekleştirmiş olduk. İhtiyaç halinde kullanılacak yani.
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() => //Geriye IMapper interface'ini implemente eden ve geriye de herhangi bir parametre istemeyen bir method'u işaret etmemizi istiyor new Lazy<IMapper> dedikten sonra.function'lar bir delegate'dir method'ları işaretleyen. Function'lar nasıl bir method'u işaret eder >>>>> parametre almayan ama geriye de IMapper interface'ini implemente eden bir class'ı istemektedir. Parametre almayacağı için parantez açıp kapadık ve geriye IMapper dönecek.
        {//mapping dosyalarını alt alta birden fazla ekleyebiliriz.
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CustomMapping>();
            });
            return config.CreateMapper(); //lazy bizden geriye IMapper'ı implemente eden bir sınıf dönmemizi istediği için burada bunu yapabiliriz. CreateMapper() metodu IMapper döner.
        });

        //static olan sınıflar uygulama ayağa kalktığı anda yüklenmiş olur fakat biz burada Lazy sınıfını kullandığımız için bu durum gerçekleşmeyecektir.

        public static IMapper Mapper => lazy.Value;
        //Uygulama ayağa kalktığında bu kod blokları çalışmıyor olacak. Ne zaman ObjectMapper üzerinden Mapper'a erişirsem o zaman yukarıdaki kodlar çalışır hale gelecektir.
    }

    internal class CustomMapping:Profile //bu kısım api ve mvc uygulamalarında aynıdır.
    {
        public CustomMapping()
        {
            CreateMap<ProductDto3AutoMapper, Product>().ReverseMap(); //ProductDto3AutoMapper Sınıfını Product sınıfına maple demiş olduk. ReverseMap ile de Product sınıfını da ProductDto3AutoMapper'a maplemiş olduk. Tersi işlem yapmak da mümkün hale geldi.
        }
    }
}
