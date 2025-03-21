using ProjetoPGE.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPGE.Domain.InterfaceRepositories
{
    public interface PessoaInterfaceRepository
    {
        Task<List<Pessoa>> GetPessoas();
        Task<Pessoa> GetPessoaById(int id);
        Task<Pessoa> PostPessoa(Pessoa pessoa);
        Task<Pessoa> PutPessoa(Pessoa pessoa);
        Task<Pessoa> DeletePessoa(int id);
        Task<bool> SaveAllAsync();
        Task<List<Pessoa>> GetClientes();
        Task<List<Pessoa>> GetProcuradores();
        Task<List<Pessoa>> GetPessoasByProcesso(int idProcesso);
        Task<List<Pessoa>> GetProcuradoresByProcesso(int idProcesso);
        Task<List<Pessoa>> GetClientesByProcesso(int idProcesso);
        Task<Pessoa> GetClienteById(int id);
        Task<Pessoa> GetProcuradorById(int id);
        Task<List<Pessoa>> GetAdministradores();
        Task<Pessoa> GetAdministradorById(int id);

    }
}
