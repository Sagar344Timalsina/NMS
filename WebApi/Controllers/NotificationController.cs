using Application.Common.DTOs.Notification;
using Application.Common.DTOs.User;
using Application.Notifications.Commands.Create;
using Application.Notifications.Commands.Update;
using Application.Notifications.Queries.GetAllWithPagination;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;
using WebApi.Validators.Notification;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController(ISender _sender, IValidator<NotificationRequestDTO> _validator, IValidator<NotificationUpdateRequestDTO> _validatorupdate) : BaseApiController
    {
        private ErrorController errorController { get; set; } = new();
        [HttpPost()]
        public async Task<IActionResult> createNotification(NotificationRequestDTO requestModel)
        {
            var validate = await _validator.ValidateAsync(requestModel);
            if (!validate.IsValid)
            {
                return errorController.ValidationErrorResponse(validate);
            }
            var requestCommand = new AddNotificationCommand(requestModel);
            var response = await _sender.Send(requestCommand);
            return Ok(response);
        }
        [HttpGet()]
        public async Task<IActionResult> getAllNotificationWithPagination(int userId,int PageNumber = 1, string sortColumn = null, string sortDirection = "asc", string filters = null)
        {
            if (userId == 0 || userId == null)
            {
                throw new ArgumentException("Please provide userId first!!");
            }
            var notification = new GetAllNotificationWithPagination(userId,PageNumber, sortColumn, sortDirection, filters);
            var response = await _sender.Send(notification);
            return Ok(response);

        }
        [HttpPut]
        public async Task<IActionResult> updateNotification(NotificationUpdateRequestDTO requestModel)
        {
            var validate = await _validatorupdate.ValidateAsync(requestModel);
            if (!validate.IsValid)
            {
                return errorController.ValidationErrorResponse(validate);
            }
            var requestCommand = new UpdateNotificationCommand(requestModel);
            var response = await _sender.Send(requestCommand);
            return Ok(response);
        }
    }
}
