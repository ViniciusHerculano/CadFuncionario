using System.Linq;
using System;
using Bogus;
using Bogus.Extensions.Brazil;
using CadFuncionario.Domain.Entities;
using Xunit;

namespace CadFuncionario.Validations.Tests
{
    public class FuncionarioValidationTest
    {
        private readonly Faker _faker;

        public FuncionarioValidationTest()
        {
            _faker = new Faker("pt_BR");
        }

        [Fact(DisplayName = "Validar funcionario com dados requeridos falhando")]
        [Trait("Grupo", "Validations")]
        public void FuncionarioValidation_FalharDadosRequeridos()
        {
            // Arrange
            var funcionario =
                new Funcionario(Guid.Empty, Guid.Empty, "", null, "", "", null);
            var validation = new FuncionarioValidation();

            // Act
            var validationResult = validation.Validate(funcionario);

            // Asserts
            Assert.NotNull(validationResult);
            Assert.False(validationResult.IsValid);
            Assert.Equal(6, validationResult.Errors.Count);
        }

        [Fact(DisplayName = "Validar funcionario com dados de entrada falhando")]
        [Trait("Grupo", "Validations")]
        public void FuncionarioValidation_DadosDeEntrada_Falhando()
        {
            // Arrange
            var funcionario = new Funcionario(Guid.Empty, _faker.Random.Guid(),
                _faker.Person.Cpf(), _faker.Random.String(11),
                _faker.Random.String(101), _faker.Random.String(21),
                null);

            var validation = new FuncionarioValidation();

            // Act
            var validationResult = validation.Validate(funcionario);

            // Asserts
            Assert.NotNull(validationResult);
            Assert.False(validationResult.IsValid);
            Assert.Equal(4, validationResult.Errors.Count);
        }

        [Fact(DisplayName = "Validar funcionario com dados de entrada válidos1")]
        [Trait("Grupo", "Validations")]
        public void FuncionarioValidation_DadosDeEntrada_Valido()
        {
            // Arrange
            var funcionario = new Funcionario(Guid.Empty, _faker.Random.Guid(),
                _faker.Person.Cpf(false), _faker.Random.String(9),
                _faker.Person.FullName, _faker.Random.String(15),
                DateTime.Now.AddYears(-18));

            var validation = new FuncionarioValidation();

            // Act
            var validationResult = validation.Validate(funcionario);

            // Asserts
            Assert.NotNull(validationResult);
            Assert.True(validationResult.IsValid);
            Assert.Empty(validationResult.Errors);
        }
    }
}