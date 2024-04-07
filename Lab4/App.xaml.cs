using Lab4.Database;

namespace Lab4;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    public App()
    {
        DbConnection.Start();
    }
}