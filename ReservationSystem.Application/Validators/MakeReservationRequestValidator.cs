using FluentValidation;
using ReservationSystem.Application.Models.Reservations;

namespace ReservationSystem.Application.Validators
{
    public class MakeReservationRequestValidator : AbstractValidator<MakeReservationRequest>
    {
        public MakeReservationRequestValidator()
        {
            RuleFor(x => x.HostName).NotEmpty();

            RuleFor(x => x.OsName).NotEmpty();

            RuleFor(x => x.From).NotEmpty();

            RuleFor(x => x.To).NotEmpty();
        }
    }
}
