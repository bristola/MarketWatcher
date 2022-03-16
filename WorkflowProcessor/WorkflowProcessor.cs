using Data.constants;
using Data.context;
using DataAccess.contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utilities.services.contracts;
using WorkFlowProcessor.contracts;

namespace WorkFlowProcessor
{
    public class WorkflowProcessor : IWorkflowProcessor
    {
        private readonly IConfigurationService _configurationService;
        private readonly IWorkflowService _workflowService;

        public WorkflowProcessor(IConfigurationService configurationService, IWorkflowService workflowService)
        {
            _configurationService = configurationService;
            _workflowService = workflowService;
        }

        public async Task Execute(CancellationToken cancellationToken)
        {
            var page = 0;
            var limit = 10;
            List<Workflow> flows = new List<Workflow>();

            do
            {
                flows = _workflowService.GetWorkflows(page++, limit);
                foreach (var flow in flows)
                {
                    _workflowService.Execute(flow);
                }
            }
            while (flows.Count == limit);

            var delaySeconds = _configurationService.GetInt(ConfigurationConstants.DataCollectionCoinbaseApiDelay);
            await Task.Delay(delaySeconds * 1000, cancellationToken);
        }
    }
}
