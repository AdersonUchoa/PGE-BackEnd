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
    public interface PrazoInterfaceService
    {
        Task<ObjectResult> GetPrazos();
        Task<ObjectResult> GetPrazoById(int id);
        Task<ObjectResult> PostPrazo(PrazoDTO prazoDTO);
        Task<ObjectResult> PutPrazo(PrazoDTO prazoDTO);
        Task<ObjectResult> GetPrazosByProcesso(int idProcesso);
        Task<ObjectResult> DeletePrazo(int id);
    }
}
