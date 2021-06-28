using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CadFuncionario.Domain.Entities;

namespace CadFuncionario.Application.Interfaces
{
    public interface IProfissaoAppService : IDisposable
    {
        Task<bool> AdicionarAsync(Profissao profissao);
        Task<bool> AdicionarStepAsync(StepProfissao stepProfissao);
        Task<bool> AtualizarAsync(Profissao profissao);
        Task<bool> AtualizarStepAsync(StepProfissao stepProfissao);
        Task<Profissao> ObterAsync(Guid profissaoId);
        Task<StepProfissao> ObterStepAsync(Guid stepProfissaoId);
        Task<ICollection<Profissao>> ObterTodosAsync();
    }
}