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
    public class DocumentoService : DocumentoInterfaceService
    {
        private readonly DocumentoInterfaceRepository _repository;
        private readonly IMapper _mapper;

        public DocumentoService(DocumentoInterfaceRepository documentoInterfaceRepository, IMapper mapper)
        {
            _repository = documentoInterfaceRepository;
            _mapper = mapper;
        }

        public async Task<ObjectResult> DeleteDocumento(int id)
        {
            try
            {
                var documento = await _repository.DeleteDocumento(id);
                if (documento == null)
                {
                    return new NotFoundStatus<DocumentoDTO>(
                        success: false,
                        message: "Documento não encontrado!",
                        error: ""
                    );
                }
                var data = _mapper.Map<DocumentoDTO>(documento);
                return new SuccessStatus<DocumentoDTO>(
                    success: true,
                    message: "Documento excluído com sucesso!",
                    body: data
                );
            }
            catch (Exception ex)
            {
                return new InternalServerErrorStatus<DocumentoDTO>(
                    success: false,
                    message: "Erro ao excluir documento!",
                    error: ex.Message
                );
            }
        }

        public async Task<ObjectResult> GetDocumentoById(int id)
        {
            try
            {
                var documento = await _repository.GetDocumentoById(id);
                if (documento == null)
                {
                    return new NotFoundStatus<DocumentoDTO>(
                        success: false,
                        message: "Documento não encontrado!",
                        error: ""
                    );
                }
                var data = _mapper.Map<DocumentoDTO>(documento);
                return new SuccessStatus<DocumentoDTO>(
                    success: true,
                    message: "Documento retornado com sucesso!",
                    body: data
                );
            }
            catch (Exception ex)
            {
                return new InternalServerErrorStatus<DocumentoDTO>(
                    success: false,
                    message: "Erro ao deletar documento!",
                    error: ex.Message
                );
            }
        }

        public async Task<ObjectResult> GetDocumentos()
        {
            try
            {
                var documentos = await _repository.GetDocumentos();
                if(documentos == null)
                {
                    return new NotFoundStatus<List<DocumentoDTO>> (
                        success: false,
                        message: "Nenhum documento encontrado!",
                        error: ""
                    );
                }
                var data = _mapper.Map<List<DocumentoDTO>>(documentos);
                return new SuccessStatus<List<DocumentoDTO>>(
                    success: true,
                    message: "Documentos retornados com sucesso!",
                    body: data
                );
            }
            catch (Exception ex) {
                return new InternalServerErrorStatus<List<DocumentoDTO>>(
                    success: false,
                    message: "Erro ao buscar documentos!",
                    error: ex.Message
                );
            }
        }

        public async Task<ObjectResult> PostDocumento(DocumentoDTO documentoDTO)
        {
            try
            {
                var documento = await _repository.PostDocumento(_mapper.Map<ProjetoPGE.Domain.Models.Documento>(documentoDTO));
                var data = _mapper.Map<DocumentoDTO>(documento);
                return new SuccessStatus<DocumentoDTO>(
                    success: true,
                    message: "Documento incluído com sucesso!",
                    body: data
                );
            }
            catch (Exception ex)
            {
                return new InternalServerErrorStatus<DocumentoDTO>(
                    success: false,
                    message: "Erro ao incluir documento!",
                    error: ex.Message
                );
            }
        }

        public async Task<ObjectResult> PutDocumento(DocumentoDTO documentoDTO)
        {
            try
            {
                var documento = await _repository.PutDocumento(_mapper.Map<ProjetoPGE.Domain.Models.Documento>(documentoDTO));
                if (documento == null)
                {
                    return new NotFoundStatus<DocumentoDTO>(
                        success: false,
                        message: "Documento não encontrado!",
                        error: ""
                    );
                }
                var data = _mapper.Map<DocumentoDTO>(documento);
                return new SuccessStatus<DocumentoDTO>(
                    success: true,
                    message: "Documento alterado com sucesso!",
                    body: data
                );
            }
            catch (Exception ex)
            {
                return new InternalServerErrorStatus<DocumentoDTO>(
                    success: false,
                    message: "Erro ao alterar documento!",
                    error: ex.Message
                );
            }
        }

        public async Task<ObjectResult> GetDocumentosByProcesso(int idProcesso)
        {
            try
            {
                var documentos = await _repository.GetDocumentosByProcesso(idProcesso);
                if (documentos == null)
                {
                    return new NotFoundStatus<List<DocumentoDTO>>(
                        success: false,
                        message: "Documentos não encontrados!",
                        error: ""
                    );
                }
                var data = _mapper.Map<List<DocumentoDTO>>(documentos);
                return new SuccessStatus<List<DocumentoDTO>>(
                    success: true,
                    message: "Documentos encontrados com sucesso!",
                    body: data
                );
            }
            catch (Exception ex)
            {
                return new InternalServerErrorStatus<List<DocumentoDTO>>(
                    success: false,
                    message: "Erro ao buscar documentos!",
                    error: ex.Message
                );
            }
        }
    }
}
