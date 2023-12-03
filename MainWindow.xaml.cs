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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ShowPopUpBDD(); // Appelez la méthode pour afficher le UserControl PopUpBDD.
        }

        private void ShowPopUpBDD()
        {
            // Affichaeg de la première vue - le choix de la base de données
            PopUpBDD popUpBDD = new PopUpBDD();

            List<Player> players = new List<Player>();
            players.Add(new Player() { Name = "Joueur 1", Color = "Red" });
            players.Add(new Player() { Name = "Joueur 2", Color = "Blue" });

            //DatabaseUse database = new DatabaseUse("MySQL");

            //GameView gameView = new GameView(players, database);


            // Affichez le UserControl dans la fenêtre principale.
            this.Content = popUpBDD;

        }
    }
}
