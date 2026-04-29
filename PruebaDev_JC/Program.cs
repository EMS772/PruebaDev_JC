using Microsoft.EntityFrameworkCore;
using PruebaDev_JC.Models.Data;
using PruebaDev_JC.Repositories;
using PruebaDev_JC.Repositories.Interfaces;
using PruebaDev_JC.Services;
using PruebaDev_JC.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CalculadoraDB")));

builder.Services.AddScoped<ICalculadoraRepository, CalculadoraRepository>();
builder.Services.AddScoped<ICalculadoraService, CalculadoraService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();