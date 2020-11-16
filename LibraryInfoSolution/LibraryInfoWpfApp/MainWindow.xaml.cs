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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibraryInfoWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //string isbn = "123456";
            //string title = "Test Book";
            //API api = new API();
            //int numPages = 100;            
            //string description = "description";
            //string publisher = "Publiching Co";
            //string yearPublished = "1999";
            //string language = "English";
            //int numCopies = 10;
            //string subject = "Math";
            //string firstname = "Jason";
            //string lastname = "Yabandeh";
            //string bio = "List of books";


            ////var author = api.CreateAuthor(firstname, lastname, bio);
            //var author = new Author()
            //{
            //    Bio = bio,
            //    Person = new Person()
            //    {
            //        Firstname = firstname,
            //        Lastname = lastname
            //    }
            //};
            //var book = api.CreateBook(isbn, title, author, numPages, subject, description, publisher, yearPublished, language, numCopies);
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void FindBookButton_Click(object sender, RoutedEventArgs e)
        {
            LibraryPatronWindow libraryPatronWindow = new LibraryPatronWindow();
            libraryPatronWindow.Show();
        }
             
        private void LibrarianLoginButton_Click(object sender, RoutedEventArgs e)
        {
            LibrarianLoginWindow librarianLoginWindow = new LibrarianLoginWindow();
            librarianLoginWindow.Show();
        }
    }
}
