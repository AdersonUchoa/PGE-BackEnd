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
    public interface PessoaInterfaceService
    {
        Task<ObjectResult> GetPessoas();
        Task<ObjectResult> GetPessoaById(int id);
        Task<ObjectResult> PostPessoa(PessoaCadastroDTO pessoaDTO);
        Task<ObjectResult> PutPessoa(PessoaAtualizacaoDTO pessoaDTO);
        Task<ObjectResult> DeletePessoa(int id);
        Task<ObjectResult> GetClientes();
        Task<ObjectResult> GetProcuradores();
        Task<ObjectResult> GetPessoasByProcesso(int idProcesso);
        Task<ObjectResult> GetProcuradoresByProcesso(int idProcesso);
        Task<ObjectResult> GetClientesByProcesso(int idProcesso);
        Task<ObjectResult> GetClienteById(int id);
        Task<ObjectResult> GetProcuradorById(int id);
        Task<ObjectResult> CadastroPessoa(PessoaCadastroDTO pessoaCadastroDTO);
        Task<ObjectResult> GetAdministradores();
        Task<ObjectResult> GetAdministradorById(int id);
    }
}
