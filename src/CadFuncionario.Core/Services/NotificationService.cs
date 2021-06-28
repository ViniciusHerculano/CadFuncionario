using System.Collections.Generic;
using CadFuncionario.Core.DomainObjects;
using CadFuncionario.Core.Services.Interfaces;
using FluentValidation.Results;

namespace CadFuncionario.Core.Services
{
    public class NotificationService : INotificationService
    {
        private List<NotificationMessage> _notificationMessage;
        private List<ValidationFailure> _notificationsValidationFailure;

        public NotificationService()
        {
            Clear();
        }


        public void Add(NotificationMessage notificationMessage) => _notificationMessage.Add(notificationMessage);

        public void Add(ValidationFailure validationFailure) => _notificationsValidationFailure.Add(validationFailure);

        public List<NotificationMessage> GetMessages() => _notificationMessage;

        public List<ValidationFailure> GetValidationFailures() => _notificationsValidationFailure;


        public void Clear()
        {
            _notificationMessage = new List<NotificationMessage>();
            _notificationsValidationFailure = new List<ValidationFailure>();
        }
    }
}