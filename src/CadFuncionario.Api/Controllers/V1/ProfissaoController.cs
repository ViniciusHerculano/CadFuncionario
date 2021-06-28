using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using CadFuncionario.Application.Interfaces;
using CadFuncionario.Core.Services.Interfaces;
using CadFuncionario.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CadFuncionario.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/profissao")]
    [ApiController]
    public class ProfissaoController : MainController
    {
        private readonly IProfissaoAppService _profissaoAppService;

        public ProfissaoController(INotificationService notificationService,
            IProfissaoAppService profissaoAppService) : base(notificationService)
        {
            _profissaoAppService = profissaoAppService;
        }

        [HttpPost("Adicionar")]
        public async Task<ActionResult<bool>> Adicionar([FromBody] Profissao profissao)
        {
            return Result(await _profissaoAppService.AdicionarAsync(profissao));
        }

        [HttpPost("AdicionarStep")]
        public async Task<ActionResult<bool>> AdicionarStep([FromBody] StepProfissao stepProfissao)
        {
            return Result(await _profissaoAppService.AdicionarStepAsync(stepProfissao));
        }

        [HttpPut("Atualizar")]
        public async Task<ActionResult<bool>> Atualizar([FromBody] Profissao profissao)
        {
            return Result(await _profissaoAppService.AtualizarAsync(profissao));
        }

        [HttpPut("AtualizarStep")]
        public async Task<ActionResult<bool>> AtualizarStep([FromBody] StepProfissao stepProfissao)
        {
            return Result(await _profissaoAppService.AtualizarStepAsync(stepProfissao));
        }

        [HttpGet("Obter/{profissaoId}")]
        public async Task<ActionResult<Profissao>> Obter(Guid profissaoId)
        {
            return Result(await _profissaoAppService.ObterAsync(profissaoId));
        }

        [HttpGet("ObterStep/{stepProfissaoId}")]
        public async Task<ActionResult<StepProfissao>> ObterStep(Guid stepProfissaoId)
        {
            return Result(await _profissaoAppService.ObterStepAsync(stepProfissaoId));
        }

        [HttpGet("ObterTodos")]
        public async Task<ActionResult<ICollection<Profissao>>> ObterTodos()
        {
            return Result(await _profissaoAppService.ObterTodosAsync());
        }
    }
}