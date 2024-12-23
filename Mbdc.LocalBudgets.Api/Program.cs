using System.Buffers;
using System.Net;
using System.Security.Authentication;
using System.Security.Claims;
using idunno.Authentication.Basic;
using Mapster;
using MbdcLocalBudgetsApi;
using MbdcLocalBudgetsApi.Middlewares;
using MbdcLocalBudgetsApplication;
using MbdcLocalBudgetsDomain.Options;
using MbdcLocalBudgetsDomain.Persistence;
using MbdcLocalBudgetsInfrastructure.MongoDb;
using MbdcLocalBudgetsPresentation;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using OfficeOpenXml;
using Scrutor;

var builder = WebApplication.CreateBuilder(args);

// Options
builder.Services.Configure<BasicAuthOptions>(builder.Configuration.GetSection("BasicAuth"));

// Basic Authentication
builder.Services.AddAuthentication(BasicAuthenticationDefaults.AuthenticationScheme)
    .AddBasic(opts =>
    {
        opts.Realm = builder.Configuration.GetValue<string>("BasicAuth:Realm");
        opts.Events = new BasicAuthenticationEvents
        {
            OnValidateCredentials = context =>
            {
                var basicAuthOptions = context.HttpContext.RequestServices
                    .GetRequiredService<IOptions<BasicAuthOptions>>().Value;
                if (context.Username == basicAuthOptions.Username &&
                    context.Password == basicAuthOptions.Password)
                {
                    context.Principal = new ClaimsPrincipal(new ClaimsIdentity([
                        new Claim(ClaimTypes.NameIdentifier, context.Username)
                    ], context.Scheme.Name));
                    context.Success();
                }
                else
                {
                    context.Response.BodyWriter.Write(
                        "<body><center><h1>401 Unauthorized</h1>An error occurred while authorizing</center><hr/</body>"u8.ToArray()
                    );
                    context.Fail("unauthorized");
                }

                return Task.CompletedTask;
            },
            OnAuthenticationFailed = context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Task.CompletedTask;
            }
        };
    });

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

// Middlewares
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();


app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
app.Run();