using ReservationSystem.Application.Models;
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
            var hosts = await _hostRepository.GetAll();

            return new GetAllHostsResponse(
                hosts.Select(x => new GetAllHostsResponse.Host(x.Name, x.Status)).ToList());
        }
           
    }
}
