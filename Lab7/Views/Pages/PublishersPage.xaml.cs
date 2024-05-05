using System.Windows.Controls;
using Lab7.Context;

namespace Lab7.Views.Pages;

public partial class PublishersPage : Page
{
    public PublishersPage(LibraryDbContext dbContext)
    {
        InitializeComponent();
        
        PublishersGrid.DataContext = dbContext.Publishers.ToList();
    }
}