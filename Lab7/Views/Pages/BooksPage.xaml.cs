using System.Windows.Controls;
using Lab7.Context;

namespace Lab7.Views.Pages;

public partial class BooksPage : Page
{
    public BooksPage()
    {
        InitializeComponent();
        
        using var dbContext = new LibraryDbContext();
        ReloadGrid(dbContext);
    }

    public void ReloadGrid(LibraryDbContext dbContext)
    {
        BooksGrid.DataContext = dbContext.Books.ToList();
    }
}