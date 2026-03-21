using ControleGastos.Api.Db;
using ControleGastos.Api.Repositories;
using ControleGastos.Api.Services;
using ControleGastos.Core.CoreViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System.Data;


var builder = WebApplication.CreateBuilder(args);

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

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .SelectMany(x => x.Value.Errors)
            .Select(x => x.ErrorMessage)
            .ToList();

        var response = new ResultViewModel<object>
        {
            Success = false,
            Data = null,
            Errors = errors
        };

        return new BadRequestObjectResult(response);
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("WWW-Authenticate");
    });
});

var app = builder.Build();

app.UseCors("AllowAll");



using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DbInicializer>();
    db.Initialize();
}

// PEGA DO APPSETTINGS
if (builder.Configuration.GetValue<bool>("EnableSwagger"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
