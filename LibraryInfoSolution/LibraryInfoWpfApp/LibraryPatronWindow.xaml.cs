using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LibraryInfoWpfApp
{
    /// <summary>
    /// Interaction logic for LibraryPatronWindow.xaml
    /// </summary>
    public partial class LibraryPatronWindow : Window
    {
        public LibraryPatronWindow()
        {
            InitializeComponent();
        }

        private void ClosePatronButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SearchPatronButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = this.SearchPatronTextBox.Text.Trim();
            var books= API.FindBooks(searchText);
            this.SearchResultsPatronListBox.ItemsSource = books.Select(b => new { b.ISBN, Title = b.ISBN + " | " + b.Title }).ToList();
            this.SearchResultsPatronListBox.SelectedValuePath = "ISBN";
            this.SearchResultsPatronListBox.DisplayMemberPath = "Title";
        }

        private void SearchResultsPatronListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string isbn = this.SearchResultsPatronListBox.SelectedValue.ToString();
            var book = API.GetBook(isbn);
            if(book != null)
            {
                this.IsbnTextBox.Text = book.ISBN;
                this.TitleTextBox.Text = book.Title;
                this.AuthorTextBox.Text = $"{book.Author.Person.Firstname} {book.Author.Person.Lastname}";
                this.NumPagesTextBox.Text = book.NumberPages.ToString();
                this.SubjectTextBox.Text = book.Subject;
                this.PublisherTextBox.Text = book.Publisher;
                this.YearPublishedTextBox.Text = book.YearPublished;
                this.LanguageTextBox.Text = book.Language;
                this.NumCopiesTextBox.Text = book.NumberOfCopies.ToString();
                this.DescriptionTextBox.Text = book.Description;
                this.NumberAvailableTextBox.Text = API.GetNumberAvailableBookCopies(book).ToString();
            }
            else
            {
                MessageBox.Show("Book not found.");
            }
        }
    }
}
