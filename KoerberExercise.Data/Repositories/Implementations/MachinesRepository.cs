using KoerberExercise.Data.Contexts;
using KoerberExercise.Data.Models.Implementations;
using KoerberExercise.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoerberExercise.Data.Repositories.Implementations
{
    public class MachinesRepository : Repository<MachineEntity>, IMachinesRepository
    {
        public MachinesRepository(MachinesContext context): base(context)
        {
        }
    }
}
