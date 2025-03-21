using ProjetoPGE.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPGE.Domain.Account
{
    public interface IAuthenticate
    {
        Task<bool> AuthenticateAsync(string username, string password);
        Task<bool> pessoaExiste(string username);
        Task<string> GenerateToken(int id, string username);
        public Task<Pessoa> GetPessoaByLogin(string username);
    }
}
