﻿namespace InterviewTaskWebApi.Application.Dto.Projects
{
    public class CreateProjectDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ProjectStatus Status { get; set; }
    }
}
