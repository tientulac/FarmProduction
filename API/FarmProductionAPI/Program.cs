using FarmProductionAPI;
using FarmProductionAPI.Domain;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true);
var configuration = configBuilder.Build();
var sqlServerSetting = configuration.GetSection(nameof(SqlServerSetting)).Get<SqlServerSetting>();

builder.Services.AddDbContext<DataContext>(
                options =>
                {
                    options.UseSqlServer(sqlServerSetting?.ConnectionString, builder => builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
                });


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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

