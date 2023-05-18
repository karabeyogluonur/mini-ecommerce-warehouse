using MW.Domain.Enums.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW.Application.Interfaces.Services.Messages
{
    public interface INotificationService
    {
        void Notification(NotificationType type, string message);

        void SuccessNotification(string message);

        void WarningNotification(string message);

        void ErrorNotification(string message);
    }
}
