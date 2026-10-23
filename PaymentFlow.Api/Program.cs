using Microsoft.EntityFrameworkCore;
using PaymentFlow.Domain.Interfaces;
using PaymentFlow.Application.Services;
using PaymentFlow.Infrastructure.Data;
using PaymentFlow.Application.Interfaces;
using PaymentFlow.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Pega a string de conexão chamada DefaultConnection lá do appsettings.json e guarda na variável connectionString
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Registre PaymentFlowContext. Quando alguém pedir um, construa as options usando MySQL,
// com essa connection string, e descubra a versão do servidor automaticamente
builder.Services.AddDbContext<PaymentFlowContext>(options =>
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)
    )
);

// Registra no container de injeção de dependencias o ClienteService .
// Através da inversão de dependencia, quando o construtor receber a interface IClienteService ele vai referencia ao ClienteService.
builder.Services.AddScoped<IClienteService, ClienteService>();

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
