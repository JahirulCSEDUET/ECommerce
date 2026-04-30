using ECommerce.Business.Modules.Carts;
using ECommerce.Business.Modules.Categories;
using ECommerce.Business.Modules.Orders;
using ECommerce.Business.Modules.Products;
using ECommerce.DataAccess.Data;
using ECommerce.DataAccess.Identity;
using ECommerce.DataAccess.Modules.Carts;
using ECommerce.DataAccess.Modules.Categories;
using ECommerce.DataAccess.Modules.Orders;
using ECommerce.DataAccess.Modules.Products;
using ECommerce.Presentation.Modules.Carts;
using ECommerce.Presentation.Modules.Categories;
using ECommerce.Presentation.Modules.Categories.Interfaces;
using ECommerce.Presentation.Modules.Orders;
using ECommerce.Presentation.Modules.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
//Logging related configuration 
//builder.Logging.ClearProviders();
//builder.Logging.AddDebug();
//builder.Logging.AddConsole();

//Serilog configuration
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log_.txt", rollingInterval: RollingInterval.Day, outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] [{SourceContext}] {Message:lj} {NewLine}{Exception}")
    .CreateLogger();
builder.Services.AddSerilog();

//AutoMapper 
builder.Services.AddAutoMapper(cfg => { }, typeof(CategoryMappingProfile).Assembly);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ECommerceDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
//Session Relted Services
builder. Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly =true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();

//Authentication
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<ECommerceDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddScoped<IdentityRoleSeeder>();

//Creating Policy
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("FullNameOnly", policy => policy.RequireClaim("FullName","Super Admin")); 
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Error/Unauthorized";
});
//Interface register
builder.Services.AddScoped<ICategoryViewModelProvider, CategoryViewModelProvider>();
builder.Services.AddScoped<ICategoryService, CategoryService> ();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<IProductViewModelProvider, ProductViewModelProvider>();
builder.Services.AddScoped<IProductService, ProductService> ();
builder.Services.AddScoped<IProductRepository,ProductRepository>();

builder.Services.AddScoped<ICartRepository, SessionCartRepository>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICartViewModelProvider, CartViewModelProvider>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderViewModelprovider, OrderViewModelProvider>();
builder.Services.AddScoped<ICheckoutViewModelProvider, CheckoutViewModelProvider>();

var app = builder.Build();

app.Logger.LogTrace("This is  trace lag");
app.Logger.Log(LogLevel.Debug, "This is debug log");
app.Logger.LogInformation("This is Information log");
app.Logger.LogWarning("This is warning Log");
app.Logger.LogError("This is error log");
app.Logger.LogCritical("This is Critical log");

app.UseStatusCodePagesWithReExecute("/Error/StatusCode","?statusCode={0}");
//app.UseExceptionHandler("/Error/ServerError");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error/ServerError");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapControllerRoute(
    name: "admin",
    pattern: "admin/{controller=Home}/{action=Index}/{id?}", 
    defaults: new { area = "Admin" }, 
    constraints: new {area="Admin"});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using(var scope = app.Services.CreateScope())
{
    var seed = scope.ServiceProvider.GetRequiredService<IdentityRoleSeeder>();
    await seed.SeedAsync();

}
app.Run();
