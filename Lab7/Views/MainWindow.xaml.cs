using System.Windows;
using System.Windows.Controls;
using Lab7.Context;
using Lab7.Views.Pages;

namespace Lab7.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly Dictionary<string, Page> _pages;
    
    public MainWindow()
    {
        InitializeComponent();

        _pages = new Dictionary<string, Page>();
        CreatePages();
        
        MainFrame.Navigate(_pages["MainPage"]);
    }

    private void CreatePages()
    {
        using var dbContext = new LibraryDbContext();
        
        _pages["MainPage"] = new MainPage(MainFrame, _pages, dbContext);
        _pages["BooksPage"] = new BooksPage(dbContext);
        _pages["PublishersPage"] = new PublishersPage(dbContext);
    }
}