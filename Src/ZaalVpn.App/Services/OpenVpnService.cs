using System.Diagnostics;
using System.IO;
using ZaalVpn.App.ViewModels;

namespace ZaalVpn.App.Services
{
    public class OpenVpnService
    {
        private bool IsConnect;
        private int ProcessId;


        public event EventHandler<NotifyChangeEventArgs> NotifyChanges;
        public event EventHandler<NotifyErrorChangeEventArgs> NotifyErrorChanges;

        public void Disconnect()
        {
            var prc = Process.GetProcessById(ProcessId);
            //if (prc is null) return;
            if (prc is not null && prc.Id != 0)
            {
                prc.Kill();
                prc.WaitForExit();

            }
        }

        public async Task<bool> Connect(string configFile, string userName, string password)
        {
            //Disconnect();
            IsConnect = false;
            var LogFile = "Log.txt";
            // var appDirectory = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            var auth = userName + "\n" + password;
            await File.WriteAllTextAsync(Helper.AppDirectory + "OpenVpn\\profile.ovpn", configFile);

            try
            {

                using (Process process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = Helper.AppDirectory + "OpenVpn\\openvpn.exe",
                        Arguments = string.Format("--config " + Helper.AppDirectory +
                                                         "OpenVpn\\profile.ovpn" + " --auth-user-pass " +
                                                         Helper.AppDirectory +
                                                         "OpenVpn\\auth.txt"),
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        Verb = "runas"
                    }
                })
                {

                    process.OutputDataReceived += (sender, eventArgs) =>
                    {
                        if (!string.IsNullOrEmpty(eventArgs.Data))
                        {
                            File.AppendAllText("File.txt", eventArgs.Data);
                            if (eventArgs.Data.Contains("Initialization Sequence Completed"))
                            {
                                IsConnect = true;
                            }
                            if (eventArgs.Data.Contains("auth-failure") || eventArgs.Data.Contains("AUTH_FAILED"))
                            {
                                IsConnect = false;
                            }

                            if (eventArgs.Data.Contains("Connection reset, restarting [-1]"))
                            {
                                IsConnect = false;
                            }
                            NotifyChanges.Invoke(this, new NotifyChangeEventArgs(eventArgs.Data, IsConnect));

                        }
                    };
                    process.ErrorDataReceived += (sender, args) =>
                    {
                        if (!string.IsNullOrWhiteSpace(args.Data))
                            NotifyErrorChanges.Invoke(this, new(args.Data));
                    };

                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    ProcessId = process.Id;
                    await Task.Run(() =>
                    {
                        process.WaitForExit(TimeSpan.FromSeconds(50));
                    });
                }

                return IsConnect;
            }
            catch (Exception ex)
            {
                ProcessId = 0;
                IsConnect = false;
                return false;
            }
        }


        public void Dispose()
        {
            //Process.GetProcessById(ProcessId).Kill();
        }
    }
}
