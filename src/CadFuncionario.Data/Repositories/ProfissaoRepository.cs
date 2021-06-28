using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CadFuncionario.Domain.Entities;
using CadFuncionario.Domain.Interfaces.Data;
using Microsoft.EntityFrameworkCore;

namespace CadFuncionario.Data.Repositories
{
    public class ProfissaoRepository : IProfissaoRepository
    {
        private readonly Context _context;

        public ProfissaoRepository(Context context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Profissao profissao)
        {
            _context.Profissoes.Add(profissao);
            await _context.SaveChangesAsync();
        }

        public async Task AdicionarStepAsync(StepProfissao stepProfissao)
        {
            _context.StepProfissoes.Add(stepProfissao);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Profissao profissao)
        {
            //_context.Profissoes.Update(profissao);
            _context.Profissoes.Attach(profissao);
            _context.Entry(profissao).Property(p => p.Descricao).IsModified = true;
            _context.Entry(profissao).Property(p => p.SalarioBase).IsModified = true;


            await _context.SaveChangesAsync();
        }

        public async Task AtualizarStepAsync(StepProfissao stepProfissao)
        {
            _context.StepProfissoes.Update(stepProfissao);
            await _context.SaveChangesAsync();
        }

        public async Task<Profissao> ObterAsync(Guid profissaoId)
        {
            return await _context.Profissoes
                .AsNoTracking()
                .Include(p => p.StepProfissoes)
                .FirstOrDefaultAsync(p => p.ProfissaoId == profissaoId);
        }

        public async Task<StepProfissao> ObterStepAsync(Guid stepProfissaoId)
        {
            return await _context.StepProfissoes
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.StepProfissaoId == stepProfissaoId);
        }

        public async Task<ICollection<Profissao>> ObterTodosAsync()
        {
            return await _context.Profissoes
                .AsNoTracking()
                .Include(p => p.StepProfissoes)
                .ToListAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
