using InterviewTaskWebApi.Domain.Models;
namespace InterviewTaskWebApi.Domain.Repositories
{
    public interface ITaskRepository:IGenericRepository<TaskModel>
    {
        IEnumerable<TaskModel> GetAllTasksByStatus(TaskStatus s);

    }
}
