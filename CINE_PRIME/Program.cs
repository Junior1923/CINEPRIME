using CINE_PRIME.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using CINE_PRIME.Models;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();


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



// -------------------------
// Agregar servicios MVC y sesión
// -------------------------
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        options.LoginPath = "/Account/Login";      // Redirige al login si no está autenticado
//        options.LogoutPath = "/Account/Logout";    // Ruta de logout
//        options.ExpireTimeSpan = TimeSpan.FromHours(2);
//    });

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
