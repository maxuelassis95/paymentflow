using Microsoft.EntityFrameworkCore;
using PaymentFlow.Application.Services;
using PaymentFlow.Infrastructure.Data;

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

builder.Services.AddScoped<ClienteService>();

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
