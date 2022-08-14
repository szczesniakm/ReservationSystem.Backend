using System.Diagnostics;

namespace ReservationSystem.Application.Processes
{
    public class PowerOnProcess : Process
    {

        public PowerOnProcess(string hostName)
        {
            StartInfo.FileName = @"cmd.exe";
            StartInfo.Arguments = $"/c mkdir {hostName}";
            StartInfo.CreateNoWindow = true;
            StartInfo.UseShellExecute = false;
            StartInfo.RedirectStandardOutput = true;
            StartInfo.RedirectStandardError = true;
        }

        public async Task<int> RunAndReturnExitCode()
        {
            Start();
            await WaitForExitAsync();
            return ExitCode;
        }
    }
}
