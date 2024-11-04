using Moq;
using Moq.Protected;
using nps_backend_brunella_doege.Application.Service;
using nps_backend_brunella_doege.Application.ViewModels;
using nps_backend_brunella_doege.Domain.Entities;
using nps_backend_brunella_doege.Domain.Repositories;
using System.Net;

namespace nps_backend_brunella_doege.Tests
{
    public class NpsResultServiceTests
    {
        private readonly Mock<INpsResultRepository> _mockRepository;
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly HttpClient _mockHttpClient;
        private readonly NpsResultService _service;

        public NpsResultServiceTests()
        {
            _mockRepository = new Mock<INpsResultRepository>();
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            _mockHttpClient = new HttpClient(_httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("http://example.com/")
            };
            _service = new NpsResultService(_mockRepository.Object, _mockHttpClient);
        }

        [Fact]
        public async Task IncludeNps_ShouldThrowArgumentNullException_WhenNpsInputViewModelIsNull()
        {
            string urlCreate = "http://example.com/api/nps";
            string systemId = "testSystemId";

            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.IncludeNpsAsync(null, urlCreate, systemId));
        }

        [Fact]
        public async Task Include_ShouldThrowArgumentNullException_WhenUrlCreateIsNull()
        {
            var npsInputViewModel = new NpsResultInputViewModel();
            string systemId = "testSystemId";

            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.IncludeNpsAsync(npsInputViewModel, null, systemId));
        }

        [Fact]
        public async Task IncludeNps_ShouldReturnGuid_WhenPostAndIncludeAreSuccessful()
        {
            var npsInputViewModel = new NpsResultInputViewModel { Score = 9, Comments = "Good service", UserId = "user123" };
            string urlCreate = "http://example.com/api/nps";
            string systemId = Guid.NewGuid().ToString();

            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            _mockRepository.Setup(repo => repo.IncludeAsync(It.IsAny<NpsResult>()))
                .Returns(Task.CompletedTask);

            var result = await _service.IncludeNpsAsync(npsInputViewModel, urlCreate, systemId);

            Assert.Equal(Guid.Empty, result);
        }

        [Fact]
        public async Task SelectAllNps_ShouldReturnListOfNpsResultViewModel()
        {
            var npsList = new List<NpsResult>
        {
            new NpsResult { Id = Guid.NewGuid(), Score = 9, UserId = "user123", CreatedDate = DateTime.UtcNow, Comments = "Excellent" }
        };

            _mockRepository.Setup(repo => repo.SelectAllAsync())
                .ReturnsAsync(npsList);

            var result = await _service.SelectAllNpsAsync();

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(npsList.First().Score, result.First().Score);
            Assert.Equal(npsList.First().Comments, result.First().Comments);
        }

        [Fact]
        public async Task GetNps_ShouldThrowArgumentNullException_WhenUserIsNullOrEmpty()
        {
            string urlQuestion = "http://example.com/api/question";
            string systemId = "testSystemId";

            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.GetNpsAsync(null, urlQuestion, systemId));
        }

        [Fact]
        public async Task GetNps_ShouldReturnHttpResponse_WhenUserAndUrlAreValid()
        {
            string user = "user123";
            string urlQuestion = "http://example.com/api/question";
            string systemId = "testSystemId";

            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            var result = await _service.GetNpsAsync(user, urlQuestion, systemId);

            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}