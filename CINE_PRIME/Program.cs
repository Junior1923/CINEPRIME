using CINE_PRIME.Data;
using CINE_PRIME.Interfaces;
using CINE_PRIME.Models;
using CINE_PRIME.Models.Tmdb;
using CINE_PRIME.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


#region DBCONTEXT CONFIGURATION
//Add service to DbContext
builder.Services.AddDbContext<CinePrimeContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion

#region IDENTITY SERVICE CONFIGURATION
//Add Identity services
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => 
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
})
    .AddEntityFrameworkStores<CinePrimeContext>()
    .AddDefaultTokenProviders();
#endregion

#region TMDB API SERVICE CONFIGUTATION
//--Inyenccion de dependencias para el servicio TMDB API--//

//Vincula la sección JSON de appsettings.json con la clase C# TmdbSettings
builder.Services.Configure<TmdbSettings>(builder.Configuration.GetSection("TmdbSettings"));

//Registra el servicio HTTP para ITmdbService y su implementación TmdbService
builder.Services.AddHttpClient<ITmdbService, TmdbService>();

#endregion

#region FAVORITO SERVICE CONFIGURATION
//--Inyección de dependencias para el servicio Favorito--//

builder.Services.AddScoped<IFavoritoService, FavoritoService>();

#endregion


builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();


var app = builder.Build();

// -------------------------
// Middleware
// -------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
