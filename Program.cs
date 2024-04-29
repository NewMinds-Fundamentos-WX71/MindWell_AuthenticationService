using AuthenticationService.Authentication.Domain.Repositories;
using AuthenticationService.Authentication.Domain.Services;
using AuthenticationService.Authentication.Mapping;
using AuthenticationService.Authentication.Persistence.Repositories;
using AuthenticationService.Authentication.Services;
using AuthenticationService.Shared.Persistence.Contexts;
using AuthenticationService.Shared.Persistence.Repositories;
using Consul;
using Microsoft.EntityFrameworkCore;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Database Connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>( options => options.UseSqlite(connectionString)
    .LogTo(Console.WriteLine, LogLevel.Information)
    .EnableSensitiveDataLogging()
    .EnableDetailedErrors());

// Add lowercase routes
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Dependency injection
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// AutoMapper Configuration
builder.Services.AddAutoMapper(
    typeof(ModelToResourceProfile),
    typeof(ResourceToModelProfile));


var consulConfig = new ConsulClientConfiguration
{
    Address = new Uri("http://localhost:8500") // Cambia esto por la dirección de tu agente Consul
};

var consulClient = new ConsulClient(consulConfig);

var registration = new AgentServiceRegistration
{
    ID = "mindwell-service-id", // Cambia esto por un ID único para tu servicio en Consul
    Name = "Authentication-service", // Nombre de tu servicio
    Address = "localhost", // Dirección donde se ejecuta tu servicio
    Port = 5118, // Puerto en el que se ejecuta tu servicio (puerto de Swagger)
    Tags = new[]
    {
        "http://localhost:5118/api/v1/patients"
    }, 
    
    Check = new AgentServiceCheck
    {
    HTTP = "http://localhost:5118/swagger/index.html", 
    Interval = TimeSpan.FromSeconds(10) 
    }
};

await consulClient.Agent.ServiceRegister(registration);

var app = builder.Build();





// Validation for ensuring Database Objects are created
using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<AppDbContext>())
{
    context.Database.EnsureCreated();
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