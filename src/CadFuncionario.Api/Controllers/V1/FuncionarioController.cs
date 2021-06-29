using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CadFuncionario.Application.Interfaces;
using CadFuncionario.Core.Services.Interfaces;
using CadFuncionario.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CadFuncionario.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/funcionario")]
    [ApiController]
    public class FuncionarioController : MainController
    {
        private readonly IFuncionarioAppService _funcionarioAppService;
        public FuncionarioController(INotificationService notificationService, IFuncionarioAppService funcionarioAppService) : base(notificationService)
        {
            _funcionarioAppService = funcionarioAppService;
        }

        [HttpPost("Adicionar")]
        public async Task<ActionResult<bool>> Adicionar([FromBody] Funcionario funcionario)
        {
            return Result(await _funcionarioAppService.AdicionarAsync(funcionario));
        }

        [HttpPut("Atualizar")]
        public async Task<ActionResult<bool>> Atualizar([FromBody] Funcionario funcionario)
        {
            return Result(await _funcionarioAppService.AtualizarAsync(funcionario));
        }

        [HttpGet("Obter/{funcionarioId}")]
        public async Task<ActionResult<Funcionario>> Obter(Guid funcionarioId)
        {
            return Result(await _funcionarioAppService.ObterAsync(funcionarioId));
        }

        [HttpGet("ObterTodos")]
        public async Task<ActionResult<ICollection<Funcionario>>> ObterTodos()
        {
            return Result(await _funcionarioAppService.ObterTodosAsync());
        }


    }
}
