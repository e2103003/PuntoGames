using Punto.Database;
using System;
using System.Collections.Generic;

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
                    return false;
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

        public bool CheckWin()
        {
            // Vérifier les lignes
            for (int i = 0; i < gameView.bord.Count; i++)
            {
                int consecutiveCount = 0;
                for (int j = 0; j < gameView.bord[i].Count; j++)
                {
                    if (gameView.bord[i][j].Card != null && gameView.bord[i][j].Card.Color == currentPlayer.Color)
                    {
                        consecutiveCount++;
                        if (consecutiveCount == 5)
                        {
                            return true; // Le joueur a gagné
                        }
                    }
                    else
                    {
                        consecutiveCount = 0; // Réinitialiser le compteur s'il y a une interruption
                    }
                }
            }

            // Vérifier les colonnes
            for (int j = 0; j < gameView.bord[0].Count; j++)
            {
                int consecutiveCount = 0;
                for (int i = 0; i < gameView.bord.Count; i++)
                {
                    if (gameView.bord[i][j].Card != null && gameView.bord[i][j].Card.Color == currentPlayer.Color)
                    {
                        consecutiveCount++;
                        if (consecutiveCount == 5)
                        {
                            return true; // Le joueur a gagné
                        }
                    }
                    else
                    {
                        consecutiveCount = 0; // Réinitialiser le compteur s'il y a une interruption
                    }
                }
            }

            // Vérifier les diagonales
            for (int i = 0; i < gameView.bord.Count - 4; i++)
            {
                for (int j = 0; j < gameView.bord[i].Count - 4; j++)
                {
                    if (gameView.bord[i][j].Card != null && gameView.bord[i][j].Card.Color == currentPlayer.Color)
                    {
                        // Vérifier la diagonale vers le bas à droite
                        if (gameView.bord[i + 1][j + 1].Card != null && gameView.bord[i + 1][j + 1].Card.Color == currentPlayer.Color &&
                            gameView.bord[i + 2][j + 2].Card != null && gameView.bord[i + 2][j + 2].Card.Color == currentPlayer.Color &&
                            gameView.bord[i + 3][j + 3].Card != null && gameView.bord[i + 3][j + 3].Card.Color == currentPlayer.Color &&
                            gameView.bord[i + 4][j + 4].Card != null && gameView.bord[i + 4][j + 4].Card.Color == currentPlayer.Color)
                        {
                            return true; // Le joueur a gagné
                        }
                    }
                }
            }

            for (int i = 4; i < gameView.bord.Count; i++)
            {
                for (int j = 0; j < gameView.bord[i].Count - 4; j++)
                {
                    if (gameView.bord[i][j].Card != null && gameView.bord[i][j].Card.Color == currentPlayer.Color)
                    {
                        // Vérifier la diagonale vers le bas à gauche
                        if (gameView.bord[i - 1][j + 1].Card != null && gameView.bord[i - 1][j + 1].Card.Color == currentPlayer.Color &&
                            gameView.bord[i - 2][j + 2].Card != null && gameView.bord[i - 2][j + 2].Card.Color == currentPlayer.Color &&
                            gameView.bord[i - 3][j + 3].Card != null && gameView.bord[i - 3][j + 3].Card.Color == currentPlayer.Color &&
                            gameView.bord[i - 4][j + 4].Card != null && gameView.bord[i - 4][j + 4].Card.Color == currentPlayer.Color)
                        {
                            return true; // Le joueur a gagné
                        }
                    }
                }
            }

            return false; // Aucune victoire détectée
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
