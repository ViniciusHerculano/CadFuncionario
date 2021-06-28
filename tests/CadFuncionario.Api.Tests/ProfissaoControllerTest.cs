using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CadFuncionario.Api.Tests.Config;
using CadFuncionario.Domain.Entities;
using Xunit;

namespace CadFuncionario.Api.Tests
{
    [TestCaseOrderer("CadFuncionario.Api.Tests.Config.PriorityOrderer", "CadFuncionario.Api.Tests")]
    [Collection(nameof(IntegracaoTestsFixtureCollection))]
    public class ProfissaoControllerTest
    {
        private readonly IntegracaoTestsFixture _testsFixture;

        public ProfissaoControllerTest(IntegracaoTestsFixture testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Adicionar profissao"), TestPriority(1)]
        [Trait("Grupo", "IntegracaoAPI")]
        public async Task ProfissaoController_Adicionar()
        {
            // Arrange
            var faker = _testsFixture.Faker;
            var profissao = new Profissao(Guid.Empty, faker.Company.CompanyName(),
                faker.Random.Decimal(1000, 5000));
            _testsFixture.ProfissaoId = profissao.ProfissaoId;

            // Act
            var response = await _testsFixture.Client
                .PostAsJsonAsync("api/profissao/adicionar", profissao);

            // Assert
            response.EnsureSuccessStatusCode();

            var retornoApi = await response.Content.ReadAsAsync<bool>();
            Assert.True(retornoApi);
        }

        [Fact(DisplayName = "Adicionar step profissao"), TestPriority(2)]
        [Trait("Grupo", "IntegracaoAPI")]
        public async Task ProfissaoController_AdicionarStep()
        {
            // Arrange
            var stepProfissao = new StepProfissao(Guid.Empty, _testsFixture.ProfissaoId,
                _testsFixture.Faker.Random.Decimal(3, 7));
            _testsFixture.StepProfissaoId = stepProfissao.StepProfissaoId;

            // Act
            var response = await _testsFixture.Client
                .PostAsJsonAsync("api/profissao/adicionarstep", stepProfissao);

            // Assert
            response.EnsureSuccessStatusCode();

            var retornoApi = await response.Content.ReadAsAsync<bool>();
            Assert.True(retornoApi);
        }

        [Fact(DisplayName = "Obter Profissao"), TestPriority(3)]
        [Trait("Grupo", "IntegracaoAPI")]
        public async Task ProfissaoController_Obter()
        {
            //Arrange & Act
            var response = await _testsFixture.Client
                .GetAsync($"api/profissao/obter/{_testsFixture.ProfissaoId}");


            //Assert
            response.EnsureSuccessStatusCode();

            _testsFixture.ProfissaoApi = await response.Content.ReadAsAsync<Profissao>();

            Assert.NotNull(_testsFixture.ProfissaoApi);
            Assert.NotEqual(Guid.Empty, _testsFixture.ProfissaoApi.ProfissaoId);
        }

        [Fact(DisplayName = "Atualizar profissao"), TestPriority(4)]
        [Trait("Grupo", "IntegracaoAPI")]
        public async Task ProfissaoController_Atualizar()
        {
            //Arrange
            _testsFixture.ProfissaoApi.AlterarDescricao($"{_testsFixture.ProfissaoApi.Descricao}-- Alterado");

            //Act
            var response = await _testsFixture.Client
                .PutAsJsonAsync($"api/profissao/atualizar", _testsFixture.ProfissaoApi);

            //Assert
            response.EnsureSuccessStatusCode();

            var retornoApi = await response.Content.ReadAsAsync<bool>();
            Assert.True(retornoApi);
        }

        [Fact(DisplayName = "Obter Step Profissao"), TestPriority(5)]
        [Trait("Grupo", "IntegracaoAPI")]
        public async Task ProfissaoController_ObterStepProfissao()
        {
            //Arrange & Act
            var response = await _testsFixture.Client
                .GetAsync($"api/profissao/obterstep/{_testsFixture.StepProfissaoId}");


            //Assert
            response.EnsureSuccessStatusCode();

            _testsFixture.StepProfissaoApi = await response.Content.ReadAsAsync<StepProfissao>();

            Assert.NotNull(_testsFixture.StepProfissaoApi);
            Assert.NotEqual(Guid.Empty, _testsFixture.StepProfissaoApi.StepProfissaoId);
        }

        [Fact(DisplayName = "Atualizar step profissao"), TestPriority(6)]
        [Trait("Grupo", "IntegracaoAPI")]
        public async Task ProfissaoController_AtualizarStepProfissao()
        {
            //Arrange
            _testsFixture.StepProfissaoApi
            .AlterarPercentualAumento(1);

            //Act
            var response = await _testsFixture.Client
                .PutAsJsonAsync($"api/profissao/atualizarstep", _testsFixture.StepProfissaoApi);

            //Assert
            response.EnsureSuccessStatusCode();

            var retornoApi = await response.Content.ReadAsAsync<bool>();
            Assert.True(retornoApi);
        }

        [Fact(DisplayName = "Obter todas as profissoes"), TestPriority(7)]
        [Trait("Grupo", "IntegracaoAPI")]
        public async Task ProfissaoController_ObterTodasProfissoes()
        {
            //Arrange & Act
            var response = await _testsFixture.Client
                .GetAsync($"api/profissao/obtertodos");


            //Assert
            response.EnsureSuccessStatusCode();

            var listaProfissao = await response.Content.ReadAsAsync<List<Profissao>>();

            Assert.NotEmpty(listaProfissao);
            //Assert.Contains(_testsFixture.ProfissaoApi, listaProfissao);
        }

    }
}
