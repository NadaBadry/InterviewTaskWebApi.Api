using InterviewTaskWebApi.Domain.DataDbContext;
using InterviewTaskWebApi.Domain.Models;
using InterviewTaskWebApi.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InterviewTaskWebApi.Infrustructure.Repository.ImplementRepository
{
    public class ProjectRepository :BaseRepository<Project> , IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext _context):base(_context)
        {
           
        }
        public override IEnumerable<Project> GetAll()
        {
            return _context.Projects.Include(p=>p.Tasks).ToList();
        }
        public Project GetById(Guid id)
        {
            //return _dbSet.Find(id);
            return _context.Projects.Include(p => p.Tasks).FirstOrDefault(p=>p.Id==id);

        }
    }
}
