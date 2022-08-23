using FluentValidation;
using ReservationSystem.Application.Models;
using ReservationSystem.Domain.Models;
using ReservationSystem.Domain.Repositories;

namespace ReservationSystem.Application.Services
{
    public class HostsService
    {
        private readonly IHostRepository _hostRepository;
        private readonly IValidator<GetAvaliableHostsRequest> _validator;

        public HostsService(IHostRepository hostRepository, IValidator<GetAvaliableHostsRequest> validator)
        {
            _hostRepository = hostRepository;
            _validator = validator;
        }

        public async Task<IEnumerable<AvaliableHost>> GetAvaliableHosts(GetAvaliableHostsRequest model)
        {
            _validator.ValidateAndThrow(model);
            return await _hostRepository.GetAvaliableBetween(model.From, model.To);
        }
           
    }
}
