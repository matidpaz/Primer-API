var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

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
}

app.UseCors(MyAllowOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
