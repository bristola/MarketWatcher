using AutoMapper;
using Data.constants;
using Data.context;
using DataAccess;
using DataAccess.contracts;
using DataAccess.services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Utilities.services;

var configurationService = new ConfigurationService();

var builder = WebApplication.CreateBuilder(args);

// Mappers.
builder.Services.AddAutoMapper(typeof(MapperProfile));

// Data Services
builder.Services.AddScoped<IWorkflowDataService, WorkflowDataService>();
builder.Services.AddScoped<IMarketDataService, MarketDataService>();

// Data Access
builder.Services.AddScoped<IMarketDataCommands, MarketDataCommands>();
builder.Services.AddScoped<IMarketDataQueries, MarketDataQueries>();
builder.Services.AddScoped<IWorkflowQueries, WorkflowQueries>();
builder.Services.AddScoped<IWorkflowCommands, WorkflowCommands>();

builder.Services.AddDbContext<MarketContext>(options => options
    .UseLazyLoadingProxies()
    .UseSqlServer(configurationService.GetConnectionString(ConfigurationConstants.DatabaseConnection)));

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
