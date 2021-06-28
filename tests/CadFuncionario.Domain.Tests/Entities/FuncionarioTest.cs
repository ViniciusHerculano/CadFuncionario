using System;
using Bogus;
using Bogus.Extensions.Brazil;
using CadFuncionario.Domain.Entities;
using Xunit;

namespace CadFuncionario.Domain.Tests.Entities
{
    public class FuncionarioTest
    {
        private readonly Faker _faker;

        public FuncionarioTest()
        {
            _faker = new Faker("pt_BR");
        }


        [Fact(DisplayName = "Testando geração do FuncionarioId")]
        [Trait("Grupo", "Entities")]
        public void Funcionario_GerandoId_ComSucesso()
        {
            // Arrange & Act
            var funcionario = GerarFuncionario();

            // Assert
            Assert.NotNull(funcionario.FuncionarioId);
        }

        [Fact(DisplayName = "Alterar o step do funcionario")]
        [Trait("Grupo", "Entities")]
        public void Funcionario_AlterarStep()
        {
            // Arrange 
            var funcionario = GerarFuncionario();
            var stepOriginal = funcionario.StepProfissaoId;

            // Act
            funcionario.AlterarStep(_faker.Random.Guid());

            // Assert
            Assert.NotEqual(stepOriginal, funcionario.StepProfissaoId);
        }

        [Fact(DisplayName = "Obter mensagem aniversário com data nascimento nulo")]
        [Trait("Grupo", "Entities")]
        public void Funcionario_ObterMensagemAniversarioDataNascimentoNulo()
        {
            // Arrange 
            var funcionario = GerarFuncionario(false);

            // Act & Assert
            Assert.Null(funcionario.ObterMensagemAniversario());
        }

        [Fact(DisplayName = "Obter mensagem aniversário, quando o dia e o mês atual for diferente do aniversário")]
        [Trait("Grupo", "Entities")]
        public void Funcionario_ObterMensagemAniversario_DiaMes_Diferente()
        {
            // Arrange
            var funcionario = GerarFuncionario();

            if (funcionario.DataNascimento.Value.Day == DateTime.Now.Day)
                funcionario.AlterarDataNascimento(funcionario.DataNascimento.Value.AddDays(-1));

            // Act & Assert
            Assert.Null(funcionario.ObterMensagemAniversario());
        }

        [Fact(DisplayName = "Obter mensagem aniversário com sucesso")]
        [Trait("Grupo", "Entities")]
        public void Funcionario_ObterMensagemAniversario_Sucesso()
        {
            // Arrange
            var funcionario = GerarFuncionario(false);
            var dataAtual = DateTime.Now;

            funcionario.AlterarDataNascimento(
                new DateTime(_faker.Random.Int(1990, 1999),
                dataAtual.Month, dataAtual.Day));

            // Act
            var mensagemAniversario = funcionario.ObterMensagemAniversario();

            // Assert
            Assert.NotNull(mensagemAniversario);
            Assert.NotEmpty(mensagemAniversario);
            Assert.Contains("Feliz aniversário!", mensagemAniversario);
        }

        [Fact(DisplayName = "Alterar a data de nascimento")]
        [Trait("Grupo", "Entities")]
        public void Funcionario_AlterarDataNascimento()
        {
            // Arrange 
            var funcionario = GerarFuncionario();
            var dataNascimentoOriginal = funcionario.DataNascimento;

            // Act
            funcionario.AlterarDataNascimento(dataNascimentoOriginal.Value.AddDays(-80));

            // Assert
            Assert.NotEqual(dataNascimentoOriginal, funcionario.DataNascimento);
        }

        private Funcionario GerarFuncionario(bool gerarDataNascimento = true)
        {
            DateTime? dataNascimento = null;
            if (gerarDataNascimento)
                dataNascimento = _faker.Person.DateOfBirth;

            return new Funcionario(
                Guid.Empty,
                _faker.Random.Guid(),
                _faker.Person.Cpf(false),
                _faker.Random.String(_faker.Random.Int(8, 10)),
                _faker.Person.FullName,
                _faker.Random.String(_faker.Random.Int(8, 10)),
                dataNascimento
            );
        }
    }
}