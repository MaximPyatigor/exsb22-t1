using BudgetManager.API.Seeding;
using BudgetManager.CQRS.Mapping;
using BudgetManager.DataAccess.MongoDbAccess.Repositories;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using BudgetManager.Shared.DataAccess.MongoDB.DatabaseSettings;
using BudgetManager.Shared.Utils.Helpers;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

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

builder.Services.AddScoped<IBaseRepository<Category>, CategoryRepository>();
builder.Services.AddScoped<IBaseRepository<User>, UserRepository>();
builder.Services.AddScoped<IBaseRepository<Wallet>, WalletRepository>();
builder.Services.AddScoped<IBaseRepository<Notification>, NotificationRepository>();
builder.Services.AddScoped<IBaseRepository<Transaction>, TransactionRepository>();
builder.Services.AddScoped<IBaseRepository<Country>, CountryRepository>();
builder.Services.AddScoped<IBaseRepository<Currency>, CurrencyRepository>();
builder.Services.AddScoped<IBaseRepository<DefaultCategory>, DefaultCategoryRepository>();

builder.Services.AddScoped<ISeedingService, SeedingService>();

builder.Services.AddMediatR(typeof(MappingProfile).Assembly);
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

var app = builder.Build();

using var scope = app.Services.CreateScope();
var someService = scope.ServiceProvider.GetRequiredService<ISeedingService>();
someService.Seed();
scope.Dispose();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
