using System;
using System.Collections.Generic;

namespace ProjetoPGE.Domain.Models;

public partial class Pessoa
{
    public int Id { get; set; }

    public string NomePessoa { get; set; } = null!;

    public string TipoPessoa { get; set; } = null!;

    public bool Oab { get; set; }

    public string LoginPessoa { get; set; } = null!;

    public byte[]? passwordSalt { get; set; }
    public byte[]? passwordHash { get; set; }

    public virtual ICollection<Processo> IdProcessos { get; set; } = new List<Processo>();
}
