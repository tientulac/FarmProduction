using FarmProductionAPI;
using FarmProductionAPI.Core;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain;
using FarmProductionAPI.Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
var configBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true);
var configuration = configBuilder.Build();
var sqlServerSetting = configuration.GetSection(nameof(SqlServerSetting)).Get<SqlServerSetting>();
builder.Host.UseSerilog((hostContext, services, configuration) =>
{
    configuration.ReadFrom.Services(services).Enrich.FromLogContext();
});
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(IFarmProductionInfrastructureMarker));
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                .AllowAnyMethod();
        });
});

// DEPENDENCY INJECTION
builder.Services.AddDbContext<DataContext>(
                options =>
                {
                    options.UseSqlServer(sqlServerSetting?.ConnectionString, builder => builder.MigrationsAssembly("FarmProductionAPI"));
                    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                });

builder.Services.AddTransient<IRepository<Brand>, BaseRepository<Brand>>();
builder.Services.AddTransient<IRepository<Category>, BaseRepository<Category>>();
builder.Services.AddTransient<IRepository<Order>, BaseRepository<Order>>();
builder.Services.AddTransient<IRepository<OrderItem>, BaseRepository<OrderItem>>();
builder.Services.AddTransient<IRepository<Product>, BaseRepository<Product>>();
builder.Services.AddTransient<IRepository<ProductAttribute>, BaseRepository<ProductAttribute>>();
builder.Services.AddTransient<IRepository<ProductImage>, BaseRepository<ProductImage>>();
builder.Services.AddTransient<IRepository<ProductDescription>, BaseRepository<ProductDescription>>();
builder.Services.AddTransient<IRepository<Role>, BaseRepository<Role>>();
builder.Services.AddTransient<IRepository<UserAccount>, BaseRepository<UserAccount>>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.InstallServicesInAssembly<IFarmProductionInfrastructureMarker>(configuration);

//JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

var app = builder.Build();
app.ConfigureServicesInAssembly<IFarmProductionInfrastructureMarker>();
app.UseCors(
            p =>
            {
                p.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed(_ => true);
            });

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

app.UseStaticFiles();

