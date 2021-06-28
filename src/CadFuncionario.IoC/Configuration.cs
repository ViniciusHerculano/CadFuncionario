using CadFuncionario.Application;
using CadFuncionario.Application.Interfaces;
using CadFuncionario.Core.Services;
using CadFuncionario.Core.Services.Interfaces;
using CadFuncionario.Data.Repositories;
using CadFuncionario.Domain.Interfaces.Data;
using Microsoft.Extensions.DependencyInjection;

namespace CadFuncionario.IoC
{
    public static class Configuration
    {
        public static void RegisterServicesIoc(this IServiceCollection serviceCollection)
        {
            #region Services

            serviceCollection.AddScoped<INotificationService, NotificationService>();
            serviceCollection.AddScoped<IProfissaoAppService, ProfissaoAppService>();

            #endregion

            #region Repositories

            serviceCollection.AddScoped<IProfissaoRepository, ProfissaoRepository>();
            serviceCollection.AddScoped<IFuncionarioRepository, FuncionarioRepository>();

            #endregion
        }
    }
}