using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BudgetManager.API.Configuration
{
    public class SwaggerConfigureOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public SwaggerConfigureOptions(IApiVersionDescriptionProvider provider) => _provider = provider ?? throw new ArgumentNullException(nameof(provider));

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var desc in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(desc.GroupName, CreateInfo(desc));
            }
        }

        private OpenApiInfo CreateInfo(ApiVersionDescription description)
        {
            var info = new OpenApiInfo
            {
                Title = "My Test API",
                Version = description.ApiVersion.ToString(),
            };
            return info;
        }
    }
}
