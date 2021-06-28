using System;
using Bogus;
using CadFuncionario.Domain.Entities;
using Xunit;

namespace CadFuncionario.Validations.Tests
{
    public class StepProfissaoValidationTest
    {
        private readonly Faker _faker;

        public StepProfissaoValidationTest()
        {
            _faker = new Faker("pt_BR");
        }

        [Fact(DisplayName = "Validar step profissao com dados requeridos falhando")]
        [Trait("Grupo", "Validations")]
        public void StepProfissaoValidation_FalharDadosRequeridos()
        {
            // Arrange
            var stepProfissao = new StepProfissao(Guid.Empty, Guid.Empty, 0);
            var validation = new StepProfissaoValidation();

            // Act
            var validationResult = validation.Validate(stepProfissao);

            // Asserts
            Assert.NotNull(validationResult);
            Assert.False(validationResult.IsValid);
            Assert.Equal(2, validationResult.Errors.Count);
        }

        [Fact(DisplayName = "Validar step profissao com dados validos")]
        [Trait("Grupo", "Validations")]
        public void StepProfissaoValidation_DadosDeEntrada_Valido()
        {
            // Arrange
            var stepProfissao = new StepProfissao(Guid.Empty, _faker.Random.Guid(),
                _faker.Random.Decimal(5, 15));
            var validation = new StepProfissaoValidation();

            // Act
            var validationResult = validation.Validate(stepProfissao);

            // Asserts
            Assert.NotNull(validationResult);
            Assert.True(validationResult.IsValid);
            Assert.Empty(validationResult.Errors);
        }
    }
}