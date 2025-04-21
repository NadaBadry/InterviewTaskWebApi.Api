using InterviewTaskWebApi.Application.Dto.Tasks;
using InterviewTaskWebApi.Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace InterviewTaskWebApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        [HttpGet("GetAllTasks")]
        public IActionResult GetAllTasks([FromQuery] int pagenumber, [FromQuery] int pagesize)
        {
            var tasks = _taskService.GetTasksWithProjectName(pagenumber, pagesize);
            return Ok(tasks);
        }
        [HttpPost("AddTask")]
        public IActionResult AddNewTask([FromBody] CreateTask task)
        {
            try
            {
                string insertResult = _taskService.Insert(task);
                if (insertResult == "Success")
                {
                    return Ok("Task Inserted Successfully");
                }
                else
                {
                    return BadRequest(insertResult);
                }

            }
            catch (Exception)
            {
                return BadRequest(new { message = "Task not Inserted Successfully" }/*new { error = ex.Message }*/);
            }
        }
        [HttpPut("{id}/CompletedTasks")]
        public IActionResult GetMarkedTasks(Guid id)
        {
            var result = _taskService.MarkTaskCompleted(id);
            if (result == false)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpGet("FilterByStatus")]
        public IActionResult GetTasksByStatus([FromQuery] string s)
        {
            try
            {
                var tasks = _taskService.FilterByStatus(s);
                return Ok(tasks);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}/UpdateTask")]
        public IActionResult UpdateTasks(Guid id, UpdateTask Task)
        {
            try
            {
                _taskService.Update(id, Task);
                return Ok(new { message = "Task Updated Successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
