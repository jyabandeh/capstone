using LibraryInfoDataModelClassLibrary;
using System;
using System.Windows;

namespace LibraryInfoWpfApp
{
    class StartUp
    {
        [System.STAThreadAttribute]
        static void Main()
        {
            try
            {
                CreateAndInitializeDB.CreateAndInitializeDatabase();
                App.Main();
            }
            catch(Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
    }
}
