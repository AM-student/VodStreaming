using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VodStreaming.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("VodStreamingDataContextConnection") ?? throw new InvalidOperationException("Connection string 'VodStreamingDataContextConnection' not found.");

builder.Services.AddDbContext<VodStreamingDataContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<VodStreamingUsers>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<VodStreamingDataContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Authorization

AddAuthorizationPolicies(builder.Services);

#endregion

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
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();

void AddAuthorizationPolicies(IServiceCollection services)
{
    services.AddAuthorization(options =>
    {
        options.AddPolicy("EmployeeOnly", policy => policy.RequireClaim("EmployeeNumber"));
    });
}
