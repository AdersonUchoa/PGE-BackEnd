using Microsoft.EntityFrameworkCore;
using ProjetoPGE.Domain.InterfaceRepositories;
using ProjetoPGE.Domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPGE.Infra.Data.Repositories
{
    public class ProcessoRepository : ProcessoInterfaceRepository
    {

        private readonly PgedbContext _context;

        public ProcessoRepository(PgedbContext context)
        {
            _context = context;
        }

        public async Task<Processo> DeleteProcesso(int id)
        {
            var processo = await _context.Processos.FindAsync(id);
            if (processo == null)
            {
                return null;
            }

            _context.Processos.Remove(processo);
            bool saved = await SaveAllAsync();

            return saved ? processo : null;
        }

        public async Task<Processo?> GetProcessoById(int id)
        {
            var processo = await _context.Processos.FindAsync(id);
            if (processo == null) {
                Console.WriteLine("Processo não encontrado.");
            }
            return processo;
        }

        public async Task<List<Processo>> GetProcessos()
        {
            return await _context.Processos.ToListAsync();
        }

        public async Task<List<Processo>> BuscarProcessos(String buscar)
        {
            return await _context.Processos.Where(processo => processo.Id.ToString().Contains(buscar)).ToListAsync();
        }

        public async Task<Processo> PostProcesso(Processo processo)
        {
            _context.Processos.Add(processo);
            bool saved = await SaveAllAsync();

            return saved ? processo : null;
        }

        public async Task<Processo> PutProcesso(Processo processo)
        {
            var processoExiste = await _context.Processos.AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == processo.Id);

            if (processoExiste == null)
            {
                return null;
            }

            _context.Processos.Update(processo);
            bool saved = await SaveAllAsync();

            return saved ? processo : null;
        }

        public async Task AtualizarProcuradorNoProcesso(int idProcesso, int idNovoProcurador)
        {
            // Verifica se o processo existe
            var processo = await _context.Processos.FindAsync(idProcesso);
            if (processo == null) throw new Exception("Processo não encontrado.");

            // Verifica se o novo procurador existe e se ele realmente é um procurador
            var novoProcurador = await _context.Pessoas.FirstOrDefaultAsync(p => p.Id == idNovoProcurador && p.Oab == true);
            if (novoProcurador == null) throw new Exception("Novo procurador não encontrado ou não é um procurador válido.");

            // Remove o procurador atual do processo
            await _context.Database.ExecuteSqlRawAsync(
                "DELETE FROM PessoaProcesso WHERE idProcesso = {0} AND idPessoa IN (SELECT id FROM Pessoa WHERE oab = 1)",
                idProcesso);

            // Adiciona o novo procurador ao processo
            await _context.Database.ExecuteSqlRawAsync(
                "INSERT INTO PessoaProcesso (idPessoa, idProcesso) VALUES ({0}, {1})",
                idNovoProcurador, idProcesso);

            await _context.SaveChangesAsync();
        }

        public async Task RemoverProcuradorDoProcesso(int idProcesso)
        {
            var query = "DELETE FROM PessoaProcesso WHERE idProcesso = {0} AND idPessoa IN (SELECT id FROM Pessoa WHERE oab = 1)";
            await _context.Database.ExecuteSqlRawAsync(query, idProcesso);
        }

        public async Task AdicionarProcuradorAoProcesso(int idProcesso, int idProcurador)
        {
            var query = "INSERT INTO PessoaProcesso (idPessoa, idProcesso) VALUES ({0}, {1})";
            await _context.Database.ExecuteSqlRawAsync(query, idProcurador, idProcesso);
        }

        public async Task<List<Processo>> GetProcessosByCliente(int idPessoa)
        {
            return await _context.Pessoas
                .Where(p => p.Id == idPessoa && p.TipoPessoa == "Cliente") // Garante que é um cliente
                .SelectMany(p => p.IdProcessos) // Acessa os processos relacionados
                .ToListAsync();
        }

        public async Task<List<Processo>> GetProcessosByProcurador(int idPessoa)
        {
            return await _context.Pessoas
            .Where(p => p.Id == idPessoa && p.TipoPessoa == "Procurador") // Garante que é um procurador
                .SelectMany(p => p.IdProcessos) // Acessa os processos relacionados
                .ToListAsync();
        }
        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
