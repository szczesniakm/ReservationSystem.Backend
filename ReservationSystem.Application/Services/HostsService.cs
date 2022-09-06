using Microsoft.Extensions.Options;
using ReservationSystem.Application.Exceptions;
using ReservationSystem.Application.Models;
using ReservationSystem.Domain.Entities;
using ReservationSystem.Domain.Repositories;
using ReservationSystem.Infrastructure;
using ReservationSystem.Infrastructure.Settings;

namespace ReservationSystem.Application.Services
{
    public class HostsService
    {
        private readonly IHostRepository _hostRepository;
        private readonly AmtSettings _amtSettigns;

        public HostsService(IHostRepository hostRepository, IOptions<AmtSettings> amtSettings)
        {
            _hostRepository = hostRepository;
            _amtSettigns = amtSettings.Value;
        }

        public async Task<GetAllHostsResponse> GetHosts()
        {
            var hosts = await _hostRepository.GetAll();

            return new GetAllHostsResponse(
                hosts.Select(x => new GetAllHostsResponse.Host(x.Name, x.Status)).ToList());
        }

        public async Task PowerOn(PowerOnRequest request)
        {
            var host = await _hostRepository.Get(request.HostName);
            
            if(host is null)
            {
                throw new ServiceException($"Nie znaleziono hosta {request.HostName}.");
            }

            if(host.Status != HostStatus.PowerOff)
            {
                throw new ServiceException($"Host {request.HostName} nie może zostać uruchomiony. Status {host.Status}");
            }

            var (output, exitCode) = await CommandExecutionHelper.ExecuteAsync("meshcmd", $"AmtPower --poweron --host {request.HostName} --pass {_amtSettigns.Password}");

            if (exitCode != 0 || output.Trim() != "SUCCESS")
            {
                throw new ServiceException($"Błąd podczas uruchamiania hosta {request.HostName}");
            }
        }
           
    }
}
