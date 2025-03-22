using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoPGE.Application.DTOs;
using ProjetoPGE.Application.InterfaceResponseHandler;
using ProjetoPGE.Application.InterfaceServices;
using ProjetoPGE.Application.Services;
using ProjetoPGE.Domain.Models;
using System.Security.Claims;

namespace ProjetoPGE.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProcessoController : Controller
    {
        private readonly ProcessoInterfaceService _service;
        private readonly PessoaInterfaceService _pessoaService;

        public ProcessoController(ProcessoInterfaceService service, PessoaInterfaceService pessoaService)
        {
            _service = service;
            _pessoaService = pessoaService;
        }

        /// <summary>
        /// Rota para buscar todos os processos.
        /// </summary>
        /// <remarks>Retorna todos os processos cadastrados no sistema.</remarks>
        /// <response code="200">Retorna todos os processos com sucesso.</response>
        /// <response code="500">Erro interno no servidor.</response>
        /// <returns>Lista de todos os processos</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ISuccessStatus<List<ProcessoDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IInternalServerErrorStatus<List<ProcessoDTO>>), StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "ProcuradorOuAdmin")]
        public async Task<IActionResult> GetProcessos()
        {
            return await _service.GetProcessos();
        }

        /// <summary>
        /// Segunda rota para buscar todos os processos.
        /// </summary>
        /// <remarks>Retorna todos os processos cadastrados no sistema.</remarks>
        /// <response code="200">Retorna todos os processos com sucesso.</response>
        /// <response code="500">Erro interno no servidor.</response>
        /// <returns>Lista de todos os processos</returns>
        [HttpGet("buscarProcessos")]
        [ProducesResponseType(typeof(ISuccessStatus<List<ProcessoDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IInternalServerErrorStatus<List<ProcessoDTO>>), StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "ProcuradorOuAdmin")]
        public async Task<IActionResult> BuscarProcessos(String busca)
        {
            return await _service.BuscarProcessos(busca);
        }

        /// <summary>
        /// Rota para incluir um processo.
        /// </summary>
        /// <remarks>Inclusão de um processo no sistema.</remarks>
        /// <response code="200">Processo incluído com sucesso.</response>
        /// <response code="500">Erro interno do servidor</response>
        /// <returns>Inclusão de um processo</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ISuccessStatus<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IInternalServerErrorStatus<string>), StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "ProcuradorOuAdmin")]
        public async Task<ActionResult> PostProcesso(ProcessoDTO processoDTO)
        {
            return await _service.PostProcesso(processoDTO);
        }

        /// <summary>
        /// Rota para alterar um processo.
        /// </summary> 
        /// <remarks>Alteração de um processo no sistema.</remarks>
        /// <response code="200">Processo alterado com sucesso.</response>
        /// <response code="500">Erro interno do servidor</response>
        /// <returns>Alteração de um processo</returns>
        [HttpPut]
        [ProducesResponseType(typeof(ISuccessStatus<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IInternalServerErrorStatus<string>), StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "ProcuradorOuAdmin")]
        public async Task<ActionResult> PutProcesso(ProcessoDTO processoDTO)
        {
            return await _service.PutProcesso(processoDTO);
        }

        /// <summary>
        /// Rota para deletar um processo.
        /// </summary>
        /// <remarks>Exclusão de um processo no sistema.</remarks>
        /// <response code="200">Processo excluído com sucesso.</response>
        /// <response code="500">Erro interno do servidor</response>
        /// <returns>Exclusão de um processo</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ISuccessStatus<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IInternalServerErrorStatus<string>), StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "ProcuradorOuAdmin")]
        public async Task<ActionResult> DeleteProcesso(int id)
        {
            return await _service.DeleteProcesso(id);
        }

        /// <summary>
        /// Rota para buscar um processo por id.
        /// </summary>
        /// <remarks>Retorna um processo cadastrado no sistema.</remarks>
        /// <response code="200">Retorna um processo com sucesso.</response>
        /// <response code="500">Erro interno no servidor.</response>
        /// <returns>Processo</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ISuccessStatus<ProcessoDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IInternalServerErrorStatus<ProcessoDTO>), StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "ProcuradorOuAdmin")]
        public async Task<ActionResult> GetProcessoById(int id)
        {
            return await _service.GetProcessoById(id);
        }

        /// <summary>
        /// Rota para redistribuir um processo de um procurador para outro.
        /// </summary>
        /// <remarks>Redistribuição de um processo de um procurador para outro.</remarks>
        /// <response code="200">Processo redistribuído com sucesso.</response>
        /// <response code="500">Erro interno no servidor.</response>
        /// <returns>Um novo procurador associado a um processo</returns>
        [HttpPut("{idProcesso}/redistribuir")]
        [ProducesResponseType(typeof(ISuccessStatus<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IInternalServerErrorStatus<string>), StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> AlterarProcurador(int idProcesso, int idNovoProcurador)
        {
            return await _service.AlterarProcuradorAsync(idProcesso, idNovoProcurador);
        }

        /// <summary>
        /// Rota para buscar todos os processos de um cliente.
        /// </summary>
        /// <remarks>Retorna todos os processos de um cliente cadastrados no sistema.</remarks>
        /// <response code="200">Retorna todos os processos de um cliente com sucesso.</response>
        /// <response code="500">Erro interno no servidor.</response> 
        /// <returns>Processos do cliente</returns>
        [HttpGet("cliente/{idPessoa}/processos")]
        [ProducesResponseType(typeof(ISuccessStatus<List<ProcessoDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IInternalServerErrorStatus<List<ProcessoDTO>>), StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "ClienteOuAdmin")]
        public async Task<IActionResult> GetProcessosByCliente(int idPessoa)
        {
            return await _service.GetProcessosByCliente(idPessoa);
        }

        /// <summary>
        /// Rota para buscar todos os processos de um procurador.
        /// </summary>
        /// <remarks>Retorna todos os processos de um procurador cadastrados no sistema.</remarks>
        /// <response code="200">Retorna todos os processos de um procurador com sucesso.</response>
        /// <response code="500">Erro interno no servidor.</response>
        /// <returns>Processos de um procurador.</returns>
        [HttpGet("procurador/{idPessoa}/processos")]
        [ProducesResponseType(typeof(ISuccessStatus<List<ProcessoDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IInternalServerErrorStatus<List<ProcessoDTO>>), StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "ProcuradorOuAdmin")]
        public async Task<IActionResult> GetProcessosByProcurador(int idPessoa)
        {
            return await _service.GetProcessosByProcurador(idPessoa);
        }
    }
}
