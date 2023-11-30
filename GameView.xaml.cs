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


        public List<List<Cell>> bord;

        private Game game;

        private DatabaseUse database;


        public GameView(List<Player> players, DatabaseUse database)
        //public GameView()
        {
            this.database = database;
            InitializeComponent();

            bord = new List<List<Cell>>();

            int nombreLignes = 11;
            int nombreColonnes = 11;



            // Initialisation de grille de jeu
            for (int i = 0; i < nombreLignes; ++i)
            {
                bord.Add(new List<Cell>());
                for (int j = 0; j < nombreColonnes; ++j)
                {
                    bord[i].Add(null);
                    
                }
            }

            // D'après les regles du jeu, la première carte doit être placée au milieu de la grille
            Cell cellule = new Cell();
            cellule.IsPlayable = true;
            bord[5][5] = cellule;



            Card card = new Card();
            card.Color = "Blue";
            card.Number = 6;

            Cell cell = new Cell();
            cell.Card = card;
            cell.IsPlayable = true;

            bord[1][2] = cell;

            card = new Card();
            card.Color = "Red";
            card.Number = 3;

            Cell cell2 = new Cell();
            cell2.Card = card;
            cell2.IsPlayable = true;
            bord[2][3] = cell2;




            card = new Card();
            card.Color = "Yellow";
            card.Number = 9;
            Cell cell3 = new Cell();
            cell3.Card = card;
            cell3.IsPlayable = true;
            bord[3][4] = cell3;





            UpdateGrid();

            game = new Game(players, this);


        }

        
        public void UpdateGrid()
        {
            for (int i = 0; i < bord[0].Count; ++i)
            {
                for (int j = 0; j < bord.Count; ++j)
                {
                    Button b = new Button();

                    if (bord[i][j] != null && bord[i][j].Card != null)
                    {
                        b.Content = bord[i][j].Card.Number;
                        b.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(bord[i][j].Card.Color));
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

                    // desactiver le bouton si la cellule n'est pas jouable
                    if (bord[i][j] == null || !bord[i][j].IsPlayable)
                    {
                        b.IsEnabled = false;
                    }
                    Grid.SetRow(b, i);
                    Grid.SetColumn(b, j);
                    BoardGrid.Children.Add(b);

                }
            }
        }

        internal void EndGame(Player winner)
        {
            // on affiche le gagnant
            MessageBox.Show($"Le gagnant est { winner.Name} !");

            // On enregistre sa victoire dans la base de données
            database.AddVictory(winner);


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }

    }
