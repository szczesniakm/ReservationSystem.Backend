using ReservationSystem.Domain.Entities;
using ReservationSystem.Domain.Repositories;

namespace ReservationSystem.Application.Services
{
    public class HostsService
    {
        private readonly IHostRepository _hostRepository;

        public HostsService(IHostRepository hostRepository)
        {
            _hostRepository = hostRepository;
        }

        public async Task<GetAllHostsResponse> GetHosts()
        {
            return await _hostRepository.GetAll();
        }
           
    }
}
