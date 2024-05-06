using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Lab7.Context;
using Lab7.Repositories;
using Lab7.Views.Forms;

namespace Lab7.Views.Pages;

public partial class MainPage : Page, IDisposable
{
    private readonly Frame _frame;
    private readonly Dictionary<string, Page> _pages;
    private readonly LibraryDbContext _dbContext;

    public MainPage(Frame frame, Dictionary<string, Page> pages)
    {
        InitializeComponent();

        _frame = frame;
        _pages = pages;
        _dbContext = new LibraryDbContext();
        
        InitializeJoinedTable();
    }
    
    private void InitializeJoinedTable()
    {
        var joined = _dbContext.Books.Join(_dbContext.Publishers,
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
        new CreateForm(_dbContext).Show();
    }
    
    private void EditCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = true;
    }
    private void EditCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        var item = JoinedGrid.SelectedItem;
        
        if (item is null)
        {
            MessageBox.Show(messageBoxText: "Choose some row if you want to edit it!",
                caption: "Error",
                button: MessageBoxButton.OK,
                icon: MessageBoxImage.Error,
                defaultResult: MessageBoxResult.OK);

            return;
        }

        dynamic dynamicItem = item;
        var isbn = (string)dynamicItem.Isbn;
        
        new EditForm(_dbContext, isbn).Show();
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
    
    private void ReloadCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = true;
    }
    private void ReloadCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        InitializeJoinedTable();
    }
    
    
    private void DeleteCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
        e.CanExecute = true;
    }
    private void DeleteCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e) {
        var item = JoinedGrid.SelectedItem;
        
        if (item is null)
        {
            MessageBox.Show(messageBoxText: "Choose some row if you want to edit it!",
                caption: "Error",
                button: MessageBoxButton.OK,
                icon: MessageBoxImage.Error,
                defaultResult: MessageBoxResult.OK);

            return;
        }

        dynamic dynamicItem = item;
        var isbn = (string)dynamicItem.Isbn;

        try
        {
            new BooksRepository(_dbContext).Delete(isbn);
        }
        catch (Exception exception)
        {
            MessageBox.Show(messageBoxText: exception.Message,
                caption: "Error",
                button: MessageBoxButton.OK,
                icon: MessageBoxImage.Error,
                defaultResult: MessageBoxResult.OK);
        }
        
        ReloadCommandBinding_Executed(null!, null!);
    }
    

    private void HyperlinkBooks_OnClick(object sender, RoutedEventArgs e)
    {
        _frame.Navigate(_pages["BooksPage"]);
        
        var page = (BooksPage) _pages["BooksPage"];
        page.ReloadGrid(_dbContext);
    }

    private void HyperlinkPublishers_OnClick(object sender, RoutedEventArgs e)
    {
        _frame.Navigate(_pages["PublishersPage"]);
        
        var page = (PublishersPage) _pages["PublishersPage"];
        page.ReloadGrid(_dbContext);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}