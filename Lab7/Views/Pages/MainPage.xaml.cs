using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Lab7.Views.Pages;

public partial class MainPage : Page
{
    private readonly Frame _frame;
    private readonly Dictionary<string, Page> _pages;

    public MainPage(Frame frame, Dictionary<string, Page> pages)
    {
        InitializeComponent();

        _frame = frame;
        _pages = pages;
    }

    private void UndoButton_OnClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void CreateButton_OnClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void EditButton_OnClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void SaveButton_OnClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void FindButton_OnClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }
    
    private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void HyperlinkBooks_OnClick(object sender, RoutedEventArgs e)
    {
        _frame.Navigate(_pages["BooksPage"]);
    }

    private void HyperlinkPublishers_OnClick(object sender, RoutedEventArgs e)
    {
        _frame.Navigate(_pages["PublishersPage"]);
    }
}