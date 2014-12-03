using System.Collections.Generic;
using System.Web.Http;
using WebApi2Book.Common;
using WebApi2Book.Web.Api.MaintenanceProcessing;
using WebApi2Book.Web.Api.Models;
using WebApi2Book.Web.Common;
using WebApi2Book.Web.Common.Routing;

namespace WebApi2Book.Web.Api.Controllers.V1
{
    [ApiVersion1RoutePrefix("tasks")]
    [UnitOfWorkActionFilter]
    [Authorize(Roles = Constants.RoleNames.SeniorWorker)]
    public class TaskUsersController : ApiController
    {
        private readonly ITaskUsersMaintenanceProcessor _taskUsersMaintenanceProcessor;

        public TaskUsersController(ITaskUsersMaintenanceProcessor taskUsersMaintenanceProcessor)
        {
            _taskUsersMaintenanceProcessor = taskUsersMaintenanceProcessor;
        }

        [Route("{taskId:long}/users", Name = "ReplaceTaskUsersRoute")]
        [HttpPut]
        public Task ReplaceTaskUsers(long taskId, [FromBody] IEnumerable<long> userIds)
        {
            return _taskUsersMaintenanceProcessor.ReplaceTaskUsers(taskId, userIds);
        }

        [Route("{taskId:long}/users", Name = "DeleteTaskUsersRoute")]
        [HttpDelete]
        public Task DeleteTaskUsers(long taskId)
        {
            return _taskUsersMaintenanceProcessor.DeleteTaskUsers(taskId);
        }

        [Route("{taskId:long}/users/{userId:long}", Name = "AddTaskUserRoute")]
        [HttpPut]
        public Task AddTaskUser(long taskId, long userId)
        {
            return _taskUsersMaintenanceProcessor.AddTaskUser(taskId, userId);
        }

        [Route("{taskId:long}/users/{userId:long}", Name = "DeleteTaskUserRoute")]
        [HttpDelete]
        public Task DeleteTaskUser(long taskId, long userId)
        {
            return _taskUsersMaintenanceProcessor.DeleteTaskUser(taskId, userId);
        }
    }
}