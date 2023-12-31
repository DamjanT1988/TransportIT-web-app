using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using KnowIT_TransportIT_webapp.Data;
using Microsoft.AspNetCore.Identity;
using System.Configuration;


//create builder
var builder = WebApplication.CreateBuilder(args);

//create db connection
builder.Services.AddDbContext<TicketsContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("TicketsContext") ?? throw new InvalidOperationException("Connection string 'TicketsContext' not found.")));

builder.Services.AddDbContext<BillingContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BillingContext") ?? throw new InvalidOperationException("Connection string 'BillingContext' not found.")));

var connectionString = builder.Configuration.GetConnectionString("UserContext") ?? throw new InvalidOperationException("Connection string 'UserContex' not found.");
builder.Services.AddDbContext<UserContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddDbContext<PassangerContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("PassangerContext") ?? throw new InvalidOperationException("Connection string 'PassangerContext' not found.")));

builder.Services.AddDbContext<FreeDayContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("FreeDayContext") ?? throw new InvalidOperationException("Connection string 'FreeDayContext' not found.")));

//add sign-in req
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<UserContext>();


// Register the BillingService
builder.Services.AddScoped<Service>();  // Registering the Service


// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Razor Pages support.
builder.Services.AddRazorPages();


//add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});


//build the app
var app = builder.Build();


//use CORS
app.UseCors("CorsPolicy");

//use HTTPS redirections
app.UseHttpsRedirection();

//use files in wwwroot etc
app.UseStaticFiles();

//use routing
app.UseRouting();

//use authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

//Razor
app.MapRazorPages();

//map the controller routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=AdminBillings}/{action=Index}/{id?}");

//run the app
app.Run();
