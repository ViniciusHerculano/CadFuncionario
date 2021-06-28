using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CadFuncionario.Application.Interfaces;
using CadFuncionario.Core.Services.Interfaces;
using CadFuncionario.Domain.Entities;
using CadFuncionario.Domain.Interfaces.Data;
using CadFuncionario.Validations;

namespace CadFuncionario.Application
{
    public class FuncionarioAppService : AppServiceBase, IFuncionarioAppService
    {
        private readonly IFuncionarioRepository _funcionarioRepository;
        public FuncionarioAppService(INotificationService notificationService, IFuncionarioRepository funcionarioRepository) : base(notificationService)
        {
            _funcionarioRepository = funcionarioRepository;
        }
        public async Task<bool> AdicionarAsync(Funcionario funcionario)
        {
            if (!Validar(new FuncionarioValidation(), funcionario))
                return false;

            await _funcionarioRepository.AdicionarAsync(funcionario);
            return true;
        }

        public async Task<bool> AtualizarAsync(Funcionario funcionario)
        {
            if (!Validar(new FuncionarioValidation(), funcionario))
                return false;

            await _funcionarioRepository.AtualizarAsync(funcionario);
            return true;
        }


        public async Task<Funcionario> ObterAsync(Guid funcionarioId)
        {
            if (funcionarioId != null && funcionarioId != Guid.Empty)
                return await _funcionarioRepository.ObterAsync(funcionarioId);

            Notify("Favor, informe um FuncionarioID válido");
            return null;
        }

        public async Task<ICollection<Funcionario>> ObterTodosAsync()
        {
            return await _funcionarioRepository.ObterTodosAsync();
        }

        public void Dispose()
        {
            _funcionarioRepository?.Dispose();
        }
    }
}
