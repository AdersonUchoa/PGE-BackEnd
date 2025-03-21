using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPGE.Application.Mappings
{
    public class ModelsToDTO : Profile
    {
        public ModelsToDTO() {
            CreateMap<ProjetoPGE.Domain.Models.Pessoa, ProjetoPGE.Application.DTOs.PessoaDTO>().ReverseMap();
            CreateMap<ProjetoPGE.Domain.Models.Documento, ProjetoPGE.Application.DTOs.DocumentoDTO>().ReverseMap();
            CreateMap<ProjetoPGE.Domain.Models.Prazo, ProjetoPGE.Application.DTOs.PrazoDTO>().ReverseMap();
            CreateMap<ProjetoPGE.Domain.Models.Processo, ProjetoPGE.Application.DTOs.ProcessoDTO>().ReverseMap();
            CreateMap<ProjetoPGE.Domain.Models.Pessoa, ProjetoPGE.Application.DTOs.PessoaCadastroDTO>().ReverseMap();
            CreateMap<ProjetoPGE.Domain.Models.Pessoa, ProjetoPGE.Application.DTOs.AdministradorDTO>().ReverseMap();
            CreateMap<ProjetoPGE.Domain.Models.Pessoa, ProjetoPGE.Application.DTOs.PessoaAtualizacaoDTO>().ReverseMap();
        }
    }
}
