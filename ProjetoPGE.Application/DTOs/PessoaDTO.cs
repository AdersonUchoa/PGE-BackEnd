using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Configuration.Annotations;

namespace ProjetoPGE.Application.DTOs
{
    public class PessoaDTO
    {
        public int Id { get; set; }

        public string NomePessoa { get; set; } = null!;

        public string TipoPessoa { get; set; } = null!;

        public bool Oab { get; set; }

        public string LoginPessoa { get; set; } = null!;
    }
}
