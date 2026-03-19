using ControleGastos.Api.Db;
using ControleGastos.Api.Repositories;
using ControleGastos.Api.Services;
using Microsoft.Data.Sqlite;
using System.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddTransient<IDbConnection>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var relativePath = configuration.GetConnectionString("DefaultConnection");

    var basePath = Directory.GetCurrentDirectory();

    var dbPath = relativePath.Replace("Data Source=", "");

    var fullPath = Path.Combine(basePath, dbPath);

    var directory = Path.GetDirectoryName(fullPath);
    if (!Directory.Exists(directory))
    {
        Directory.CreateDirectory(directory);
    }

    var connectionString = $"Data Source={fullPath}";

    return new SqliteConnection(connectionString);
});

builder.Services.AddTransient<DbInicializer>();

//Repositories
builder.Services.AddScoped<PessoaRepository>();
builder.Services.AddScoped<CategoriaRepository>();
builder.Services.AddScoped<TransacaoRepository>();

//Services
builder.Services.AddScoped<PessoaService>();
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<TransacaoService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DbInicializer>();
    db.Initialize();
}

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
