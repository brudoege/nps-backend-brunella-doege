using nps_backend_brunella_doege.Application.ViewModels;
using nps_backend_brunella_doege.Domain.Entities;
using nps_backend_brunella_doege.Domain.Enums;
using nps_backend_brunella_doege.Domain.Repositories;
using System.Net.Http.Json;

namespace nps_backend_brunella_doege.Application.Service
{
    public class NpsResultService : INpsResultService
    {
        private readonly INpsResultRepository _npsRepository;
        private readonly HttpClient _httpClient;

        public NpsResultService(INpsResultRepository npsRepository, HttpClient httpClient)
        {
            _npsRepository = npsRepository;
            _httpClient = httpClient;
        }

        public async Task<Guid> IncludeNpsAsync(NpsResultInputViewModel npsInputViewModel, string urlCreate, string systemId)
        {
            if (npsInputViewModel == null)
            {
                throw new ArgumentNullException(nameof(npsInputViewModel));
            }

            if (urlCreate == null)
            {
                throw new ArgumentNullException(nameof(urlCreate));
            }

            var createdDate = DateTime.UtcNow;

            var result = await PostNpsAsync(npsInputViewModel, createdDate, urlCreate, systemId);

            if (result.IsSuccessStatusCode)
            {
                return await IncludeNpsSystemAsync(npsInputViewModel, createdDate, systemId);
            }
            else
            {
                throw new Exception($"Erro ao tentar fazer o POST do resultado de NPS. Status: {result.StatusCode}");
            }
        }

        public async Task<List<NpsResultViewModel>> SelectAllNpsAsync()
        {
            var listaNps = await _npsRepository.SelectAllAsync();

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


        private async Task<HttpResponseMessage> PostNpsAsync(NpsResultInputViewModel npsInputViewModel, DateTime createdDate, string urlCreate, string systemId)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, urlCreate)
            {
                Content = JsonContent.Create(new
                {
                    createdDate,
                    npsInputViewModel.Score,
                    comments = npsInputViewModel.Comments,
                    user = npsInputViewModel.UserId,
                    surveyType = 0,
                    systemId,
                    categoryId = npsInputViewModel.Category.ToGuid()
                })
            };
            request.Headers.Add("Authorization", systemId);

            return await _httpClient.SendAsync(request);
        }

        private async Task<Guid> IncludeNpsSystemAsync(NpsResultInputViewModel npsInputViewModel, DateTime createdDate, string systemId)
        {
            var nps = new NpsResult
            {
                SystemId = Guid.Parse(systemId),
                CreatedDate = createdDate,
                CategoryId = npsInputViewModel.Category.ToGuid(),
                Comments = npsInputViewModel.Comments,
                Score = npsInputViewModel.Score,
                UserId = npsInputViewModel.UserId
            };

            await _npsRepository.IncludeAsync(nps);
            return nps.Id;
        }

        public async Task<HttpResponseMessage?> GetNpsAsync(string user, string urlQuestion, string systemId)
        {
            if (string.IsNullOrEmpty(user))
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrEmpty(urlQuestion))
            {
                throw new ArgumentNullException(nameof(urlQuestion));
            }

            var url = $"{urlQuestion}?user={user}&systemId={systemId}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Authorization", systemId);

            return await _httpClient.SendAsync(request);
        }
    }
}
