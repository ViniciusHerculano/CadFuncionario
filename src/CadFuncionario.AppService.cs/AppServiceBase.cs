using CadFuncionario.Core.DomainObjects;
using CadFuncionario.Core.Services.Interfaces;
using FluentValidation;
using FluentValidation.Results;

namespace CadFuncionario.Application
{
    public abstract class AppServiceBase
    {
        private readonly INotificationService _notificationService;

        public AppServiceBase(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        protected bool Validar<TV, TE>(TV tValidacao, TE tEntidade) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = tValidacao.Validate(tEntidade);
            if (validator.IsValid)
                return true;

            Notify(validator);
            return false;
        }

        protected void Notify(string message) => _notificationService.Add(new NotificationMessage(message));

        protected void Notify(ValidationResult validationResult) => validationResult.Errors.ForEach(vf => _notificationService.Add(vf));

        protected void Notify(string propertyName, string message) => _notificationService.Add(new ValidationFailure(propertyName, message));
    }
}