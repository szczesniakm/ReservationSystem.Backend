using System.Diagnostics;

namespace ReservationSystem.Application.Processes
{
    public class PowerOffProcess : Process
    {

        public PowerOffProcess(string hostName)
        {
            StartInfo.FileName = @"cmd.exe";
            StartInfo.Arguments = $"/c rmdir {hostName} Linux";
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
