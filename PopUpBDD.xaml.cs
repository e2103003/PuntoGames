using MySql.Data.MySqlClient;
using Punto.Database;
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

namespace Punto
{
    /// <summary>
    /// Logique d'interaction pour PopUpBDD.xaml
    /// </summary>
    public partial class PopUpBDD : UserControl
    {
        public PopUpBDD()
        {
            InitializeComponent();
        }

        private void MySQL_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DatabaseUse database = new DatabaseUse("MySQL");
                ChangeView(database);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Erreur à la connexion a la BDD MySQL: {ex}");
            }            
        }

        private void MongoDB_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DatabaseUse database = new DatabaseUse("MongoDB");
                ChangeView(database);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Erreur à la connexion a la BDD MongoDB: {ex}");
            }
        }

        private void SQLite_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DatabaseUse database = new DatabaseUse("SQLite");
                ChangeView(database);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Erreur à la connexion a la BDD SQLite: {ex}");
            }

        }

        private void ChangeView(DatabaseUse database)
        {
            Window mainWindow = Window.GetWindow(this);

            if (mainWindow != null)
            {
                // Fermez la fenêtre actuelle (PopUpBDD).
                ChoosePlayers choosePlayersView = new ChoosePlayers(database);
                mainWindow.Content = choosePlayersView;
            }
            else
            {
                MessageBox.Show("MainWindow null");
            }
        }

    }
}
