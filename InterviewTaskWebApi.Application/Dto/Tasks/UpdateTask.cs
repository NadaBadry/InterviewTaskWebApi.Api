namespace InterviewTaskWebApi.Application.Dto.Tasks
{
    public class UpdateTask
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public TaskStatus Status { get; set; }
        public TaskPriority priority { get; set; }
    }
}
