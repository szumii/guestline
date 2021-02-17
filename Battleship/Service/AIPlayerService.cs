namespace Battleship.Service
{
    using System;
    using Model;

    class AIPlayerService
    {
        public Coordinates HitRandomField(Board board)
        {
            Random random = new Random();
            Coordinates coordinates;
            do
            {
                var row = random.Next(1, 11);
                var col = random.Next(1, 11);
                coordinates = new Coordinates(row, col);
            }
            while (!board.IsEmpty(coordinates));

            return coordinates;
        }

        public Coordinates HitNeighbors(Board board, 
            ShipUnderAttack ship)
        {
            Coordinates neighbor;
            
            //if we do not know the orientation there are 4 possible shots
            //if we know the orientation there are 2 possible shots
            if(ship.Orientation == Orientation.Horizontal || ship.Orientation == Orientation.None)
            {
                neighbor = new Coordinates(ship.Coordinates.Row, ship.Coordinates.Column + ship.Lenght);
                if(neighbor.IsValid() && board.IsEmpty(neighbor))
                {
                    return neighbor;
                }
                neighbor = new Coordinates(ship.Coordinates.Row, ship.Coordinates.Column - 1);
                if(neighbor.IsValid() && board.IsEmpty(neighbor))
                {
                    return neighbor;
                }
            }
            if(ship.Orientation == Orientation.Vertical || ship.Orientation == Orientation.None)
            {
                neighbor = new Coordinates(ship.Coordinates.Row + ship.Lenght, ship.Coordinates.Column);
                if(neighbor.IsValid() && board.IsEmpty(neighbor))
                {
                    return neighbor;
                }
                neighbor = new Coordinates(ship.Coordinates.Row - 1, ship.Coordinates.Column);
                if(neighbor.IsValid() && board.IsEmpty(neighbor))
                {
                    return neighbor;
                }
            }

            //there is no empty neighbor on the board
            throw new ArgumentException();
        }
    }
}