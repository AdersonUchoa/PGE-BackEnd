using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPGE.Application.DTOs
{
    public class ProcessoDTO
    {
        [IgnoreDataMember]
        public int Id { get; set; }

        public int NumeroProcesso { get; set; }

        public string OrgaoTramitacao { get; set; } = null!;

        public string Assunto { get; set; } = null!;

        public string StatusProcesso { get; set; } = null!;
    }
}
