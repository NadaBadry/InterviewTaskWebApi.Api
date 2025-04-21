using InterviewTaskWebApi.Application.Dto.Projects;
using InterviewTaskWebApi.Application.IServices;
using InterviewTaskWebApi.Domain.Models;
using InterviewTaskWebApi.Domain.Repositories;

namespace InterviewTaskWebApi.Application.Services
{
    public class ProjectServise : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectServise(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public IEnumerable<ProjectDto> GetProjects(int pagenumber, int pagesize)
        {
            var projects = _projectRepository.GetAll();
            var result = projects.Where(p => p.Status != ProjectStatus.Archived)
                .Skip((pagenumber - 1) * pagesize).Take(pagesize)
                .Select(p => new ProjectDto
                {
                    Id = p.Id,
                    Description = p.Description,
                    Title = p.Title,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    Status = p.Status,
                    Progress = Math.Round(p.Progress, 2),
                    Tasks = p.Tasks
                .Select(t => new Dto.Tasks.TaskDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    DueDte = t.DueDte,
                    Description = t.Description,
                    Status = t.Status,
                    priority = t.priority,
                    ProjectName = p.Title



                })
                .ToList()
                });
            return result;
        }

        public ProjectDto GetById(Guid id)
        {
            Project p = _projectRepository.GetById(id);
            if (p != null)
            {
                return new ProjectDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    Status = p.Status,
                    Description = p.Description,
                    Tasks = p.Tasks.Select(t => new Dto.Tasks.TaskDto
                    {
                        Id = t.Id,
                        Title = t.Title,
                        DueDte = t.DueDte,
                        Description = t.Description,
                        Status = t.Status,
                        priority = t.priority

                    }).ToList()
                };

            }
            return null;

        }

        public void Insert(CreateProjectDto dto)
        {
            var project = new Project
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Status = dto.Status

            };
            if (project.EndDate < project.StartDate)
            {
                throw new ArgumentException("End date mustt be greater thsn start date");
            }
            _projectRepository.Insert(project);
            _projectRepository.Save();

        }

        public void Update(Guid id, UpdateProject entity)
        {
            var project = _projectRepository.GetById(id);
            if (project == null)
            {
                throw new Exception("project not found");
            }
            project.Title = entity.Title;
            project.StartDate = entity.StartDate;
            project.EndDate = entity.EndDate;
            project.Description = entity.Description;

            _projectRepository.Update(project);
            _projectRepository.Save();

        }


        public bool ArchivedProject(Guid id)
        {
            var project = _projectRepository.GetById(id);
            if (project == null) return false;
            project.Status = ProjectStatus.Archived;
            _projectRepository.Update(project);
            _projectRepository.Save();
            return true;

        }

        public void UpdateProjectStatus(Guid id)
        {
            var p = _projectRepository.GetById(id);
            if (p != null)
            {
                if (p.Progress == 100)
                {
                    p.Status = ProjectStatus.Completed;
                }
                else p.Status = ProjectStatus.Active;

            }
            _projectRepository.Update(p);
            _projectRepository.Save();

        }
    }
}
