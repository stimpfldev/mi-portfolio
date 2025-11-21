using PersonalWeb.Services;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// Desactivar antiforgery completamente para Render
builder.Services.AddSingleton<Microsoft.AspNetCore.Antiforgery.IAntiforgery, PersonalWeb.Services.NoAntiforgery>();

// Email Service
builder.Services.AddSingleton<EmailService>();

var app = builder.Build();

// Error handling
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
