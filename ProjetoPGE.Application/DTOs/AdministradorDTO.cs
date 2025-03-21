using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPGE.Application.DTOs
{
    public class AdministradorDTO
    {
        public int Id { get; set; }

        public string NomePessoa { get; set; } = null!;

        public string TipoPessoa { get; set; } = null!;

        public string LoginPessoa { get; set; } = null!;
    }
}
