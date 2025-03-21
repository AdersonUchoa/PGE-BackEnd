using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoPGE.Application.DTOs;
using ProjetoPGE.Application.InterfaceResponseHandler;
using ProjetoPGE.Application.InterfaceServices;
using ProjetoPGE.Application.ResponseHandler;

namespace ProjetoPGE.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = "ProcuradorOuAdmin")]
    public class DocumentoController : Controller
    {
        private readonly DocumentoInterfaceService _service;

        public DocumentoController(DocumentoInterfaceService service)
        {
            _service = service;
        }

        /// <summary>
        /// Rota para buscar todos os documentos.
        /// </summary>
        /// <remarks>Retorna todos os documentos cadastrados no sistema.</remarks>
        /// <response code="200">Retorna todos os documentos com sucesso.</response>
        /// <response code="500">Erro interno no servidor.</response>
        /// <returns>Lista de documentos</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ISuccessStatus<List<DocumentoDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IInternalServerErrorStatus<List<DocumentoDTO>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDocumentos()
        {
            return await _service.GetDocumentos();
        }


        /// <summary>
        /// Rota para incluir um documento.
        /// </summary>
        /// <remarks>Inclusão de um documento no sistema.</remarks>
        /// <response code="200">Documento incluído com sucesso.</response>
        /// <response code="500">Erro interno do servidor</response>
        /// <returns>Inclusão de um documento</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ISuccessStatus<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IInternalServerErrorStatus<string>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PostDocumento(DocumentoDTO documentoDTO)
        {
            return await _service.PostDocumento(documentoDTO);
            
        }

        /// <summary>
        /// Rota para alterar um documento.
        /// </summary>
        /// <remarks>Alteração de um documento no sistema.</remarks>
        /// <response code="200">Documento alterado com sucesso.</response>
        /// <response code="500">Erro interno do servidor</response>
        /// <returns>Alteração de um documento</returns>
        [HttpPut]
        [ProducesResponseType(typeof(ISuccessStatus<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IInternalServerErrorStatus<string>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PutDocumento(DocumentoDTO documentoDTO)
        {
           return await _service.PutDocumento(documentoDTO);
        }


        /// <summary>
        /// Rota para deletar um documento.
        /// </summary>
        /// <remarks>Remoção de um documento no sistema.</remarks>
        /// <response code="200">Documento incluído com sucesso.</response>
        /// <response code="500">Erro interno do servidor</response>
        /// <returns>Remoção de um documento</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ISuccessStatus<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IInternalServerErrorStatus<string>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteDocumento(int id)
        {
            return await _service.DeleteDocumento(id);
        }

        /// <summary>
        /// Rota para buscar um documento pelo seu ID.
        /// </summary>
        /// <remarks>Busca de um documento no sistema.</remarks>
        /// <response code="200">Documento encontrado com sucesso.</response>
        /// <response code="500">Erro interno do servidor</response>
        /// <returns>Busca de um documento por ID</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ISuccessStatus<DocumentoDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IInternalServerErrorStatus<DocumentoDTO>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetDocumentoById(int id)
        {
            return await _service.GetDocumentoById(id);
        }

        /// <summary>
        /// Rota para buscar todos os documentos de um processo.
        /// </summary>
        /// <remarks> Retorna todos os documentos de um processo.</remarks>
        /// <response code="200">Documentos encontrados com sucesso.</response>
        /// <response code="500">Erro interno do servidor</response>
        /// <returns> Documentos de um processo.</returns>
        [HttpGet("{idProcesso}/documentos")]
        [ProducesResponseType(typeof(ISuccessStatus<List<DocumentoDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IInternalServerErrorStatus<List<DocumentoDTO>>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetDocumentosByProcesso(int idProcesso)
        {
            return await _service.GetDocumentosByProcesso(idProcesso);
        }
    }
}
