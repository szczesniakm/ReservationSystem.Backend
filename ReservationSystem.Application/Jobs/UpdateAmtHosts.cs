﻿using Microsoft.Extensions.Options;
using Quartz;
using ReservationSystem.Domain.Entities;
using ReservationSystem.Domain.Repositories;
using ReservationSystem.Infrastructure;
using ReservationSystem.Infrastructure.Settings;
using System.Text.RegularExpressions;

namespace ReservationSystem.Application.Jobs
{
    internal class UpdateAmtHosts : IJob
    {
        private readonly IHostRepository _hostRepository;
        private readonly AmtSettings _amtSettigns;

        public UpdateAmtHosts(IHostRepository hostRepository, IOptions<AmtSettings> amtSettings)
        {
            _hostRepository = hostRepository;
            _amtSettigns = amtSettings.Value;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var hostsUrls = await GetAvaliableAmtHostUrls();
            var hosts = await GetUpdatedHosts(hostsUrls);

            await _hostRepository.UpdateHosts(hosts);
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
                    hosts.Add(new Host(x, status));
                }
            });

            return hosts;
        }

        private static string GetStatusFromOutput(string output)
        {
            var status = output.Split(": ")[1];

            switch (status)
            {
                case HostStatus.PowerOn:
                    return HostStatus.PowerOn;
                case HostStatus.PowerOff:
                    return HostStatus.PowerOff;
                default:
                    return HostStatus.Unknown;
            }
        }
    }
}
