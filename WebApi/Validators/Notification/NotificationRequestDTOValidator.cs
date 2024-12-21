using Application.Common.DTOs.Notification;
using FluentValidation;

namespace WebApi.Validators.Notification
{
    public class NotificationRequestDTOValidator : AbstractValidator<NotificationRequestDTO>
    {
        public NotificationRequestDTOValidator()
        {
            RuleFor(x => x.UserId)
             .NotEmpty().WithMessage("Please provide UserId.");


            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Message is required.");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Type is required.");
        }
    }
}
