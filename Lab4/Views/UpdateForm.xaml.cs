using System.Data;
using System.Windows;

namespace Lab4.Views;

public partial class UpdateForm : Window
{
    public UpdateForm(DataRowView row)
    {
        InitializeComponent();

        ISBNBox.Text = row["isbn"].ToString()!;
        TitleBox.Text = row["title"].ToString()!;
        AuthorsBox.Text = row["authors"].ToString()!;
        PubBox.Text = row["publisher"].ToString()!;
        PubYearBox.Text = row["publication_year"].ToString()!;
    }

    private void OkButton_OnClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
}