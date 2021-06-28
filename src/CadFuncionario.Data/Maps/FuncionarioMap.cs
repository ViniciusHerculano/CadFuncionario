using CadFuncionario.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CadFuncionario.Data.Maps
{
    public class FuncionarioMap : IEntityTypeConfiguration<Funcionario>
    {
        public void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            builder.HasKey(f => f.FuncionarioId);

            builder.HasOne(f => f.StepProfissao)
                   .WithMany(s => s.Funcionarios)
                   .HasForeignKey(f => f.StepProfissaoId);

            builder.Property(f => f.StepProfissaoId).IsRequired();
            builder.Property(f => f.Cpf).HasColumnType("varchar(11)").IsRequired();
            builder.Property(f => f.Rg).HasColumnType("varchar(10)");
            builder.Property(f => f.Nome).HasColumnType("varchar(100)").IsRequired();
            builder.Property(f => f.Ctps).HasColumnType("varchar(20)").IsRequired();

            builder.ToTable("funcionario");
        }
    }
}