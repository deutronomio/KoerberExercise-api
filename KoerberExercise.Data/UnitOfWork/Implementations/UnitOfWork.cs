using KoerberExercise.Data.Contexts;
using KoerberExercise.Data.Repositories.Interfaces;
using KoerberExercise.Data.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KoerberExercise.Data.UnitOfWork.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private protected MachinesContext dbContext;

        public UnitOfWork(
            MachinesContext dbContext,
            IMachinesRepository machinesRepository)
        {
            this.dbContext = dbContext;
            MachinesRepo = machinesRepository;
        }

        public IMachinesRepository MachinesRepo { get; private set; }

        public async Task<bool> SaveChangesAsync()
        {
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}
