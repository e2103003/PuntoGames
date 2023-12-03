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
        private List<Player> players;

        public List<List<Cell>> bord;
        private int nombreLignes;
        private int nombreColonnes;

        private Game game;

        private DatabaseUse database;

        // variable de la carte a jouer 
        public Card CardToPlay { get; set; }


        public GameView(List<Player> players, DatabaseUse database)
        {
            this.database = database;
            this.players = players;
            InitializeComponent();

            restartGame();

            

        }

        
        public void UpdateGrid()
        {
            // on vide la grille
            BoardGrid.Children.Clear();

            

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

                    

                    // desactiver le bouton si la cellule n'est pas jouable
                    if (bord[i][j] == null || !bord[i][j].IsPlayable)
                    {
                        b.IsEnabled = true;
                        b.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
                        b.BorderThickness = new Thickness(0);
                    }
                    else
                    {
                        b.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("White"));

                        b.BorderThickness = new Thickness(3.5);
                        b.Click += new RoutedEventHandler(Button_Click);
                    }
                    Grid.SetRow(b, i);
                    Grid.SetColumn(b, j);
                    BoardGrid.Children.Add(b);

                }
            }

            CurrentPlayerText.Text = $" C'est à {game.getCurrentPlayer().Name} de jouer sa carte : ";

            NumberCardCurrentPlayerText.Text = $"Il te reste {game.getCurrentPlayer().Paquet.Cards.Count.ToString()} carte(s) dans ton paquet";

            CardToPlayView.Content = this.CardToPlay.Number;
            CardToPlayView.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(this.CardToPlay.Color));
            CardToPlayView.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(this.CardToPlay.Color));

        }



        /// <summary>
        ///  Met à jour les cellules jouables
        /// </summary>
        public void UpdatePlayableCells()
        {
            // on parcourt la grille
            for (int i = 0; i < bord[0].Count; ++i)
            {
                int nbCardInRow = 0;
                for (int j = 0; j < bord.Count; ++j)
                {
                    if (bord[i][j] != null && bord[i][j].Card != null)
                    {
                        nbCardInRow++;
                    }

                    if (nbCardInRow > 6)
                    {
                        bord[i][j].IsPlayable = false;

                        

                    }

                    // on regarde si la cellule a une carte adjacente
                    else if (HasAdjacentCard(i, j) )
                    {
                        bord[i][j].IsPlayable = true;
                    }
                    else
                    {
                        bord[i][j].IsPlayable = false;
                    }
                }
            }
        }

        

        internal void EndGame(Player winner)
        {
            UpdateGrid();
            // on affiche le gagnant
            MessageBox.Show($"Le gagnant est { winner.Name} !");



            // On enregistre sa victoire dans la base de données
            database.AddVictory(winner);


        }

        private void restartGame()
        {
            bord = new List<List<Cell>>();

            int nombreLignes = 11;
            int nombreColonnes = 11;

            // Initialisation de grille de jeu
            for (int i = 0; i < nombreLignes; ++i)
            {
                bord.Add(new List<Cell>());
                for (int j = 0; j < nombreColonnes; ++j)
                {
                    bord[i].Add(new Cell());

                }
            }

            // D'après les regles du jeu, la première carte doit être placée au milieu de la grille
            Cell cellule = new Cell();
            cellule.IsPlayable = true;
            bord[5][5] = cellule;


            game = new Game(players, this);

            CardToPlay = game.CardToPlay();

            CardToPlayView.Content = CardToPlay.Number;
            CardToPlayView.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(CardToPlay.Color));

            UpdateGrid();



        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //
            Button b = (Button)sender;
            int row = Grid.GetRow(b);
            int column = Grid.GetColumn(b);


            // on recupere la carte jouée
            Card card = this.CardToPlay;
            
            bool canPlay = game.PlayCard(card, row, column);

            if(!canPlay)
            {
                MessageBox.Show("Vous ne pouvez pas jouer cette carte ici !");
            }
            else
            {

                bord[row][column].Card = card;

                

                this.CardToPlay = game.CardToPlay();

                // on met a jour la taille de grille de jeu
                //reduceGrid(card, row, column);


                

            }
            var test = bord;

            // on met a jour la grille
            UpdateGrid();

            
        }

        private void Button_SkipTurn(object sender, RoutedEventArgs e)
        {
            // on passe au joueur suivant
            game.NextPlayer();

            // on met a jour la grille
            UpdateGrid();
        }


           

        

        

        /// <summary>
        /// Fonction qui regarde si la cellule a une carte adjacente, en haut, en bas, à gauche, à droite ou en diagonale
        /// </summary>
        /// <param name="i"> l'indice de la colonne </param>
        /// <param name="j"> l'indice de la ligne </param>
        /// <returns></returns>
        private bool HasAdjacentCard(int i, int j)
        {
            bool ret = false;

            // on regarde si la cellule a une carte adjacente
            // on regarde d'abord en haut
            if (i > 0 && bord[i - 1][j] != null && bord[i - 1][j].Card != null)
            {
                ret = true;
            }
            // puis en bas
            else if (i < bord[0].Count - 1 && bord[i + 1][j] != null && bord[i + 1][j].Card != null)
            {
                ret = true;
            }
            // puis à gauche
            else if (j > 0 && bord[i][j - 1] != null && bord[i][j - 1].Card != null)
            {
                ret = true;
            }
            // puis à droite
            else if (j < bord.Count - 1 && bord[i][j + 1] != null && bord[i][j + 1].Card != null)
            {
                ret = true;
            }
            // puis en diagonale haut gauche
            else if (i > 0 && j > 0 && bord[i - 1][j - 1] != null && bord[i - 1][j - 1].Card != null)
            {
                ret = true;
            }
            // puis en diagonale haut droite
            else if (i > 0 && j < bord.Count - 1 && bord[i - 1][j + 1] != null && bord[i - 1][j + 1].Card != null)
            {
                ret = true;
            }
            // puis en diagonale bas gauche
            else if (i < bord[0].Count - 1 && j > 0 && bord[i + 1][j - 1] != null && bord[i + 1][j - 1].Card != null)
            {
                ret = true;
            }
            // puis en diagonale bas droite
            else if (i < bord[0].Count - 1 && j < bord.Count - 1 && bord[i + 1][j + 1] != null && bord[i + 1][j + 1].Card != null)
            {
                ret = true;
            }
            return ret; 
            
        }
    }

    }
