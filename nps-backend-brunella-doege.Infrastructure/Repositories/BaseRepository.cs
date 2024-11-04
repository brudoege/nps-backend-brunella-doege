using Microsoft.EntityFrameworkCore;
using nps_backend_brunella_doege.Domain.Entities;
using nps_backend_brunella_doege.Domain.Repositories;

namespace nps_backend_brunella_doege.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IEntity
    {
        protected readonly Context _context;

        public BaseRepository(Context context)
        {
            _context = context;
        }

        public async Task IncludeAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> SelectAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}