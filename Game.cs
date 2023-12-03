using Punto.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto
{
    /// <summary>
    /// Classe pour la logique du jeu
    /// </summary>
    internal class Game
    {
        private GameView gameView;

        private List<Player> players;

        private Player currentPlayer;
        private Player winner;

        private int nombreLignes;
        private int nombreColonnes;
        private int nombreCartes;


        public Game (List<Player> players, GameView gameView)
        {
            this.gameView = gameView;

            // Initialisation des paquets de cartes et des joueurs
            this.players = players;
            this.currentPlayer = players[0];
            foreach (Player player in players)
            {
                player.Paquet = new Paquet(player.Color);
            }

            


        }


        public Card CardToPlay()
        {
            Card ret = null;
            // on choisit une carte au hasard dans le paquet du joueur et on la supprime du paquet
            if(currentPlayer.Paquet.Cards.Count == 0)
            {
                // on a plus de cartes, on a perdu
                gameView.EndGame(currentPlayer);
            }
            else
            {
                ret = currentPlayer.Paquet.Cards[new Random().Next(currentPlayer.Paquet.Cards.Count)];
                currentPlayer.Paquet.Cards.Remove(ret);
            }
            

            return ret;
        }

        public void NextPlayer()
        {
            // on passe au joueur suivant
            int index = players.IndexOf(currentPlayer);
            if (index == players.Count - 1)
            {
                currentPlayer = players[0];
            }
            else
            {
                currentPlayer = players[index + 1];
            }
        }

        public bool PlayCard(Card cardToPlay, int row, int column)
        {
            bool ret = false;
            if(gameView.bord[row][column].Card == null || (gameView.bord[row][column].Card.Color != cardToPlay.Color && gameView.bord[row][column].Card.Number < cardToPlay.Number) )
            {
                gameView.bord[row][column].Card = cardToPlay;


                // on vérifie si le joueur a gagné
                if (CheckWin())
                {
                    winner = currentPlayer;
                    // on arrête le jeu
                    gameView.EndGame(winner);
                }
                else
                {
                    // on passe au joueur suivant
                    NextPlayer();
                }



                // comme la carte a été jouée, on peut mettre à jour les cellules jouables, càd les cellules qui ont une carte adjacente
                gameView.UpdatePlayableCells();
                ret = true;
            }

            return ret;
            
        }

        private bool CheckWin()
        {
            // Pour qu'un joueur gagne, il faut qu'il ait soit une ligne, soit une colonne, soit une diagonale complète de 5 cartes de sa couleur
            // On vérifie donc les lignes
            for (int i = 0; i < nombreLignes; i++)
            {
                int compteur = 0;
                for (int j = 0; j < nombreColonnes; j++)
                {
                    if (gameView.bord[i][j].Card.Color == currentPlayer.Color)
                    {
                        compteur++;
                    }
                }
                if (compteur == 5)
                {
                    return true;
                }
            }

            // On vérifie les colonnes
            for (int i = 0; i < nombreColonnes; i++)
            {
                int compteur = 0;
                for (int j = 0; j < nombreLignes; j++)
                {
                    if (gameView.bord[j][i].Card.Color == currentPlayer.Color)
                    {
                        compteur++;
                    }
                }
                if (compteur == 5)
                {
                    return true;
                }
            }

            // On vérifie la diagonale haut gauche - bas droite
            int compteurDiag = 0;
            for (int i = 0; i < nombreLignes; i++)
            {
                if (gameView.bord[i][i].Card.Color == currentPlayer.Color)
                {
                    compteurDiag++;
                }
            }
            if (compteurDiag == 5)
            {
                return true;
            }

            // On vérifie la diagonale haut droite - bas gauche
            compteurDiag = 0;
            for (int i = 0; i < nombreLignes; i++)
            {
                if (gameView.bord[i][nombreColonnes - i - 1].Card.Color == currentPlayer.Color)
                {
                    compteurDiag++;
                }
            }
            if (compteurDiag == 5)
            {
                return true;
            }

            compteurDiag = 0;



            return false;

            
        }

        public Player getCurrentPlayer()
        {
            return currentPlayer;
        }




        public Player getWinner()
        {
            return winner;
        }


    }


   



}
