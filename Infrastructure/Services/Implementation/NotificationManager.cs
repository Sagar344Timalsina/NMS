using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Services;
using Infrastructure.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Implementation
{
    public class NotificationManager:INotificationManager
    {
        private readonly INotificationService _signalRService;
        private readonly IMessagePublisher _rabbitMqService;
        private readonly INotificationRepository repositories;

        //private readonly NotificationRepository _repository;
        public NotificationManager(INotificationService signalRService, IMessagePublisher rabbitMqService, INotificationRepository repositories)
        {
            _signalRService = signalRService;
            _rabbitMqService = rabbitMqService;
            this.repositories = repositories;

        }

        public async Task<bool> ProcessNotificationAsync(Notification notification)
        {
            try
            {
                //if (notification.Type.Equals("Live", StringComparison.OrdinalIgnoreCase))
                //{
                //    await _signalRService.SendNotificationAsync(notification);
                //    notification.IsSent = true; 
                //}
                //else
                //{
                //   // await _rabbitMqService.SendNotificationAsync(notification);
                //    //notification.IsSent = false;
                //}
                if (notification.Type.ToLower() == "email")
                {
                    await _rabbitMqService.publishAsync("emailQueue",notification);
                }
                else if(notification.Type.ToLower() == "app")
                {
                    await _rabbitMqService.publishAsync("appQueue", notification);
                }
                else if(notification.Type.ToLower() == "sms")
                {
                    await _rabbitMqService.publishAsync("smsQueue", notification);
                }
  
                var response = await repositories.AddAsync(notification);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
