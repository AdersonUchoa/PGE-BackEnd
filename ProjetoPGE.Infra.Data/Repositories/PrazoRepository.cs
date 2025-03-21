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
    public class PrazoRepository : PrazoInterfaceRepository
    {
        private readonly PgedbContext _context;

        public PrazoRepository(PgedbContext context)
        {
            _context = context;
        }

        public async Task<Prazo> DeletePrazo(int id)
        {
            var prazo = await _context.Prazos.FindAsync(id);
            if (prazo == null)
            {
                return null;
            }

            _context.Prazos.Remove(prazo);
            bool saved = await SaveAllAsync();

            return saved ? prazo : null;
        }

        public async Task<Prazo> GetPrazoById(int id)
        {
            return await _context.Prazos.FindAsync(id);
        }

        public async Task<List<Prazo>> GetPrazos()
        {
            return await _context.Prazos.ToListAsync();
        }

        public async Task<Prazo> PostPrazo(Prazo prazo)
        {
            _context.Prazos.Add(prazo);
            bool saved = await SaveAllAsync();

            return saved ? prazo : null;
        }

        public async Task<Prazo> PutPrazo(Prazo prazo)
        {
            var prazoExiste = await _context.Prazos.AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == prazo.Id);

            if (prazoExiste == null)
            {
                return null;
            }

            _context.Prazos.Update(prazo);
            bool saved = await SaveAllAsync();

            return saved ? prazo : null;
        }

        public async Task<List<Prazo>> GetPrazosByProcesso(int idProcesso)
        {
            var processo = await _context.Processos.FindAsync(idProcesso);
            if (processo == null)
            {
                Console.WriteLine("Processo não encontrado.");
            }
            return await _context.Prazos
                .Where(p => p.IdProcesso == idProcesso)
                .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
