using Microsoft.EntityFrameworkCore;
using Streamberry.Data;
using Streamberry.Repository.Interfaces;
using Streamberry.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<StreamberryDbContexto>
    (options => options.UseMySql(
        builder.Configuration.GetConnectionString("DataBase"),
        Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.34-mysql")));

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();
builder.Services.AddScoped<IStreamingRepository, StreamingRepository>();
builder.Services.AddScoped<IFilmeRepository, FilmeRepository>();
builder.Services.AddScoped<IStreamingFilmeRepository, StreamingFilmeRepository>();

builder.Services.AddAuthentication("Identity.Login")
    .AddCookie("Identity.Login", config =>
    {
        config.Cookie.Name = "Identity.Login";
        config.LoginPath = "/Login";
        config.AccessDeniedPath = "/Home";
        config.ExpireTimeSpan = TimeSpan.FromHours(1);
    });

builder.Services.AddSession(options =>
{
    options.Cookie.Name = "Streamberry.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(128); //QUASE 12 HORAS
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
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
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();

public partial class Program { }