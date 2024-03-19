using Blazored.Toast;
using Microsoft.EntityFrameworkCore;
using Soccer_Care.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SoccerCareDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("SoccerCareConnection")));
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

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

//Add admin area 
app.MapAreaControllerRoute(
name: "Admin",
areaName: "Admin",
pattern: "Admin/{controller=Admin}/{action=Index}/{id?}");

app.MapControllerRoute(
name: "default",
pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
