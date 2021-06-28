using System.Net.Http;
using System;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Bogus;
using Bogus.Extensions.Brazil;

using CadFuncionario.Domain.Entities;


namespace CadFuncionario.Api.Tests.Config
{
    [CollectionDefinition(nameof(IntegracaoTestsFixtureCollection))]
    public class IntegracaoTestsFixtureCollection : ICollectionFixture<IntegracaoTestsFixture> { }

    public class IntegracaoTestsFixture : IDisposable
    {
        private readonly ApiFactory<Startup> _factory;

        public HttpClient Client;
        public Faker Faker;
        public Guid ProfissaoId;
        public Profissao ProfissaoApi;

        public Guid StepProfissaoId;
        public StepProfissao StepProfissaoApi;

        public Guid FuncionarioId;
        public Funcionario FuncionarioApi;

        public IntegracaoTestsFixture()
        {
            Faker = new Faker("pt_BR");

            _factory = new ApiFactory<Startup>();
            Client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            { BaseAddress = new Uri("http://localhost:5000") });
        }


        public void Dispose()
        {
            Client.Dispose();
            _factory.Dispose();
        }
    }
}
