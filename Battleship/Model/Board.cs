namespace Battleship.Model
{
    using System.Collections.Generic;
    using System.Linq;
    using Helper;

    public class Board
    {
        public Board()
        {
            Fields = new List<Field>();
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    Fields.Add(new Field
                    {
                        Coordinates = new Coordinates(i, j)
                    });
                }
            }
        }

        public List<Field> Fields { get; set; }

        public bool IsEmpty(Coordinates coordinates)
        {
            return GetField(coordinates).FieldType == FieldType.Empty;
        }

        public (bool, bool) Hit(Coordinates coordinates)
        {
            var field = GetField(coordinates);
            if (field.FieldType == FieldType.Ship)
            {
                field.FieldType = FieldType.Hit;
                field.Ship.Hit();
                return (true, field.Ship.IsSunk());
            }
            else if (field.FieldType == FieldType.Empty)
            {
                field.FieldType = FieldType.Miss;
                return (false, false);
            }
            //hit the same field once more
            else if (field.FieldType == FieldType.Hit)
            {
                return (true, false);
            }
            return (false, false);
        }

        public void MarkField(Coordinates coordinates, FieldType fieldType)
        {
            GetField(coordinates).FieldType = fieldType;
        }

        public Field GetField(Coordinates coordinates)
        {
            return Fields.FirstOrDefault(f => f.Coordinates.Column == coordinates.Column
                && f.Coordinates.Row == coordinates.Row);
        }

        public void AddShip(BoardShipConfig shipConf, Ship ship)
        {
            if (shipConf.IsValid(this))
            {
                var fields = shipConf.ShipToFields();
                foreach (var field in fields)
                {
                    SetField(field.Coordinates, FieldType.Ship, ship);
                }
            }
        }

        private void SetField(Coordinates coordinates, FieldType fieldType, Ship ship)
        {
            var field = Fields.First(f => f.Coordinates.Row == coordinates.Row
                                && f.Coordinates.Column == coordinates.Column);
            field.FieldType = fieldType;
            field.Ship = ship;
        }

        public List<Ship> LoadShipsFromConfig(string url)
        {
            var boardShipsFromConfig = JsonHelper.LoadShips(url);
            var ships = new List<Ship>();

            try
            {
                foreach (var shipConf in boardShipsFromConfig)
                {
                    var ship = new Ship
                    {
                        Lenght = shipConf.Lenght
                    };
                    ships.Add(ship);
                    AddShip(shipConf, ship);
                }
                return ships;
            }
            catch
            {
                //log
                throw;
            }
        }
    }
}