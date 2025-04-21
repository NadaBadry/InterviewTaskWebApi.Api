
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
    private readonly Mock<IProjectService> _projectService;

    public InsertTTaskest()
    {
        _taskRepository = new Mock<ITaskRepository>();
        _projectRepo = new Mock<IProjectRepository>();
        _projectService = new Mock<IProjectService>();
        _taskservice = new TaskService(_taskRepository.Object, _projectRepo.Object, _projectService.Object);
    }

    //[Fact]
    //public void Insert_WhenValidationFails_ReturnsValidationErrors()
    //{
    //    // Arrange
    //    var task = new CreateTask
    //    {
    //        ProjectId = Guid.Empty,
    //        Title = "",
    //        DueDte = DateTime.UtcNow
    //    };

    //    // Act
    //    var result = _taskservice.Insert(task);

    //    // Assert
    //    Assert.Contains("Title is required.", result);
    //    Assert.Contains("ProjectId is required.", result);
    //    Assert.Contains("DueDate is required.", result);
    //}

    [Fact]
    public void Insert_WhenProjectNotFound_ReturnsProjectNotFound()
    {
        // Arrange
        var task = new CreateTask
        {
            ProjectId = Guid.NewGuid(),
            Title = "Task Test",
            DueDte = DateTime.UtcNow
        };

        _projectRepo.Setup(repo => repo.GetById(task.ProjectId)).Returns((Project)null);

        // Act
        var result = _taskservice.Insert(task);

        // Assert
        Assert.Equal("Project Not Found", result);
    }

    [Fact]
    public void Insert_WhenProjectIsArchived_ReturnsArchivedError()
    {
        // Arrange
        var task = new CreateTask
        {
            ProjectId = Guid.NewGuid(),
            Title = "Task Test",
            DueDte = DateTime.Now
        };

        var project = new Project
        {
            Id = task.ProjectId,
            Status = ProjectStatus.Archived,
            StartDate = DateTime.Now.AddDays(-10),
            EndDate = DateTime.Now.AddDays(10)
        };

        _projectRepo.Setup(repo => repo.GetById(task.ProjectId)).Returns(project);

        // Act
        var result = _taskservice.Insert(task);

        // Assert
        Assert.Equal("Project Is Archived Cannot Added Tasks", result);
    }

    [Fact]
    public void Insert_WhenDueDateOutOfRange_ReturnsDueDateError()
    {
        // Arrange
        var task = new CreateTask
        {
            ProjectId = Guid.NewGuid(),
            Title = "Task Test",
            DueDte = DateTime.Now.AddMonths(2)
        };

        var project = new Project
        {
            Id = task.ProjectId,
            Status = ProjectStatus.Active,
            StartDate = DateTime.Now.AddDays(-10),
            EndDate = DateTime.Now.AddDays(10)
        };

        _projectRepo.Setup(repo => repo.GetById(task.ProjectId)).Returns(project);

        // Act
        var result = _taskservice.Insert(task);

        // Assert
        Assert.Equal("Task DueDate Must be between StartDate and EndDate", result);
    }

    //[Fact]
    //public void Insert_WhenEverythingIsValid_ReturnsSuccess()
    //{
    //    // Arrange
    //    var task = new CreateTask
    //    {
    //        ProjectId = Guid.NewGuid(),
    //        Title = "Valid Task",
    //        DueDte = DateTime.Now
    //    };

    //    var project = new Project
    //    {
    //        Id = task.ProjectId,
    //        Status = ProjectStatus.Active,
    //        StartDate = DateTime.Now.AddDays(-10),
    //        EndDate = DateTime.Now.AddDays(10)
    //    };

    //    _projectRepo.Setup(repo => repo.GetById(task.ProjectId)).Returns(project);

    //    // Act
    //    var result = _taskservice.Insert(task);

    //    // Assert
    //    Assert.Equal("Success", result);

    //    _taskRepository.Verify(r => r.Insert(It.IsAny<TaskModel>()), Times.Once);
    //    _taskRepository.Verify(r => r.Save(), Times.Once);
    //}
}
