using nps_backend_brunella_doege.Domain.Entities;
using nps_backend_brunella_doege.Domain.Repositories;

namespace nps_backend_brunella_doege.Infrastructure.Repositories
{
    public class NpsResultRepository : BaseRepository<NpsResult>, INpsResultRepository    // BaseRepository usa Generics
    {
        public NpsResultRepository(Context contexto) : base(contexto)
        {
        }
    }
}
