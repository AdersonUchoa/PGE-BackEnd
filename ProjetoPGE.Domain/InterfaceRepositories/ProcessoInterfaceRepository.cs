using ProjetoPGE.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPGE.Domain.InterfaceRepositories
{
    public interface ProcessoInterfaceRepository
    {
        Task<List<Processo>> GetProcessos();
        Task<List<Processo>> BuscarProcessos(String busca);
        Task<Processo?> GetProcessoById(int id);
        Task<Processo> PostProcesso(Processo processo);
        Task<Processo> PutProcesso(Processo processo);
        Task<Processo> DeleteProcesso(int id);
        Task<bool> SaveAllAsync();
        Task RemoverProcuradorDoProcesso(int idProcesso);
        Task AdicionarProcuradorAoProcesso(int idProcesso, int idProcurador);
        Task<List<Processo>> GetProcessosByCliente(int idPessoa);
        Task<List<Processo>> GetProcessosByProcurador(int idPessoa);
    }
}
