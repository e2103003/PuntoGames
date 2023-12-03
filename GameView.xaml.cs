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
        private int nombreLignes;
        private int nombreColonnes;

        private Game game;

        private DatabaseUse database;

        // variable de la carte a jouer 
        public Card CardToPlay { get; set; }


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


            game = new Game(players, this);

            CardToPlay = game.CardToPlay();

            CardToPlayView.Content = CardToPlay.Number;
            CardToPlayView.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(CardToPlay.Color));

            UpdateGrid();

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
            // on parcourt toutes les cellules
            for (int i = 0; i < bord[0].Count; ++i)
            {
                for (int j = 0; j < bord.Count; ++j)
                {
                    // si la cellule est vide, on regarde si elle est jouable
                    if (bord[i][j] == null)
                    {
                        // on regarde si la cellule a une carte adjacente
                        if (HasAdjacentCard(i, j))
                        {
                            // si oui, on la rend jouable
                            Cell cellule = new Cell();
                            cellule.IsPlayable = true;
                            bord[i][j] = cellule;
                        }
                    }
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
                reduceGrid(card, row, column);


                

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
        /// Cette méthode permet de réduire la taille de la grille de jeu, càd atteindre sa taille minimale de 6x6. Si la grille est déjà de taille minimale, on ne fait rien
        /// Cette méthode est appelée à chaque fois qu'une carte est jouée, on réduit la taille la grille à l'opposé de la carte jouée
        /// </summary>
        private void reduceGrid(Card cardPlayed, int row, int column )
        {
            // on regarde si la grille est de taille minimale
            if (bord[0].Count > 6 && bord.Count > 6)
            {
                // Si la carte jouée est sur le bord gauche, on supprime la colonne de droite
                if (row > 5)
                {
                    // on supprime la colonne de droite
                    bord[0].RemoveAt(bord[0].Count - 1);
                    bord[1].RemoveAt(bord[1].Count - 1);
                    bord[2].RemoveAt(bord[2].Count - 1);
                    bord[3].RemoveAt(bord[3].Count - 1);
                    bord[4].RemoveAt(bord[4].Count - 1);
                    bord[5].RemoveAt(bord[5].Count - 1);
                    bord[6].RemoveAt(bord[6].Count - 1);
                    bord[7].RemoveAt(bord[7].Count - 1);
                    bord[8].RemoveAt(bord[8].Count - 1);
                    bord[9].RemoveAt(bord[9].Count - 1);
                    bord[10].RemoveAt(bord[10].Count - 1);

                   
                } 
                // Si la carte jouée est sur le bord droit, on supprime la colonne de gauche
                else if (row < 5)
                {
                    // on supprime la colonne de gauche
                    bord[0].RemoveAt(0);
                    bord[1].RemoveAt(0);
                    bord[2].RemoveAt(0);
                    bord[3].RemoveAt(0);
                    bord[4].RemoveAt(0);
                    bord[5].RemoveAt(0);
                    bord[6].RemoveAt(0);
                    bord[7].RemoveAt(0);
                    bord[8].RemoveAt(0);
                    bord[9].RemoveAt(0);
                    bord[10].RemoveAt(0);
                }
                // Si la carte jouée est sur le bord haut, on supprime la ligne du bas
                else if (column > 5)
                {
                    // on supprime la ligne du bas
                    bord.RemoveAt(bord.Count - 1);
                }
                // Si la carte jouée est sur le bord bas, on supprime la ligne du haut
                else if (column < 5)
                {
                    // on supprime la ligne du haut
                    bord.RemoveAt(0);
                }
                else
                {

                }
               
                
            }
           

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
