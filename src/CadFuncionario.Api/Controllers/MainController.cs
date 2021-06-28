using System.Linq;
using CadFuncionario.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CadFuncionario.Api.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public MainController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        protected ActionResult Result(object result = null)
        {
            if (_notificationService.GetMessages().Any())
                return BadRequest(_notificationService.GetMessages().Select(n => n.Message));

            if (_notificationService.GetValidationFailures().Any())
                return BadRequest(_notificationService.GetValidationFailures()
                    .Select(v => new { v.PropertyName, v.ErrorMessage }));

            return Ok(result);
        }
    }
}