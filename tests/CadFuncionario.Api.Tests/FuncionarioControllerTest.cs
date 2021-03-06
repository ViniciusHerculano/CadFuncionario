using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CadFuncionario.Api.Tests.Config;
using CadFuncionario.Domain.Entities;
using Bogus.Extensions.Brazil;
using Xunit;

namespace CadFuncionario.Api.Tests
{
    [TestCaseOrderer("CadFuncionario.Api.Tests.Config.PriorityOrderer", "CadFuncionario.Api.Tests")]
    [Collection(nameof(IntegracaoTestsFixtureCollection))]
    public class FuncionarioControllerTest
    {
        private readonly IntegracaoTestsFixture _testsFixture;

        public FuncionarioControllerTest(IntegracaoTestsFixture testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Adicionar funcionario"), TestPriority(1)]
        [Trait("Grupo", "IntegracaoAPI")]
        public async Task FuncionarioController_Adicionar()
        {
            //Arrange
            var faker = _testsFixture.Faker;

            var profissao = MontarProfissao();
            _testsFixture.ProfissaoId = profissao.ProfissaoId;
            var stepProfissao = MontarStepProfissao();
            _testsFixture.StepProfissaoId = stepProfissao.StepProfissaoId;

            var funcionario = new Funcionario(
            faker.Random.Guid(),
            _testsFixture.StepProfissaoId,
            faker.Person.Cpf(false),
            faker.Address.ZipCode(),
            faker.Person.FullName,
            faker.Person.Cpf(false),
            faker.Person.DateOfBirth
            );
            _testsFixture.FuncionarioId = funcionario.FuncionarioId;

            //Act
            await _testsFixture.Client.PostAsJsonAsync("api/profissao/adicionar", profissao);
            await _testsFixture.Client.PostAsJsonAsync("api/profissao/adicionarstep", stepProfissao);
            var response = await _testsFixture.Client
            .PostAsJsonAsync("api/funcionario/adicionar", funcionario);

            //Assert
            response.EnsureSuccessStatusCode();

            var retornoApi = await response.Content.ReadAsAsync<bool>();
            Assert.True(retornoApi);
        }

        [Fact(DisplayName = "Obter funcionario"), TestPriority(2)]
        [Trait("Grupo", "IntegracaoAPI")]
        public async Task FuncionarioController_Obter()
        {
            //Arrange  & Act
            var response = await _testsFixture.Client
                .GetAsync($"api/funcionario/obter/{_testsFixture.FuncionarioId}");

            //Assert
            response.EnsureSuccessStatusCode();

            _testsFixture.FuncionarioApi = await response.Content.ReadAsAsync<Funcionario>();

            Assert.NotNull(_testsFixture.FuncionarioApi);
            Assert.NotEqual(Guid.Empty, _testsFixture.FuncionarioApi.FuncionarioId);
        }

        [Fact(DisplayName = "Atualizar funcionario"), TestPriority(3)]
        [Trait("Grupo", "IntegracaoAPI")]
        public async Task FuncionarioController_Atualizar()
        {
            //Arrange
            _testsFixture.FuncionarioApi.AlterarDataNascimento(DateTime.Now);

            //Act
            var response = await _testsFixture.Client
            .PutAsJsonAsync("api/funcionario/atualizar", _testsFixture.FuncionarioApi);

            //Assert
            response.EnsureSuccessStatusCode();

            var retornoApi = await response.Content.ReadAsAsync<bool>();
            Assert.True(retornoApi);
        }



        [Fact(DisplayName = "Obter todos os funcionarios"), TestPriority(4)]
        [Trait("Grupo", "IntegracaoAPI")]
        public async Task FuncionarioController_ObterTodos()
        {
            //Arrange & Act
            var response = await _testsFixture.Client
                .GetAsync($"api/funcionario/obtertodos");

            //Assert
            response.EnsureSuccessStatusCode();

            var listaFuncionarios = await response.Content.ReadAsAsync<List<Funcionario>>();

            Assert.NotEmpty(listaFuncionarios);
        }

        private StepProfissao MontarStepProfissao()
        {
            var faker = _testsFixture.Faker;
            return new StepProfissao(Guid.Empty, _testsFixture.ProfissaoId,
                faker.Random.Decimal(5, 20));
        }

        private Profissao MontarProfissao()
        {
            var faker = _testsFixture.Faker;
            return new Profissao(Guid.Empty, faker.Company.CompanyName(),
                faker.Random.Decimal(1000, 5000));
        }
    }
}
