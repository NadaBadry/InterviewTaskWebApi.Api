using InterviewTaskWebApi.Domain.Repositories;
using InterviewTaskWebApi.Infrustructure.Repository.ImplementRepository;
using Microsoft.Extensions.DependencyInjection;

namespace InterviewTaskWebApi.Infrustructure.DI
{
    public static class ExtentionMethod
    {
        public static IServiceCollection AddInfService(this IServiceCollection services)
        {
         //   services.AddScoped<IGenericRepository,BaseRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            return services;
        }
    }
}
