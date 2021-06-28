using CadFuncionario.Domain.Entities;
using FluentValidation;

namespace CadFuncionario.Validations
{
    public class ProfissaoValidation : AbstractValidator<Profissao>
    {
        public ProfissaoValidation(bool ehAtualizar = false)
        {
            RuleFor(p => p.Descricao)
                .NotEmpty().WithMessage("Informe a descrição")
                .MaximumLength(80).WithMessage("A descrição deve conter no máximo 80 caracteres");

            RuleFor(p => p.SalarioBase)
                .NotEmpty().WithMessage("Informe o salário base");

            if (!ehAtualizar)
                RuleForEach(p => p.StepProfissoes)
                    .SetValidator(new StepProfissaoValidation());
        }
    }
}