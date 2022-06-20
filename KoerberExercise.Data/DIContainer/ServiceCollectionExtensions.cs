using KoerberExercise.Data.Contexts;
using KoerberExercise.Data.Repositories.Implementations;
using KoerberExercise.Data.Repositories.Interfaces;
using KoerberExercise.Data.UnitOfWork.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace KoerberExercise.Data.DIContainer
{
    public static class ServiceCollectionExtensions
    {
        public static void Add(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork.Implementations.UnitOfWork>();
            services.AddScoped<IMachinesRepository, MachinesRepository>();

            var server = configuration["DBServer"] ?? "localhost";
            var port = configuration["DBPort"] ?? "1433";
            var user = configuration["DBUser"] ?? "sa";
            var password = configuration["DBPassword"] ?? "Test123!";
            var database = configuration["Database"] ?? "MachinesDB";

            services.AddDbContext<MachinesContext>(opts =>
            {
                var connString = $"Server={server},{port};Initial Catalog={database};User ID={user};Password={password}";
                //configuration.GetConnectionString("DefaultConnection");
                opts.UseSqlServer(connString, options =>
                {
                    options.MigrationsAssembly(typeof(MachinesContext).Assembly.FullName.Split(',')[0]);
                });
            });
        }
    }
}
