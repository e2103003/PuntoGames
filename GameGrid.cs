using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto
{
    public class GameGrid : ObservableCollection<ObservableCollection<CardViewModel>>
    {
        public GameGrid(int rows, int columns)
        {
            for (int i = 0; i < rows; i++)
            {
                var row = new ObservableCollection<CardViewModel>();
                for (int j = 0; j < columns; j++)
                {
                    row.Add(new CardViewModel());
                }
                Add(row);
            }
        }
    }

}
