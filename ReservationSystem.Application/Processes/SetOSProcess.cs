using System.Diagnostics;

namespace ReservationSystem.Application.Processes
{
    public class SetOSProcess : Process
    {

        public SetOSProcess(string osName)
        {
            StartInfo.FileName = @"cmd.exe";
            StartInfo.Arguments = $"/c mkdir {osName}";
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
