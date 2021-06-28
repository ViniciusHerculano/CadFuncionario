using System.Collections.Generic;
using CadFuncionario.Core.DomainObjects;
using FluentValidation.Results;

namespace CadFuncionario.Core.Services.Interfaces
{
    public interface INotificationService
    {
        void Add(NotificationMessage notificationMessage);
        void Add(ValidationFailure validationFailure);
        List<NotificationMessage> GetMessages();
        List<ValidationFailure> GetValidationFailures();
        void Clear();
    }
}