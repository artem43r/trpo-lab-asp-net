
using laba1.Data;
using laba1.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Добавление MVC
builder.Services.AddControllersWithViews();
// РЕГИСТРАЦИЯ КОНТЕКСТА БАЗЫ ДАННЫХ
builder.Services.AddDbContext<AppDbContext>(options =>

options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
 .LogTo(Console.WriteLine, LogLevel.Information) // Логирование SQL
 .EnableSensitiveDataLogging() // Показывать параметры
);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();
builder.Services.AddScoped<IProductRepository, EfProductRepository>();
//builder.Services.AddSingleton<IClientRepository, InMemoryClientRepository>();
builder.Services.AddScoped<IClientRepository, EfClientRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await SeedData.InitializeAsync(dbContext);
}

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

app.MapControllerRoute(
    name: "about",
    pattern: "about-us",
    defaults: new { controller = "Home", action = "Privacy" });

app.MapControllerRoute(
    name: "userProfile",
    pattern: "user/{username}/{action=Profile}",
    defaults: new { controller = "Demo" });

app.MapControllerRoute(
    name: "product",
    pattern: "product/{id:int}",
    defaults: new { controller = "Demo", action = "ProductDetails" });

app.Run();
