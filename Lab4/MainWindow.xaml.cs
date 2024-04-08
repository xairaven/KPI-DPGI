using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Lab4.Database;

namespace Lab4;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        var wrapper = new AdoWrapper();

        const string query = "SELECT * FROM Books";

        var table = await wrapper.ExecuteReader(query);
        
        MainListBox.SelectedIndex = 0;
        MainListBox.Focus();
        MainListBox.DataContext = table;
    }

}