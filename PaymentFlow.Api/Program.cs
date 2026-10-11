using Microsoft.EntityFrameworkCore;
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

var app = builder.Build();

// Obtendo o Context após o build
// O DbContext é registrado com escopo (scoped), então você não pode pegá-lo direto de app.Services

using(var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PaymentFlowContext>();

    if(!await context.Database.CanConnectAsync())
    {
        throw new InvalidOperationException("Não foi possivel conectar ao banco de dados");
    }
}

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
