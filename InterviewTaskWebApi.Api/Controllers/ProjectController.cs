using InterviewTaskWebApi.Application.Dto.Projects;
using InterviewTaskWebApi.Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace InterviewTaskWebApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectServise;

        public ProjectController(IProjectService projectServise)
        {
            _projectServise = projectServise;
        }
        [HttpGet("GetAllProjects")]
        public IActionResult GetAllProject([FromQuery] int pagenumber, [FromQuery] int pagesize)
        {
            try
            {
                return Ok(_projectServise.GetProjects(pagenumber, pagesize));
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpGet("{id}/GetProjectWithId")]
        public IActionResult GetProject(Guid id)
        {
            // _projectServise.UpdateProjectStatus(id);
            return Ok(_projectServise.GetById(id));
        }
        [HttpPut("{id}/UpdateProject")]
        public IActionResult UpdateProject(Guid id, UpdateProject project)
        {
            try
            {
                _projectServise.Update(id, project);
                // _projectServise.UpdateProjectStatus(id);
                return Ok(new { message = "project updated" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpPost("InsertNewProject")]
        public IActionResult CreateProject([FromBody] CreateProjectDto project)
        {
            try
            {
                _projectServise.Insert(project);
                return Ok(new { message = "Added Project Successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpPut("{id}/ArchivedProject")]
        public IActionResult DeleteProject(Guid id)
        {

            var result = _projectServise.ArchivedProject(id);
            if (result == false)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
