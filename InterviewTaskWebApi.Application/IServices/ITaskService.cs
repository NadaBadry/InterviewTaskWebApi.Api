using InterviewTaskWebApi.Application.Dto.Tasks;

namespace InterviewTaskWebApi.Application.IServices
{
    public interface ITaskService
    {
        IEnumerable<TaskDto> GetTasksWithProjectName(int pagenumber, int pagesize);
        // TaskDto CheckAndUpdateProjectStatus(Guid TaskId); 
        void Update(Guid id, UpdateTask entity);
        string Insert(CreateTask task);

        IEnumerable<TaskDto> FilterByStatus(string s);
        public bool MarkTaskCompleted(Guid id);
    }
}
