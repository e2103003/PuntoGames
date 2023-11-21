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
        private ObservableCollection<ObservableCollection<CardViewModel>> grid;
        public ObservableCollection<ObservableCollection<CardViewModel>> Grid
        {
            get { return grid; }
            set
            {
                grid = value;
                OnPropertyChanged(nameof(Grid));
            }
        }

        public GameGridViewModel()
        {
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            // Initialisez la grille avec des lignes et des colonnes vides
            Grid = new ObservableCollection<ObservableCollection<CardViewModel>>();

            for (int i = 0; i < 11; i++)
            {
                var row = new ObservableCollection<CardViewModel>();
                for (int j = 0; j < 11; j++)
                {
                    row.Add(new CardViewModel());
                }
                Grid.Add(row);
            }
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
