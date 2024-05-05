using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
    
    private void UndoCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
        e.CanExecute = true;
    }
    private void UndoCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e) {
        
    }
    
    private void CreateCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
        e.CanExecute = true;
    }
    private void CreateCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e) {
        
    }
    
    private void EditCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
        e.CanExecute = true;
    }
    private void EditCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e) {
        
    }
    
    private void SaveCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
        e.CanExecute = true;
    }
    private void SaveCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e) {
        
    }
    
    private void FindCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
        e.CanExecute = true;
    }
    private void FindCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e) {
        
    }
    
    private void DeleteCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
        e.CanExecute = true;
    }
    private void DeleteCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e) {
        
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