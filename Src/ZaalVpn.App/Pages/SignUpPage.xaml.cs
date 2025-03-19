using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Extensions.DependencyInjection;
using ZaalVpn.App.Services;
using ZaalVpn.ViewModel;

namespace ZaalVpn.App.Pages
{
    /// <summary>
    /// Interaction logic for SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Page
    {
        private readonly IApiService _api;

        private readonly IServiceProvider _provider;
        private LoginPage page;

        public SignUpPage(IApiService api, IServiceProvider provider)
        {
            _api = api;
            _provider = provider;
            InitializeComponent();
            Loaded+=OnLoaded;
            page = _provider.GetRequiredService<LoginPage>();
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            var genders = await _api.GetAsync<Dictionary<string, string>,object>(App.Genders, null);
            vbGenders.ItemsSource = genders.Response;
        }


        private async void BtnLogin_OnClick(object sender, RoutedEventArgs e)
        {
            string emailPattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            string usernamePattern = @"^[a-zA-Z0-9]+$";

            if (!Regex.IsMatch(txtEmail.Text, emailPattern) || string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Valid Email Address.");
                return;
            }
            else if (!Regex.IsMatch(txtUserName.Text, usernamePattern) || string.IsNullOrEmpty(txtUserName.Text))
            {
                MessageBox.Show("Valid Username.");
                return;
            }
            else if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Valid Password.");
                return;

            }
            if (vbGenders.SelectedValue is null)
            {
                MessageBox.Show("Select a Gender.");
                return;
            }
            var model = new CreateAccountViewModel();
            model.Email = txtEmail.Text.Trim();
            model.Password = txtPassword.Text.Trim();
            model.UserName = txtUserName.Text.Trim();
            model.GenderId = vbGenders.SelectedValue.ToString();
            var res = await _api.PostAsync(App.Registration, model);
            MessageBox.Show(res.Messages[0]);
            if(res.Success)
                NavigationService.Navigate(page);

        }

        private void Back_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(page);
        }
    }
}
