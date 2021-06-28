using System;
using System.Collections.Generic;
using CadFuncionario.Core.DomainObjects;

namespace CadFuncionario.Domain.Entities
{
    public class StepProfissao : Entity
    {
        public StepProfissao(Guid stepProfissaoId, Guid profissaoId, decimal percentualAumento)
        {
            StepProfissaoId = stepProfissaoId == Guid.Empty ? Guid.NewGuid() : stepProfissaoId;
            ProfissaoId = profissaoId;
            PercentualAumento = percentualAumento;
        }

        public Guid StepProfissaoId { get; private set; }
        public Guid ProfissaoId { get; private set; }
        public decimal PercentualAumento { get; private set; }

        public virtual Profissao Profissao { get; private set; }
        public virtual ICollection<Funcionario> Funcionarios { get; private set; }


        public void AlterarPercentualAumento(decimal percentualAumento)
        {
            PercentualAumento = percentualAumento;
        }
    }
}