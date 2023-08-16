using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using KnowIT_TransportIT_webapp.Data;
using Microsoft.AspNetCore.Identity;


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

//add sign-in req
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<UserContext>();


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


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


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
