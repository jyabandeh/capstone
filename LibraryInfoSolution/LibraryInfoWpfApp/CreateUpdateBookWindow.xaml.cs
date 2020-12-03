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
            string isbn = IsbnTextBox.Text;
            string title = TitleTextBox.Text;
            int numberPages;
            int.TryParse(NumPagesTextBox.Text, out numberPages);
            string subject = SubjectTextBox.Text;
            string publisher = PublisherTextBox.Text;
            string yearPublished = YearPublishedTextBox.Text;
            string language = LanguageTextBox.Text;
            int numberCopies;
            int.TryParse(NumCopiesTextBox.Text, out numberCopies);
            string description = DescriptionTextBox.Text;
            Author author = API.GetAuthor(int.Parse(AuthorComboBox.SelectedValue.ToString()));
            var book = API.CreateBook(isbn, title, author, numberPages, subject, description, publisher, yearPublished, language, numberCopies);
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
            var book = API.GetBookByID(int.Parse(BookIdLabel.Content.ToString()));
            string isbn = IsbnTextBox.Text;
            string title = TitleTextBox.Text;
            int numberPages;
            int.TryParse(NumPagesTextBox.Text, out numberPages);
            string subject = SubjectTextBox.Text;
            string publisher = PublisherTextBox.Text;
            string yearPublished = YearPublishedTextBox.Text;
            string language = LanguageTextBox.Text;
            int numberCopies;
            int.TryParse(NumCopiesTextBox.Text, out numberCopies);
            string description = DescriptionTextBox.Text;
            Author author = API.GetAuthor(int.Parse(AuthorComboBox.SelectedValue.ToString()));
            book = API.UpdateBook(book, isbn, title, author, numberPages, subject, description, publisher, yearPublished, language, numberCopies);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
