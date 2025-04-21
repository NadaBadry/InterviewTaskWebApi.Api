using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTaskWebApi.Application.Dto.Tasks
{
    public class GetTask
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime DueDte { get; set; }
        public TaskStatus Status { get; set; }
        public TaskPriority priority { get; set; }
    }
}
