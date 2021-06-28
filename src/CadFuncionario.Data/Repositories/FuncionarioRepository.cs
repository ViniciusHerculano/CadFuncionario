using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CadFuncionario.Domain.Entities;
using CadFuncionario.Domain.Interfaces.Data;
using Microsoft.EntityFrameworkCore;

namespace CadFuncionario.Data.Repositories
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly Context _context;

        public FuncionarioRepository(Context context)
        {
            _context = context;
        }


        public async Task AdicionarAsync(Funcionario funcionario)
        {
            _context.Funcionarios.Add(funcionario);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Funcionario funcionario)
        {
            _context.Funcionarios.Update(funcionario);
            await _context.SaveChangesAsync();
        }

        public async Task<Funcionario> ObterAsync(Guid funcionarioId)
        {
            return await _context.Funcionarios
                .AsNoTracking()
                .Include(f => f.StepProfissao.Profissao)
                .FirstOrDefaultAsync(f => f.FuncionarioId == funcionarioId);
        }

        public async Task<ICollection<Funcionario>> ObterTodosAsync()
        {
            return await _context.Funcionarios
                .AsNoTracking()
                .Include(f => f.StepProfissao.Profissao)
                .ToListAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}