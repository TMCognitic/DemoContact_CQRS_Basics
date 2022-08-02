using DemoContact.Infrastructure;
using DemoContact.Tools.CQRS;
using System.Data.Common;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DemoContact");

// Add services to the container.
builder.Services.AddControllersWithViews();

//Add Queries Handlers and Command Handlers
builder.Services.AddTransient<DbConnection>(sp => new SqlConnection(connectionString));
builder.Services.AddHandlers();
builder.Services.AddScoped<Dispatcher>();

//Add Session Services
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.Name = "DemoContact.Session";
    options.IdleTimeout = TimeSpan.FromHours(1);
});

//Add SessionManager
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<SessionManager>();

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
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
