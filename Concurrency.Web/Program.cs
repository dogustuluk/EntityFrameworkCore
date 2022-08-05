//CONCURRENCY START
//birden fazla kullanýcý ayný zamanda ayný datayý güncellemeye çalýþýrsa ne olur? > temel soru
//iki tip concurrency vardýr >> 1)Pessimistic concurrency control (xlock),  2) Optimistic concurrency control
//1)Pessimistic concurrency control (xlock) >>>>>>>>>>>>> ef core'un default olarak bu tipi yoktur. Pek iyi bir þey olmadýðý bilinmektedir. Herhangi bir query'e 'xlock' yazýlýr. Bu sayede bir baþka transaction okuma dahi yapamaz. dolayýsýyla performansý ciddi bir þekilde düþürür. bu yüzden tercih edilmemektedir.
//2)Optimistic concurrency control >>>>> Ef core'da default olarak vardýr.
//>>>>>>>>>>>>>>>>>>>>>>>>>>bu çalýþmada ikinci tip olacaktýr.
//Eðer ayný milisaniyede gelirse sql server rastgele bir þekilde gelen güncellemeyi seçip alýr.
//ilk transaction sonrasýnda gelen transaction yaptýðý iþlem ile ilk transaction'ý ezebilir. Yani her iki transaction'dan en son gelen transaction, ilk transaction'daki deðerleri ezebilir. Buna >>>> Client Wins ya da Last in Wins scenario (automatically) <<<< denmektedir.
//Eðer güncelleme esnasýnda ilgili alanýn bir baþka transaction'ýn güncellediðini ve yine de bunu ezmek isteyip istemediðimizi kuracaðýmýz mekanizmanýn adýna >>>>>>>>>>>>>>>>>>> Store Wins denmektedir.
//Store Wins >>>>>>>>>>>> "DbConcurrencyException", "Row version"<<<<<<<<<<<<<<
//Row version ile bir nevi güncelleme sürümünü kontrol edebiliriz. Ef Core Row version'ý kendisi update edebiliyor yani her güncelleme esnasýnda kendisi bu alana bir atama yapacaktýr ek olarak biz Rowversion için bir kodlama yapmamýza gerek yoktur. Sadece ilgili tabloya Rowversion adýnda bir alan açmamýz gerekmektedir.

//CONCURRENCY END




using Concurrency.Web.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
