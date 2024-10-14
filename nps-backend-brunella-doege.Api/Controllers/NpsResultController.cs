using Microsoft.AspNetCore.Mvc;
using nps_backend_brunella_doege.Application.Service;
using nps_backend_brunella_doege.Application.ViewModels;
using System.Net;

namespace nps_backend_brunella_doege.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NpsResultController : ControllerBase
    {
        private readonly INpsResultService _service;

        public NpsResultController(INpsResultService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NpsResultManipulacaoViewModel npsManipulacaoViewModel)
        {
            if (npsManipulacaoViewModel == null)
            {
                return StatusCode(400, new { StatusCode = 400, Message = "Dados inválidos." });
            }

            try
            {
                var npsId = await _service.Incluir(npsManipulacaoViewModel);
                return StatusCode(200, new { StatusCode = 200, Message = "Resultado de Nps incluido com sucesso!", NpsId = npsId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao incluir o resultado de Nps: {ex.Message}");
            }
        }

        [HttpGet("listar")]
        public async Task<IActionResult> GetAllResults()
        {
            try
            {
                var resultados = await _service.ListarTodos();

                if (resultados == null || !resultados.Any())
                {
                    return StatusCode(404, new { StatusCode = 404, Message = "Nenhum resultado de Nps encontrado." });
                }

                return StatusCode(200, new { StatusCode = 200, Message = "Resultados de Nps encontrados com sucesso!", Resultados = resultados });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = $"Erro ao listar os resultados de Nps: {ex.Message}" });
            }
        }

        [HttpGet("nps/{user}")]
        public async Task<IActionResult> GetNpsByUser(string user)
        {
            try
            {
                var pesquisa = await _service.BuscarPesquisaNps(user);

                if (pesquisa == null)
                {
                    return StatusCode(404, new { StatusCode = 404, Message = "Nenhuma pesquisa de Nps encontrada." });
                }

                if (pesquisa.StatusCode == HttpStatusCode.NoContent)
                {
                    return StatusCode(409, new { StatusCode = 409, Message = "Você já respondeu a pesquisa!" });
                }

                return StatusCode(200, new { StatusCode = 200, Message = "Pesquisa de Nps encontrada com sucesso!", Pesquisa = pesquisa.Content.ReadAsStringAsync() });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = $"Erro ao buscar pesquisa de Nps: {ex.Message}" });
            }
        }

    }
}
