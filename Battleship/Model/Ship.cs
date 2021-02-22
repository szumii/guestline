namespace Battleship.Model
{
    public class Ship
    {
        public int Lenght { get; set; }
        private int Hits { get; set; }

        public void Hit()
        {
            Hits++;
        }

        public bool IsSunk()
        {
            return Hits >= Lenght;
        }
    }

}

