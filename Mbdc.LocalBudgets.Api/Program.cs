using System.Security.Authentication;
using Mapster;
using MbdcLocalBudgetsApi;
using MbdcLocalBudgetsApi.Middlewares;
using MbdcLocalBudgetsApplication;
using MbdcLocalBudgetsDomain.Persistence;
using MbdcLocalBudgetsInfrastructure.MongoDb;
using MbdcLocalBudgetsPresentation;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Scrutor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddApplicationPart(PresentationLayerAssemblyMarker.Assembly)
    .AddJsonOptions(o => o.JsonSerializerOptions.PropertyNameCaseInsensitive = true);

// Register Middlewares as Services
builder.Services.Scan(scan => scan
    .FromAssemblies(WebApiAssemblyMarker.Assembly)
    .AddClasses(classes => classes.Where(x => x.Name.EndsWith("Middleware")))
    .UsingRegistrationStrategy(RegistrationStrategy.Throw)
    .AsSelf()
    .WithScopedLifetime());

// Add MongoDB
builder.Services.AddScoped<IMongoClient>(_ =>
{
    var mongoConnectionString = builder.Configuration.GetConnectionString("Mongo");
    var settings = MongoClientSettings.FromConnectionString(mongoConnectionString);
    settings.SslSettings = new SslSettings
    {
        EnabledSslProtocols = SslProtocols.Tls12
    };
    return new MongoClient(settings);
});

builder.Services.AddScoped<IMongoDatabase>(provider =>
{
    var mongoClient = provider.GetRequiredService<IMongoClient>();
    return mongoClient.GetDatabase("localBudgets");
});

builder.Services.AddScoped<IReportingDbContext, MongoReportingDbContext>();

builder.Services.AddSwaggerGen(o => o.SwaggerDoc("v1", new OpenApiInfo { Title = "LocalBudgetsAnalysis", Version = "v1" }));

// Repositories
builder.Services.Scan(scan => scan
    .FromAssemblies(ApplicationLayerAssemblyMarker.Assembly)
    .AddClasses(filter => filter.Where(x => x.Name.EndsWith("Repository")))
    .UsingRegistrationStrategy(RegistrationStrategy.Throw)
    .AsImplementedInterfaces()
    .WithScopedLifetime());

// Services
builder.Services.Scan(scan => scan
    .FromAssemblies(ApplicationLayerAssemblyMarker.Assembly)
    .AddClasses(filter => filter.Where(x => x.Name.EndsWith("Service")))
    .UsingRegistrationStrategy(RegistrationStrategy.Throw)
    .AsImplementedInterfaces()
    .WithScopedLifetime());

builder.Services.AddMapster();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UsePathBase(new PathString("/api"));
app.UseRouting();
app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

// Middlewares
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();