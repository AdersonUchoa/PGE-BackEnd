using Microsoft.AspNetCore.Mvc;
using ProjetoPGE.Application.DTOs;
using ProjetoPGE.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPGE.Application.InterfaceServices
{
    public interface DocumentoInterfaceService
    {
        Task<ObjectResult> GetDocumentos();
        Task<ObjectResult> GetDocumentoById(int id);
        Task<ObjectResult> PostDocumento(DocumentoDTO documentoDTO);
        Task<ObjectResult> PutDocumento(DocumentoDTO documentoDTO);
        Task<ObjectResult> DeleteDocumento(int id);
        Task<ObjectResult> GetDocumentosByProcesso(int idProcesso);
    }
}
