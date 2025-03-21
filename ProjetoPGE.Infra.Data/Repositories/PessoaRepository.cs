using Microsoft.EntityFrameworkCore;
using ProjetoPGE.Domain.InterfaceRepositories;
using ProjetoPGE.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPGE.Infra.Data.Repositories
{
    public class PessoaRepository : PessoaInterfaceRepository
    {
        private readonly PgedbContext _context;

        public PessoaRepository(PgedbContext context)
        {
            _context = context;
        }

        public async Task<Pessoa> DeletePessoa(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);

            if (pessoa == null) // Se a pessoa não existir, retorna null antes de remover
            {
                return null;
            }

            _context.Pessoas.Remove(pessoa);
            bool saved = await SaveAllAsync();

            return saved ? pessoa : null;
        }


        public async Task<Pessoa> GetPessoaById(int id)
        {
            return await _context.Pessoas.FindAsync(id);
        }

        public async Task<List<Pessoa>> GetPessoas()
        {
            return await _context.Pessoas.ToListAsync();
        }

        public async Task<Pessoa> PostPessoa(Pessoa pessoa)
        {
            _context.Pessoas.Add(pessoa);
            bool saved = await SaveAllAsync();

            return saved ? pessoa : null; // Retorna a pessoa salva ou null em caso de falha
        }

        public async Task<Pessoa> PutPessoa(Pessoa pessoa)
        {
            var pessoaExiste = await _context.Pessoas.AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == pessoa.Id);

            if (pessoaExiste == null)
            {
                return null;
            }

            _context.Pessoas.Update(pessoa);
            bool saved = await SaveAllAsync();

            return saved ? pessoa : null;
        }

        public async Task<List<Pessoa>> GetClientes()
        {
            return await _context.Pessoas
                .Where(p => p.TipoPessoa == "Cliente")
                .ToListAsync();
        }

        public async Task<List<Pessoa>> GetProcuradores()
        {
            return await _context.Pessoas
                .Where(p => p.TipoPessoa == "Procurador")
                .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Pessoa> GetClienteById(int id)
        {
            return await _context.Pessoas
                .Where(p => p.TipoPessoa == "Cliente")
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Pessoa> GetProcuradorById(int id)
        {
            return await _context.Pessoas
                .Where(p => p.TipoPessoa == "Procurador")
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Pessoa>> GetPessoasByProcesso(int idProcesso)
        {
            var processo = await _context.Processos.FindAsync(idProcesso);
            if (processo == null)
            {
                Console.WriteLine("Processo não encontrado.");
            }
            return await _context.Processos
                .Where(p => p.Id == idProcesso)
                .SelectMany(p => p.IdPessoas) // Navega pelo relacionamento Many-to-Many
                .ToListAsync();
        }

        public async Task<List<Pessoa>> GetProcuradoresByProcesso(int idProcesso)
        {
            var processo = await _context.Processos.FindAsync(idProcesso);
            if (processo == null)
            {
                Console.WriteLine("Processo não encontrado.");
            }
            return await _context.Processos
                .Where(p => p.Id == idProcesso)
                .SelectMany(p => p.IdPessoas)
                .Where(p => p.Oab == true)
                .ToListAsync();
        }

        public async Task<List<Pessoa>> GetClientesByProcesso(int idProcesso)
        {
            var processo = await _context.Processos.FindAsync(idProcesso);
            if (processo == null)
            {
                Console.WriteLine("Processo não encontrado.");
            }
            return await _context.Processos
                .Where(p => p.Id == idProcesso)
                .SelectMany(p => p.IdPessoas)
                .Where(p => p.Oab == false)
                .ToListAsync();
        }

        public async Task<List<Pessoa>> GetAdministradores()
        {
            return await _context.Pessoas
                .Where(p => p.TipoPessoa == "Admin")
                .ToListAsync();
        }

        public async Task<Pessoa> GetAdministradorById(int id)
        {
            return await _context.Pessoas
                .Where(p => p.TipoPessoa == "Admin")
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
