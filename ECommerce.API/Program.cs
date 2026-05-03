using ECommerce.Business.Modules.Categories;
using ECommerce.DataAccess.Data;
using ECommerce.DataAccess.Modules.Categories;
using ECommerce.Presentation.Modules.Categories;
using ECommerce.Presentation.Modules.Categories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log_.txt", rollingInterval: RollingInterval.Day, outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] [{SourceContext}] {Message:lj} {NewLine}{Exception}")
    .CreateLogger();
builder.Services.AddSerilog();
builder.Services.AddAutoMapper(cgf => { }, typeof(CategoryMappingProfile).Assembly);
builder.Services.AddDbContext<ECommerceDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<ICategoryViewModelProvider,CategoryViewModelProvider>();
builder.Services.AddScoped<ICategoryService,CategoryService>();
builder.Services.AddScoped<ICategoryRepository ,CategoryRepository>();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.Logger.LogTrace("This is  trace lag");
app.Logger.Log(LogLevel.Debug, "This is debug log");
app.Logger.LogInformation("This is Information log");
app.Logger.LogWarning("This is warning Log");
app.Logger.LogError("This is error log");
app.Logger.LogCritical("This is Critical log");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
        options.RoutePrefix = string.Empty; 
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
