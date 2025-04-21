using InterviewTaskWebApi.Domain.DataDbContext;
using InterviewTaskWebApi.Domain.Models;
using InterviewTaskWebApi.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTaskWebApi.Infrustructure.Repository.ImplementRepository
{
    public class TaskRepository:BaseRepository<TaskModel>,ITaskRepository
    {
        public TaskRepository(ApplicationDbContext _contect) : base(_contect)
        {
        }
             public override IEnumerable<TaskModel> GetAll()
        {
            return _context.Tasks.Include(p => p.Project);
        }

        public IEnumerable<TaskModel> GetAllTasksByStatus(TaskStatus s)
        {
            return _context.Tasks.Where(t => t.Status==s).Include(p=>p.Project)
                .ToList();
            
        }
    }
}
