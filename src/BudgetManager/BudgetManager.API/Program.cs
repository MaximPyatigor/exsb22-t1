using BudgetManager.Shared.DataAccess.MongoDB;
using BudgetManager.Shared.DataAccess.MongoDB.DbContexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<ExadelBudgetDatabaseSettings>(
    builder.Configuration.GetSection("ExadelBudgetDatabase"));
builder.Services.AddSingleton<IMongoDbContext, MongoDbContext>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
