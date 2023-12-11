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

            if(database.databaseTechno == "Neo4j")
            {
                ButtonNeo4j.Visibility = Visibility.Visible;
            }
            else
            {
                ButtonNeo4j.Visibility = Visibility.Hidden;
                
            }

            this.database = database;
            this.playerList = new List<Player>();

            database.LoadDatasAsync();

            
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
            if (playerList.Count > 1 && playerList.Count <=4)
            {
                //on compare que les joueurs n'ont pas la même couleur
                bool sameColor = false;
                for (int i = 0; i < playerList.Count; i++)
                {
                    for (int j = i + 1; j < playerList.Count; j++)
                    {
                        if (playerList[i].Color == playerList[j].Color)
                        {
                            sameColor = true;
                        }
                    }
                }
                if (sameColor)
                {
                       MessageBox.Show("Vous ne pouvez pas avoir deux joueurs avec la même couleur !");
                    return;
                }
                else
                {
                    GameView gameView = new GameView(playerList, database);
                    this.Content = gameView;
                }
            }

                
            else
            {
                MessageBox.Show("Vous devez sélectionner au moins deux et maximum 4 joueurs pour lancer une partie !");
            }

        }

        private void ButtonNeo4j_Click(object sender, RoutedEventArgs e)
        {
            Neo4jBrowser neo4jBrowser = new Neo4jBrowser(database);
            this.Content = neo4jBrowser;

        }
    }
}
