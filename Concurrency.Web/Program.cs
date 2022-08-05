//CONCURRENCY START
//birden fazla kullan�c� ayn� zamanda ayn� datay� g�ncellemeye �al���rsa ne olur? > temel soru
//iki tip concurrency vard�r >> 1)Pessimistic concurrency control (xlock),  2) Optimistic concurrency control
//1)Pessimistic concurrency control (xlock) >>>>>>>>>>>>> ef core'un default olarak bu tipi yoktur. Pek iyi bir �ey olmad��� bilinmektedir. Herhangi bir query'e 'xlock' yaz�l�r. Bu sayede bir ba�ka transaction okuma dahi yapamaz. dolay�s�yla performans� ciddi bir �ekilde d���r�r. bu y�zden tercih edilmemektedir.
//2)Optimistic concurrency control >>>>> Ef core'da default olarak vard�r.
//>>>>>>>>>>>>>>>>>>>>>>>>>>bu �al��mada ikinci tip olacakt�r.
//E�er ayn� milisaniyede gelirse sql server rastgele bir �ekilde gelen g�ncellemeyi se�ip al�r.
//ilk transaction sonras�nda gelen transaction yapt��� i�lem ile ilk transaction'� ezebilir. Yani her iki transaction'dan en son gelen transaction, ilk transaction'daki de�erleri ezebilir. Buna >>>> Client Wins ya da Last in Wins scenario (automatically) <<<< denmektedir.
//E�er g�ncelleme esnas�nda ilgili alan�n bir ba�ka transaction'�n g�ncelledi�ini ve yine de bunu ezmek isteyip istemedi�imizi kuraca��m�z mekanizman�n ad�na >>>>>>>>>>>>>>>>>>> Store Wins denmektedir.
//Store Wins >>>>>>>>>>>> "DbConcurrencyException", "Row version"<<<<<<<<<<<<<<
//Row version ile bir nevi g�ncelleme s�r�m�n� kontrol edebiliriz. Ef Core Row version'� kendisi update edebiliyor yani her g�ncelleme esnas�nda kendisi bu alana bir atama yapacakt�r ek olarak biz Rowversion i�in bir kodlama yapmam�za gerek yoktur. Sadece ilgili tabloya Rowversion ad�nda bir alan a�mam�z gerekmektedir.

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
