using InterviewTaskWebApi.Application.Dto.Projects;

namespace InterviewTaskWebApi.Application.IServices
{
    public interface IProjectService
    {
        IEnumerable<ProjectDto> GetProjects(int pagenumber, int pagesize);
        ProjectDto GetById(Guid id);
        void Insert(CreateProjectDto entity);
        void Update(Guid id, UpdateProject entity);
        void UpdateProjectStatus(Guid id);
        public bool ArchivedProject(Guid id);
    }
}
