public enum TaskStatus
{
    ToDo,//0
    InProgress,//1
    Done//2
}
public enum TaskPriority
{
    Low,//0
    Medium,//1
    High//2
}
namespace InterviewTaskWebApi.Domain.Models
{
    public class TaskModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime DueDte { get; set; }
        public TaskStatus Status { get; set; }
        public TaskPriority priority { get; set; }
        public Guid ProjectId {  get; set; }
        public Project Project { get; set; }
    }
}
