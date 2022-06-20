using KoerberExercise.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoerberExercise.Data.UnitOfWork.Interfaces
{
    public interface IUnitOfWork
    {
        IMachinesRepository MachinesRepo { get; }
        Task<bool> SaveChangesAsync();
    }
}
