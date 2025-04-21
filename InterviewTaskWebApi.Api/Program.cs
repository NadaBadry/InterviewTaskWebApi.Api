using InterviewTaskWebApi.Application.IServices;
using InterviewTaskWebApi.Application.Services;
using InterviewTaskWebApi.Domain.DataDbContext;
using InterviewTaskWebApi.Domain.Repositories;
using InterviewTaskWebApi.Infrustructure.Repository.ImplementRepository;
using System.Text.Json.Serialization;
namespace InterviewTaskWebApi.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(op => //convert enum to string
            {
                op.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //  builder.Services.AddValidatorsFromAssemblyContaining<TaskDtoValidator>();
            // builder.Services.AddFluentValidationAutoValidation();

            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ApplicationDbContext>(op =>
            {
                op.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
            });
            builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
            builder.Services.AddScoped<IProjectService, ProjectServise>();
            builder.Services.AddScoped<ITaskService, TaskService>();
            builder.Services.AddScoped<ITaskRepository, TaskRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
