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
        private readonly AmtSettings _amtSettigns;
        private readonly IHubContext<HostsHub> _hostsHubContext;

        public HostsService(IHostRepository hostRepository, IOptions<AmtSettings> amtSettings, IHubContext<HostsHub> hostsHubContext)
        {
            _hostRepository = hostRepository;
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

            var (output, exitCode) = await CommandExecutionHelper.ExecuteAsync("meshcmd", $"AmtPower --poweron --host {request.HostName} --pass {_amtSettigns.Password}");

            if (exitCode != 0 || output.Trim() != "SUCCESS")
            {
                throw new ServiceException($"Błąd podczas uruchamiania hosta {request.HostName}");
            }

            UpdateHostsStatus();
        }

        private async Task PowerOff(string hostUrl)
        {
            var (output, exitCode) = await CommandExecutionHelper.ExecuteAsync("meshcmd", $"AmtPower --poweroff --host {hostUrl} --pass {_amtSettigns.Password}");
        }

        public async Task UpdateHostsStatus()
        {
            var hostsUrls = await GetAvaliableAmtHostUrls();
            var hosts = await GetUpdatedHosts(hostsUrls);

            await _hostRepository.UpdateHosts(hosts);
            await _hostsHubContext.Clients.All.SendAsync("update_hosts", hosts);
        }

        private async Task<List<string>> GetAvaliableAmtHostUrls()
        {
            var (output, exitCode) = await CommandExecutionHelper.ExecuteAsync("meshcmd", "amtscan --scan 10.146.225.0/24");
            Regex IPAd = new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
            return IPAd.Matches(output).Skip(1).Select(x => x.ToString()).ToList();
        }

        private async Task<List<Host>> GetUpdatedHosts(List<string> avaliableAmtHostUrls)
        {
            List<Host> hosts = new List<Host>();
            avaliableAmtHostUrls.ForEach(async x =>
            {
                var (output, exitCode) = await CommandExecutionHelper.ExecuteAsync("meshcmd", $"AmtPower --host {x} --pass {_amtSettigns.Password}");
                if (exitCode == 0)
                {
                    var status = GetStatusFromOutput(output);
                    if(status == HostStatus.DeepSleep)
                    {
                        await PowerOff(x);
                    }
                    hosts.Add(new Host(x, status));
                }
            });

            return hosts;
        }

        private static string GetStatusFromOutput(string output)
        {
            var status = output.Trim().Split(": ")[1];

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
