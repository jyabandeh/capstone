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
    /// Interaction logic for LibrarianLoginWindow.xaml
    /// </summary>
    public partial class LibrarianLoginWindow : Window
    {
        public LibrarianLoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LibrarianMenuWindow librarianMenuWindow = new LibrarianMenuWindow();
            librarianMenuWindow.Show();
            this.Close();
        }

        private void LoginCloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
