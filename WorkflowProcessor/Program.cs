using Data.constants;
using Data.context;
using DataAccess;
using DataAccess.contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nito.AsyncEx;
using System;
using System.Threading;
using System.Threading.Tasks;
using Utilities.services;
using Utilities.services.contracts;
using WorkflowProcessor.contracts;
using WorkflowProcessor.expressions;
using WorkflowProcessor.validators;
using WorkFlowProcessor.contracts;

namespace WorkFlowProcessor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configurationService = new ConfigurationService();
            var serviceProvider = new ServiceCollection()
                .AddScoped<IWorkflowProcessor, WorkflowProcessor>()
                .AddScoped<IMarketDataCommands, MarketDataCommands>()
                .AddScoped<IMarketDataQueries, MarketDataQueries>()
                .AddScoped<IWorkflowQueries, WorkflowQueries>()
                .AddScoped<IWorkflowService, WorkflowService>()
                .AddScoped<IWorkflowActionProcessorFactory, WorkflowActionProcessFactory>()
                .AddScoped<IConditionValidatorFactory, ConditionValidatorFactory>()
                .AddScoped<IExpressionCalculatorFactory, ExpressionCalculatorFactory>()
                .AddSingleton<IConfigurationService, ConfigurationService>()
                .AddDbContext<MarketContext>(options => options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(configurationService.GetConnectionString(ConfigurationConstants.DatabaseConnection)))
                .BuildServiceProvider();

            AsyncContext.Run(() => Execute(serviceProvider));
        }

        private static async Task Execute(ServiceProvider serviceProvider)
        {
            var token = new CancellationToken();
            while (!token.IsCancellationRequested)
            {
                try
                {
                    var processor = serviceProvider.GetService<IWorkflowProcessor>();
                    if (processor != null)
                    {
                        await processor.Execute(token);
                    }
                    else
                    {
                        throw new Exception("Could not resolve service: IWorkflowProcessor");
                    }
                }
                catch (Exception e)
                {
                    // We should probably log that there was an error, but continue
                }
            }
        }
    }
}
