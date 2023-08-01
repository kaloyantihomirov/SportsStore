using Microsoft.EntityFrameworkCore;
using SportsStore.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<StoreDbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:StortsStoreConnection"]);
});

//AddScoped method creates a service where each HTTP requests gets its own repository object
builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();

var app = builder.Build();

app.UseStaticFiles();

app.MapControllerRoute("pagination", "Products/Page{productPage}", new { Controller = "Home", action = "Index" });
app.MapDefaultControllerRoute();

SeedData.EnsurePopulated(app);

app.Run();
