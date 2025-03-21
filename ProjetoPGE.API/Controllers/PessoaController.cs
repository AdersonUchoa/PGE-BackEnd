using Microsoft.AspNetCore.Mvc;
using ProjetoPGE.Application.DTOs;
using ProjetoPGE.Application.InterfaceServices;
using ProjetoPGE.API.Token;
using ProjetoPGE.Application.InterfaceResponseHandler;
using ProjetoPGE.Domain.Account;
using ProjetoPGE.Application.ResponseHandler;
using Microsoft.AspNetCore.Authorization;
using ProjetoPGE.Application.Services;
using System.Security.Claims;
using ProjetoPGE.Domain.Models;

namespace ProjetoPGE.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : Controller
    {
        private readonly PessoaInterfaceService _service;
        private readonly IAuthenticate _authenticate;

        public PessoaController(PessoaInterfaceService service, IAuthenticate authenticate)
        {
            _service = service;
            _authenticate = authenticate;
        }

        /// <summary>
        /// Rota para buscar todas as pessoas, sejam clientes ou procuradores.
        /// </summary>
        /// <remarks>Retorna todos as pessoas cadastrados no sistema.</remarks>
        /// <response code="200">Retorna todos as pessoas com sucesso.</response>
        /// <response code="500">Erro interno no servidor.</response>
        /// <returns>Lista de pessoas</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ISuccessStatus<List<PessoaDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IInternalServerErrorStatus<List<PessoaDTO>>), StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetPessoas()
        {
            return await _service.GetPessoas();
        }

        /// <summary>
        /// Rota para incluir uma pessoa.
        /// </summary>
        /// <remarks>Inclusão de uma pessoa no sistema.</remarks>
        /// <response code="200">Pessoa incluída com sucesso.</response>
        /// <response code= "500">Erro interno do servidor</response>
        /// <returns>Inclusão de uma pessoa</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ISuccessStatus<PessoaDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IInternalServerErrorStatus<PessoaDTO>), StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult> PostPessoa(PessoaCadastroDTO pessoaDTO)
        {
            return await _service.PostPessoa(pessoaDTO);
        }

        /// <summary>
        /// Rota para alterar uma pessoa.
        /// </summary>
        /// <remarks>Alteração de uma pessoa no sistema.</remarks>
        /// <response code="200">Pessoa alterada com sucesso.</response>
        /// <response code="500">Erro interno do servidor</response>
        /// <returns>Alteração de uma pessoa</returns>
        [HttpPut]
        [ProducesResponseType(typeof(ISuccessStatus<PessoaDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IInternalServerErrorStatus<PessoaDTO>), StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<ActionResult> PutPessoa(PessoaAtualizacaoDTO pessoaDTO)
        {
            return await _service.PutPessoa(pessoaDTO);
        }

        /// <summary>
        /// Rota para excluir uma pessoa.
        /// </summary>
        /// <remarks>Exclusão de uma pessoa no sistema.</remarks>
        /// <response code="200">Pessoa excluída com sucesso.</response>
        /// <response code="500">Erro interno do servidor</response>
        /// <returns>Exclusão de uma pessoa</returns>    
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ISuccessStatus<PessoaDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IInternalServerErrorStatus<PessoaDTO>), StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<ActionResult> DeletePessoa(int id)
        {
            return await _service.DeletePessoa(id);
        }

        /// <summary>
        /// Rota para buscar uma pessoa pelo ID.
        /// </summary>
        /// <remarks>Retorna uma pessoa cadastrada no sistema.</remarks>
        /// <response code="200">Retorna a pessoa com sucesso.</response>
        /// <response code="500">Erro interno no servidor.</response>
        /// <returns>Busca de uma pessoa</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ISuccessStatus<PessoaDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IInternalServerErrorStatus<PessoaDTO>), StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<ActionResult> GetPessoaById(int id)
        {
            return await _service.GetPessoaById(id);
        }

        /// <summary>
        /// Rota para buscar todos os clientes.
        /// </summary>
        /// <remarks>Retorna todos os clientes cadastrados no sistema.</remarks>
        /// <response code="200">Retorna todos os clientes com sucesso.</response>
        /// <response code="500">Erro interno no servidor.</response>
        /// <returns>Lista de todos os clientes</returns>
        [HttpGet("clientes")]
        [ProducesResponseType(typeof(ISuccessStatus<List<PessoaDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IInternalServerErrorStatus<List<PessoaDTO>>), StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult> GetClientes()
        {
            return await _service.GetClientes();
        }

        /// <summary>
        /// Rota para buscar todos os procuradores.
        /// </summary>
        /// <remarks>Retorna todos os procuradores cadastrados no sistema.</remarks>
        /// <response code="200">Retorna todos os procuradores com sucesso.</response>
        /// <response code="500">Erro interno no servidor.</response>
        /// <returns>Lista de todos os procuradores</returns>
        [HttpGet("procuradores")]
        [ProducesResponseType(typeof(ISuccessStatus<List<PessoaDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IInternalServerErrorStatus<List<PessoaDTO>>), StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult> GetProcuradores()
        {
            return await _service.GetProcuradores();
        }

        /// <summary>
        /// Rota para buscar um cliente pelo ID.
        /// </summary>
        /// <remarks>Retorna um cliente cadastrado no sistema.</remarks>
        /// <response code="200">Retorna o cliente com sucesso.</response>
        /// <response code="500">Erro interno no servidor.</response>
        /// <returns>Busca de um cliente</returns>
        [HttpGet("clientes/{id}")]
        [ProducesResponseType(typeof(ISuccessStatus<PessoaDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IInternalServerErrorStatus<PessoaDTO>), StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult> GetClienteById(int id)
        {
            return await _service.GetClienteById(id);
        }

        /// <summary>
        /// Rota para buscar um procurador pelo ID.
        /// </summary>
        /// <remarks>Retorna um procurador cadastrado no sistema</remarks>
        /// <response code="200">Retorna o procurador com sucesso.</response>
        /// <response code="500">Erro interno no servidor.</response>
        /// <returns>Busca de um procurador</returns>
        [HttpGet("procuradores/{id}")]
        [ProducesResponseType(typeof(ISuccessStatus<PessoaDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IInternalServerErrorStatus<PessoaDTO>), StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult> GetProcuradorById(int id)
        {
            return await _service.GetProcuradorById(id);
        }

        /// <summary>
        /// Rota para buscar todas as pessoas de um processo.
        /// </summary>
        /// <remarks>Retorna todas as pessoas de um processo cadastradas no sistema.</remarks>
        /// <response code="200">Retorna todas as pessoas de um processo com sucesso.</response>
        /// <response code="500">Erro interno no servidor.</response>
        /// <returns>Todas as pessoas de um processo, sejam clientes ou procuradores</returns>
        [HttpGet("{idProcesso}/pessoas")]
        [ProducesResponseType(typeof(ISuccessStatus<List<PessoaDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IInternalServerErrorStatus<List<PessoaDTO>>), StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetPessoasByProcesso(int idProcesso)
        {
            return await _service.GetPessoasByProcesso(idProcesso);
        }

        /// <summary>
        /// Rota para buscar o procurador de um processo.
        /// </summary>
        /// <remarks>Retorna o procurador de um processo cadastrado no sistema.</remarks>
        /// <response code="200">Retorna o procurador de um processo com sucesso.</response>
        /// <response code="500">Erro interno no servidor.</response>
        /// <returns>Obtem o procurador associado a um processo</returns>
        [HttpGet("{idProcesso}/procurador")]
        [ProducesResponseType(typeof(ISuccessStatus<List<PessoaDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IInternalServerErrorStatus<List<PessoaDTO>>), StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "ProcuradorOuAdmin")]
        public async Task<IActionResult> GetProcuradorByProcesso(int idProcesso)
        {
            return await _service.GetProcuradoresByProcesso(idProcesso);
        }

        /// <summary>
        /// Rota para buscar os clientes de um processo.
        /// </summary>
        /// <remarks>Retorna os clientes de um processo cadastrados no sistema.</remarks>
        /// <response code="200">Retorna os clientes de um processo com sucesso.</response>
        /// <response code="500">Erro interno no servidor.</response>
        /// <returns>Obtem todos os clientes associados a um processo</returns>
        [HttpGet("{idProcesso}/clientes")]
        [ProducesResponseType(typeof(ISuccessStatus<List<PessoaDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IInternalServerErrorStatus<List<PessoaDTO>>), StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetClientesByProcesso(int idProcesso)
        {
            return await _service.GetClientesByProcesso(idProcesso);
        }

        /// <summary>
        /// Rota para buscar todos os administradores.
        /// </summary>
        /// <remarks>Retorna todos os administradores cadastrados no sistema.</remarks>
        /// <response code = "200">Retorna todos os administradores com sucesso.</response>
        /// <response code = "500">Erro interno no servidor.</response>
        /// <returns>Lista de administradores</returns>
        [HttpGet("administradores")]
        [ProducesResponseType(typeof(ISuccessStatus<List<PessoaDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IInternalServerErrorStatus<List<PessoaDTO>>), StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetAdministradores()
        {
            return await _service.GetAdministradores();
        }


        /// <summary>
        /// Rota para buscar um administrador pelo ID.
        /// </summary>
        /// <remarks>Retorna um administrador cadastrado no sistema.</remarks>
        /// <response code="200">Retorna o administrador com sucesso.</response>
        /// <response code="500">Erro interno no servidor.</response>
        /// <returns>Retorna um administrador pelo ID</returns>
        [HttpGet("administradores/{id}")]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IInternalServerErrorStatus<UserToken>), StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetAdministradorById(int id)
        {
            return await _service.GetAdministradorById(id);
        }

        [HttpGet("perfil")]
        public IActionResult GetPerfil()
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
            {
                return Unauthorized("Usuário não autenticado.");
            }

            var pessoa = _service.GetPessoaById(userId);
            if (pessoa == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            return Ok(pessoa);
        }

        // Endpoint para atualizar os dados do perfil
        [HttpPut("perfil")]
        public async Task<IActionResult> UpdatePerfil([FromBody] PessoaAtualizacaoDTO pessoaCadastroDTO)
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
            {
                return Unauthorized("Usuário não autenticado.");
            }

            var resultado = await _service.GetPessoaById(userId);
            if (resultado == null || resultado.Value == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            // Converte o ObjectResult para Pessoa (assumindo que resultado.Value contém uma Pessoa)
            var pessoa = resultado.Value as PessoaAtualizacaoDTO;
            if (pessoa == null)
            {
                return StatusCode(500, "Erro ao processar os dados do usuário.");
            }

            // Atualiza os dados permitidos
            pessoa.NomePessoa = pessoaCadastroDTO.NomePessoa;
            pessoa.LoginPessoa = pessoaCadastroDTO.LoginPessoa;
            pessoa.Senha = pessoaCadastroDTO.Senha;

            await _service.PutPessoa(pessoa); // Atualiza no banco

            return Ok("Perfil atualizado com sucesso.");
        }


        /// <summary>
        /// Rota para registrar uma pessoa com senha hash.
        /// </summary>
        /// <remarks>Retorna um token de autenticação.</remarks>
        /// <response code="200">Retorna o token com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="400">Login já cadastrado.</response>
        /// <response code="400">Ocorreu um erro ao cadastrar.</response>
        /// <response code="500">Erro interno no servidor.</response>
        /// <returns>Cadastro com token de autenticação</returns>
        [HttpPost("registrar")]
        [ProducesResponseType(typeof(UserToken), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IInternalServerErrorStatus<UserToken>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserToken>> Cadastrar(PessoaCadastroDTO pessoaCadastroDTO)
        {
            if (pessoaCadastroDTO == null)
            {
                return BadRequest("Dados inválidos");
            }

            var loginExiste = await _authenticate.pessoaExiste(pessoaCadastroDTO.LoginPessoa);
            if (loginExiste)
            {
                return BadRequest("Login já cadastrado");
            }

            var pessoaResult = await _service.CadastroPessoa(pessoaCadastroDTO);

            // Agora, pessoaResult já é SuccessStatus<PessoaCadastroDTO>
            if (pessoaResult is SuccessStatus<PessoaCadastroDTO> successStatus)
            {
                var pessoa = successStatus.Body;
                var token = _authenticate.GenerateToken(pessoa.Id, pessoa.NomePessoa);
                return new UserToken { TokenString = await token };
            }

            return BadRequest("Ocorreu um erro ao cadastrar.");

        }

        /// <summary>
        /// Rota para autenticar uma pessoa.
        /// </summary>
        /// <remarks>Retorna um token de autenticação.</remarks>
        /// <response code="200">Retorna o token com sucesso.</response>
        /// <response code="401">Usuário ou senha inválidos.</response>
        /// <response code="401">Usuário não existe.</response>
        /// <response code="500">Erro interno no servidor.</response>
        /// <returns>Login com token de autenticação</returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(UserToken), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UnauthorizedObjectResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(IInternalServerErrorStatus<UserToken>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserToken>> Login(LoginModel loginModel)
        {
            var existe = await _authenticate.pessoaExiste(loginModel.loginPessoa);
            if (!existe)
            {
                return Unauthorized("Usuário não existe");
            }

            var result = await _authenticate.AuthenticateAsync(loginModel.loginPessoa, loginModel.password); 
            if(!result)
            {
                return Unauthorized("Usuário ou senha inválidos");
            }

            var pessoa = await _authenticate.GetPessoaByLogin(loginModel.loginPessoa);
            var token = _authenticate.GenerateToken(pessoa.Id, loginModel.loginPessoa);
            return new UserToken { TokenString = await token };
        }
    }
}
