using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using BudgetManager.API.Configuration;
using BudgetManager.API.Seeding;
using BudgetManager.Authorization;
using BudgetManager.Authorization.TokenService;
using BudgetManager.CQRS.Mapping;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using BudgetManager.DataAccess.MongoDbAccess.Repositories;
using BudgetManager.Model;
using BudgetManager.Model.AuthorizationModels;
using BudgetManager.Scheduler;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using BudgetManager.Shared.DataAccess.MongoDB.DatabaseSettings;
using BudgetManager.Shared.Utils.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var mongoDbSettings = builder.Configuration.GetSection(nameof(MongoDbSettings));
var mongoDbConfig = mongoDbSettings.Get<MongoDbSettings>();
builder.Services.Configure<MongoDbSettings>(mongoDbSettings);
builder.Services.AddSingleton<IMongoDbSettings>(sp =>
    sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
{
    BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
    return new MongoClient(mongoDbConfig.ConnectionString);
});

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
        .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(
            mongoDbConfig.ConnectionString, mongoDbConfig.DatabaseName);

builder.Services.AddScoped<IBaseRepository<Category>, CategoryRepository>();
builder.Services.AddScoped<IBaseRepository<User>, UserRepository>();
builder.Services.AddScoped<IBaseRepository<Wallet>, WalletRepository>();
builder.Services.AddScoped<IBaseRepository<Notification>, NotificationRepository>();
builder.Services.AddScoped<IBaseRepository<Country>, CountryRepository>();
builder.Services.AddScoped<IBaseRepository<Currency>, CurrencyRepository>();
builder.Services.AddScoped<IBaseRepository<DefaultCategory>, DefaultCategoryRepository>();
builder.Services.AddSingleton<IBaseRepository<CurrencyRates>, CurrencyRatesRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

builder.Services.AddScoped<ISeedingService, SeedingService>();

builder.Services.AddSingleton<ITokenSettings, TokenSettings>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IAuthorizationManager, AuthorizationManager>();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigureOptions>();

builder.Services.AddMediatR(typeof(MappingProfile).Assembly);
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});
var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
var xmlFilePath = Path.Combine(AppContext.BaseDirectory, xmlFile);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(xmlFilePath);
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

var tokenOptions = builder.Configuration.GetSection("TokenSettings");
var tokenSettings = tokenOptions.Get<TokenSettings>();

builder.Services.Configure<TokenSettings>(tokenOptions);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenSettings.JwtKey.ToString())),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

builder.Services.AddAuthorization(options =>
{
    var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
        JwtBearerDefaults.AuthenticationScheme);
    defaultAuthorizationPolicyBuilder =
        defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
    options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
});

SchedulerService.AddQuartz(builder.Services);
var app = builder.Build();

using var scope = app.Services.CreateScope();
var seedingService = scope.ServiceProvider.GetRequiredService<ISeedingService>();
seedingService.Seed();
scope.Dispose();

// Configure the HTTP request pipeline.
app.UseSwagger(options =>
{
    options.PreSerializeFilters.Add((swagger, req) =>
    {
        swagger.Servers = new List<OpenApiServer>() { new OpenApiServer() { Url = $"https://{req.Host}" } };
    });
});

app.UseSwaggerUI(options =>
{
    var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    foreach (var desc in apiVersionDescriptionProvider.ApiVersionDescriptions)
    {
        options.SwaggerEndpoint($"../swagger/{desc.GroupName}/swagger.json", desc.ApiVersion.ToString());
        options.DefaultModelsExpandDepth(1);
        options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    }
});

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
