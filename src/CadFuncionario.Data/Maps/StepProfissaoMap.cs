using CadFuncionario.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CadFuncionario.Data.Maps
{
    public class StepProfissaoMap : IEntityTypeConfiguration<StepProfissao>
    {
        public void Configure(EntityTypeBuilder<StepProfissao> builder)
        {
            builder.HasKey(s => s.StepProfissaoId);

            builder.HasOne(s => s.Profissao)
                   .WithMany(p => p.StepProfissoes)
                   .HasForeignKey(s => s.ProfissaoId);

            builder.Property(s => s.ProfissaoId).IsRequired();
            builder.Property(s => s.PercentualAumento).IsRequired();

            builder.ToTable("stepprofissao");
        }
    }
}