using EntityFrameworkCore.Query.DAL;
using EntityFrameworkCore.Query.Models;
using EntityFrameworkCore.Relationships.DAL;
using EntityFrameworkCore.StoredProcedureAndFunction.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EntityFrameworkCore.CodeFirst.DAL
{
    public class AppDbContext:DbContext
    {
       
        //public DbSet<Person> People { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> productFeatures { get; set; }
        public DbSet<ProductFull> productFulls { get; set; }

        public DbSet<ProductFull2> productFull2s { get; set; }
        public DbSet<ProductFullForFunction> productFullForFunctions { get; set; }
        public DbSet<ProductFullForFunction2> productFullForFunction2s { get; set; }
        public DbSet<ProductCount> productCounts { get; set; }

        //public DbSet<ProductEseential> productEseentials { get; set; }
        //public DbSet<ProductWithFeature> productWithFeatures { get; set; }

        //public DbSet<ProductFull> productFulls { get; set; }

        //public DbSet<Student> Students { get; set; }
        //public DbSet<Teacher> Teachers { get; set; }
        //

        //public DbSet<BasePerson> Persons { get; set; } 

        //>>>>>>>>>function konusu önemli
        //oop'ye göre daha düzenli ve best practise olması için function'ları model üzerinden maplemek yerine method içerisinde yazmamız gerekmektedir.
        public IQueryable<ProductWithFeaturesFunctionMethod> GetProductWithFeaturesFunctions(int categoryId) => FromExpression(()=> GetProductWithFeaturesFunctions(categoryId)); //Eğer method içerisinde tek satırlık bir kod var ise; lambda ile direkt olarak kodu yazabiliriz.
        
        public IQueryable<ProductWithFeaturesFunctionMethodColorParameter> GetProductWithFeaturesFunctionMethodColorParameters(string color) => FromExpression(()=> GetProductWithFeaturesFunctionMethodColorParameters(color));

        public IQueryable<ProductWithFeaturesFunctionMethodColorParameter> GetProductWithFeaturesFunctionMethodNameParameters(string name) => FromExpression(() => GetProductWithFeaturesFunctionMethodNameParameters(name));
        public IQueryable<ProductWithFeaturesFunctionMethodColorParameter> GetProductWithFeaturesFunctionMethodColorAndCategoryParameters(string color, int categoryId) => FromExpression(() => GetProductWithFeaturesFunctionMethodColorAndCategoryParameters(color, categoryId));
        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        //scalar value function
        public int GetProductCount(int categoryId)
        {
            //  throw new Exception(); //bunun nedeni>>> bu şekilde bir tanımlama yapıldığı zaman, bu metodu direkt olarak ef core üzerinden kullanırsak hata almak için yazdık. Çünkü ef core'un geriye tek bir değer dönen function'ları tek bir seferde kullanmamıza imkan vermiyor. Örnek program.cs kodu >> "var count = context.GetProductCount(1);"
            //geriye int,decimal gibi tek bir scalar değer dönen function'ları mutlaka ef core'un içerisinde kullanmamız gerekmektedir.
            throw new NotSupportedException("Bu method ef core tarafından çağrılmaktadır."); //ef core dokümantasyonunda bu verilir.
            
        }
        //--------------------------
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            Initializer.Build();
             optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information).UseSqlServer(Initializer.Configuration.GetConnectionString("SqlCon"));
          

        }
        public override int SaveChanges()
        {

            return base.SaveChanges();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductFull>().HasNoKey();
            modelBuilder.Entity<ProductFull2>().HasNoKey();
            modelBuilder.Entity<ProductFullForFunction>().ToFunction("fc_product_full");
            modelBuilder.HasDbFunction(typeof(AppDbContext).GetMethod(nameof(GetProductWithFeaturesFunctions), new[] { typeof(int) })!).HasName("fc_product_full_parameter");
            modelBuilder.HasDbFunction(typeof(AppDbContext).GetMethod(nameof(GetProductWithFeaturesFunctionMethodColorParameters), new[] { typeof(string) })!).HasName("fc_product_with_color_parameter");
            modelBuilder.HasDbFunction(typeof(AppDbContext).GetMethod(nameof(GetProductWithFeaturesFunctionMethodNameParameters), new[] { typeof(string) })!).HasName("fc_product_full_parameter2");
            modelBuilder.HasDbFunction(typeof(AppDbContext).GetMethod(nameof(GetProductWithFeaturesFunctionMethodColorAndCategoryParameters), new[] { typeof(string), typeof(int) })!).HasName("fc_color_not_null_and_category_1");


            //parantez içerisinde önce tipini vermemiz lazım. Bir reflection yapılacaktır. İlgili metodun bulunduğu sınıfın tipini veriyoruz. Bu örnekteki sınıf AppDbContext oluyor çünkü bizim "GetProductWithFeaturesFunctions(int categoryId)" metodumuz bu sınıfta. Burada bizden bir reflection ile ilgili metoda erişmemizi istiyor.
            //Daha sonrasında tipini belirttiğimiz yeri, nokta ile beraber metodunu veriyoruz.
            //Ardından virgül ile alacağı parametreyi giriyoruz. Virgülden sonra "new" ile beraber "[]" bir dizin belirtiyoruz çünkü bir metot birden fazla parametre de alabilmektedir. Fakat bizim burada yapacağımız örnekte tek bir parametre almaktadır oluşturulan metot.
            //ünlem işaretini nullabler olabilme durumundan dolayı koyuyoruz. Kod tarafında herhangi bir farklılık yaratmaz, sadece altındaki yeşil çizgiyi yok eder.
            //En son "HasName" ifadesi ile hangi function'a mapleneceğini vermemiz gerekmektedir.
            //>>>
            //eğer bu yöntemi kullanırsak dbset olarak girmeyeceğimiz için herhangi bir migration yaparken ilgili model'i silmek zorunda kalmayız.
            modelBuilder.HasDbFunction(typeof(AppDbContext).GetMethod(nameof(GetProductCount), new[] { typeof(int) })!).HasName("fc_get_product_count"); //scalar value function
            modelBuilder.Entity<ProductCount>().HasNoKey();//function5
            base.OnModelCreating(modelBuilder);
        }

    }
   
}
