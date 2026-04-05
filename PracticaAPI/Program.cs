using Microsoft.AspNetCore.OpenApi;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using PracticaAPI.Middlewares;
using Scalar.AspNetCore;
using System.Reflection;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLogging(); //Con esto agrego la referencia de los servicio que tiene que ver con Logging
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen(options => 
{
    //Configuracion de titulo, descripcion y contacto
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API de Pronostico Meteorologico",
        Version = "v1",
        Description = "Esta API permite gestionar el clima de una lista local.",
        Contact = new OpenApiContact
        {
            Name = "Mi nombre",
            Email = "miemail@aprendiendoapis.com"
        }
    });
    //Esto es para que me agregue los comentarios de los controllers a la documentacion
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

var MyAllowOrigins = "MyAllowOrigins";

builder.Services.AddCors(opcions => 
{
    opcions.AddPolicy(name: MyAllowOrigins,
        p =>
        {
            p.AllowAnyHeader();
            p.AllowAnyOrigin();
            //p.WithOrigins() => Para especificar los origenes con permiso (como realmente deberia hacerse)
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

//Custom Middlewares
app.UseRequestLogging();

app.UseResponseTime();

app.MapControllers();

app.Run();
