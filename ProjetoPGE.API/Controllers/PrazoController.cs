using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoPGE.Application.DTOs;
using ProjetoPGE.Application.InterfaceResponseHandler;
using ProjetoPGE.Application.InterfaceServices;

namespace ProjetoPGE.API.Controllers
{
    [Authorize(Policy = "ProcuradorOuAdmin")]
    [ApiController]
    [Route("[controller]")]
    public class PrazoController : Controller
    {
        private readonly PrazoInterfaceService _service;

        public PrazoController(PrazoInterfaceService service)
        {
            _service = service;
        }

        /// <summary>
        /// Rota para buscar todos os prazos.
        /// </summary>
        /// <remarks>Retorna todos os prazos cadastrados no sistema.</remarks>
        /// <response code="200">Retorna todos os prazos com sucesso.</response>
        /// <response code="500">Erro interno no servidor.</response>
        /// <returns>Lista de prazos</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ISuccessStatus<List<PrazoDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IInternalServerErrorStatus<List<PrazoDTO>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPrazos()
        {
            return await _service.GetPrazos();
        }

        /// <summary>
        /// Rota para incluir um prazo.
        /// </summary>
        /// <remarks>Inclusão de um prazo no sistema.</remarks>
        /// <response code="200">Prazo incluído com sucesso.</response>
        /// <response code="500">Erro interno do servidor</response>
        /// <returns>Inclusão de um prazo</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ISuccessStatus<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IInternalServerErrorStatus<string>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PostPrazo(PrazoDTO prazoDTO)
        {
            return await _service.PostPrazo(prazoDTO);
        }

        /// <summary>
        /// Rota para alterar um prazo.
        /// </summary>
        /// <remarks>Alteração de um prazo no sistema.</remarks>
        /// <response code="200">Prazo alterado com sucesso.</response>
        /// <response code="500">Erro interno do servidor</response>
        /// <returns>Alteração de um prazo</returns>
        [HttpPut]
        [ProducesResponseType(typeof(ISuccessStatus<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IInternalServerErrorStatus<string>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PutPrazo(PrazoDTO prazoDTO)
        {
            return await _service.PutPrazo(prazoDTO);
        }

        /// <summary>   
        /// Rota para deletar um prazo.
        /// </summary>
        /// <remarks>Exclusão de um prazo no sistema.</remarks>
        /// <response code="200">Prazo excluído com sucesso.</response>
        /// <response code="500">Erro interno do servidor</response>
        /// <returns>Exclusão de um prazo</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ISuccessStatus<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IInternalServerErrorStatus<string>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeletePrazo(int id)
        {
            return await _service.DeletePrazo(id);
        }

        /// <summary>
        /// Rota para buscar um prazo por id.
        /// </summary>
        /// <remarks>Retorna um prazo cadastrado no sistema.</remarks>
        /// <response code="200">Retorna um prazo com sucesso.</response>
        /// <response code="500">Erro interno no servidor.</response>
        /// <returns>Prazo</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ISuccessStatus<PrazoDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IInternalServerErrorStatus<PrazoDTO>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetPrazoById(int id)
        {
            return await _service.GetPrazoById(id);
        }

        /// <summary>
        /// Rota para buscar os prazos de um processo.
        /// </summary>
        /// <remarks>Retorna os prazos de um processo cadastrados no sistema.</remarks>
        /// <response code="200">Retorna os prazos de um processo com sucesso.</response>
        /// <response code="500">Erro interno no servidor.</response>
        /// <returns>Todos os prazos de um processo</returns>
        [HttpGet("{idProcesso}/prazos")]
        [ProducesResponseType(typeof(ISuccessStatus<List<PrazoDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IInternalServerErrorStatus<List<PrazoDTO>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPrazosByProcesso(int idProcesso)
        {
            return await _service.GetPrazosByProcesso(idProcesso);
        }
    }
}
