using challenge.src.Application.Ventas;
using challenge.src.Infrastructure.Ventas;
using challenge.src.Infrastructure.Modelos;
using challenge.src.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IVentaBusiness, VentaBusiness>();
builder.Services.AddScoped<IVentaRepository, VentaRepository>();
builder.Services.AddScoped<IModeloRepository, ModeloRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Middleware para medir el tiempo de ejecución de cada request
app.UseMiddleware<ExecutionTimeMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
