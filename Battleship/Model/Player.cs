namespace Battleship.Model
{
    using System.Collections.Generic;
    using System.Linq;

    public class Player
    {
        public Player()
        {
            Ships = new List<Ship>();
            MyBoard = new Board();
            OpponentBoard = new Board();
        }

        public Board MyBoard { get; set; }

        public Board OpponentBoard { get; set; }

        public List<Ship> Ships { get; set; }

        public bool TryAddShip(BoardShipConfig boardShipConfig)
        {
            if (boardShipConfig.IsValid(MyBoard))
            {
                var ship = new Ship()
                {
                    Lenght = boardShipConfig.Lenght
                };
                MyBoard.AddShip(boardShipConfig, ship);
                Ships.Add(ship);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsLost()
        {
            return Ships.All(s => s.IsSunk());
        }
    }
}