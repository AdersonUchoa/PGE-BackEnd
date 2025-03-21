using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPGE.Application.DTOs
{
    public class PessoaAtualizacaoDTO
    {
        public int Id { get; set; }
        public string NomePessoa { get; set; } = null!;
        public string? TipoPessoa { get; set; }
        public bool Oab { get; set; }
        public string? LoginPessoa { get; set; }

        public string? Senha { get; set; }
    }
}
