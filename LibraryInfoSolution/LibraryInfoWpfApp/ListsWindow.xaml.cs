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
    /// Interaction logic for ListsWindow.xaml
    /// </summary>
    public partial class ListsWindow : Window
    {
        public ListsWindow()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DisplayLibrarians();
            DisplayCardholders();
            DisplayAuthors();
            DisplayOverdueBooks();
        }

        private void DisplayLibrarians()
        {
            var sbLibrarians = new StringBuilder();
            var librarianList = API.GetAllLibrarians();
            foreach (var librarian in librarianList)
            {
                sbLibrarians.Append(librarian.ToString() + "\n------------------\n");
            }
            LibrariansTextBox.Text = sbLibrarians.ToString().Trim();
        }

        private void DisplayCardholders()
        {
            var sbCardholders = new StringBuilder();
            var cardholdersList = API.GetAllCardholders();
            foreach (var cardholder in cardholdersList)
            {
                sbCardholders.Append(cardholder.ToString() + "\n");
                var checkoutLogList = API.GetCheckOutLogByCardholder(cardholder);
                if (checkoutLogList.Count == 0)
                    sbCardholders.Append("\nCardholder has no books checked out");
                else
                {
                    foreach (var log in checkoutLogList)
                    {
                        sbCardholders.Append("\n" + log.Book.ToString() + "\n");
                        sbCardholders.Append(log.ToString() + "\n");
                        if (API.IsBookOverdue(log))
                            sbCardholders.Append("**OVERDUE**\n");
                    }
                }
                sbCardholders.Append("--------------------\n");
            }
            CardholdersTextBox.Text = sbCardholders.ToString().Trim();
        }

        private void DisplayAuthors()
        {
            var sbAuthors = new StringBuilder();
            var authorsList = API.GetAuthors();
            foreach (var author in authorsList)
            {
                sbAuthors.Append(author.ToString() + "\n");
                var authorBooksList = API.GetBooksByAuthor(author);
                if (authorBooksList.Count == 0)
                    sbAuthors.Append("\nAuthor has no associated books\n");
                else
                {
                    foreach (var book in authorBooksList)
                    {
                        sbAuthors.Append("\n" + book.ToString() + "\n");
                        if (book.Publisher != null)
                            sbAuthors.Append($"{book.Publisher}\n");
                    }
                }
                sbAuthors.Append("---------------------------\n");
            }
            AuthorsTextBox.Text = sbAuthors.ToString().Trim();
        }

        private void DisplayOverdueBooks()
        {
            var sbOverdue = new StringBuilder();
            var overdueList = API.GetOverdueCheckoutLogs();
            foreach(var log in overdueList)
            {
                sbOverdue.Append(log.Book.ToString() + "\n");
                sbOverdue.Append(log.ToString() + "\n");
                sbOverdue.Append("\n" + log.Cardholder.ToString() + "\n");
                sbOverdue.Append("------------------\n");
            }
            OverdueBooksTextBox.Text = sbOverdue.ToString().Trim();
        }
    }
}
