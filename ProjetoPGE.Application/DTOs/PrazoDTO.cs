using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPGE.Application.DTOs
{
    public class PrazoDTO
    {
        [IgnoreDataMember]
        public int Id { get; set; }

        public int IdProcesso { get; set; }

        public DateOnly DataVencimento { get; set; }

        public string StatusPrazo { get; set; } = null!;

        public string TipoPrazo { get; set; } = null!;
    }
}
