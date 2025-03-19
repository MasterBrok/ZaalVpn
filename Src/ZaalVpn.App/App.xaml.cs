using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using Microsoft.Extensions.DependencyInjection;
using ZaalVpn.App.Pages;
using ZaalVpn.App.Services;
using ZaalVpn.App.Windows;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace ZaalVpn.App;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{

    public IServiceProvider ServiceProvider { get; private set; }
    public static List<string> Flags;

    private Helper helper;

    public App()
    {
        this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        helper = new();
        helper.InstallTapDriver();
        this.Exit += OnExit;
        Flags = Assembly.GetExecutingAssembly().GetManifestResourceNames().Where(a => a.StartsWith("flag")).ToList();
    }

    private void OnExit(object sender, ExitEventArgs e)
    {
        Application.Current.Shutdown();
        notifyIcon.Dispose();
    }

    private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
        MessageBox.Show(e.Exception.Message);
        e.Handled = true;
    }
    protected override void OnExit(ExitEventArgs e)
    {
        if (mutex != null)
        {
            mutex.ReleaseMutex();
            mutex.Close();
        }

        base.OnExit(e);
    }



    private NotifyIcon notifyIcon;

    private void CreateNotifyIcon()
    {
        notifyIcon = new NotifyIcon();

        var stream = Application.GetResourceStream(new Uri("pack://application:,,,/ZaalVpn.App;component/zaal.ico")).Stream;

        notifyIcon.Icon = new Icon(stream);

        notifyIcon.Visible = true;

        //ContextMenu contextMenu = new ContextMenu();
        //contextMenu.MenuItems.Add("Open", ShowWindow);
        //contextMenu.MenuItems.Add("Exit", OnExit);

        ContextMenuStrip con = new();
        con.Items.Add("Open", null, ShowWindow);
        con.Items.Add("Exit", null, (sender, args) =>
        {
            Application.Current.Shutdown();
        });
        notifyIcon.ContextMenuStrip = con;

    }
    private void ShowWindow(object sender, EventArgs e)
    {
        (Application.Current.MainWindow).Show();
        (Application.Current.MainWindow).Activate();
        //this.Show();
        //this.WindowState = WindowState.Normal;
        //this.Activate();
    }

    private const string MutexName = "ZaalVpnHost";
    private static Mutex mutex;

    protected override void OnStartup(StartupEventArgs e)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        ServiceProvider = serviceCollection.BuildServiceProvider();

        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();


        bool createdNew;

        mutex = new Mutex(true, MutexName, out createdNew);

        if (!createdNew)
        {
            ActivateExistingWindow();
            Application.Current.Shutdown();
            return;
        }
        CreateNotifyIcon();


        base.OnStartup(e);
    }


    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);

    private void ActivateExistingWindow()
    {
        Process currentProcess = Process.GetCurrentProcess();
        Process existingProcess = Process.GetProcessesByName(currentProcess.ProcessName)
            .FirstOrDefault(p => p.Id != currentProcess.Id);

        if (existingProcess != null)
        {
            IntPtr hWnd = existingProcess.MainWindowHandle;
            if (hWnd != IntPtr.Zero)
            {
                SetForegroundWindow(hWnd);
            }
        }
    }


    private void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IApiService, ApiService>();

        // ثبت MainWindow و سایر صفحات
        services.AddTransient<MainWindow>();
        services.AddTransient<LoginPage>();
        services.AddTransient<MainPage>();
        services.AddTransient<SignUpPage>();
    }

    public static BitmapImage ConvertToImage(string fileName)
    {
        BitmapImage image = new();
        image.BeginInit();
        var stream = Application.GetResourceStream(new Uri($"pack://application:,,,/ZaalVpn.App;component/Resources/Countries/flag-{fileName}.png")).Stream;
        image.StreamSource = stream;
        image.CacheOption = BitmapCacheOption.OnLoad;
        image.EndInit();
        return image;
    }

    public const string Login = "api/Auth/Login";
    public const string Registration = "api/Auth/Registration";
    public const string Genders = "api/Gender/Genders";
    public const string Logout = "api/Auth/Logout";
    public const string Servers = "api/Vpn/Servers";
}

