namespace BudgetManager.API.IOC
{
    public class DependencyInjectionService
    {
        private readonly WebApplicationBuilder _builder;

        public DependencyInjectionService(WebApplicationBuilder builder)
        {
            this._builder = builder;
        }

        public void AddDependencyInjection()
        {
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
    }
}
