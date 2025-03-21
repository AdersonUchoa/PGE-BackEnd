using ProjetoPGE.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPGE.Domain.InterfaceRepositories
{
    public interface PrazoInterfaceRepository
    {
        Task<List<Prazo>> GetPrazos();
        Task<Prazo> GetPrazoById(int id);
        Task<Prazo> PostPrazo(Prazo prazo);
        Task<Prazo> PutPrazo(Prazo prazo);
        Task<Prazo> DeletePrazo(int id);
        Task<List<Prazo>> GetPrazosByProcesso(int idProcesso);
        Task<bool> SaveAllAsync();
    }
}
