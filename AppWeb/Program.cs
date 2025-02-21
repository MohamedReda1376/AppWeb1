using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using AppWeb;
using AppWeb;

var builder = WebApplication.CreateBuilder(args);

// Ajout des services à l’application
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddRazorPages(); // Ajout du support des Razor Pages (nécessaire pour éviter l’erreur 404)

// Construction de l’application
var app = builder.Build();

// Configuration du pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapRazorPages(); // Assure que Razor Pages est bien accessible
app.MapDefaultControllerRoute(); // Active les routes pour les contrôleurs MVC

app.Run();