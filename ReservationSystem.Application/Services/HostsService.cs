using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using ReservationSystem.Application.Exceptions;
using ReservationSystem.Application.Models;
using ReservationSystem.Domain.Entities;
using ReservationSystem.Domain.Repositories;
using ReservationSystem.Infrastructure;
using ReservationSystem.Infrastructure.Hubs;
using ReservationSystem.Infrastructure.Settings;
using System.Text.RegularExpressions;

namespace ReservationSystem.Application.Services
{
    public class HostsService
    {
        private readonly IHostRepository _hostRepository;
        private readonly IOSRepository _osRepository;
        private readonly AmtSettings _amtSettigns;
        private readonly IHubContext<HostsHub> _hostsHubContext;

        public HostsService(IHostRepository hostRepository, IOSRepository osRepository, IOptions<AmtSettings> amtSettings, IHubContext<HostsHub> hostsHubContext)
        {
            _hostRepository = hostRepository;
            _osRepository = osRepository;
            _amtSettigns = amtSettings.Value;
            _hostsHubContext = hostsHubContext;
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

            var os = await _osRepository.Get(request.OsName);
            if(os is null)
            {
                throw new ServiceException($"Nie znaleziono systemu {request.OsName}.");
            }

            var setSystemCommand = _amtSettigns.SetSystemCommand.Replace("{os}", request.OsName).Replace("{hostname}", request.HostName);

            await CommandExecutionHelper.ExecuteAsync("sh", $"-c {setSystemCommand}");

            var (output, exitCode) = await CommandExecutionHelper.ExecuteAsync("meshcmd", $"AmtPower --poweron --host {request.HostName} --pass {_amtSettigns.Password}");

            if (exitCode != 0 || output.Trim() != "SUCCESS")
            {
                throw new ServiceException($"Błąd podczas uruchamiania hosta {request.HostName}");
            }

            host.SetStatus(HostStatus.PowerOn);
            await _hostRepository.UpdateHost(host);
            await SendHostsToClients();
        }

        public async Task UpdateHostsStatus()
        {
            var hosts = await _hostRepository.GetAll();

            foreach(var host in hosts)
            {
                var updatedStatus = await GetUpdatedHostStatus(host.Name);
                host.SetStatus(updatedStatus);
            }

            await _hostRepository.UpdateHosts(hosts);
            await SendHostsToClients();
        }

        private async Task SendHostsToClients()
        {
            var hosts = await _hostRepository.GetAll();
            await _hostsHubContext.Clients.All.SendAsync("update_hosts", hosts);
        }

        private async Task<string> GetUpdatedHostStatus(string hostName)
        {
            var (output, exitCode) = await CommandExecutionHelper.ExecuteAsync("meshcmd", $"AmtPower --host {hostName} --pass {_amtSettigns.Password}");
            return GetStatusFromOutput(output);
        }

        private static string GetStatusFromOutput(string output)
        {
            var status = "Unknown";
            try
            {
                status = output.Trim().Split(": ")[1];
            }
            catch { }

            switch (status)
            {
                case HostStatus.PowerOn:
                    return HostStatus.PowerOn;
                case HostStatus.PowerOff:
                    return HostStatus.PowerOff;
                case HostStatus.DeepSleep:
                    return HostStatus.PowerOff;
                default:
                    return HostStatus.Unknown;
            }
        }
    }
}
