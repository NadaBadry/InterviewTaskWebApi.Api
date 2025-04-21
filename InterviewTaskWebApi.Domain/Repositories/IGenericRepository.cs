namespace InterviewTaskWebApi.Domain.Repositories
{
    public interface IGenericRepository<T>where T : class
    {
        IEnumerable<T> GetAll();
       T GetById(Guid id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(Guid id);
        void Save();
    }
}
