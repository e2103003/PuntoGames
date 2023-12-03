using MySql.Data.MySqlClient;
using System.Windows;
using System.Windows.Controls;

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

        private void Transfert_Click(object sender, RoutedEventArgs e)
        {
            Window mainWindow = Window.GetWindow(this);

            if (mainWindow != null)
            {
                // Fermez la fenêtre actuelle (PopUpBDD).
                TransferView transferView = new TransferView();
                mainWindow.Content = transferView;
            }
            else
            {
                MessageBox.Show("MainWindow null");
            }

        }
    }
}
