using System.Configuration;
using System.Windows;

namespace Lab6;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public App()
    {
        var key = ConfigurationManager.AppSettings["SyncFusionKey"];
        
        //Register Syncfusion license
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(key);
    }
}