using InterviewTaskWebApi.Application.Dto.Tasks;
using InterviewTaskWebApi.Application.IServices;
using InterviewTaskWebApi.Application.Validation;
using InterviewTaskWebApi.Domain.Models;
using InterviewTaskWebApi.Domain.Repositories;

namespace InterviewTaskWebApi.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectService _projectService;
        public TaskService(ITaskRepository taskRepository, IProjectRepository projectRepository, IProjectService projectService)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _projectService = projectService;
        }


        public IEnumerable<TaskDto> FilterByStatus(string s)
        {
            if (!Enum.TryParse<TaskStatus>(s, true, out var taskStatus))
            {
                return null;
            }
            return _taskRepository.GetAllTasksByStatus(taskStatus)
                 .Select(t => new TaskDto
                 {
                     Id = t.Id,
                     Title = t.Title,
                     Description = t.Description,
                     Status = t.Status,
                     DueDte = t.DueDte,
                     priority = t.priority,
                     ProjectName = t.Project.Title

                 });

        }

        public IEnumerable<TaskDto> GetTasksWithProjectName(int pagenumber, int pagesize)
        {

            var task = _taskRepository.GetAll();
            var result = task.Skip((pagenumber - 1) * pagesize).Take(pagesize)
                .Select(t => new TaskDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    DueDte = t.DueDte,
                    priority = t.priority,
                    Status = t.Status,
                    ProjectName = t.Project.Title
                }).ToList();
            return result;
        }

        public string Insert(CreateTask task)
        {
            var validator = new TaskValidation();
            var validationResult = validator.Validate(task);

            if (!validationResult.IsValid)
            {
                return string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var p = _projectRepository.GetById(task.ProjectId);

            if (p == null)
            {
                return "Project Not Found";
            }
            else if (p.Status == ProjectStatus.Archived)
            {
                return "Project Is Archived Cannot Added Tasks";
            }

            var t = new TaskModel
            {
                Title = task.Title,
                Description = task.Description,
                priority = task.priority,
                Status = task.Status,
                ProjectId = task.ProjectId,
                DueDte = task.DueDte


            };
            if (t.DueDte >= p.StartDate && t.DueDte <= p.EndDate)
            {
                _taskRepository.Insert(t);
                _taskRepository.Save();
                return " Success";
            }
            else
            {
                return "Task DueDate Must be between StartDate and EndDate";
            }
        }

        public bool MarkTaskCompleted(Guid id)
        {
            var task = _taskRepository.GetById(id);
            if (task == null) return false;
            task.Status = TaskStatus.Done;

            _taskRepository.Update(task);
            _taskRepository.Save();
            _projectService.UpdateProjectStatus(task.ProjectId);
            return true;
        }

        public void Update(Guid id, UpdateTask entity)
        {
            var task = _taskRepository.GetById(id);
            if (task == null)
            {
                throw new Exception("project not found");
            }
            task.Title = entity.Title;
            task.Description = entity.Description;
            task.Status = entity.Status;
            task.priority = entity.priority;

            _taskRepository.Update(task);
            _taskRepository.Save();
        }


    }
}
