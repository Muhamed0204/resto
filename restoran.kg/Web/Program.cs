using Microsoft.AspNetCore.Authentication.Google;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices(builder.Configuration);

// Добавление аутентификации через Google
builder.Services.AddAuthentication()
    .AddGoogle(options =>

    {
        IConfigurationSection googleAuthNSection =
            builder.Configuration.GetSection("Google");

        options.ClientId = googleAuthNSection["ClientId"];
        options.ClientSecret = googleAuthNSection["ClientSecret"];
        options.CallbackPath = "/signin-google";
        
    });

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Включаем аутентификацию и авторизацию
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Если используешь Razor Pages (например, Identity по шаблону)
app.MapRazorPages();

app.Run();
