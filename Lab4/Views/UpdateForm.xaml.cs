using System.Data;
using System.Windows;
using Lab4.Database;

namespace Lab4.Views;

public partial class UpdateForm : Window
{
    private readonly string _initIsbn;
    private readonly MainWindow _window;
    private readonly AdoWrapper _wrapper;

    public UpdateForm(DataRowView row, MainWindow window)
    {
        _window = window;
        _wrapper = new AdoWrapper();
        
        InitializeComponent();

        _initIsbn = row["isbn"].ToString()!;

        ISBNBox.Text = _initIsbn;
        TitleBox.Text = row["title"].ToString()!;
        AuthorsBox.Text = row["authors"].ToString()!;
        PubBox.Text = row["publisher"].ToString()!;
        PubYearBox.Text = row["publication_year"].ToString()!;
    }

    private async void OkButton_OnClick(object sender, RoutedEventArgs e)
    {
        var isIsbnExist = !(await CheckIsbn(ISBNBox.Text.Trim()));
        
        if (!VerifyFields() || isIsbnExist || !CheckCastYear(PubYearBox.Text.Trim())) return;
        
        var query = @$"UPDATE Books
                        SET isbn = '{ISBNBox.Text.Trim()}',
                            title = '{TitleBox.Text.Trim()}',
                            authors = '{AuthorsBox.Text.Trim()}',
                            publisher = '{PubBox.Text.Trim()}',
                            publication_year = '{PubYearBox.Text.Trim()}'
                        WHERE isbn = '{_initIsbn}'";
        
        var affectedRows = await _wrapper.ExecuteNonQuery(query);
        
        MessageBox.Show(messageBoxText: $"Affected rows: {affectedRows}",
            caption: "Update operation results",
            button: MessageBoxButton.OK,
            icon: MessageBoxImage.Information,
            defaultResult: MessageBoxResult.OK);
        
        await _window.LoadListBox();
        
        Close();
    }

    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private bool VerifyFields()
    {
        if (!ISBNBox.Text.Trim().Equals("") &&
            !TitleBox.Text.Trim().Equals("") &&
            !AuthorsBox.Text.Trim().Equals("") &&
            !PubBox.Text.Trim().Equals("") &&
            !PubYearBox.Text.Trim().Equals("")) return true;
        
        
        MessageBox.Show(messageBoxText: "Some field is empty.",
            caption: "Error!",
            button: MessageBoxButton.OK,
            icon: MessageBoxImage.Error,
            defaultResult: MessageBoxResult.OK);

        return false;
    }

    private async Task<bool> CheckIsbn(string isbn)
    {
        const string query = "SELECT isbn FROM Books";

        var table = await _wrapper.ExecuteReader(query);
        
        foreach(DataRow row in table.Rows)
        {
            var tableIsbn = row["isbn"].ToString()!;

            if (isbn.Trim().Equals(_initIsbn.Trim()))
            {
                continue;
            }

            if (isbn.Trim().Equals(tableIsbn.Trim()))
            {
                MessageBox.Show(messageBoxText: "There's already some ISBN like this",
                    caption: "Error!",
                    button: MessageBoxButton.OK,
                    icon: MessageBoxImage.Error,
                    defaultResult: MessageBoxResult.OK);
                
                return false;
            }
        }

        return true;
    }

    private bool CheckCastYear(string year)
    {
        var canBeCasted = int.TryParse(year, out _);

        if (!canBeCasted)
        {
            MessageBox.Show(messageBoxText: "Something wrong with year",
                caption: "Error!",
                button: MessageBoxButton.OK,
                icon: MessageBoxImage.Error,
                defaultResult: MessageBoxResult.OK);
        }

        return canBeCasted;
    }
}