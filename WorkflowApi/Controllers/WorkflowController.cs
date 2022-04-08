using Data.data;
using DataAccess.contracts;
using Microsoft.AspNetCore.Mvc;

namespace WorkflowApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkflowController : ControllerBase
    {
        private readonly IWorkflowQueries _workflowQueries;
        private readonly IWorkflowCommands _workflowCommands;

        public WorkflowController(IWorkflowQueries workflowQueries, IWorkflowCommands workflowCommands)
        {
            _workflowQueries = workflowQueries;
            _workflowCommands = workflowCommands;
        }

        [HttpGet]
        public List<WorkflowDTO> GetWorkflows()
        {
            return _workflowQueries.GetWorkflows(0, 10);
        }

        [HttpPost]
        public ActionResult SaveWorkflow(WorkflowDTO workflow)
        {
            _workflowCommands.SaveWorkflow(workflow);
            return Ok(true);
        }
    }
}
