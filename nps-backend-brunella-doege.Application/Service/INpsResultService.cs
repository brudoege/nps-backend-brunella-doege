using nps_backend_brunella_doege.Application.ViewModels;

namespace nps_backend_brunella_doege.Application.Service
{
    public interface INpsResultService
    {
        Task<Guid> Incluir(NpsResultManipulacaoViewModel npsViewManipulacaoModel);
        Task<List<NpsResultViewModel?>?> ListarTodos();
        Task<HttpResponseMessage?> BuscarPesquisaNps(string user);
    }
}
