using LibraryInfoDataModelClassLibrary;
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
    /// Interaction logic for CreateUpdateBookWindow.xaml
    /// </summary>
    public partial class CreateUpdateBookWindow : Window
    {        
        public CreateUpdateBookWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Will only run this code if user is adding a new book rather than updating a selected book
            if(this.AddNewBookButton.IsVisible)
                DisplayAuthorsList();
        }

        private void AddNewBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (AreFieldsValid())
            {
                string isbn = IsbnTextBox.Text.Trim();
                string title = TitleTextBox.Text.Trim();
                int numberPages;
                int.TryParse(NumPagesTextBox.Text.Trim(), out numberPages);
                string subject = SubjectTextBox.Text.Trim();
                string publisher = PublisherTextBox.Text.Trim();
                string yearPublished = YearPublishedTextBox.Text.Trim();
                string language = LanguageTextBox.Text.Trim();
                int numberCopies;
                int.TryParse(NumCopiesTextBox.Text.Trim(), out numberCopies);
                string description = DescriptionTextBox.Text.Trim();
                Author author = API.GetAuthor(int.Parse(AuthorComboBox.SelectedValue.ToString()));
                var book = API.CreateBook(isbn, title, author, numberPages, subject, description, publisher, yearPublished, language, numberCopies);
                if (book != null)
                    MessageBox.Show($"Added '{book.Title}' with Book ID {book.BookID}");
            }
        }

        private void CreateAuthorButton_Click(object sender, RoutedEventArgs e)
        {
            CreateAuthorWindow createAuthorWindow = new CreateAuthorWindow();
            createAuthorWindow.Show();
            
        }

        public void DisplayAuthorsList()
        {
            var authors = API.GetAuthors();
            this.AuthorComboBox.ItemsSource = authors.Select(a => new { a.ID, Name = $"{a.Person.Firstname} {a.Person.Lastname}" }).ToList();
            this.AuthorComboBox.DisplayMemberPath = "Name";
            this.AuthorComboBox.SelectedValuePath = "ID";
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            DisplayAuthorsList();
        }

        private void UpdateBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (AreFieldsValid())
            {
                var book = API.GetBookByID(int.Parse(BookIdLabel.Content.ToString()));
                string isbn = IsbnTextBox.Text.Trim();
                string title = TitleTextBox.Text.Trim();
                int numberPages;
                int.TryParse(NumPagesTextBox.Text.Trim(), out numberPages);
                string subject = SubjectTextBox.Text.Trim();
                string publisher = PublisherTextBox.Text.Trim();
                string yearPublished = YearPublishedTextBox.Text.Trim();
                string language = LanguageTextBox.Text.Trim();
                int numberCopies;
                int.TryParse(NumCopiesTextBox.Text.Trim(), out numberCopies);
                string description = DescriptionTextBox.Text.Trim();
                Author author = API.GetAuthor(int.Parse(AuthorComboBox.SelectedValue.ToString()));
                book = API.UpdateBook(book, isbn, title, author, numberPages, subject, description, publisher, yearPublished, language, numberCopies);
                if (book != null)
                    MessageBox.Show($"Book ID {book.BookID} has been updated.");
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool AreFieldsValid()
        {
            string isbn = IsbnTextBox.Text.Trim();
            string title = TitleTextBox.Text.Trim();            
            string pages = NumPagesTextBox.Text.Trim();
            string copies = NumCopiesTextBox.Text.Trim();

            if (string.IsNullOrEmpty(isbn) ||
                string.IsNullOrEmpty(title) ||
                string.IsNullOrEmpty(copies) ||
                AuthorComboBox.SelectedValue == null)
            {
                MessageBox.Show("Please fill out all required (*) fields.");
                return false;
            }

            int copiesInt;
            if (!int.TryParse(copies, out copiesInt))   
            {
                MessageBox.Show("Number of copies must be an integer.");
                return false;
            }
            if(copiesInt < 0)
            {
                MessageBox.Show("Number of copies must be positive.");
                return false;
            }

            int pagesInt;
            if (!string.IsNullOrEmpty(pages))
            {
                if(!int.TryParse(pages, out pagesInt))
                {
                    MessageBox.Show("Number of pages must be an integer.");
                    return false;
                }
                if(pagesInt < 0)
                {
                    MessageBox.Show("Number of pages must be positive.");
                    return false;
                }
            }

            return true;

        }
    }
}
