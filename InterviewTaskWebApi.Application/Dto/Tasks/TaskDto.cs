using InterviewTaskWebApi.Application.Dto.Projects;

namespace InterviewTaskWebApi.Application.Dto.Tasks
{
    public class TaskDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime DueDte { get; set; }
        public TaskStatus Status { get; set; }
        public TaskPriority priority { get; set; }
        public ProjectDateDTO project {  get; set; }
        public string ProjectName { get; set; }

    }
}
