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
            string connectionString = "Server=127.0.0.1;Database=PuntoDatabase;Uid=username;Pwd=password;";
            DatabaseUse.MySqlConnection = new MySqlConnection(connectionString);

            try
            {
                DatabaseUse.MySqlConnection.Open();



            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Erreur à la connexion a la BDD MySQL: {ex}");
                // Gérer les erreurs de connexion à la base de données ici.
            }

            Window mainWindow = Window.GetWindow(this);

            if (mainWindow != null)
            {
                // Fermez la fenêtre actuelle (PopUpBDD).
                ChooseGame chooseGameView = new ChooseGame();
                mainWindow.Content = chooseGameView;
            }
            else
            {
                MessageBox.Show("MainWindow null");
            }
        }

        private void MongoDB_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SQLite_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
