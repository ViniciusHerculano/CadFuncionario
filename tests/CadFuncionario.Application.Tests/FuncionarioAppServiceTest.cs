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
    public class FuncionarioAppServiceTest
    {
        private readonly Faker _faker;

        public FuncionarioAppServiceTest()
        {
            _faker = new Faker("pt_BR");
        }

        [Fact(DisplayName = "Adicionar funcionario com dados invalidos")]
        [Trait("Grupo", "Services")]
        public async Task FuncionarioAppService_Adicionar_Invalido()
        {
            //Arrange
            var funcionario = MontarFuncionario(true);
            var mocker = new AutoMocker();
            var funcionarioAppService = mocker.CreateInstance<FuncionarioAppService>();

            //Act
            var result = await funcionarioAppService.AdicionarAsync(funcionario);

            //Assert
            Assert.False(result);
        }

        [Fact(DisplayName = "Adicionar funcionario com dados validos")]
        [Trait("Grupo", "Services")]
        public async Task FuncionarioAppService_Adicionar_Valido()
        {
            //Arrange
            var funcionario = MontarFuncionario();
            var mocker = new AutoMocker();
            var funcionarioAppService = mocker.CreateInstance<FuncionarioAppService>();

            //Act
            var result = await funcionarioAppService.AdicionarAsync(funcionario);

            //Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Atualizar funcionario com dados invalidos")]
        [Trait("Grupo", "Services")]
        public async Task FuncionarioAppService_Atualizar_Invalido()
        {
            //Arrange
            var funcionario = MontarFuncionario(true);
            var mocker = new AutoMocker();
            var funcionarioAppService = mocker.CreateInstance<FuncionarioAppService>();

            //Act
            var result = await funcionarioAppService.AtualizarAsync(funcionario);

            //Assert
            Assert.False(result);
        }

        [Fact(DisplayName = "Atualizar funcionario com dados validos")]
        [Trait("Grupo", "Services")]
        public async Task FuncionarioAppService_Atualizar_Valido()
        {
            //Arrange
            var funcionario = MontarFuncionario();
            var mocker = new AutoMocker();
            var funcionarioAppService = mocker.CreateInstance<FuncionarioAppService>();

            //Act
            var result = await funcionarioAppService.AtualizarAsync(funcionario);

            //Assert
            Assert.True(result);
        }


        [Fact(DisplayName = "Obter funcionario com dados invalidos")]
        [Trait("Grupo", "Services")]
        public async Task FuncionarioAppService_Obter_Invalido()
        {
            //Arrange
            var funcionario = MontarFuncionario(true);
            var mocker = new AutoMocker();
            var funcionarioAppService = mocker.CreateInstance<FuncionarioAppService>();

            //Act
            var result = await funcionarioAppService.ObterAsync(funcionario.FuncionarioId);

            //Assert
            Assert.Null(result);
        }


        [Fact(DisplayName = "Obter funcionario com dados validos")]
        [Trait("Grupo", "Services")]
        public async Task FuncionarioAppService_Obter_Sucesso()
        {
            //Arrange
            var funcionarioId = _faker.Random.Guid();
            var funcionarioResultMock = MontarFuncionario();
            var mocker = new AutoMocker();
            var mockFuncionarioRepository = mocker.GetMock<IFuncionarioRepository>();
            var funcionarioAppService = mocker.CreateInstance<FuncionarioAppService>();

            mockFuncionarioRepository.Setup(p => p.ObterAsync(funcionarioId))
                .Returns(Task.FromResult(funcionarioResultMock));

            //Act
            var result = await funcionarioAppService.ObterAsync(funcionarioId);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(funcionarioResultMock, result);
        }


        [Fact(DisplayName = "Obter todos os funcionarios")]
        [Trait("Grupo", "Services")]
        public async Task FuncionarioAppService_ObterTodos_Sucesso()
        {
            //Arrange
            var listaFuncionariosMockResult = new List<Funcionario>
            {
                MontarFuncionario(),
                MontarFuncionario(),
                MontarFuncionario()
            };
            var mocker = new AutoMocker();
            var mockFuncionarioRepository = mocker.GetMock<IFuncionarioRepository>();
            var funcionarioAppService = mocker.CreateInstance<FuncionarioAppService>();

            mockFuncionarioRepository.Setup(p => p.ObterTodosAsync())
                .Returns(Task.FromResult(listaFuncionariosMockResult as ICollection<Funcionario>));

            //Act
            var result = await funcionarioAppService.ObterTodosAsync();

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(listaFuncionariosMockResult.Count, result.Count);
            Assert.Equal(listaFuncionariosMockResult, result.ToList());

        }

        private Funcionario MontarFuncionario(bool invalido = false)
        {
            if (invalido)
                return new Funcionario(Guid.Empty, Guid.Empty, null, null, null, null, null);

            return new Funcionario(
            Guid.Empty,
            _faker.Random.Guid(),
            _faker.Random.String(11),
            _faker.Random.String(10),
            _faker.Person.FullName,
            _faker.Random.String(8),
            _faker.Person.DateOfBirth
            );
        }
    }
}
