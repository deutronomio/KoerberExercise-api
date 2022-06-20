using KoerberExercise.Data.Models.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoerberExercise.Data.Contexts
{
    public class MachinesContext : DbContext
    {
        public MachinesContext(DbContextOptions<MachinesContext> opt)
            : base(opt)
        {

        }

        public DbSet<MachineEntity> Machines { get; set; }
    }
}
