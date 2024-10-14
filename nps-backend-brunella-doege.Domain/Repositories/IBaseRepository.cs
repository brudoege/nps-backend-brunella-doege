namespace nps_backend_brunella_doege.Domain.Repositories
{
    public interface IBaseRepository<T>
    {
        Task IncluirAsync(T entity);

        Task<List<T>> SelecionarTudoAsync();
    }
}
