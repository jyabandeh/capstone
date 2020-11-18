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
    /// Interaction logic for LibrarianMenuWindow.xaml
    /// </summary>
    public partial class LibrarianMenuWindow : Window
    {
        public LibrarianMenuWindow()
        {
            InitializeComponent();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CheckOutButton_Click(object sender, RoutedEventArgs e)
        {
            var cardholder = API.GetCardholder(this.CardNumberTextBox.Text);
            var book = API.GetBook(this.isbnCheckOutTextBox.Text);
            if(book != null)
            {
                if(API.GetNumberAvailableBookCopies(book) > 0)
                {
                    if (!API.IsAccountOverdue(cardholder))
                    {
                        API.CheckOutBook(book, cardholder);
                    }
                }
            }
        }

        private void CreateBookButton_Click(object sender, RoutedEventArgs e)
        {
            CreateUpdateBookWindow createUpdateBookWindow = new CreateUpdateBookWindow();
            createUpdateBookWindow.Show();
        }

        private void CheckInButton_Click(object sender, RoutedEventArgs e)
        {
            var cardholder = API.GetCardholder(this.CardNumberTextBox.Text);
            var book = API.GetBook(this.isbnCheckOutTextBox.Text);
            if (book != null)
            {
                API.CheckInBook(book, cardholder);
            }
        }

        private void SearchLibrarianButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = this.SearchLibrarianTextBox.Text.Trim();
            var books = API.FindBooks(searchText);
            this.SearchResultsLibrarianListBox.ItemsSource = books.Select(b => new { b.ISBN, Title = b.ISBN + " | " + b.Title }).ToList();
            this.SearchResultsLibrarianListBox.SelectedValuePath = "ISBN";
            this.SearchResultsLibrarianListBox.DisplayMemberPath = "Title";
        }

        private void SearchResultsLibrarianListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.SearchResultsLibrarianListBox.ItemsSource != null)
            {
                string isbn = this.SearchResultsLibrarianListBox.SelectedValue.ToString();
                var book = API.GetBook(isbn);
                if (book != null)
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

        private void ClearLibrarianButton_Click(object sender, RoutedEventArgs e)
        {
            this.SearchLibrarianTextBox.Text = null;
            this.SearchResultsLibrarianListBox.ItemsSource = null;
            this.IsbnTextBox.Text = null;
            this.TitleTextBox.Text = null;
            this.AuthorTextBox.Text = null;
            this.NumPagesTextBox.Text = null;
            this.SubjectTextBox.Text = null;
            this.PublisherTextBox.Text = null;
            this.YearPublishedTextBox.Text = null;
            this.LanguageTextBox.Text = null;
            this.NumCopiesTextBox.Text = null;
            this.DescriptionTextBox.Text = null;
            this.NumberAvailableTextBox.Text = null;
        }
    }
}
