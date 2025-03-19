using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;
using ZaalVpn.App.Services;
using ZaalVpn.App.ViewModels;
using ZaalVpn.ViewModel;

namespace ZaalVpn.App.Pages
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private readonly IApiService _apiService;
        private readonly DispatcherTimer timer;
        private TimeSpan elapsedTime = TimeSpan.Zero;
        private bool isConnect;
        private bool isCancel = true;
        public MainPage(IApiService apiService)
        {
            _apiService = apiService;
            InitializeComponent();
            open.NotifyChanges += OpenOnNotifyChanges;
            open.NotifyErrorChanges += Open_NotifyErrorChanges;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;

        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            elapsedTime = elapsedTime.Add(TimeSpan.FromSeconds(1));
            tbTimer.Text = elapsedTime.ToString(@"hh\:mm\:ss");
        }

        private void Open_NotifyErrorChanges(object? sender, NotifyErrorChangeEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                tbErrors.Clear();
                MessageBox.Show(e.Message);
                tbErrors.Text += e.Message + "\n";
                open.Disconnect();
                ResetTimer();
                isCancel = true;
            });
        }


        private void OpenOnNotifyChanges(object? sender, NotifyChangeEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                isConnect = e.IsConnect;
                if (isConnect)
                {
                    tbState.Text = "Connected";
                    timer.Start();
                }
                tbErrors.Text += e.Message + "\n";
                btnConnect.Tag = e.IsConnect ? StateEnum.On : StateEnum.Off;
                tbErrors.ScrollToEnd();
            });

        }

        private List<ServerViewModel> _servers;
        private OpenVpnService open = new();

        private async void MainPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            var servers = await _apiService.GetAsync<List<ServerViewModel>, object>(App.Servers, null);

            if (servers.Success)
            {
                _servers = servers.Response;
                imgCountry.Source = App.ConvertToImage(_servers[0].Country);
                SetServer(_servers[0].Country);
                ChangeConfig();
            }
            else
            {
                MessageBox.Show(string.Join("\n", servers.Messages));
                MessageBox.Show($"HttpCode : {servers.HttpCode}");
                Application.Current.Shutdown();
            }
        }


        private int index = 0;

        private void PreviusServer_OnClick(object sender, RoutedEventArgs e)
        {
            if (index <= 0)
                index = _servers.Count - 1; // به آخرین سرور برمی‌گردد

            imgCountry.Source = App.ConvertToImage(_servers[index].Country);
            SetServer(_servers[index].Country);

            index--; // کاهش index بعد از استفاده
        }

        private void NextServer_OnClick(object sender, RoutedEventArgs e)
        {
            index++; // افزایش index اول

            if (index >= _servers.Count)
                index = 0; // به اولین سرور برمی‌گردد

            imgCountry.Source = App.ConvertToImage(_servers[index].Country);
            SetServer(_servers[index].Country);
        }

        private ServerViewModel currentServer;
        public void SetServer(string country)
        {
            currentServer = _servers.FirstOrDefault(a => a.Country == country);
            cIndex = 0;
        }


        private int cIndex = 0;
        public void ChangeConfig()
        {
            tbCongif.Text = currentServer.Configs[cIndex].City;

        }


        private void PreviousConfig_OnClick(object sender, RoutedEventArgs e)
        {
            if (cIndex == -1 || cIndex > currentServer.Configs.Count - 1)
                cIndex = currentServer.Configs.Count - 1;
            ChangeConfig();
            cIndex--;
        }

        private void NextConfig_OnClick(object sender, RoutedEventArgs e)
        {
            if (cIndex > currentServer.Configs.Count - 1 || cIndex == -1)
                cIndex = 0;
            ChangeConfig();
            cIndex++;
        }


        private async void BtnConnect_OnClick(object sender, RoutedEventArgs e)
        {
            ResetTimer();
            isCancel = !isCancel;

            if (isCancel)
                open.Disconnect();
            else
            {
                tbState.Text = "Connecting";
                await open.Connect(currentServer.Configs[cIndex].Config, "dev", "123");
            }


        }


        private void ResetTimer()
        {
            timer.Stop();
            tbErrors.Clear();
            isConnect = false;
            //isCancel = true;
            elapsedTime = TimeSpan.Zero;
            tbState.Text = "No Connected";
            tbTimer.Text = elapsedTime.ToString(@"hh\:mm\:ss");
        }

    }
}
