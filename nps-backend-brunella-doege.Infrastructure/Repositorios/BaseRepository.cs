using Microsoft.EntityFrameworkCore;
using nps_backend_brunella_doege.Domain.Entidades;
using nps_backend_brunella_doege.Domain.Repositories;

namespace nps_backend_brunella_doege.Infrastructure.Repositorios
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IEntity
    {
        protected readonly Contexto _contexto;

        public BaseRepository(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task IncluirAsync(T entity)
        {
            await _contexto.Set<T>().AddAsync(entity);
            await _contexto.SaveChangesAsync();
        }

        public async Task<List<T>> SelecionarTudoAsync()
        {
            return await _contexto.Set<T>().ToListAsync();
        }

        public void Dispose()
        {
            _contexto.Dispose();
        }
    }

}