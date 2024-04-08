using System.Data;
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
    private readonly AdoWrapper _wrapper;
    
    public MainWindow()
    {
        InitializeComponent();
        
        _wrapper = new AdoWrapper();
    }

    private async void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        await LoadListBox();
    }

    private async Task LoadListBox()
    {
        const string query = "SELECT * FROM Books";

        var table = await _wrapper.ExecuteReader(query);
        
        MainListBox.SelectedIndex = 0;
        MainListBox.Focus();
        MainListBox.DataContext = table;
    }
    
    private void ButtonCreate_OnClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void ButtonUpdate_OnClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private async void ButtonDelete_OnClick(object sender, RoutedEventArgs e)
    {
        var selectedItem = MainListBox.SelectedItem as DataRowView;
        
        if (selectedItem is null)
        {
            MessageBox.Show(messageBoxText: "Select item if you want delete it, please.",
                caption: "Error!",
                button: MessageBoxButton.OK,
                icon: MessageBoxImage.Error,
                defaultResult: MessageBoxResult.OK);
            
            return;
        }
        
        var isbn = selectedItem["isbn"].ToString();

        var query = $"DELETE FROM Books WHERE isbn = {isbn}";
        var affectedRows = await _wrapper.ExecuteNonQuery(query);
        
        MessageBox.Show(messageBoxText: $"Affected rows: {affectedRows}",
            caption: "Delete operation results",
            button: MessageBoxButton.OK,
            icon: MessageBoxImage.Information,
            defaultResult: MessageBoxResult.OK);
        
       await LoadListBox();
    }
}