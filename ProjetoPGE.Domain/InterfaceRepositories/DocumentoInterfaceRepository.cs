using ProjetoPGE.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPGE.Domain.InterfaceRepositories
{
    public interface DocumentoInterfaceRepository
    {
        Task<List<Documento>> GetDocumentos();
        Task<Documento> GetDocumentoById(int id);
        Task<Documento> PostDocumento(Documento documento);
        Task<Documento> PutDocumento(Documento documento);
        Task<Documento> DeleteDocumento(int id);
        Task<List<Documento>> GetDocumentosByProcesso(int idProcesso);
        Task<bool> SaveAllAsync();
    }
}
