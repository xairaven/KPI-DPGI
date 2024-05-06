using System.Windows.Controls;
using Lab7.Context;

namespace Lab7.Views.Pages;

public partial class PublishersPage : Page
{
    public PublishersPage()
    {
        InitializeComponent();

        using var dbContext = new LibraryDbContext();
        ReloadGrid(dbContext);
    }

    public void ReloadGrid(LibraryDbContext dbContext)
    {
        PublishersGrid.DataContext = dbContext.Publishers.ToList();
    }
}