namespace Battleship
{
    public class Coordinates
    {
        public Coordinates(int row, int column)
        {
            Row = row;
            Column = column;
        }
        
        public int Row { get; set; }
        public int Column { get; set; }

        public bool IsValid()
        {
            return Row >= 1 && Row <= 10 && Column >= 1 && Column <= 10;
        }
    }
}