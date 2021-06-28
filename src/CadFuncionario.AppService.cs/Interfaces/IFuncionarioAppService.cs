using System.Threading.Tasks;
using System;
using CadFuncionario.Domain.Entities;
using System.Collections.Generic;

namespace CadFuncionario.Application.Interfaces
{
    public interface IFuncionarioAppService : IDisposable
    {
        Task<bool> AdicionarAsync(Funcionario funcionario);
        Task<bool> AtualizarAsync(Funcionario funcionario);
        Task<Funcionario> ObterAsync(Guid funcionarioId);
        Task<ICollection<Funcionario>> ObterTodosAsync();
    }
}