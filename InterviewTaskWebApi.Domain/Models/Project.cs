using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public enum ProjectStatus
{
    Active,//0
    Completed,//1
    Archived//2
}
namespace InterviewTaskWebApi.Domain.Models
{
    public class Project
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Project Title Must Not Be Empty")]
        public string Title { get; set; }
        public string? Description { get; set; }
        [NotMapped]//it is calculated
        public double Progress
        {
            get
            {
                if (Tasks.Count == 0 || Tasks == null)
                    return 0;
                else
                {
                    int CompletedTasks = Tasks.Count(t => t.Status == TaskStatus.Done);//compute number of tasks(Done)
                    double x = CompletedTasks / (double)Tasks.Count;
                    return (double)x * 100;


                }
            }
        }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ProjectStatus Status { get; set; }
        public List<TaskModel> Tasks { get; set; } = new List<TaskModel>();
    }
}
