using System.Windows;
using Lab5.Context;
using Microsoft.EntityFrameworkCore;

namespace Lab5.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        using var dbContext = new LibraryDbContext();
        BooksGrid.DataContext = dbContext.Books.ToList();
        PublishersGrid.DataContext = dbContext.Publishers.ToList();

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
}