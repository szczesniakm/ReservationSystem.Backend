using ReservationSystem.Application.Models.Hosts;
using FluentValidation;

namespace ReservationSystem.Application.Validators
{
    public class GetAvaliableHostsRequestValidator : AbstractValidator<GetAvaliableHostsRequest>
    {
        public GetAvaliableHostsRequestValidator()
        {
            RuleFor(x => x.From)
                .NotEmpty();

            RuleFor(x => x.To)
                .NotEmpty()
                .GreaterThan(x => x.From)
                .GreaterThan(DateTime.UtcNow);
        }
    }
}
