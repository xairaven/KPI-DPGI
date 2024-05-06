using System.Windows;
using Lab7.Context;
using Lab7.Entities;
using Lab7.Utils;

namespace Lab7.Views.Forms;

public partial class CreateForm : Window
{
    private LibraryDbContext _dbContext;

    public CreateForm(LibraryDbContext dbContext)
    {
        InitializeComponent();

        _dbContext = dbContext;
    }

    private void OkButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (!IsValidationPassed()) return;

        var publisherFromForm = PubBox.Text.Trim();

        var publishers = _dbContext.Publishers;
        
        var publisher = publishers.FirstOrDefault(p => p.Name.Equals(publisherFromForm));
        if (publisher is null)
        {
            _dbContext.Add(new Publisher
            {
                Name = publisherFromForm
            });
            
            _dbContext.SaveChanges();
            
            publisher = publishers.FirstOrDefault(p => p.Name.Equals(publisherFromForm));
        }

        var publisherId = publisher!.Id;
        var publicationYear = short.Parse(PubYearBox.Text.Trim());

        _dbContext.Add(new Book
        {
            Isbn = ISBNBox.Text.Trim(),
            Title = TitleBox.Text.Trim(),
            Authors = AuthorsBox.Text.Trim(),
            PublisherCode = publisherId,
            PublicationYear = publicationYear
        });
        
        _dbContext.SaveChanges();
        
        // Load window

        MessageBox.Show(messageBoxText: "Success! Entry added.",
            caption: "Entry added.",
            button: MessageBoxButton.OK,
            icon: MessageBoxImage.Information,
            defaultResult: MessageBoxResult.OK);
        
        Close();
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