using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CadFuncionario.Domain.Entities;

namespace CadFuncionario.Domain.Interfaces.Data
{
    public interface IFuncionarioRepository : IDisposable
    {
        Task AdicionarAsync(Funcionario funcionario);
        Task AtualizarAsync(Funcionario funcionario);
        Task<Funcionario> ObterAsync(Guid funcionarioId);
        Task<ICollection<Funcionario>> ObterTodosAsync();
    }
}