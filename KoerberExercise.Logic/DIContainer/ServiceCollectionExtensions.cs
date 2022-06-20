using KoerberExercise.Logic.Services.Implementations;
using KoerberExercise.Logic.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KoerberExercise.Logic.DIContainer
{
    public static class ServiceCollectionExtensions
    {
        public static void Add(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IMachinesService, MachinesService>();
            KoerberExercise.Data.DIContainer.ServiceCollectionExtensions.Add(services, configuration);
        }
    }
}
