using Punto.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto
{
    public class GameGridViewModel : INotifyPropertyChanged
    {
        private GameGrid grid;
        public GameGrid Grid
        {
            get { return grid; }
            set
            {
                grid = value;
                OnPropertyChanged(nameof(Grid));
            }
        }

        public GameGridViewModel(int rows, int columns)
        {
            Grid = new GameGrid(rows, columns);
        }

        // Vous pouvez appeler cette méthode pour mettre à jour visuellement la grille
        public void UpdateGrid(List<List<Card>> cards)
        {
            // Mettez à jour la grille avec les nouvelles cartes
            for (int i = 0; i < Math.Min(cards.Count, Grid.Count); i++)
            {
                for (int j = 0; j < Math.Min(cards[i].Count, Grid[i].Count); j++)
                {
                    if (cards[i][j] != null)
                    {
                        Grid[i][j].Number = cards[i][j].Number;
                        Grid[i][j].Color = cards[i][j].Color;

                    }
                    
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
