using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Lab7.Context;

namespace Lab7.Views.Pages;

public partial class MainPage : Page
{
    private readonly Frame _frame;
    private readonly Dictionary<string, Page> _pages;

    public MainPage(Frame frame, Dictionary<string, Page> pages, LibraryDbContext dbContext)
    {
        InitializeComponent();

        _frame = frame;
        _pages = pages;
        
        InitializeJoinedTable(dbContext);
    }
    
    private void InitializeJoinedTable(LibraryDbContext dbContext)
    {
        var joined = dbContext.Books.Join(dbContext.Publishers,
            x => x.PublisherCode,
            y => y.Id,
            (x, y) => new
            {
                x.Isbn,
                x.Title,
                x.Authors,
                Publisher = y.Name,
                x.PublicationYear
            });

        JoinedGrid.DataContext = joined.ToList();
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