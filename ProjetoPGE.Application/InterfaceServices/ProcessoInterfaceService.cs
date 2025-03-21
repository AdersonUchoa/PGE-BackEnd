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
    public interface ProcessoInterfaceService
    {
        Task<ObjectResult> GetProcessos();
        Task<ObjectResult> GetProcessoById(int id);
        Task<ObjectResult> PostProcesso(ProcessoDTO processoDTO);
        Task<ObjectResult> PutProcesso(ProcessoDTO processoDTO);
        Task<ObjectResult> DeleteProcesso(int id);
        Task<ObjectResult> AlterarProcuradorAsync(int idProcesso, int idNovoProcurador);
        Task<ObjectResult> GetProcessosByCliente(int idPessoa);
        Task<ObjectResult> GetProcessosByProcurador(int idPessoa);
    }
}
