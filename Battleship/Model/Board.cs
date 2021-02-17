namespace Battleship.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Helper;

    public class Board
    {
        public Board()
        {
            Fields = new List<Field>();
            for(int i = 1; i < 11; i++)
            {
                for(int j = 1; j < 11; j++)
                {
                    Fields.Add(new Field{
                        Coordinates = new Coordinates(i,j)
                     });
                }
            }
        }

        public List<Field> Fields { get; set; }
        
        public bool IsEmpty(Coordinates coordinates)
        {
            return Fields.First(f => f.Coordinates.Row == coordinates.Row 
                && f.Coordinates.Column == coordinates.Column).FieldType == FieldType.Empty;
        }

        public void LoadShipsFromConfig(string url)
        {
            try
            {
                var boardShipsFromConfig = JsonHelper.LoadShips(url);

                foreach(var ship in boardShipsFromConfig)
                {
                    if(ship.IsValid())
                    {
                        if(ship.Orientation == Orientation.Horizontal)
                        {
                            for(int i = (int)ship.Column; i < (int)ship.Column + ship.Lenght; i++)
                            {
                                var coordinates = new Coordinates(ship.Row, i);
                                // check if ships cross then throw exception
                                if(!IsEmpty(coordinates))
                                {
                                    throw new ArgumentException();
                                }
                                SetField(coordinates, FieldType.Ship);
                            }
                        }
                        else if(ship.Orientation == Orientation.Vertical)
                        {
                            for(int i = ship.Row; i < ship.Row + ship.Lenght; i++)
                            {
                                var coordinates = new Coordinates(i, (int)ship.Column);
                                if(!IsEmpty(coordinates))
                                {
                                    throw new ArgumentException();
                                }
                                SetField(new Coordinates(i, (int)ship.Column), FieldType.Ship);
                            }
                        }
                    }
                }
            }
            catch
            {
                //log
            }
        }

        private void SetField(Coordinates coordinates, FieldType fieldType)
        {
            var field = Fields.First(f => f.Coordinates.Row == coordinates.Row 
                                && f.Coordinates.Column == coordinates.Column);
            field.FieldType = fieldType;
        }
    }
}