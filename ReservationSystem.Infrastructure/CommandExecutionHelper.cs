using System.Diagnostics;
using System.Text;

namespace ReservationSystem.Infrastructure
{
    public class CommandExecutionHelper
    {
        public static async Task<(string, int)> ExecuteAsync(string program, string args)
        {
            StringBuilder output = new StringBuilder();
            var isProcessing = false;

            var process = new Process
            {
                EnableRaisingEvents = true,
                StartInfo =
                {
                    FileName = program,
                    Arguments = args,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                }
            };

            process.OutputDataReceived += (obj, e) => output.AppendLine(e.Data);
            process.ErrorDataReceived += (obj, e) => output.AppendLine(e.Data);
            process.Exited += (obj, e) =>
            {
                ((Process)obj).WaitForExit();
                isProcessing = false;
            };
            
            isProcessing = true;
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            while (isProcessing)
            {
                Thread.Sleep(0);
            }

            var res = output.ToString();

            return (output.ToString(), process.ExitCode);
        }
    }
}
