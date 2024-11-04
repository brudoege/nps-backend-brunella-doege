using nps_backend_brunella_doege.Application.ViewModels;

namespace nps_backend_brunella_doege.Application.Service
{
    public interface INpsResultService
    {
        Task<Guid> IncludeNpsAsync(NpsResultInputViewModel npsViewInputModel, string urlCreate, string systemId);
        Task<List<NpsResultViewModel>> SelectAllNpsAsync();
        Task<HttpResponseMessage?> GetNpsAsync(string user, string urlQuestion, string systemId);
    }
}
