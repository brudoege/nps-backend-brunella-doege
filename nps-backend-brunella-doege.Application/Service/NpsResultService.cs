using nps_backend_brunella_doege.Application.ViewModels;
using nps_backend_brunella_doege.Domain.Entidades;
using nps_backend_brunella_doege.Domain.Enums;
using nps_backend_brunella_doege.Domain.Repositories;
using System.Net.Http.Json;

namespace nps_backend_brunella_doege.Application.Service
{
    public class NpsResultService : INpsResultService
    {
        private readonly INpsResultRepository _npsRepository;
        private readonly HttpClient _httpClient;
        private static readonly string _systemId = "3c477fc7-0d4d-458a-6078-08dc43a0a620";

        public NpsResultService(INpsResultRepository npsRepository, HttpClient httpClient)
        {
            _npsRepository = npsRepository;
            _httpClient = httpClient;
        }

        public async Task<Guid> Incluir(NpsResultManipulacaoViewModel npsManipulacaoViewModel)
        {
            if (npsManipulacaoViewModel == null)
            {
                throw new ArgumentNullException(nameof(npsManipulacaoViewModel));
            }

            var createdDate = DateTime.UtcNow;

            var result = await PostNpsAsync(npsManipulacaoViewModel, createdDate);

            if (result.IsSuccessStatusCode)
            {
                return await IncluirNpsAsync(npsManipulacaoViewModel, createdDate);
            }
            else
            {
                throw new Exception($"Erro ao tentar fazer o POST do resultado de NPS. Status: {result.StatusCode}");
            }
        }

        public async Task<List<NpsResultViewModel?>?> ListarTodos()
        {
            var listaNps = await _npsRepository.SelecionarTudoAsync();

            var npsViewModels = listaNps.Select(nps => new NpsResultViewModel
            {
                Id = nps.Id,
                SystemId = nps.SystemId,
                CreatedDate = nps.CreatedDate,
                CategoryId = nps.CategoryId,
                Category = nps.CategoryId.HasValue ? CategoryTypeExtensions.FromGuid(nps.CategoryId.Value) : default,
                Comments = nps.Comments,
                Score = nps.Score,
                UserId = nps.UserId
            }).ToList();
            return npsViewModels;
        }


        private async Task<HttpResponseMessage> PostNpsAsync(NpsResultManipulacaoViewModel npsManipulacaoViewModel, DateTime createdDate)
        {
            var urlCreate = "https://nps-stg.ambevdevs.com.br/api/survey/create";

            var request = new HttpRequestMessage(HttpMethod.Post, urlCreate)
            {
                Content = JsonContent.Create(new
                {
                    createdDate = createdDate,
                    npsManipulacaoViewModel.Score,
                    comments = npsManipulacaoViewModel.Comments,
                    user = npsManipulacaoViewModel.UserId,
                    surveyType = 0,
                    systemId = _systemId,
                    categoryId = npsManipulacaoViewModel.Category.ToGuid()
                })
            };
            request.Headers.Add("Authorization", _systemId);

            return await _httpClient.SendAsync(request);
        }

        private async Task<Guid> IncluirNpsAsync(NpsResultManipulacaoViewModel npsManipulacaoViewModel, DateTime createdDate)
        {
            var nps = new NpsResult
            {
                SystemId = Guid.Parse(_systemId),
                CreatedDate = createdDate,
                CategoryId = npsManipulacaoViewModel.Category.ToGuid(),
                Comments = npsManipulacaoViewModel.Comments,
                Score = npsManipulacaoViewModel.Score,
                UserId = npsManipulacaoViewModel.UserId
            };

            await _npsRepository.IncluirAsync(nps);
            return nps.Id;
        }

        public async Task<HttpResponseMessage?> BuscarPesquisaNps(string user)
        {
            if (string.IsNullOrEmpty(user))
            {
                throw new ArgumentNullException(nameof(user));
            }

            var urlQuestion = "https://nps-stg.ambevdevs.com.br/api/question/check";

            var url = $"{urlQuestion}?user={user}&systemId={_systemId}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Authorization", _systemId);

            return await _httpClient.SendAsync(request);
        }
    }
}
