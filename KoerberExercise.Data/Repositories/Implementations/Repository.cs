using KoerberExercise.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KoerberExercise.Data.Repositories.Implementations
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, new()
    {
        protected readonly DbContext _context;

        public Repository(DbContext context)
        {
            this._context = context;
        }

        public void Add(TEntity obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            _context.Set<TEntity>().Add(obj);
        }

        public void Remove(TEntity obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            _context.Set<TEntity>().Remove(obj);
        }

        public void Update(TEntity obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            _context.Set<TEntity>().Update(obj);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return await _context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetAsync(int id)
        {
            if (id < 1)
                throw new ArgumentException($"{nameof(id)} is not valid.");

            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<bool> ExistAsync(int id)
        {
            if (id < 1)
                throw new ArgumentException($"{nameof(id)} is not valid.");

            var obj = await _context.Set<TEntity>().FindAsync(id);
            return obj != null;
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return await _context.Set<TEntity>().AnyAsync(predicate);
        }

        public async Task<TEntity> FirstOrDefaultAsync()
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync();
        }
    }
}
