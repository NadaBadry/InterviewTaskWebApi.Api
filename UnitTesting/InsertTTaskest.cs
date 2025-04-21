
using InterviewTaskWebApi.Application.Dto.Tasks;
using InterviewTaskWebApi.Application.IServices;
using InterviewTaskWebApi.Application.Services;
using InterviewTaskWebApi.Domain.Models;
using InterviewTaskWebApi.Domain.Repositories;
using Moq;
public class InsertTTaskest
{
    private readonly Mock<ITaskRepository> _taskRepository;
    private readonly ITaskService _taskservice;
    private readonly Mock<IProjectRepository> _projectRepo;

    public InsertTTaskest()
    {
        _taskRepository = new Mock<ITaskRepository>();
        _projectRepo = new Mock<IProjectRepository>();
        _taskservice = new TaskService(_taskRepository.Object, _projectRepo.Object);
    }


    [Fact]
    public void Insert_ProjectNotFound_ReturnsProjectNotFound()
    {
        // Arrange
        var task = new CreateTask { ProjectId = Guid.NewGuid() };
        _projectRepo.Setup(repo => repo.GetById(task.ProjectId)).Returns((Project)null);

        // Act
        var result = _taskservice.Insert(task);

        // Assert
        Assert.Equal("Project Not Found", result);
    }

    [Fact]
    public void Insert_ProjectArchived_ReturnsProjectIsArchived()
    {
        // Arrange
        var task = new CreateTask { ProjectId = Guid.NewGuid() };
        var project = new Project { Id = Guid.NewGuid(), Status = ProjectStatus.Archived };
        _projectRepo.Setup(repo => repo.GetById(task.ProjectId)).Returns(project);

        // Act
        var result = _taskservice.Insert(task);

        // Assert
        Assert.Equal("Project Is Archived Cannot Added Tasks", result);
    }

    [Fact]
    public void Insert_DueDateNotInRange_ReturnsDueDateError()
    {
        // Arrange
        var task = new CreateTask { ProjectId = Guid.NewGuid(), DueDte = DateTime.Now.AddMonths(2) };
        var project = new Project
        {
            Id = Guid.NewGuid(),
            Status = ProjectStatus.Active,
            StartDate = DateTime.Now.AddDays(-10),
            EndDate = DateTime.Now.AddDays(10)
        };
        _projectRepo.Setup(repo => repo.GetById(task.ProjectId)).Returns(project);

        // Act
        var result = _taskservice.Insert(task);

        // Assert
        Assert.Equal("DueDate Must between StartDate and EndDate", result);
    }

    [Fact]
    public void Insert_ValidTask_ReturnsSuccess()
    {
        // Arrange
        var task = new CreateTask
        {
            ProjectId = Guid.NewGuid(),
            Title = "Test",
            Description = "Test Desc",
            priority = TaskPriority.Low,
            Status = TaskStatus.ToDo,
            DueDte = DateTime.Now
        };
        var project = new Project
        {
            Id = Guid.NewGuid(),
            Status = ProjectStatus.Active,
            StartDate = DateTime.Now.AddDays(-5),
            EndDate = DateTime.Now.AddDays(5)
        };
        _projectRepo.Setup(repo => repo.GetById(task.ProjectId)).Returns(project);

        // Act
        var result = _taskservice.Insert(task);

        // Assert
        Assert.Equal(" Success", result);
        _taskRepository.Verify(r => r.Insert(It.IsAny<TaskModel>()), Times.Once);
        _taskRepository.Verify(r => r.Save(), Times.Once);
    }
}

