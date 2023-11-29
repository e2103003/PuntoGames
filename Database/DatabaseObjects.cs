using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto.Database
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public int Wins { get; set; }
        public string LastWin { get; set; }
    }

  
    // Pas stocker dans les base de données
    public class Card
    {
        public string Color { get; set; }
        public int Number { get; set; }
    }

    public class Cell
    {
        
        public Card Card { get; set; }
        public bool IsPlayable { get; set; }


        
    }

}
