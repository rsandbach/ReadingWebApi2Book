using System.Net.Http;
using System.Web.Http;
using WebApi2Book.Data.Entities;
using WebApi2Book.Web.Common.Routing;

namespace WebApi2Book.Web.Api.Controllers.V2
{
    [ApiVersion1RoutePrefix("tasks")]
    public class TaskController : ApiController
    {
        [Route("", Name = "AddTaskRouteV2")]
        [HttpPost]
        public Task AddTask(HttpRequestMessage requestMessage, Task newTask)
        {
            return new Task
            {
                Subject = "In v2, newTask.Subject = " + newTask.Subject
            };
        }
    }
}