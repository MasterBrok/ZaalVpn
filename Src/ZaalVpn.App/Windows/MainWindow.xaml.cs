using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Extensions.DependencyInjection;
using ZaalVpn.App.Pages;
using ZaalVpn.ViewModel;

namespace ZaalVpn.App.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly IServiceProvider _provider;

        public MainWindow(IServiceProvider provider)
        {
            InitializeComponent();

            _provider = provider;
            var page = _provider.GetRequiredService<LoginPage>();
            Frame.NavigationService.Navigate(page);
        }

        private void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();

        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
        }

        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
           this.Close();
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = true;
            this.Hide();
        }


        private void Hide_OnClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
