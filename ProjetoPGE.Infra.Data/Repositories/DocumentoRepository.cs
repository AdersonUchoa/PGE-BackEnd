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
    public class DocumentoRepository : DocumentoInterfaceRepository
    {
        private readonly PgedbContext _context;

        public DocumentoRepository(PgedbContext context)
        {
            _context = context;
        }

        public async Task<Documento> DeleteDocumento(int id)
        {
            var documento = await _context.Documentos.FindAsync(id);
            if (documento == null)
            {
                return null;
            }

            _context.Documentos.Remove(documento);
            bool saved = await SaveAllAsync();

            return saved ? documento : null;
        }

        public async Task<Documento> GetDocumentoById(int id)
        {
            return await _context.Documentos.FindAsync(id);
        }

        public async Task<List<Documento>> GetDocumentos()
        {
            return await _context.Documentos.ToListAsync();
        }

        public async Task<Documento> PostDocumento(Documento documento)
        {
            _context.Documentos.Add(documento);
            bool saved = await SaveAllAsync();

            return saved ? documento : null;
        }

        public async Task<Documento> PutDocumento(Documento documento)
        {
            var documentoExiste = await _context.Documentos.AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == documento.Id);

            if (documentoExiste == null)
            {
                return null;
            }

            _context.Documentos.Update(documento);
            bool saved = await SaveAllAsync();

            return saved ? documento : null;
        }

        public async Task<List<Documento>> GetDocumentosByProcesso(int idProcesso)
        {
            var processo = await _context.Processos.FindAsync(idProcesso);
            if (processo == null)
            {
                Console.WriteLine("Processo não encontrado.");
            }
            return await _context.Documentos
                .Where(d => d.IdProcesso == idProcesso)
                .ToListAsync();
        }


        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
