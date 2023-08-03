using Microsoft.EntityFrameworkCore;

using SportsStore.Data;
using SportsStore.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<StoreDbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:StortsStoreConnection"]);
});

//AddScoped method creates a service where each HTTP requests gets its own repository object
builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();
builder.Services.AddScoped<IOrderRepository, EFOrderRepository>();

builder.Services.AddRazorPages();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddServerSideBlazor();

//ASP.NET Core receives HTTP requests and passes them along a request pipeline, which is populated with middleware components registered using the app property.
var app = builder.Build();

app.UseStaticFiles();

app.UseSession();

app.MapControllerRoute("catpage",
"{category}/Page{productPage:int}",
new { Controller = "Home", action = "Index" });

app.MapControllerRoute("page", "Page{productPage:int}",
new { Controller = "Home", action = "Index", productPage = 1 });

app.MapControllerRoute("category", "{category}",
new { Controller = "Home", action = "Index", productPage = 1 });

app.MapControllerRoute("pagination",
"Products/Page{productPage}",
new { Controller = "Home", action = "Index", productPage = 1 });

app.MapDefaultControllerRoute();
app.MapRazorPages();

app.MapBlazorHub();
app.MapFallbackToPage("/admin/{*catchall}", "/Admin/Index");

SeedData.EnsurePopulated(app);

app.Run();
