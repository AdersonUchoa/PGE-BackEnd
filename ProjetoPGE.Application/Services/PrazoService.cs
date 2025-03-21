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
    public class PrazoService : PrazoInterfaceService
    {
        private readonly PrazoInterfaceRepository _repository;
        private readonly IMapper _mapper;

        public PrazoService(PrazoInterfaceRepository prazoInterfaceRepository, IMapper mapper)
        {
            _repository = prazoInterfaceRepository;
            _mapper = mapper;
        }

        public async Task<ObjectResult> DeletePrazo(int id)
        {
            try
            {
                var prazo = await _repository.DeletePrazo(id);
                if (prazo == null)
                {
                    return new NotFoundStatus<PrazoDTO>(
                        success: false,
                        message: "Prazo não encontrado!",
                        error: ""
                    );
                }
                var data = _mapper.Map<PrazoDTO>(prazo);
                return new SuccessStatus<PrazoDTO>(
                    success: true,
                    message: "Prazo excluído com sucesso!",
                    body: data
                );
            }
            catch (Exception ex)
            {
                return new InternalServerErrorStatus<PrazoDTO>(
                    success: false,
                    message: "Erro ao excluir prazo!",
                    error: ex.Message
                );
            }
        }

        public async Task<ObjectResult> GetPrazoById(int id)
        {
            try
            {
                var prazo = await _repository.GetPrazoById(id);
                if (prazo == null)
                {
                    return new NotFoundStatus<PrazoDTO>(
                        success: false,
                        message: "Prazo não encontrado!",
                        error: ""
                    );
                }
                var data = _mapper.Map<PrazoDTO>(prazo);
                return new SuccessStatus<PrazoDTO>(
                    success: true,
                    message: "Prazo encontrado com sucesso!",
                    body: data
                );
            }
            catch (Exception ex)
            {
                return new InternalServerErrorStatus<PrazoDTO>(
                    success: false,
                    message: "Erro ao buscar prazo!",
                    error: ex.Message
                );
            }
        }

        public async Task<ObjectResult> GetPrazos()
        {
            try
            {
                var prazos = await _repository.GetPrazos();
                if (prazos == null)
                {
                    return new NotFoundStatus<List<PrazoDTO>>(
                        success: false,
                        message: "Prazos não encontrados!",
                        error: ""
                    );
                }
                var data = _mapper.Map<List<PrazoDTO>>(prazos);
                return new SuccessStatus<List<PrazoDTO>>(
                    success: true,
                    message: "Prazos encontrados com sucesso!",
                    body: data
                );
            }
            catch (Exception ex)
            {
                return new InternalServerErrorStatus<List<PrazoDTO>>(
                    success: false,
                    message: "Erro ao buscar prazos!",
                    error: ex.Message
                );
            }
        }

        public async Task<ObjectResult> PostPrazo(PrazoDTO prazoDTO)
        {
            try
            {
                var prazo = await _repository.PostPrazo(_mapper.Map<ProjetoPGE.Domain.Models.Prazo>(prazoDTO));
                if (prazo == null)
                {
                    return new NotFoundStatus<PrazoDTO>(
                        success: false,
                        message: "Prazo não encontrado!",
                        error: ""
                    );
                }
                var data = _mapper.Map<PrazoDTO>(prazo);
                return new SuccessStatus<PrazoDTO>(
                    success: true,
                    message: "Prazo incluído com sucesso!",
                    body: data
                );
            }
            catch (Exception ex)
            {
                return new InternalServerErrorStatus<PrazoDTO>(
                    success: false,
                    message: "Erro ao incluir prazo!",
                    error: ex.Message
                );
            }
        }

        public async Task<ObjectResult> PutPrazo(PrazoDTO prazoDTO)
        {
            try
            {
                var prazo = await _repository.PutPrazo(_mapper.Map<ProjetoPGE.Domain.Models.Prazo>(prazoDTO));
                if (prazo == null)
                {
                    return new NotFoundStatus<PrazoDTO>(
                        success: false,
                        message: "Prazo não encontrado!",
                        error: ""
                    );
                }
                var data = _mapper.Map<PrazoDTO>(prazo);
                return new SuccessStatus<PrazoDTO>(
                    success: true,
                    message: "Prazo alterado com sucesso!",
                    body: data
                );
            }
            catch(Exception ex)
            {
                return new InternalServerErrorStatus<PrazoDTO>(
                    success: false,
                    message: "Erro ao alterar prazo!",
                    error: ex.Message
                );
            }
        }

        public async Task<ObjectResult> GetPrazosByProcesso(int idProcesso)
        {
            try
            {
                var prazos = await _repository.GetPrazosByProcesso(idProcesso);
                if (prazos == null)
                {
                    return new NotFoundStatus<List<PrazoDTO>>(
                        success: false,
                        message: "Prazos não encontrados!",
                        error: ""
                    );
                }
                var data = _mapper.Map<List<PrazoDTO>>(prazos);
                return new SuccessStatus<List<PrazoDTO>>(
                    success: true,
                    message: "Prazos encontrados com sucesso!",
                    body: data
                );
            }
            catch (Exception ex)
            {
                return new InternalServerErrorStatus<List<PrazoDTO>>(
                    success: false,
                    message: "Erro ao buscar prazos!",
                    error: ex.Message
                );
            }
        }
    }
}
