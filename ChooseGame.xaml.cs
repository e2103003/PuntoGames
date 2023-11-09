using Punto.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour ChooseGame.xaml
    /// </summary>
    public partial class ChooseGame : UserControl
    {
        public ObservableCollection<Game> Games { get; set; }
        public ObservableCollection<Player> Players { get; set; }

        public ChooseGame(DatabaseUse database)
        {
            InitializeComponent();
            DataContext = this;

            // Initialisez vos listes de jeux et de joueurs
            Games = new ObservableCollection<Game>(database.GetGames());
            Players = new ObservableCollection<Player>(database.GetPlayers());



            // Chargez les jeux et les joueurs depuis votre base de données ici
            // Exemple: Games = Database.GetGames();
            //          Players = Database.GetPlayers();
        }
    }
}
