using Punto.Database;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
            BaseGen.ItemsSource = new List<string>() { "MySQL", "MongoDB", "SQLite" };
        }



        private void ButtonTransfer_Click(object sender, RoutedEventArgs e)
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
        private void ButtonGeneration_Click(object sender, RoutedEventArgs e)
        {
            string baseGen = BaseGen.SelectedItem.ToString();
            DatabaseUse databaseGen = new DatabaseUse(baseGen);
            int nbJoueur = int.Parse(NbJoueurGen.Text);
            databaseGen.GenerateDatas(nbJoueur);
            MessageBox.Show("Génération effectuée");
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.Content = new PopUpBDD();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.Content = new PopUpBDD();
        }

        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private void NbJoueurGen_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);


        }
    }
}
