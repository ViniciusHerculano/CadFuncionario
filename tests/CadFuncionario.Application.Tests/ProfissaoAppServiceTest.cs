using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using CadFuncionario.Domain.Entities;
using CadFuncionario.Domain.Interfaces.Data;
using Moq.AutoMock;
using Xunit;

namespace CadFuncionario.Application.Tests
{
    public class ProfissaoAppServiceTest
    {
        private readonly Faker _faker;

        public ProfissaoAppServiceTest()
        {
            _faker = new Faker("pt_BR");
        }


        [Fact(DisplayName = "Adicionar profissao com dados invalidos")]
        [Trait("Grupo", "Services")]
        public async Task ProfissaoAppService_AdicionarProfissao_Invalido()
        {
            // Arrange
            var profissao = MontarProfissao(true);
            var mocker = new AutoMocker();
            var profissaoAppService = mocker.CreateInstance<ProfissaoAppService>();

            // Act
            var result = await profissaoAppService.AdicionarAsync(profissao);

            // Assert
            Assert.False(result);
        }

        [Fact(DisplayName = "Adicionar profissao com dados validos")]
        [Trait("Grupo", "Services")]
        public async Task ProfissaoAppService_AdicionarProfissao_Sucesso()
        {
            // Arrange
            var profissao = MontarProfissao();
            var mocker = new AutoMocker();
            var profissaoAppService = mocker.CreateInstance<ProfissaoAppService>();

            // Act
            var result = await profissaoAppService.AdicionarAsync(profissao);

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Adicionar step profissao com dados invalidos")]
        [Trait("Grupo", "Services")]
        public async Task ProfissaoAppService_AdicionarStep_Invalido()
        {
            // Arrange
            var stepProfissao = MontarStepProfissao(true);
            var mocker = new AutoMocker();
            var profissaoAppService = mocker.CreateInstance<ProfissaoAppService>();

            // Act
            var result = await profissaoAppService.AdicionarStepAsync(stepProfissao);

            // Assert
            Assert.False(result);
        }

        [Fact(DisplayName = "Adicionar step profissao com dados validos")]
        [Trait("Grupo", "Services")]
        public async Task ProfissaoAppService_AdicionarStep_Sucesso()
        {
            // Arrange
            var stepProfissao = MontarStepProfissao();
            var mocker = new AutoMocker();
            var profissaoAppService = mocker.CreateInstance<ProfissaoAppService>();

            // Act
            var result = await profissaoAppService.AdicionarStepAsync(stepProfissao);

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Atualizar profissao com dados invalidos")]
        [Trait("Grupo", "Services")]
        public async Task ProfissaoAppService_AtualizarProfissao_Invalido()
        {
            // Arrange
            var profissao = MontarProfissao(true);
            var mocker = new AutoMocker();
            var profissaoAppService = mocker.CreateInstance<ProfissaoAppService>();

            // Act
            var result = await profissaoAppService.AtualizarAsync(profissao);

            // Assert
            Assert.False(result);
        }

        [Fact(DisplayName = "Atualizar profissao com dados validos")]
        [Trait("Grupo", "Services")]
        public async Task ProfissaoAppService_AtualizarProfissao_Sucesso()
        {
            // Arrange
            var profissao = MontarProfissao();
            var mocker = new AutoMocker();
            var profissaoAppService = mocker.CreateInstance<ProfissaoAppService>();

            // Act
            var result = await profissaoAppService.AtualizarAsync(profissao);

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Atualizar step profissao com dados invalidos")]
        [Trait("Grupo", "Services")]
        public async Task ProfissaoAppService_AtualizarStepProfissao_Invalido()
        {
            // Arrange
            var stepProfissao = MontarStepProfissao(true);
            var mocker = new AutoMocker();
            var profissaoAppService = mocker.CreateInstance<ProfissaoAppService>();

            // Act
            var result = await profissaoAppService.AtualizarStepAsync(stepProfissao);

            // Assert
            Assert.False(result);
        }

        [Fact(DisplayName = "Atualizar step profissao com dados validos")]
        [Trait("Grupo", "Services")]
        public async Task ProfissaoAppService_AtualizarStepProfissao_Sucesso()
        {
            // Arrange
            var stepProfissao = MontarStepProfissao();
            var mocker = new AutoMocker();
            var profissaoAppService = mocker.CreateInstance<ProfissaoAppService>();

            // Act
            var result = await profissaoAppService.AtualizarStepAsync(stepProfissao);

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Obter profissao passando dados invalidos")]
        [Trait("Grupo", "Services")]
        public async Task ProfissaoAppService_Obter_Invalido()
        {
            // Arrange
            var mocker = new AutoMocker();
            var profissaoAppService = mocker.CreateInstance<ProfissaoAppService>();

            // Act
            var result = await profissaoAppService.ObterAsync(Guid.Empty);

            // Assert
            Assert.Null(result);
        }

        [Fact(DisplayName = "Obter profissao passando dados validos")]
        [Trait("Grupo", "Services")]
        public async Task ProfissaoAppService_Obter_Sucesso()
        {
            // Arrange
            var profissaoId = _faker.Random.Guid();
            var profissaoResultMock = MontarProfissao();
            var mocker = new AutoMocker();
            var mockProfissaoRepository = mocker.GetMock<IProfissaoRepository>();
            var profissaoAppService = mocker.CreateInstance<ProfissaoAppService>();

            mockProfissaoRepository.Setup(p => p.ObterAsync(profissaoId))
                .Returns(Task.FromResult(profissaoResultMock));

            // Act
            var result = await profissaoAppService.ObterAsync(profissaoId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(profissaoResultMock, result);
        }

        [Fact(DisplayName = "Obter step profissao passando dados invalidos")]
        [Trait("Grupo", "Services")]
        public async Task ProfissaoAppService_ObterStep_Invalido()
        {
            // Arrange
            var mocker = new AutoMocker();
            var profissaoAppService = mocker.CreateInstance<ProfissaoAppService>();

            // Act
            var result = await profissaoAppService.ObterStepAsync(Guid.Empty);

            // Assert
            Assert.Null(result);
        }

        [Fact(DisplayName = "Obter step profissao passando dados validos")]
        [Trait("Grupo", "Services")]
        public async Task ProfissaoAppService_ObterStep_Sucesso()
        {
            // Arrange
            var stepProfissaoId = _faker.Random.Guid();
            var stepProfissaoResultMock = MontarStepProfissao();
            var mocker = new AutoMocker();
            var mockProfissaoRepository = mocker.GetMock<IProfissaoRepository>();
            var profissaoAppService = mocker.CreateInstance<ProfissaoAppService>();

            mockProfissaoRepository.Setup(p => p.ObterStepAsync(stepProfissaoId))
                .Returns(Task.FromResult(stepProfissaoResultMock));

            // Act
            var result = await profissaoAppService.ObterStepAsync(stepProfissaoId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(stepProfissaoResultMock, result);
        }

        [Fact(DisplayName = "Obter todas as profissoes")]
        [Trait("Grupo", "Services")]
        public async Task ProfissaoAppService_ObterTodos_Sucesso()
        {
            // Arrange
            var listaProfissoesMockResult = new List<Profissao>
            {
                MontarProfissao(),
                MontarProfissao(),
                MontarProfissao()
            };
            var mocker = new AutoMocker();
            var mockProfissaoRepository = mocker.GetMock<IProfissaoRepository>();
            var profissaoAppService = mocker.CreateInstance<ProfissaoAppService>();

            mockProfissaoRepository.Setup(p => p.ObterTodosAsync())
                .Returns(Task.FromResult(listaProfissoesMockResult as ICollection<Profissao>));

            // Act
            var result = await profissaoAppService.ObterTodosAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(listaProfissoesMockResult.Count, result.Count);
            Assert.Equal(listaProfissoesMockResult, result.ToList());
        }

        private Profissao MontarProfissao(bool invalido = false)
        {
            if (invalido)
                return new Profissao(Guid.Empty, null, 0);

            var maxDescricaoProfissao = _faker.Random.Int(15, 80);
            return new Profissao(Guid.Empty, _faker.Random.String(maxDescricaoProfissao),
                _faker.Random.Decimal(2000, 5000));
        }

        private StepProfissao MontarStepProfissao(bool invalido = false)
        {
            if (invalido)
                return new StepProfissao(Guid.Empty, Guid.Empty, 0);

            return new StepProfissao(Guid.Empty, _faker.Random.Guid(),
                _faker.Random.Decimal(5, 20));
        }
    }
}