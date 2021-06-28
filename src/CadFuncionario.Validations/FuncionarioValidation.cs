using CadFuncionario.Domain.Entities;
using FluentValidation;

namespace CadFuncionario.Validations
{
    public class FuncionarioValidation : AbstractValidator<Funcionario>
    {
        public FuncionarioValidation()
        {
            RuleFor(f => f.StepProfissaoId)
                .NotEmpty().WithMessage("Informe o step");

            RuleFor(f => f.Cpf)
                .NotEmpty().WithMessage("Informe o CPF")
                .Length(11, 11).WithMessage("O CPF deve conter 11 caracteres");

            RuleFor(f => f.Rg)
                .NotEmpty().WithMessage("Informe o RG")
                .MaximumLength(10).WithMessage("O RG deve conter no máximo 10 caracteres");

            RuleFor(f => f.Nome)
                .NotEmpty().WithMessage("Informe o Nome")
                .MaximumLength(100).WithMessage("O nome deve conter no máximo 100 caracteres");

            RuleFor(f => f.Ctps)
                .NotEmpty().WithMessage("Informe a carteira de trabalho")
                .MaximumLength(20).WithMessage("A carteira de trabalho deve conter no máximo 20 caracteres");
        }
    }
}