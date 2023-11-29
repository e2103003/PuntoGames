using Punto.Database;
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

        public ChoosePlayers(DatabaseUse database)
        {
            InitializeComponent();
            DataContext = this;
            this.database = database;

            database.LoadDatas();

            // Initialisez vos listes de jeux et de joueurs
            //Games = new ObservableCollection<Game>(database.GetGames());
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

        }
    }
}
