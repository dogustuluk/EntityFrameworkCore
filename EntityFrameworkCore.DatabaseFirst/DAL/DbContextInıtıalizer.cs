using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.DatabaseFirst.DAL
{//Bu sınıfta veri tabanı ile ilgili kodları belirtiyoruz. Veri tabanı bağlantısı veya diğer ayalar burada olacak.
    //İlk olarak veri tabanı yoluna erişmemiz gerekiyor. Bunun için aşağıdaki interface'i tanımlıyoruz.
    
    public class DbContextInıtıalizer
    {
        public static IConfigurationRoot Configuration; //appsettings.json dosyasını okumak için yazarız.

        //Ardından AppDbContext'teki options nesnesini belirtmemiz gerekmektedir.
        public static DbContextOptionsBuilder<AppDbContext> OptionsBuilder; //veri tabanı ile ilgili options'ları belirteceğimiz yerdir.
        
        public static void Build() //static olarak tanımlamamızın sebebi ise>>>>>> içerisinde bulunduğu sınıftan bir nesne örneği almak istemiyoruz, nokta ifadesi ile erişebilmek istiyoruz. Nesne örneğini almamıza gerek yok çünkü içerisinde static ifadelerimiz olacaktır. Çünkü uygulama ayağa kalktığı zaman veri tabanı ile ilgili ayarlarımız da bir kere set edilecek.
        {
            //öncelikle appsettings.json dosyamızı almamız gerekmektedir.
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            //Koda baktığımızda ilk olarak bir nesne örneği üretilir. Ardından okunacak olan dosya yolu belirtilir(Klasörü direkt olarak verdik yani uygulamanın çalıştığı klasörü aldık >> GetCurrentDirectory <<). Daha sonra bu klasöre bir json dosyası ekle diyoruz. Burada "appsetting.json" dosyasını almamızı belirtiyoruz. optional ifadesini true olarak set ediyoruz; yani dosya olabilir  de olmayabilir de anlamına gelir. reloadOnChange ifadesi ise bizim bu dosya üzerinde yaptığımız her bir değişiklikte dosya tekrardan yüklenip yüklenilmeyeceğinin ayarı yapılır.
            Configuration = builder.Build(); //okunacak olan dosyayı hazır hale getiren kod.
            //>>>>   Uygulamanın herhangi bir yerinde appsettings.json dosyasının içerisindeki istediğimiz bir key-value çiftini okumak istersek Configuration sınıfı üzerinden okuyabiliriz.

            //>> bu kısım 2.yöntemi kullandığımız taktirde kaldırılmalı.
            //OptionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            //OptionsBuilder.UseSqlServer(Configuration.GetConnectionString("SqlCon"));
            //<<
        }
    }
}
