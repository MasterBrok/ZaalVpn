using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Threading;
using Microsoft.Extensions.DependencyInjection;
using ZaalVpn.App.Services;
using ZaalVpn.ViewModel;

namespace ZaalVpn.App.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {


        private readonly IApiService _service;

        private readonly IServiceProvider _provider;

        public LoginPage(IApiService service, IServiceProvider provider)
        {

            InitializeComponent();
            InitializeAutoScroll();
            _service = service;
            _provider = provider;


            var userPass = Helper.GetPasswordFromRegistry();
            if (!string.IsNullOrEmpty(userPass.Item1))
            {
                txtUserName.Text = userPass.Item1;
                txtPassword.Text = userPass.Item2;
            }
        }

        private double currentScrollPosition = 0;
        private bool isDragging = false;
        private DispatcherTimer autoScrollTimer;

        private void Timer_Tick(object sender, EventArgs e)
        {
            svSliderImage.ScrollToHorizontalOffset(svSliderImage.HorizontalOffset + 5);
        }
        private void InitializeAutoScroll()
        {
            autoScrollTimer = new DispatcherTimer();
            autoScrollTimer.Interval = TimeSpan.FromMilliseconds(20);
            autoScrollTimer.Tick += AutoScrollTimer_Tick;
            autoScrollTimer.Start();
        }

        private void AutoScrollTimer_Tick(object sender, EventArgs e)
        {
            if (!isDragging)
            {
                currentScrollPosition += 0.5; // تنظیم سرعت اسکرول
                svSliderImage.ScrollToHorizontalOffset(currentScrollPosition);

                // بازنشانی موقعیت اسکرول هنگام رسیدن به انتها
                if (currentScrollPosition >= svSliderImage.ScrollableWidth)
                {
                    currentScrollPosition = 0;
                    svSliderImage.ScrollToHorizontalOffset(0);
                }
            }
        }

        // اختیاری: قابلیت کشیدن با ماوس
        private void svSliderImage_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
        }

        private void svSliderImage_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
        }

        private void svSliderImage_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                currentScrollPosition = svSliderImage.HorizontalOffset;
            }
        }

        private async void BtnLogin_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                btnLogin.IsEnabled = false;
                if (txtUserName.Text.Trim() == "" && txtPassword.Text.Trim() == "")
                    MessageBox.Show("UserName/Password is required");
                else
                {
                    var model = new LoginViewModel(txtUserName.Text.Trim(), txtPassword.Text.Trim());

                    var result = await _service.PostAsync(App.Login, model);
                    if (result.Success)
                    {
                        autoScrollTimer.Stop();
                        //Helper.DeleteSubKey();
                        Helper.SavePasswordToRegistry(txtUserName.Text.Trim(), txtPassword.Text.Trim());
                        var mainPage = _provider.GetRequiredService<MainPage>();
                        this.NavigationService.Navigate(mainPage);
                    }
                    else
                    {
                        MessageBox.Show(result.Messages[0]);
                        NavigationService.Refresh();
                    }
                }

                btnLogin.IsEnabled = true;

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            //string url = "https://t.me/CodeException";

            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = e.Uri.AbsoluteUri,
                    UseShellExecute = true 
                };

                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine("خطا در باز کردن مرورگر: " + ex.Message);
            }
            e.Handled = true;
        }

        private void Signup_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
           
        }

        private void BtnSignup_OnClick(object sender, RoutedEventArgs e)
        {
            var page = _provider.GetRequiredService<SignUpPage>();
            NavigationService.Navigate(page);
        }
    }
}
