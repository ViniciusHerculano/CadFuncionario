using System;
using Bogus;
using CadFuncionario.Domain.Entities;
using Xunit;

namespace CadFuncionario.Domain.Tests.Entities
{
    public class StepProfissaoTest
    {
        private readonly Faker _faker;

        public StepProfissaoTest()
        {
            _faker = new Faker("pt_BR");
        }


        [Fact(DisplayName = "Testando geração da StepProfissaoId")]
        [Trait("Grupo", "Entities")]
        public void StepProfissao_GerandoId_ComSucesso()
        {
            // Arrange & Act
            var stepProfissao = new StepProfissao(Guid.Empty, _faker.Random.Guid(), _faker.Random.Decimal(3.5M, 15));

            // Assert
            Assert.NotNull(stepProfissao.StepProfissaoId);
        }

        [Fact(DisplayName = "Alterando percentual de aumento")]
        [Trait("Grupo", "Entities")]
        public void StepProfissao_AlterarSalarioBase_Automatico()
        {
            // Arrange
            var stepProfissao = new StepProfissao(
                Guid.Empty,
                _faker.Random.Guid(),
                _faker.Random.Decimal(3.5M, 15)
            );

            var percentualOriginal = stepProfissao.PercentualAumento;

            // Act
            stepProfissao.AlterarPercentualAumento(_faker.Random.Decimal(percentualOriginal + 1, 30));

            // Assert
            Assert.NotEqual(percentualOriginal, stepProfissao.PercentualAumento);
        }
    }
}