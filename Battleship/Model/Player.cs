namespace Battleship.Model
{
    using System.Collections.Generic;
    using System.Linq;

    public class Player
    {
        public Player()
        {
            Ships = new List<Ship>();
            Ships.Add(new Battleship());
            Ships.Add(new Destroyer());
            Ships.Add(new Destroyer());
        }

        public Board MyBoard { get; set; }

        public Board OpponentBoard { get; set; }

        public List<Ship> Ships { get; set; }
        
        public bool IsLost()
        {
            return Ships.All(s => s.isSunk());
        }
    }
}