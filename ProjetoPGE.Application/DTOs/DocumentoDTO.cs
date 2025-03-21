using AutoMapper.Configuration.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPGE.Application.DTOs
{
    public class DocumentoDTO
    {
        public int Id { get; set; }

        public int IdProcesso { get; set; }

        public string NomeDocumento { get; set; } = null!;

        public string TipoDocumento { get; set; } = null!;
    }
}
