using Application.Common.DTOs.Notification;
using Domain.Entities;
using FluentValidation;

namespace WebApi.Validators.Notification
{
    public class NotificationUpdateValidator : AbstractValidator<NotificationUpdateRequestDTO>
    {
        public NotificationUpdateValidator()
        {
            RuleFor(x => x.Id)
             .NotEmpty().WithMessage("Please provide Id.");
            RuleFor(x => x.UserId)
             .NotEmpty().WithMessage("Please provide UserId.");


            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Message is required.");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Type is required.");

        }
    }
}
