using System.Windows;

namespace Lab4.Views;

public partial class UpdateForm : Window
{
    public UpdateForm()
    {
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