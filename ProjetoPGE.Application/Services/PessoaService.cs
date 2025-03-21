using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjetoPGE.Application.DTOs;
using ProjetoPGE.Application.InterfaceServices;
using ProjetoPGE.Application.ResponseHandler;
using ProjetoPGE.Domain.InterfaceRepositories;
using ProjetoPGE.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPGE.Application.Services
{
    public class PessoaService : PessoaInterfaceService
    {
        private readonly PessoaInterfaceRepository _repository;
        private readonly IMapper _mapper;

        public PessoaService(PessoaInterfaceRepository pessoaInterfaceRepository, IMapper mapper)
        {
            _repository = pessoaInterfaceRepository;
            _mapper = mapper;
        }

        public async Task<ObjectResult> DeletePessoa(int id)
        {
            try
            {
                var pessoa = await _repository.DeletePessoa(id);
                if (pessoa == null)
                {
                    return new NotFoundStatus<PessoaDTO>(
                        success: false,
                        message: "Pessoa não encontrada!",
                        error: ""
                    );
                }
                return new SuccessStatus<PessoaDTO>(
                    success: true,
                    message: "Pessoa excluída com sucesso!",
                    body: null
                );
            }
            catch (Exception ex)
            {
                return new InternalServerErrorStatus<PessoaDTO>(
                    success: false,
                    message: "Erro ao excluir pessoa!",
                    error: ex.Message
                );
            }
        }

        public async Task<ObjectResult> GetClientes()
        {
            try
            {
                var clientes = await _repository.GetClientes();
                if (clientes == null)
                {
                    return new NotFoundStatus<List<PessoaDTO>>(
                        success: false,
                        message: "Clientes não encontrados!",
                        error: ""
                    );
                }
                var data = _mapper.Map<List<PessoaDTO>>(clientes);
                return new SuccessStatus<List<PessoaDTO>>(
                    success: true,
                    message: "Clientes retornados com sucesso!",
                    body: _mapper.Map<List<PessoaDTO>>(clientes)
                );
            }
            catch (Exception ex)
            {
                return new InternalServerErrorStatus<List<PessoaDTO>>(
                    success: false,
                    message: "Erro ao buscar clientes!",
                    error: ex.Message
                );  
            }
        }

        public async Task<ObjectResult> GetPessoaById(int id)
        {
            try
            {
                var pessoa = await _repository.GetPessoaById(id);
                if (pessoa == null)
                {
                    return new NotFoundStatus<PessoaCadastroDTO>(
                        success: false,
                        message: "Pessoa não encontrada!",
                        error: ""
                    );
                }
                var data = _mapper.Map<PessoaCadastroDTO>(pessoa);
                return new SuccessStatus<PessoaCadastroDTO>(
                    success: true,
                    message: "Pessoa retornada com sucesso!",
                    body: data
                );
            }
            catch (Exception ex)
            {
                return new InternalServerErrorStatus<PessoaCadastroDTO>(
                    success: false,
                    message: "Erro ao buscar pessoa!",
                    error: ex.Message
                );
            }
        }

        public async Task<ObjectResult> GetPessoas()
        {
            try
            {
                var pessoas = await _repository.GetPessoas();
                if (pessoas == null)
                {
                    return new NotFoundStatus<List<PessoaDTO>>(
                        success: false,
                        message: "Pessoas não encontradas!",
                        error: ""
                    );
                }
                var data = _mapper.Map<List<PessoaDTO>>(pessoas);
                return new SuccessStatus<List<PessoaDTO>>(
                    success: true,
                    message: "Pessoas retornadas com sucesso!",
                    body: data
                );
            }
            catch (Exception ex)
            {
                return new InternalServerErrorStatus<List<PessoaDTO>>(
                    success: false,
                    message: "Erro ao buscar pessoas!",
                    error: ex.Message
                );
            }
        }

        public async Task<ObjectResult> GetProcuradores()
        {
            try
            {
                var procuradores = await _repository.GetProcuradores();
                if (procuradores == null)
                {
                    return new NotFoundStatus<List<PessoaDTO>>(
                        success: false,
                        message: "Procuradores não encontrados!",
                        error: ""
                    );
                }
                var data = _mapper.Map<List<PessoaDTO>>(procuradores);
                return new SuccessStatus<List<PessoaDTO>>(
                    success: true,
                    message: "Procuradores retornados com sucesso!",
                    body: data
                );
            }
            catch (Exception ex)
            {
                return new InternalServerErrorStatus<List<PessoaDTO>>(
                    success: false,
                    message: "Erro ao buscar procuradores!",
                    error: ex.Message
                );
            }
        }

        public async Task<ObjectResult> PostPessoa(PessoaCadastroDTO pessoaCadastroDTO)
        {
            try
            {
                var pessoaIncluida = await _repository.PostPessoa(_mapper.Map<ProjetoPGE.Domain.Models.Pessoa>(pessoaCadastroDTO));
                if (pessoaIncluida == null)
                {
                    return new NotFoundStatus<PessoaCadastroDTO>(
                        success: false,
                        message: "Pessoa não incluída!",
                        error: ""
                    );
                }
                var data = _mapper.Map<PessoaCadastroDTO>(pessoaIncluida);
                return new SuccessStatus<PessoaCadastroDTO>(
                    success: true,
                    message: "Pessoa incluída com sucesso!",
                    body: data
                );
            }
            catch (Exception ex)
            {
                return new InternalServerErrorStatus<PessoaCadastroDTO>(
                    success: false,
                    message: "Erro ao incluir pessoa!",
                    error: ex.Message
                );
            }
        }

        public async Task<ObjectResult> PutPessoa(PessoaAtualizacaoDTO pessoaDTO)
        {
            try
            {
                var pessoaExistente = await _repository.GetPessoaById(pessoaDTO.Id);
                if (pessoaExistente == null)
                {
                    return new NotFoundStatus<PessoaAtualizacaoDTO>(
                        success: false,
                        message: "Pessoa não encontrada!",
                        error: ""
                    );
                }

                // Se o usuário informou uma nova senha, gerar novo hash e salt
                if (!string.IsNullOrEmpty(pessoaDTO.Senha))
                {
                    using var hmac = new HMACSHA512();
                    pessoaExistente.passwordSalt = hmac.Key;
                    pessoaExistente.passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(pessoaDTO.Senha));
                }

                // Atualizar os campos permitidos
                pessoaExistente.NomePessoa = pessoaDTO.NomePessoa;
                pessoaExistente.TipoPessoa = pessoaDTO.TipoPessoa;
                pessoaExistente.Oab = pessoaDTO.Oab;
                pessoaExistente.LoginPessoa = pessoaDTO.LoginPessoa;

                // Chama o repositório para atualizar
                var pessoaAlterada = await _repository.PutPessoa(pessoaExistente);
                if (pessoaAlterada == null)
                {
                    return new NotFoundStatus<PessoaAtualizacaoDTO>(
                        success: false,
                        message: "Pessoa não alterada!",
                        error: ""
                    );
                }

                var data = _mapper.Map<PessoaAtualizacaoDTO>(pessoaAlterada);
                return new SuccessStatus<PessoaAtualizacaoDTO>(
                    success: true,
                    message: "Pessoa alterada com sucesso!",
                    body: data
                );
            }
            catch (Exception ex)
            {
                return new InternalServerErrorStatus<PessoaAtualizacaoDTO>(
                    success: false,
                    message: "Erro ao alterar pessoa!",
                    error: ex.Message
                );
            }
        }

        public async Task<ObjectResult> GetClienteById(int id)
        {
            try
            {
                var cliente = await _repository.GetClienteById(id);
                if (cliente == null)
                {
                    return new NotFoundStatus<PessoaDTO>(
                        success: false,
                        message: "Cliente não encontrado!",
                        error: ""
                    );
                }

                
                if (cliente.Oab)
                {
                    return new BadRequestStatus<List<ProcessoDTO>>(
                        success: false,
                        message: "O ID fornecido pertence a um Procurador, não a um Cliente.",
                        error: ""
                    );
                }

                var data = _mapper.Map<PessoaDTO>(cliente);
                return new SuccessStatus<PessoaDTO>(
                    success: true,
                    message: "Cliente retornado com sucesso!",
                    body: data
                );
            }
            catch(Exception ex)
            {
                return new InternalServerErrorStatus<PessoaDTO>(
                    success: false,
                    message: "Erro ao buscar cliente!",
                    error: ex.Message
                );
            }
        }

        public async Task<ObjectResult> GetProcuradorById(int id)
        {
            try
            {
                var procurador = await _repository.GetProcuradorById(id);
                if (procurador == null)
                {
                    return new NotFoundStatus<PessoaDTO>(
                        success: false,
                        message: "Procurador não encontrado!",
                        error: ""
                    );
                }

                if (!procurador.Oab)
                {
                    return new BadRequestStatus<List<ProcessoDTO>>(
                        success: false,
                        message: "O ID fornecido pertence a um Cliente, não a um Procurador.",
                        error: ""
                    );
                }

                var data = _mapper.Map<PessoaDTO>(procurador);
                return new SuccessStatus<PessoaDTO>(
                    success: true,
                    message: "Procurador retornado com sucesso!",
                    body: data
                );
            }
            catch (Exception ex)
            {
                return new InternalServerErrorStatus<PessoaDTO>(
                    success: false,
                    message: "Erro ao buscar procurador!",
                    error: ex.Message
                );
            }
        }

        public async Task<ObjectResult> GetPessoasByProcesso(int idProcesso)
        {
            try
            {
                var pessoas = await _repository.GetPessoasByProcesso(idProcesso);
                if (pessoas == null)
                {
                    return new NotFoundStatus<List<PessoaDTO>>(
                        success: false,
                        message: "Pessoas não encontradas!",
                        error: ""
                    );
                }
                var data = _mapper.Map<List<PessoaDTO>>(pessoas);
                return new SuccessStatus<List<PessoaDTO>>(
                    success: true,
                    message: "Pessoas encontradas com sucesso!",
                    body: data
                );
            }
            catch (Exception ex)
            {
                return new InternalServerErrorStatus<List<PessoaDTO>>(
                    success: false,
                    message: "Erro ao buscar pessoas!",
                    error: ex.Message
                );
            }
        }

        public async Task<ObjectResult> GetProcuradoresByProcesso(int idProcesso)
        {
            try
            {
                var procuradores = await _repository.GetProcuradoresByProcesso(idProcesso);
                if (procuradores == null)
                {
                    return new NotFoundStatus<List<PessoaDTO>>(
                        success: false,
                        message: "Procuradores não encontrados!",
                        error: ""
                    );
                }
                var data = _mapper.Map<List<PessoaDTO>>(procuradores);
                return new SuccessStatus<List<PessoaDTO>>(
                    success: true,
                    message: "Procuradores encontrados com sucesso!",
                    body: data
                );
            }
            catch (Exception ex)
            {
                return new InternalServerErrorStatus<List<PessoaDTO>>(
                    success: false,
                    message: "Erro ao buscar procuradores!",
                    error: ex.Message
                );
            }
        }

        public async Task<ObjectResult> GetClientesByProcesso(int idProcesso)
        {
            try
            {
                var clientes = await _repository.GetClientesByProcesso(idProcesso);
                if (clientes == null)
                {
                    return new NotFoundStatus<List<PessoaDTO>>(
                        success: false,
                        message: "Clientes não encontrados!",
                        error: ""
                    );
                }
                var data = _mapper.Map<List<PessoaDTO>>(clientes);
                return new SuccessStatus<List<PessoaDTO>>(
                    success: true,
                    message: "Clientes encontrados com sucesso!",
                    body: data
                );
            }
            catch (Exception ex)
            {
                return new InternalServerErrorStatus<List<PessoaDTO>>(
                    success: false,
                    message: "Erro ao buscar clientes!",
                    error: ex.Message
                );
            }
        }

        public async Task<ObjectResult> GetAdministradores()
        {
            try
            {
                var administradores = await _repository.GetAdministradores();
                if (administradores == null)
                {
                    return new NotFoundStatus<List<AdministradorDTO>>(
                        success: false,
                        message: "Administradores não encontrados!",
                        error: ""
                    );
                }
                var data = _mapper.Map<List<AdministradorDTO>>(administradores);
                return new SuccessStatus<List<AdministradorDTO>>(
                    success: true,
                    message: "Administradores retornados com sucesso!",
                    body: data
                );
            }
            catch (Exception ex)
            {
                return new InternalServerErrorStatus<List<AdministradorDTO>>(
                    success: false,
                    message: "Erro ao buscar administradores!",
                    error: ex.Message
                );
            }
        }

        public async Task<ObjectResult> GetAdministradorById(int id)
        {
            try
            {
                var administrador = await _repository.GetAdministradorById(id);
                if (administrador == null)
                {
                    return new NotFoundStatus<AdministradorDTO>(
                        success: false,
                        message: "Administrador não encontrado!",
                        error: ""
                    );
                }
                var data = _mapper.Map<AdministradorDTO>(administrador);
                return new SuccessStatus<AdministradorDTO>(
                    success: true,
                    message: "Administrador retornado com sucesso!",
                    body: data
                );
            }
            catch (Exception ex)
            {
                return new InternalServerErrorStatus<AdministradorDTO>(
                    success: false,
                    message: "Erro ao buscar administrador!",
                    error: ex.Message
                );
            }
        }

        public async Task<ObjectResult> CadastroPessoa(PessoaCadastroDTO pessoaCadastroDTO)
        {
            try
            {
                var pessoa = _mapper.Map<ProjetoPGE.Domain.Models.Pessoa>(pessoaCadastroDTO);

                if (pessoaCadastroDTO.Senha != null)
                {
                    using var hmac = new HMACSHA512();
                    pessoa.passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(pessoaCadastroDTO.Senha));
                    pessoa.passwordSalt = hmac.Key;
                }

                var pessoaIncluida = await _repository.PostPessoa(pessoa);

                if (pessoaIncluida == null)
                {
                    return new NotFoundStatus<PessoaCadastroDTO>(
                        success: false,
                        message: "Pessoa não incluída!",
                        error: ""
                    );
                }
                var data = _mapper.Map<PessoaCadastroDTO>(pessoaIncluida);
                return new SuccessStatus<PessoaCadastroDTO>(
                    success: true,
                    message: "Pessoa incluída com sucesso!",
                    body: data
                );
            }
            catch (Exception ex)
            {
                return new InternalServerErrorStatus<PessoaCadastroDTO>(
                    success: false,
                    message: "Erro ao incluir pessoa!",
                    error: ex.Message
                );
            }
        }
    }
}
