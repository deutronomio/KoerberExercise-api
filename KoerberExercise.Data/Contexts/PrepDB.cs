using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoerberExercise.Data.Contexts
{
    public static class PrepDB
    {
        public static void PrepAndPopulate(IApplicationBuilder app) 
        {
            using (var serviceScope = app.ApplicationServices.CreateScope()) 
            {
                SeedDate(serviceScope.ServiceProvider.GetService<MachinesContext>());
            }
        }

        private static void SeedDate(MachinesContext machinesContext)
        {
            System.Console.WriteLine("Start Applying Migrations.");
            machinesContext.Database.Migrate();
            System.Console.WriteLine("Done with Migrations.");

        }
    }
}
