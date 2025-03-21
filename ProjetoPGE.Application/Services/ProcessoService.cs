using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjetoPGE.Application.DTOs;
using ProjetoPGE.Application.InterfaceServices;
using ProjetoPGE.Application.ResponseHandler;
using ProjetoPGE.Domain.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPGE.Application.Services
{
    public class ProcessoService : ProcessoInterfaceService
    {
        private readonly ProcessoInterfaceRepository _repository;
        private readonly IMapper _mapper;
        private readonly PessoaInterfaceRepository _pessoaInterfaceRepository;

        public ProcessoService(ProcessoInterfaceRepository processoInterfaceRepository, IMapper mapper, PessoaInterfaceRepository pessoaInterfaceRepository)
        {
            _repository = processoInterfaceRepository;
            _mapper = mapper;
            _pessoaInterfaceRepository = pessoaInterfaceRepository;
        }

        public async Task<ObjectResult> DeleteProcesso(int id)
        {
            try
            {
                var processo = await _repository.DeleteProcesso(id);
                if (processo == null)
                {
                    return new NotFoundStatus<ProcessoDTO>(
                        success: false,
                        message: "Processo não encontrado!",
                        error: ""
                    );
                }
                var data = _mapper.Map<ProcessoDTO>(processo);
                return new SuccessStatus<ProcessoDTO>(
                    success: true,
                    message: "Processo excluído com sucesso!",
                    body: data
                );
            }
            catch (Exception ex)
            {
                return new InternalServerErrorStatus<ProcessoDTO>(
                    success: false,
                    message: "Erro ao excluir processo!",
                    error: ex.Message
                );
            }
        }

        public async Task<ObjectResult> GetProcessoById(int id)
        { 
            try
            {
                var processo = await _repository.GetProcessoById(id);
                if (processo == null)
                {
                    return new NotFoundStatus<ProcessoDTO>(
                        success: false,
                        message: "Processo não encontrado!",
                        error: ""
                    );
                }
                var data = _mapper.Map<ProcessoDTO>(processo);
                return new SuccessStatus<ProcessoDTO>(
                    success: true,
                    message: "Processo encontrado com sucesso!",
                    body: data
                );
            }
            catch (Exception ex)
            {
                return new InternalServerErrorStatus<ProcessoDTO>(
                    success: false,
                    message: "Erro ao buscar processo!",
                    error: ex.Message
                );
            }
        }

        public async Task<ObjectResult> GetProcessos()
        {
            try
            {
                var processos = await _repository.GetProcessos();
                if (processos == null)
                {
                    return new NotFoundStatus<List<ProcessoDTO>>(
                        success: false,
                        message: "Processos não encontrados!",
                        error: ""
                    );
                }
                var data = _mapper.Map<List<ProcessoDTO>>(processos);
                return new SuccessStatus<List<ProcessoDTO>>(
                    success: true,
                    message: "Processos encontrados com sucesso!",
                    body: data
                );
            }
            catch (Exception ex)
            {
                return new InternalServerErrorStatus<List<ProcessoDTO>>(
                    success: false,
                    message: "Erro ao buscar processos!",
                    error: ex.Message
                );
            }
        }

        public async Task<ObjectResult> PostProcesso(ProcessoDTO processoDTO)
        {
            try
            {
                var processo = await _repository.PostProcesso(_mapper.Map<ProjetoPGE.Domain.Models.Processo>(processoDTO));
                if (processo == null)
                {
                    return new NotFoundStatus<ProcessoDTO>(
                        success: false,
                        message: "Processo não incluído!",
                        error: ""
                    );
                }
                var data = _mapper.Map<ProcessoDTO>(processo);
                return new SuccessStatus<ProcessoDTO>(
                    success: true,
                    message: "Processo incluído com sucesso!",
                    body: data
                );
            }
            catch (Exception ex)
            {
                return new InternalServerErrorStatus<ProcessoDTO>(
                    success: false,
                    message: "Erro ao incluir processo!",
                    error: ex.Message
                );
            }
        }

        public async Task<ObjectResult> PutProcesso(ProcessoDTO processoDTO)
        {
            try
            {
                var processo = await _repository.PutProcesso(_mapper.Map<ProjetoPGE.Domain.Models.Processo>(processoDTO));
                if (processo == null)
                {
                    return new NotFoundStatus<ProcessoDTO>(
                        success: false,
                        message: "Processo não alterado!",
                        error: ""
                    );
                }
                var data = _mapper.Map<ProcessoDTO>(processo);
                return new SuccessStatus<ProcessoDTO>(
                    success: true,
                    message: "Processo alterado com sucesso!",
                    body: data
                );
            }
            catch (Exception ex)
            {
                return new InternalServerErrorStatus<ProcessoDTO>(
                    success: false,
                    message: "Erro ao alterar processo!",
                    error: ex.Message
                );
            }
        }

        public async Task<ObjectResult> GetProcessosByCliente(int idPessoa)
        {
            try
            {
                // Buscar a pessoa pelo ID
                var pessoa = await _pessoaInterfaceRepository.GetPessoaById(idPessoa);
                if (pessoa == null)
                {
                    return new NotFoundStatus<List<ProcessoDTO>>(
                        success: false,
                        message: "Pessoa não encontrada!",
                        error: ""
                    );
                }

                // Verificar se é um Procurador
                if (pessoa.Oab) // Supondo que Oab seja um booleano indicando se é Procurador
                {
                    return new BadRequestStatus<List<ProcessoDTO>>(
                        success: false,
                        message: "O ID fornecido pertence a um Procurador, não a um Cliente.",
                        error: ""
                    );
                }

                var processos = await _repository.GetProcessosByCliente(idPessoa);
                if (processos == null)
                {
                    return new NotFoundStatus<List<ProcessoDTO>>(
                        success: false,
                        message: "Processos não encontrados!",
                        error: ""
                    );
                }
                var data = _mapper.Map<List<ProcessoDTO>>(processos);
                return new SuccessStatus<List<ProcessoDTO>>(
                    success: true,
                    message: "Processos encontrados com sucesso!",
                    body: data
                );
            }
            catch (Exception ex)
            {
                return new InternalServerErrorStatus<List<ProcessoDTO>>(
                    success: false,
                    message: "Erro ao buscar processos!",
                    error: ex.Message
                );
            }
        }

        public async Task<ObjectResult> GetProcessosByProcurador(int idPessoa)
        {
            try
            {
                // Buscar a pessoa pelo ID
                var pessoa = await _pessoaInterfaceRepository.GetPessoaById(idPessoa);
                if (pessoa == null)
                {
                    return new NotFoundStatus<List<ProcessoDTO>>(
                        success: false,
                        message: "Pessoa não encontrada!",
                        error: ""
                    );
                }

                // Verificar se é um Procurador
                if (!pessoa.Oab) // Supondo que Oab seja um booleano indicando se é Procurador
                {
                    return new BadRequestStatus<List<ProcessoDTO>>(
                        success: false,
                        message: "O ID fornecido pertence a um Cliente, não a um Procurador.",
                        error: ""
                    );
                }

                var processos = await _repository.GetProcessosByProcurador(idPessoa);
                if (processos == null)
                {
                    return new NotFoundStatus<List<ProcessoDTO>>(
                        success: false,
                        message: "Processos não encontrados!",
                        error: ""
                    );
                }
                var data = _mapper.Map<List<ProcessoDTO>>(processos);
                return new SuccessStatus<List<ProcessoDTO>>(
                    success: true,
                    message: "Processos encontrados com sucesso!",
                    body: data
                );
            }
            catch (Exception ex)
            {
                return new InternalServerErrorStatus<List<ProcessoDTO>>(
                    success: false,
                    message: "Erro ao buscar processos!",
                    error: ex.Message
                );
            }
        }

        public async Task<ObjectResult> AlterarProcuradorAsync(int idProcesso, int idNovoProcurador)
        {
            try
            {
                var processo = await _repository.GetProcessoById(idProcesso);
                if (processo == null)
                {
                    return new NotFoundStatus<ProcessoDTO>(
                        success: false,
                        message: "Processo não encontrado!",
                        error: ""
                    ); 
                }

                var novoProcurador = await _pessoaInterfaceRepository.GetPessoaById(idNovoProcurador);
                if (novoProcurador == null || !novoProcurador.Oab)
                {
                    return new NotFoundStatus<ProcessoDTO>(
                        success: false,
                        message: "Procurador não encontrado ou com OAB inválida!",
                        error: ""
                    );
                }

                // Remove o procurador antigo
                await _repository.RemoverProcuradorDoProcesso(idProcesso);

                // Adiciona o novo procurador
                await _repository.AdicionarProcuradorAoProcesso(idProcesso, idNovoProcurador);

                return new SuccessStatus<ProcessoDTO>(
                    success: true,
                    message: "Procurador alterado com sucesso!",
                    body: _mapper.Map<ProcessoDTO>(processo)
                );
            }
            catch (Exception ex)
            {
                return new InternalServerErrorStatus<ProcessoDTO>(
                    success: false,
                    message: "Erro ao alterar procurador!",
                    error: ex.Message
                );
            }
        }
    }
}
