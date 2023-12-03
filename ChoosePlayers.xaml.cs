using Punto.Database;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Punto
{
    /// <summary>
    /// Logique d'interaction pour ChooseGame.xaml
    /// </summary>
    public partial class ChoosePlayers : UserControl
    {
        private DatabaseUse database;
        public ObservableCollection<Player> Players { get; set; }

        private List<Player> playerList;

        public ChoosePlayers(DatabaseUse database)
        {
            InitializeComponent();
            DataContext = this;
            this.database = database;
            this.playerList = new List<Player>();

            database.LoadDatas();

            
            Players = new ObservableCollection<Player>(database.GetPlayers());

        }

        private void ButtonAjouter_Click(object sender, RoutedEventArgs e)
        {
            ManagePlayer managePlayer = new ManagePlayer(null, database);
            this.Content = managePlayer;

        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            Player player = (Player)((Button)sender).Tag;
            ManagePlayer managePlayer = new ManagePlayer(player, database);
            this.Content = managePlayer;

        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            PopUpBDD popUpBDD = new PopUpBDD();
            this.Content = popUpBDD;
            var parentWindow = Window.GetWindow(this);

        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            bool check = (bool)((CheckBox)sender).IsChecked;
            var player = (Player)((CheckBox)sender).Tag;

            if (check)
            {
                playerList.Add(player);
            }
            else
            {
                playerList.Remove(player);
            }
        }

        private void ButtonLancerPartie_Click(object sender, RoutedEventArgs e)
        {
            if (playerList.Count > 1)
            {
                GameView gameView = new GameView(playerList, database);
                this.Content = gameView;
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner au moins deux joueurs pour lancer une partie !");
            }

        }
    }
}
