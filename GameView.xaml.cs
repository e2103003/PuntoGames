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
    /// Logique d'interaction pour GameView.xaml
    /// </summary>
    public partial class GameView : UserControl
    {

        private GameGridViewModel gameGridViewModel;

        private List<List<Card>> bord;

        public GameView(List<Player> players)
        //public GameView()
        {
            InitializeComponent();

            bord = new List<List<Card>>();

            int nombreLignes = 11;
            int nombreColonnes = 11;

            // Initialisation de la liste de listes
            for (int i = 0; i < nombreLignes; i++)
            {
                List<Card> ligne = new List<Card>();

                for (int j = 0; j < nombreColonnes; j++)
                {
                    // Initialiser chaque élément à null
                    ligne.Add(null);
                }

                bord.Add(ligne);
            }

            Card card = new Card();
            card.Color = "Blue";
            card.Number = 6;
            bord[3][4] = card;


            gameGridViewModel = new GameGridViewModel();
            UpdateGrid();
            DataContext = gameGridViewModel;
        }

        // Vous pouvez appeler cette méthode pour mettre à jour la grille visuelle
        public void UpdateGrid()
        {
            gameGridViewModel.UpdateGrid(bord);
        }
    }
}
