using BudgetManager.Shared.DataAccess.MongoDB.DatabaseSettings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var mongoDbSettings = builder.Configuration.GetSection(nameof(MongoDbSettings));
var mongoDbConfig = mongoDbSettings.Get<MongoDbSettings>();
builder.Services.Configure<MongoDbSettings>(mongoDbSettings);
builder.Services.AddSingleton<IMongoDbSettings>(sp =>
    sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient(mongoDbConfig.ConnectionString));

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// This loads an entire assembly and looks for everything we do with mediatR
// While we don't have anything in CQRS, line below is commented out
// builder.Services.AddMediatR(typeof({replace this with any class from BudgetManager.CQRS}).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
