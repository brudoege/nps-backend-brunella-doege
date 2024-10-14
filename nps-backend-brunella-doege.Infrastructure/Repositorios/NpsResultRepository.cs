using nps_backend_brunella_doege.Domain.Entidades;
using nps_backend_brunella_doege.Domain.Repositories;

namespace nps_backend_brunella_doege.Infrastructure.Repositorios
{
    public class NpsResultRepository : BaseRepository<NpsResult>, INpsResultRepository    // BaseRepository usa Generics
    {
        public NpsResultRepository(Contexto contexto) : base(contexto)
        {
        }
    }
}
