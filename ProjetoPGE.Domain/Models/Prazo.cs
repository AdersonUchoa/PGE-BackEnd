using System;
using System.Collections.Generic;

namespace ProjetoPGE.Domain.Models;

public partial class Prazo
{
    public int Id { get; set; }

    public int IdProcesso { get; set; }

    public DateOnly DataVencimento { get; set; }

    public string StatusPrazo { get; set; } = null!;

    public string TipoPrazo { get; set; } = null!;

    public virtual Processo IdProcessoNavigation { get; set; } = null!;
}
