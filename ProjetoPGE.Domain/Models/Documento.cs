using System;
using System.Collections.Generic;

namespace ProjetoPGE.Domain.Models;

public partial class Documento
{
    public int Id { get; set; }

    public int IdProcesso { get; set; }

    public string NomeDocumento { get; set; } = null!;

    public string TipoDocumento { get; set; } = null!;
}
