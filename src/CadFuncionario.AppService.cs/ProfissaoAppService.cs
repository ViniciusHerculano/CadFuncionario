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
    public class ProfissaoAppService : AppServiceBase, IProfissaoAppService
    {
        private readonly IProfissaoRepository _profissaoRepository;

        public ProfissaoAppService(INotificationService notificationService, IProfissaoRepository profissaoRepository)
            : base(notificationService)
        {
            _profissaoRepository = profissaoRepository;
        }


        public async Task<bool> AdicionarAsync(Profissao profissao)
        {
            if (!Validar(new ProfissaoValidation(), profissao))
                return false;

            await _profissaoRepository.AdicionarAsync(profissao);
            return true;
        }

        public async Task<bool> AdicionarStepAsync(StepProfissao stepProfissao)
        {
            if (!Validar(new StepProfissaoValidation(), stepProfissao))
                return false;

            await _profissaoRepository.AdicionarStepAsync(stepProfissao);
            return true;
        }

        public async Task<bool> AtualizarAsync(Profissao profissao)
        {
            if (!Validar(new ProfissaoValidation(true), profissao))
                return false;

            await _profissaoRepository.AtualizarAsync(profissao);
            return true;
        }

        public async Task<bool> AtualizarStepAsync(StepProfissao stepProfissao)
        {
            if (!Validar(new StepProfissaoValidation(), stepProfissao))
                return false;

            await _profissaoRepository.AtualizarStepAsync(stepProfissao);
            return true;
        }

        public async Task<Profissao> ObterAsync(Guid profissaoId)
        {
            if (profissaoId != null && profissaoId != Guid.Empty)
                return await _profissaoRepository.ObterAsync(profissaoId);

            Notify("Favor, informe um ProfissãoId válido");
            return null;
        }

        public async Task<StepProfissao> ObterStepAsync(Guid stepProfissaoId)
        {
            if (stepProfissaoId != null && stepProfissaoId != Guid.Empty)
                return await _profissaoRepository.ObterStepAsync(stepProfissaoId);

            Notify("StepProfissaoId", "Informe o StepProfissaoId");
            return null;
        }

        public async Task<ICollection<Profissao>> ObterTodosAsync()
        {
            return await _profissaoRepository.ObterTodosAsync();
        }

        public void Dispose()
        {
            _profissaoRepository?.Dispose();
        }
    }
}