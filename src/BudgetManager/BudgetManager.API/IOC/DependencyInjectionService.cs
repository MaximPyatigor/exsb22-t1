using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using BudgetManager.API.Configuration;
using BudgetManager.API.IOC;
using BudgetManager.API.Seeding;
using BudgetManager.Authorization;
using BudgetManager.Authorization.TokenService;
using BudgetManager.CQRS.Mapping;
using BudgetManager.CQRS.Validators;
using BudgetManager.CQRS.Validators.SubCategoryValidators;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using BudgetManager.DataAccess.MongoDbAccess.Repositories;
using BudgetManager.Model;
using BudgetManager.Model.AuthorizationModels;
using BudgetManager.Scheduler;
using BudgetManager.Scheduler.Jobs;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using BudgetManager.Shared.DataAccess.MongoDB.DatabaseSettings;
using BudgetManager.Shared.Utils.Helpers;
using FluentValidation;
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

namespace BudgetManager.API.IOC
{
    public class DependencyInjectionService
    {
        private readonly WebApplicationBuilder _builder;

        public DependencyInjectionService(WebApplicationBuilder builder)
        {
            _builder = builder;
        }

        public void AddDependencyInjection()
        {
            InjectMongoDb();
            InjectRepositories();
            InjectSeeding();
            InjectJWT();
            InjectValidators();
            InjectJsonOptions();
            InjectSwagger();
            InjectCors();

            _builder.Services.AddMediatR(typeof(MappingProfile).Assembly);
            _builder.Services.AddAutoMapper(typeof(MappingProfile));
        }

        public void InjectMongoDb()
        {
            var mongoDbSettings = _builder.Configuration.GetSection(nameof(MongoDbSettings));
            var mongoDbConfig = mongoDbSettings.Get<MongoDbSettings>();
            _builder.Services.Configure<MongoDbSettings>(mongoDbSettings);

            _builder.Services.AddSingleton<IMongoDbSettings>(sp =>
              sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);
            _builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
            {
                BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
                return new MongoClient(mongoDbConfig.ConnectionString);
            });

            _builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
              .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(
                mongoDbConfig.ConnectionString, mongoDbConfig.DatabaseName);
        }

        public void InjectRepositories()
        {
            _builder.Services.AddScoped<IBaseRepository<Category>, CategoryRepository>();
            _builder.Services.AddScoped<IBaseRepository<User>, UserRepository>();
            _builder.Services.AddScoped<IBaseRepository<Wallet>, WalletRepository>();
            _builder.Services.AddScoped<IBaseRepository<Notification>, NotificationRepository>();
            _builder.Services.AddScoped<IBaseRepository<Country>, CountryRepository>();
            _builder.Services.AddScoped<IBaseRepository<Currency>, CurrencyRepository>();
            _builder.Services.AddScoped<IBaseRepository<DefaultCategory>, DefaultCategoryRepository>();
            _builder.Services.AddSingleton<IBaseRepository<CurrencyRates>, CurrencyRatesRepository>();
            _builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
        }

        public void InjectSeeding()
        {
            _builder.Services.AddScoped<IUpdateCurrencyRatesJob, UpdateCurrencyRatesJob>();
            _builder.Services.AddScoped<ISeedingService, SeedingService>();
        }

        public void InjectJWT()
        {
            _builder.Services.AddSingleton<ITokenSettings, TokenSettings>();
            _builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
            _builder.Services.AddScoped<IAuthorizationManager, AuthorizationManager>();

            var tokenOptions = _builder.Configuration.GetSection("TokenSettings");
            var tokenSettings = tokenOptions.Get<TokenSettings>();

            _builder.Services.Configure<TokenSettings>(tokenOptions);

            _builder.Services.AddAuthentication(x =>
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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenSettings.JwtKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

            _builder.Services.AddAuthorization(options =>
            {
                var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
                  JwtBearerDefaults.AuthenticationScheme);
                defaultAuthorizationPolicyBuilder =
                  defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
                options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
            });
        }

        public void InjectValidators()
        {
            _builder.Services.AddValidatorsFromAssemblyContaining<AddCategoryValidator>();
            _builder.Services.AddValidatorsFromAssemblyContaining<UpdateCategoryValidator>();
            _builder.Services.AddValidatorsFromAssemblyContaining<UpdateSubCategoryValidator>();
            _builder.Services.AddValidatorsFromAssemblyContaining<AddSubCategoryValidator>();
            _builder.Services.AddValidatorsFromAssemblyContaining<AddExpenseTransactionValidator>();
            _builder.Services.AddValidatorsFromAssemblyContaining<AddIncomeTransactionValidator>();
            _builder.Services.AddValidatorsFromAssemblyContaining<UpdateExpenseTransactionValidator>();
            _builder.Services.AddValidatorsFromAssemblyContaining<UpdateIncomeTransactionValidator>();
        }

        public void InjectJsonOptions()
        {
            _builder.Services.AddControllers().AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
        }

        public void InjectSwagger()
        {
            _builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigureOptions>();
            _builder.Services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
            });

            _builder.Services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlFilePath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            _builder.Services.AddEndpointsApiExplorer();

            _builder.Services.AddSwaggerGen(options =>
            {
                options.IncludeXmlComments(xmlFilePath);
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer",
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
              {
                new OpenApiSecurityScheme
                {
                  Reference = new OpenApiReference
                  {
                      Type = ReferenceType.SecurityScheme,
                      Id = "Bearer",
                  },
                },
                new string[]
                {
                }
              },
            });
            });
        }

        public void InjectCors()
        {
            _builder.Services.AddCors(opt =>
            {
                opt.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
                });
            });
        }
    }
}