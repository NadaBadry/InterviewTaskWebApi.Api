using InterviewTaskWebApi.Application.Dto.Tasks;

namespace InterviewTaskWebApi.Application.Dto.Projects
{
    public class ProjectDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public double Progress { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ProjectStatus Status { get; set; }
        //public string? StatusName=>Status.ToString();
        public List<TaskDto> Tasks { get; set; } = new List<TaskDto>();
    }
}
