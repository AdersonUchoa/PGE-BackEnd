using System;
using System.Collections.Generic;

namespace ProjetoPGE.Domain.Models;

public partial class Processo
{
    public int Id { get; set; }

    public int NumeroProcesso { get; set; }

    public string OrgaoTramitacao { get; set; } = null!;

    public string Assunto { get; set; } = null!;

    public string StatusProcesso { get; set; } = null!;

    public virtual ICollection<Prazo> Prazos { get; set; } = new List<Prazo>();

    public virtual ICollection<Pessoa> IdPessoas { get; set; } = new List<Pessoa>();
}
