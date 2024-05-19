using Microsoft.AspNetCore.Authentication.Cookies;
using NEXUS.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(). AddRazorRuntimeCompilation();
builder.Services.AddDbContext<NexusDbContext>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
			   op =>
			   {
				   op.LoginPath = "/Home/login";
				   op.AccessDeniedPath = "/Home/login";
				   op.ExpireTimeSpan = TimeSpan.FromMinutes(60);
			   }
			   );

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Admin}/{action=IndexAdmin}/{id?}");

app.Run();
