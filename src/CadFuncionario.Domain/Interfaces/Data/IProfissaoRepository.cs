using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CadFuncionario.Domain.Entities;

namespace CadFuncionario.Domain.Interfaces.Data
{
    public interface IProfissaoRepository : IDisposable
    {
        Task AdicionarAsync(Profissao profissao);
        Task AdicionarStepAsync(StepProfissao stepProfissao);
        Task AtualizarAsync(Profissao profissao);
        Task AtualizarStepAsync(StepProfissao stepProfissao);
        Task<Profissao> ObterAsync(Guid profissaoId);
        Task<StepProfissao> ObterStepAsync(Guid stepProfissaoId);
        Task<ICollection<Profissao>> ObterTodosAsync();
    }
}
