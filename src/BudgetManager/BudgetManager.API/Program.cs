using BudgetManager.CQRS.Handlers.NotificationHandlers;
using BudgetManager.CQRS.Mapping;
using BudgetManager.DataAccess.MongoDbAccess.Repositories;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using BudgetManager.Shared.DataAccess.MongoDB.DatabaseSettings;
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

builder.Services.AddScoped<IBaseRepository<Notification>, NotificationRepository>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// This loads an entire assembly and looks for everything we do with mediatR
builder.Services.AddMediatR(typeof(MappingProfile).Assembly);

builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
