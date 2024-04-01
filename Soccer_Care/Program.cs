using Blazored.Toast;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Soccer_Care.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SoccerCareDbContext>(option => {
    option.UseSqlServer(builder.Configuration.GetConnectionString("SoccerCareConnection"));
    option.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
 });

builder.Services.AddIdentity<UserModel, IdentityRole>().AddDefaultUI().AddDefaultTokenProviders().AddEntityFrameworkStores<SoccerCareDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddSession(opt => opt.IdleTimeout = TimeSpan.FromHours(1));
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.MapRazorPages();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "User", "Admin", "Partner"};
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

//Add admin area 
app.MapAreaControllerRoute(
name: "Admin",
areaName: "Admin",
pattern: "Admin/{controller=Admin}/{action=Index}/{id?}");

app.MapControllerRoute(
name: "default",
pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
