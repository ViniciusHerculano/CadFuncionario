using System;
using System.Collections.Generic;
using Bogus;
using CadFuncionario.Domain.Entities;
using Xunit;

namespace CadFuncionario.Validations.Tests
{
    public class ProfissaoValidationTest
    {
        private readonly Faker _faker;

        public ProfissaoValidationTest()
        {
            _faker = new Faker("pt_BR");
        }

        [Fact(DisplayName = "Validar profissao com dados requeridos falhando")]
        [Trait("Grupo", "Validations")]
        public void ProfissaoValidation_FalharDadosRequeridos()
        {
            // Arrange
            var profissao = new Profissao(Guid.Empty, "", 0);
            var validation = new ProfissaoValidation();

            // Act
            var validationResult = validation.Validate(profissao);

            // Asserts
            Assert.NotNull(validationResult);
            Assert.False(validationResult.IsValid);
            Assert.Equal(2, validationResult.Errors.Count);
        }

        [Fact(DisplayName = "Validar profissao com dados de entrada falhando")]
        [Trait("Grupo", "Validations")]
        public void ProfissaoValidation_DadosDeEntrada_Falhando()
        {
            // Arrange
            var quantidadeChar = _faker.Random.Int(81, 200);
            var profissao = new Profissao(Guid.Empty, _faker.Random
                .String(quantidadeChar), _faker.Random.Decimal(1000, 2000));

            var validation = new ProfissaoValidation();

            // Act
            var validationResult = validation.Validate(profissao);

            // Asserts
            Assert.NotNull(validationResult);
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
        }

        [Fact(DisplayName = "Validar profissao com dados de entrada validos")]
        [Trait("Grupo", "Validations")]
        public void ProfissaoValidation_DadosDeEntrada_Valido()
        {
            // Arrange
            var quantidadeChar = _faker.Random.Int(10, 80);
            var profissao = new Profissao(Guid.Empty, _faker.Random
            .String(quantidadeChar), _faker.Random.Decimal(2000, 3000));

            var validation = new ProfissaoValidation();

            // Act
            var validationResult = validation.Validate(profissao);

            // Assert
            Assert.NotNull(validationResult);
            Assert.True(validationResult.IsValid);
            Assert.Empty(validationResult.Errors);
        }

        [Fact(DisplayName = "Validar steps da profissao com dados de entrada falhando")]
        [Trait("Grupo", "Validations")]
        public void ProfissaoValidation_DadosDeEntrada_Steps_Falhando()
        {
            // Arrange
            var quantidadeChar = _faker.Random.Int(10, 80);
            var profissao = new Profissao(Guid.Empty, _faker.Random
                .String(quantidadeChar), _faker.Random.Decimal(2000, 3000));

            profissao.StepProfissoes = new List<StepProfissao> {
                new StepProfissao(Guid.Empty,Guid.Empty, 0)
            };

            var validation = new ProfissaoValidation();

            // Act
            var validationResult = validation.Validate(profissao);

            // Assert
            Assert.NotNull(validationResult);
            Assert.False(validationResult.IsValid);
            Assert.Equal(2, validationResult.Errors.Count);
        }

        [Fact(DisplayName = "Validar steps da profissao com dados de entrada validos")]
        [Trait("Grupo", "Validations")]
        public void ProfissaoValidation_DadosDeEntrada_Steps_Valido()
        {
            // Arrange
            var quantidadeChar = _faker.Random.Int(10, 80);
            var profissao = new Profissao(Guid.Empty, _faker.Random
                .String(quantidadeChar), _faker.Random.Decimal(2000, 3000));

            profissao.StepProfissoes = new List<StepProfissao> {
                new StepProfissao(Guid.Empty,_faker.Random.Guid(),
                _faker.Random.Decimal(5, 15))
            };

            var validation = new ProfissaoValidation();

            // Act
            var validationResult = validation.Validate(profissao);

            // Assert
            Assert.NotNull(validationResult);
            Assert.True(validationResult.IsValid);
            Assert.Empty(validationResult.Errors);
        }
    }
}
