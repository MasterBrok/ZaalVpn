using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ZaalVpn.App
{
    public class Helper
    {
        public static readonly string AppDirectory = AppDomain.CurrentDomain.BaseDirectory;

        public void InstallTapDriver()
        {
            var tapDriverExisted = true;
            var driverPath = GetDriverPath();
            try
            {

                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = driverPath + "\\tapinstall.exe",
                        Arguments = "drivernodes tap0901",
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        Verb = "runas"
                    }
                };

                process.OutputDataReceived += (sender, x) =>
                {
                    if (!string.IsNullOrEmpty(x.Data))
                    {
                        File.AppendAllText("Log.txt", x.Data + "\n\n");
                        if (x.Data.Contains("No matching devices found."))
                            tapDriverExisted = false;
                    }
                };

                process.ErrorDataReceived += (sender, x) =>
                {
                    tapDriverExisted = false;
                    //    if (!string.IsNullOrEmpty(x.Data))
                    //        Console.WriteLine($"\nERROR: {x.Data}");
                };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();

                if (!tapDriverExisted)
                {
                    //MessageBox.Show("درایور مورد نیاز نصب نیست");
                    var process2 = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = driverPath + "\\tapinstall.exe",
                            Arguments = "install " + driverPath + "\\OemVista.inf tap0901",
                            RedirectStandardInput = true,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            UseShellExecute = false,
                            CreateNoWindow = true,
                            Verb = "runas"
                        }
                    };

                    process2.OutputDataReceived += (sender, x) =>
                    {
                        if (!string.IsNullOrEmpty(x.Data))
                            File.AppendAllText("Log2.txt", x.Data + "\n\n");

                    };

                    process2.ErrorDataReceived += (sender, x) =>
                    {
                        if (!string.IsNullOrEmpty(x.Data))
                            File.AppendAllText("Lo3.txt", x.Data + "\n\n");

                    };

                    process2.Start();
                    process2.BeginOutputReadLine();
                    process2.BeginErrorReadLine();
                    process2.WaitForExit(10000);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private string appDirectory = AppDomain.CurrentDomain.BaseDirectory + "OpenVpn\\";
        private string GetDriverPath()
        {
            string selectedFolder = "";

            if (Environment.Is64BitOperatingSystem)
            {
                if (Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Drivers", "64x")))
                    selectedFolder = "64x";
            }
            else
            {
                selectedFolder = Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Drivers", "86x")) ? "86x" : "32";
            }

            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Drivers", selectedFolder);
            return fullPath;
        }


        private static string _key = @"SOFTWARE\ZaalVpn";
        public static bool SavePasswordToRegistry(string username, string password)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(_key);

            key.SetValue("UserName", username);
            key.SetValue("Password", password);
            key.Close();
            return true;

        }
        public static (string, string) GetPasswordFromRegistry()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(_key);

            if (key != null)
            {
                string username = key.GetValue("Username")?.ToString();
                string password = key.GetValue("Password")?.ToString();
                key.Close();
                return (username, password);
            }

            return (string.Empty, string.Empty);
        }

        public static void DeleteSubKey()
        {
            Registry.CurrentUser.DeleteSubKey(_key);
        }

    }
}
