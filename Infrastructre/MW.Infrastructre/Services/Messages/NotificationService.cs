using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MW.Application.Interfaces.Services.Messages;
using MW.Domain.Enums.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW.Infrastructre.Services.Messages
{
    public class NotificationService : INotificationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;

        public NotificationService(IHttpContextAccessor httpContextAccessor, ITempDataDictionaryFactory tempDataDictionaryFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            _tempDataDictionaryFactory = tempDataDictionaryFactory;
        }

        protected virtual void PrepareTempData(NotificationType type, string message)
        {
            var context = _httpContextAccessor.HttpContext;
            var tempData = _tempDataDictionaryFactory.GetTempData(context);

            var messageDictionary = new Dictionary<string, string>(){
                {"Message", message},
                {"Type", type.ToString()},
            };

            tempData["Notification"] = messageDictionary;
        }
        public void ErrorNotification(string message)
        {
            PrepareTempData(NotificationType.Error, message);
        }

        public void Notification(NotificationType type, string message)
        {
            PrepareTempData(type, message);
        }

        public void SuccessNotification(string message)
        {
            PrepareTempData(NotificationType.Success, message);
        }

        public void WarningNotification(string message)
        {
            PrepareTempData(NotificationType.Warning, message);
        }
    }
}
