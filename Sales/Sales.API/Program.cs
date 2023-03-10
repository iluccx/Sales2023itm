using Microsoft.EntityFrameworkCore;
using Sales.API.Data;
using Sales.API.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); //Redundancia ciclica
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("name=DockerConnection")); //Expresion lambda para decirle cual es mi conexion
builder.Services.AddTransient<SeedDb>();
builder.Services.AddScoped<IApiService, ApiService>();

var app = builder.Build();

SeedData(app);

void SeedData(WebApplication app)
{
    IServiceScopeFactory? scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (IServiceScope? scope = scopedFactory!.CreateScope())
    {
        SeedDb? service = scope.ServiceProvider.GetService<SeedDb>();
        service!.SeedAsync().Wait();

        SeedDb? serviceCategory = scope.ServiceProvider.GetService<SeedDb>();
        serviceCategory!.SeedCategoryAsync().Wait();
    }
}

void SeedDb(WebApplication app)
{
    throw new NotImplementedException();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());
//aqui le pongo seguridad al sistema, puedo bloquear ips

app.Run();
