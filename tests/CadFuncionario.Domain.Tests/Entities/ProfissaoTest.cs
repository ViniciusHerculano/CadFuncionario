using System;
using Bogus;
using CadFuncionario.Domain.Entities;
using Xunit;

namespace CadFuncionario.Domain.Tests.Entities
{
    public class ProfissaoTest
    {
        private readonly Faker _faker;

        public ProfissaoTest()
        {
            _faker = new Faker("pt_BR");
        }


        [Fact(DisplayName = "Testando geração da ProfissaoId")]
        [Trait("Grupo", "Entities")]
        public void Profissao_GerandoId_ComSucesso()
        {
            // Arrange & Act
            var profissao = new Profissao(Guid.Empty, "Programador Jr", 2000M);

            // Assert
            Assert.NotNull(profissao.ProfissaoId);
        }

        [Fact(DisplayName = "Alterando salario base com dados manual",
            Skip = "Para exemplificar como pular um teste")]
        [Trait("Grupo", "Entities")]
        public void Profissao_AlterarSalarioBase_Manual()
        {
            // Arrange
            var profissao = new Profissao(Guid.Empty, "Programador Jr", 2000M);
            var salarioOriginal = profissao.SalarioBase;

            // Act
            profissao.AlterarSalarioBase(2100);

            // Assert
            Assert.NotEqual(salarioOriginal, profissao.SalarioBase);
        }

        [Fact(DisplayName = "Alterando salario base com dados automaticos")]
        [Trait("Grupo", "Entities")]
        public void Profissao_AlterarSalarioBase_Automatico()
        {
            // Arrange
            var tamanhoDescricao = _faker.Random.Int(10, 30);
            var profissao = new Profissao(Guid.Empty,
                _faker.Random.String(tamanhoDescricao),
                _faker.Finance.Amount(1000, 10000)
            );

            var salarioOriginal = profissao.SalarioBase;

            // Act
            profissao.AlterarSalarioBase(_faker.Finance.Amount(salarioOriginal + 1, 20000));

            // Assert
            Assert.NotEqual(salarioOriginal, profissao.SalarioBase);
        }
    }
}