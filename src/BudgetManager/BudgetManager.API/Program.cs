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
using BudgetManager.CQRS.Validators.WalletValidators;
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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var dependencyInjectionService = new DependencyInjectionService(builder);
dependencyInjectionService.AddDependencyInjection();

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
