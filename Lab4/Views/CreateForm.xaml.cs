using System.Windows;
using Lab4.Database;

namespace Lab4.Views;

public partial class CreateForm : Window
{
    private readonly MainWindow _window;
    private readonly AdoWrapper _wrapper;
    
    public CreateForm(MainWindow window)
    {
        _window = window;
        _wrapper = new AdoWrapper();
        
        InitializeComponent();
    }
    
    private void OkButton_OnClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
}