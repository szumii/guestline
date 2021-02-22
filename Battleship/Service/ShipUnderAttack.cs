namespace Battleship.Service
{
    using Battleship.Model;
    
    public class ShipUnderAttack
    {
        // top, left field of the ship
        public Coordinates Coordinates { get; set; }

        // already hit lenght
        public int Lenght { get; set; }

        // if more that 2 hits we know the orientation of the ship
        public Orientation Orientation { get; set; }
    }
}