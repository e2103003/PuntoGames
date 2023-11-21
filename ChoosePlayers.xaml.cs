﻿using Punto.Database;
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
        public ObservableCollection<Game> Games { get; set; }
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
    }
}