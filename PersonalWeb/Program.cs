using PersonalWeb.Services;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// EmailService (solo una vez, como singleton)
builder.Services.AddSingleton<EmailService>();

var app = builder.Build();

// Error handling en producción
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

// Archivos estáticos
app.UseStaticFiles();

// Routing
app.UseRouting();

// (No usás autenticación)
app.UseAuthorization();

// Default Route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
