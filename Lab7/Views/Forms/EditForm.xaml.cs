using System.Windows;
using Lab7.Context;
using Lab7.Utils;

namespace Lab7.Views.Forms;

public partial class EditForm : Window
{
    private LibraryDbContext _dbContext;
    
    public EditForm(LibraryDbContext dbContext, string initIsbn)
    {
        InitializeComponent();

        _dbContext = dbContext;

        InitializeFields(initIsbn);
    }

    private void InitializeFields(string initIsbn)
    {
        var book = _dbContext.Books.Find(initIsbn);

        if (book is null)
        {
            MessageBox.Show(messageBoxText: "Please, reload page and then try to edit row.",
                caption: "Error!",
                button: MessageBoxButton.OK,
                icon: MessageBoxImage.Error,
                defaultResult: MessageBoxResult.OK);
            
            return;
        }

        var publisher = _dbContext.Publishers.Find(book.PublisherCode);
        if (publisher is null)
        {
            MessageBox.Show(messageBoxText: "Please, reload page and then try to edit row.",
                caption: "Error!",
                button: MessageBoxButton.OK,
                icon: MessageBoxImage.Error,
                defaultResult: MessageBoxResult.OK);
            
            return;
        }

        ISBNBox.Text = book.Isbn;
        TitleBox.Text = book.Title;
        AuthorsBox.Text = book.Authors;
        PubBox.Text = publisher.Name;
        PubYearBox.Text = book.PublicationYear.ToString() ?? string.Empty;
    }

    private void OkButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (!IsValidationPassed()) return;
        
        
    }

    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private bool IsValidationPassed()
    {
        var isIsbnExist = ValidateFields.IsbnExists(_dbContext, ISBNBox.Text.Trim());

        return !IsThereEmptyField() &&
               !isIsbnExist &&
               ValidateFields.CanCastYear(PubYearBox.Text.Trim());
    }

    private bool IsThereEmptyField()
    {
        return ValidateFields.IsEmpty(ISBNBox.Text) ||
               ValidateFields.IsEmpty(TitleBox.Text) ||
               ValidateFields.IsEmpty(AuthorsBox.Text) ||
               ValidateFields.IsEmpty(PubBox.Text) ||
               ValidateFields.IsEmpty(PubYearBox.Text);
    }
}