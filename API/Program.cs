using API.Mapping;
using API.Settings;
using Infrastructure.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Identity
var mongoDbSettings = builder.Configuration.GetSection(nameof(MongoDbConfig)).Get<MongoDbConfig>();

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(opt =>
    {
        opt.Password.RequireNonAlphanumeric = false;
        opt.Password.RequireUppercase = false;
        opt.Password.RequireDigit = false;
        opt.Password.RequiredLength = 6;
        opt.User.RequireUniqueEmail = true;
    })
    .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(
        mongoDbSettings.ConnectionString, mongoDbSettings.Name
    );
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
