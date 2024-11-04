namespace nps_backend_brunella_doege.Domain.Repositories
{
    public interface IBaseRepository<T>
    {
        Task IncludeAsync(T entity);

        Task<List<T>> SelectAllAsync();
    }
}
