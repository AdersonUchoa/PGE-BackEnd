using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjetoPGE.Domain.Models;

public partial class PgedbContext : DbContext
{
    public PgedbContext()
    {
    }

    public PgedbContext(DbContextOptions<PgedbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Documento> Documentos { get; set; }

    public virtual DbSet<Pessoa> Pessoas { get; set; }

    public virtual DbSet<Prazo> Prazos { get; set; }

    public virtual DbSet<Processo> Processos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=DERSO\\SQLEXPRESS;Database=pgedb;User Id=sa;Password=sa;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Documento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__document__3213E83FB02A62F1");

            entity.ToTable("documento");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdProcesso).HasColumnName("idProcesso");
            entity.Property(e => e.NomeDocumento)
                .HasMaxLength(255)
                .HasColumnName("nomeDocumento");
            entity.Property(e => e.TipoDocumento)
                .HasMaxLength(255)
                .HasColumnName("tipoDocumento");
        });

        modelBuilder.Entity<Pessoa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__pessoa__3213E83FD5CB84CC");

            entity.ToTable("pessoa");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.NomePessoa)
                .HasMaxLength(255)
                .HasColumnName("nomePessoa");
            entity.Property(e => e.Oab).HasColumnName("oab");
            entity.Property(e => e.TipoPessoa)
                .HasMaxLength(10)
                .HasColumnName("tipoPessoa");
            entity.Property(e => e.LoginPessoa)
                .HasMaxLength(255)
                .HasColumnName("loginPessoa");
            entity.Property(e => e.passwordHash)
                .HasMaxLength(255)
                .HasColumnName("passwordHash");
            entity.Property(e => e.passwordSalt)
                .HasMaxLength(255)
                .HasColumnName("passwordSalt");

            entity.HasMany(d => d.IdProcessos).WithMany(p => p.IdPessoas)
                .UsingEntity<Dictionary<string, object>>(
                    "PessoaProcesso",
                    r => r.HasOne<Processo>().WithMany()
                        .HasForeignKey("IdProcesso")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PessoaPro__idPro__5535A963"),
                    l => l.HasOne<Pessoa>().WithMany()
                        .HasForeignKey("IdPessoa")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PessoaPro__idPes__5441852A"),
                    j =>
                    {
                        j.HasKey("IdPessoa", "IdProcesso").HasName("PK__PessoaPr__CB1F8533563877E4");
                        j.ToTable("PessoaProcesso");
                        j.IndexerProperty<int>("IdPessoa").HasColumnName("idPessoa");
                        j.IndexerProperty<int>("IdProcesso").HasColumnName("idProcesso");
                    });
        });

        modelBuilder.Entity<Prazo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__prazo__3213E83FF205F32C");

            entity.ToTable("prazo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataVencimento).HasColumnName("dataVencimento");
            entity.Property(e => e.IdProcesso).HasColumnName("idProcesso");
            entity.Property(e => e.StatusPrazo)
                .HasMaxLength(255)
                .HasColumnName("statusPrazo");
            entity.Property(e => e.TipoPrazo)
                .HasMaxLength(255)
                .HasColumnName("tipoPrazo");

            entity.HasOne(d => d.IdProcessoNavigation).WithMany(p => p.Prazos)
                .HasForeignKey(d => d.IdProcesso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__prazo__idProcess__4F7CD00D");
        });

        modelBuilder.Entity<Processo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__processo__3213E83F0224844B");

            entity.ToTable("processo");

            entity.HasIndex(e => e.NumeroProcesso, "UQ__processo__3C6B4A54B53B550E").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Assunto)
                .HasMaxLength(255)
                .HasColumnName("assunto");
            entity.Property(e => e.NumeroProcesso).HasColumnName("numeroProcesso");
            entity.Property(e => e.OrgaoTramitacao)
                .HasMaxLength(255)
                .HasColumnName("orgaoTramitacao");
            entity.Property(e => e.StatusProcesso)
                .HasMaxLength(255)
                .HasColumnName("statusProcesso");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
