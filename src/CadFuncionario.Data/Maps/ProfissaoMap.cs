using CadFuncionario.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CadFuncionario.Data.Maps
{
    public class ProfissaoMap : IEntityTypeConfiguration<Profissao>
    {
        public void Configure(EntityTypeBuilder<Profissao> builder)
        {
            builder.HasKey(p => p.ProfissaoId);

            builder.Property(p => p.Descricao).HasColumnType("varchar(80)").IsRequired();
            builder.Property(p => p.SalarioBase).IsRequired();

            builder.ToTable("profissao");
        }
    }
}