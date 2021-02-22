namespace Battleship.Service
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using Model;

    public class AIPlayerService : IAIPlayerService
    {
        private ShipUnderAttack shipUnderAttack;

        public List<BoardShip> PlaceShips(Board board)
        {
            var ships = new List<BoardShip>();
            ships.Add(PlaceShip(5, board));
            for (var _ = 0; _ < 2; _++)
            {
                ships.Add(PlaceShip(4, board));
            }
            return ships;
        }

        private BoardShip PlaceShip(int lenght, Board board)
        {
            BoardShip boardShipConfig;
            do
            {
                var row = RandomNumberGenerator.GetInt32(1, 11);
                var col = RandomNumberGenerator.GetInt32(1, 11);
                var orientation = RandomNumberGenerator.GetInt32(1, 3);
                boardShipConfig = new BoardShip()
                {
                    Lenght = lenght,
                    Column = (Column)col,
                    Row = row,
                    Orientation = (Orientation)orientation
                };
            }
            while (!boardShipConfig.IsValid(board));
            return boardShipConfig;
        }

        public Coordinates Hit(Board board)
        {
            if (shipUnderAttack != null)
            {
                return HitNeighbors(board, shipUnderAttack);
            }
            else
            {
                return HitRandomField(board);
            }
        }

        private Coordinates HitRandomField(Board board)
        {
            Coordinates coordinates;
            do
            {
                var row = RandomNumberGenerator.GetInt32(1, 11);
                var col = RandomNumberGenerator.GetInt32(1, 11);
                coordinates = new Coordinates(row, col);
            }
            while (!board.IsEmpty(coordinates));

            return coordinates;
        }

        private Coordinates HitNeighbors(Board board,
            ShipUnderAttack ship)
        {
            Coordinates neighbor;

            //if we do not know the orientation there are 4 possible shots
            //if we know the orientation there are 2 possible shots
            if (ship.Orientation == Orientation.Horizontal || ship.Orientation == Orientation.None)
            {
                neighbor = new Coordinates(ship.Coordinates.Row, ship.Coordinates.Column + ship.Lenght);
                if (neighbor.IsValid() && board.IsEmpty(neighbor))
                {
                    return neighbor;
                }
                neighbor = new Coordinates(ship.Coordinates.Row, ship.Coordinates.Column - 1);
                if (neighbor.IsValid() && board.IsEmpty(neighbor))
                {
                    return neighbor;
                }
            }
            if (ship.Orientation == Orientation.Vertical || ship.Orientation == Orientation.None)
            {
                neighbor = new Coordinates(ship.Coordinates.Row + ship.Lenght, ship.Coordinates.Column);
                if (neighbor.IsValid() && board.IsEmpty(neighbor))
                {
                    return neighbor;
                }
                neighbor = new Coordinates(ship.Coordinates.Row - 1, ship.Coordinates.Column);
                if (neighbor.IsValid() && board.IsEmpty(neighbor))
                {
                    return neighbor;
                }
            }

            //there is no empty neighbor on the board
            throw new ArgumentException();
        }

        public void HitWasSuccessfull(Coordinates firePosition)
        {
            if (shipUnderAttack == null)
            {
                shipUnderAttack = new ShipUnderAttack()
                {
                    Coordinates = firePosition,
                    Lenght = 1,
                    Orientation = Orientation.None
                };
            }
            else
            {
                shipUnderAttack.Lenght++;
                if (shipUnderAttack.Orientation == Orientation.None)
                {
                    if (shipUnderAttack.Coordinates.Row == firePosition.Row)
                    {
                        shipUnderAttack.Orientation = Orientation.Horizontal;
                    }
                    else
                    {
                        shipUnderAttack.Orientation = Orientation.Vertical;
                    }
                }
                //hit left/upper neighbor -> update top left field of ship 
                if (shipUnderAttack.Coordinates.Column > firePosition.Column)
                {
                    shipUnderAttack.Coordinates.Column = firePosition.Column;
                }
                if (shipUnderAttack.Coordinates.Row > firePosition.Row)
                {
                    shipUnderAttack.Coordinates.Row = firePosition.Row;
                }
            }
        }

        public void ShipIsSunk()
        {
            shipUnderAttack = null;
        }
    }
}