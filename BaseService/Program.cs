using Base;
using BaseService.DataContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var connectionString = EnvConstants.DbConnection!;
var serverVersion = new MySqlServerVersion(new Version(8, 0, 25));
builder.Services.AddDbContext<DBContext>(
    dbContextOptions => dbContextOptions
        .UseMySql(connectionString, serverVersion,
            builder =>
                builder.MigrationsAssembly(
                    "BaseService"))
        .EnableDetailedErrors());
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