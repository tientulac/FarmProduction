using FarmProductionAPI;
using FarmProductionAPI.Core;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain;
using FarmProductionAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var configBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true);
var configuration = configBuilder.Build();
var sqlServerSetting = configuration.GetSection(nameof(SqlServerSetting)).Get<SqlServerSetting>();
builder.Host.UseSerilog((hostContext, services, configuration) => {
    configuration.ReadFrom.Services(services).Enrich.FromLogContext();
});
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(IFarmProductionInfrastructureMarker));
builder.Services.AddAutoMapper(typeof(Program));
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("http://example.com",
                                              "http://www.contoso.com");
                      });
});

// DEPENDENCY INJECTION
builder.Services.AddDbContext<DataContext>(
                options =>
                {
                    options.UseSqlServer(sqlServerSetting?.ConnectionString, builder => builder.MigrationsAssembly("FarmProductionAPI"));
                });

builder.Services.AddTransient<IRepository<Brand>, BaseRepository<Brand>>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.InstallServicesInAssembly<IFarmProductionInfrastructureMarker> (configuration);

var app = builder.Build();
app.ConfigureServicesInAssembly<IFarmProductionInfrastructureMarker>();
app.UseCors(MyAllowSpecificOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

