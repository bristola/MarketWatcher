using Data.data;
using DataAccess.contracts;
using Microsoft.AspNetCore.Mvc;

namespace WorkflowApi.Controllers
{
    [ApiController]
    [Route("workflow")]
    public class WorkflowController : ControllerBase
    {
        private readonly IWorkflowDataService _workflowDataService;

        public WorkflowController(IWorkflowDataService workflowDataService)
        {
            _workflowDataService = workflowDataService;
        }

        [HttpGet]
        public List<WorkflowDTO> GetWorkflows([FromQuery] int page = 0, [FromQuery] int limit = 10)
        {
            return _workflowDataService.GetWorkflows(page, limit);
        }

        [HttpPost]
        public ActionResult SaveWorkflow(WorkflowDTO workflow)
        {
            _workflowDataService.SaveWorkflow(workflow);
            return Ok(true);
        }

        [HttpPost]
        [Route("condition")]
        public ActionResult SaveConditionWorkflowAction(ConditionWorkflowActionDTO conditionWorkflowAction)
        {
            _workflowDataService.SaveConditionWorkflowAction(conditionWorkflowAction);
            return Ok(true);
        }
    }
}
