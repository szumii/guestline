namespace Battleship.Model
{
    class Battleship : Ship
    {
        public Battleship()
        {
            Lenght = 5;
        }
    }

    public class Destroyer : Ship
    {
        public Destroyer()
        {
            Lenght = 4;
        }
    }

    public class Ship
    {
        public int Lenght { get; protected set; }
        protected int Hits { get; set; }

        public void Hit()
        {
            Hits++;
        }

        public bool isSunk()
        {
            return Hits >= Lenght;
        }
    }

}

