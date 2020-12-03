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
    /// Interaction logic for CreateAuthorWindow.xaml
    /// </summary>
    public partial class CreateAuthorWindow : Window
    {
        public CreateAuthorWindow()
        {
            InitializeComponent();
        }

        private void CreateAuthorButton_Click(object sender, RoutedEventArgs e)
        {
            string firstname = this.FirstNameTextBox.Text;
            string lastname = this.LastNameTextBox.Text;
            string bio = this.BioTextBox.Text;
            var author = API.CreateAuthor(firstname, lastname, bio);
            if (author != null)
                MessageBox.Show($"{firstname} {lastname} has been added as an author.");
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();            
        }
    }
}
