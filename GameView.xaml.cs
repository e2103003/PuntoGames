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


        private List<List<Card>> bord;




        public GameView(List<Player> players)
        //public GameView()
        {
            InitializeComponent();

            bord = new List<List<Card>>();

            int nombreLignes = 11;
            int nombreColonnes = 11;



            // Initialisation de grille de jeu
            for (int i = 0; i < nombreLignes; ++i)
            {
                bord.Add(new List<Card>());
                for (int j = 0; j < nombreColonnes; ++j)
                {
                    bord[i].Add(null);
                }
            }


            Card card = new Card();
            card.Color = "Blue";
            card.Number = 6;
            bord[1][2] = card;

            card = new Card();
            card.Color = "Red";
            card.Number = 3;
            bord[0][2] = card;


            card = new Card();
            card.Color = "Yellow";
            card.Number = 9;
            bord[10][7] = card;


            UpdateGrid();


        }

        // Vous pouvez appeler cette méthode pour mettre à jour la grille visuelle
        public void UpdateGrid()
        {
            for (int i = 0; i < bord[0].Count; ++i)
            {
                for (int j = 0; j < bord.Count; ++j)
                {
                    Button b = new Button();

                    
                    

                    if (bord[i][j] != null)
                    {
                        b.Content = bord[i][j].Number;
                        b.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(bord[i][j].Color));
                    }
                    else
                    {
                        b.Content = "";
                        b.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("White"));
                    }
                    b.Height = 47;
                    b.Width = 47;

                    b.BorderThickness = new Thickness(3.5);
                    b.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("White"));
                    b.Click += new RoutedEventHandler(Button_Click);
                    // modifie la couleur de la couleur du boutton pendant le survol de la souris
                    Grid.SetRow(b, i);
                    Grid.SetColumn(b, j);
                    BoardGrid.Children.Add(b);

                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }

    }
