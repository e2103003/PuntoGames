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

    public class Cell
    {
        public int Id { get; set; }
        public bool IsEmpty { get; set; }
        public int? PlayerId { get; set; }
    }

    public class Game
    {
        public int Id { get; set; }
        public int Player1Id { get; set; }
        public int Player2Id { get; set; }
        public DateTime Date { get; set; }
        //public List<String> Cells { get; set; } // Utiliser le type de données approprié pour votre application
        public String Cells { get; set; }
    }

}
