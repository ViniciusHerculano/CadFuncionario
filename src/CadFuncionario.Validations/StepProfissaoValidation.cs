using CadFuncionario.Domain.Entities;
using FluentValidation;

namespace CadFuncionario.Validations
{
    public class StepProfissaoValidation : AbstractValidator<StepProfissao>
    {
        public StepProfissaoValidation()
        {
            RuleFor(s => s.ProfissaoId)
                .NotEmpty().WithMessage("Informe a profissão");

            RuleFor(s => s.PercentualAumento)
                .NotEmpty().WithMessage("Informe o percentual de aumento");
        }
    }
}
