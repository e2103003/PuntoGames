using Punto.Database;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Punto
{
    /// <summary>
    /// Logique d'interaction pour TransferView.xaml
    /// </summary>
    public partial class TransferView : UserControl
    {
        
        public TransferView()
        {
            InitializeComponent();

            Source.ItemsSource = new List<string>() { "MySQL", "MongoDB", "SQLite" };
            Cible.ItemsSource = new List<string>() { "MySQL", "MongoDB", "SQLite" };
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string source = Source.SelectedItem.ToString();
            string cible = Cible.SelectedItem.ToString();

            if (source == cible)
            {
                MessageBox.Show("Les deux bases de données sont identiques");
            }
            else
            {

                DatabaseUse databaseSource = new DatabaseUse(source);
                DatabaseUse databaseCible = new DatabaseUse(cible);

                List<Player> playersSource = databaseSource.GetPlayers();
                List<Player> playersCible = databaseCible.GetPlayers();
                foreach (Player player in playersSource)
                {
                    // On compare les noms des joueurs
                    if (playersCible.Where(p => p.Name == player.Name).Count() == 0)
                    {
                        databaseCible.AddPlayer(player);
                    }
                    
                       

                }
                MessageBox.Show("Transfert effectué");
                MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
                mainWindow.Content = new PopUpBDD();



            }

        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.Content = new PopUpBDD();
        }
    }
}
