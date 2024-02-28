using System.Windows;
using Lab2.Views.Task1;

namespace Lab2.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Task1Grid(object sender, RoutedEventArgs e)
    {
        new GridTask().Show();
    }
    
    private void Task1StackPanel(object sender, RoutedEventArgs e)
    {
        new StackPanelTask().Show();
    }
}