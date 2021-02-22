namespace Battleship.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json.Serialization;

    public class BoardShipConfig : IEquatable<BoardShipConfig>
    {
        [JsonPropertyName("lenght")]
        public int Lenght { get; set; }

        [JsonPropertyName("column")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Column Column { get; set; }
        
        [JsonPropertyName("row")]
        public int Row { get; set; }
        
        [JsonPropertyName("orientation")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Orientation Orientation { get; set; }

        public bool IsValid(Board board = null)
        {
            if(Lenght < 4 || Lenght > 5)
            {
                return false;
            }
            if(Column == Column.None)
            {
                return false;
            }
            if(Row < 1 || Row > 10)
            {
                return false;
            }
            if(Orientation == Orientation.None)
            {
                return false;
            }
            if(Orientation == Orientation.Horizontal && ((int)Column + Lenght > 10))
            {
                return false;
            }
            if(Orientation == Orientation.Vertical &&  (Row + Lenght > 10))
            {
                return false;
            }
            //check if ships cross on the board
            if(board != null)
            {
                var shipFields = ShipToFields().Select(f => f.Coordinates);
                var ocupiedFields = board.Fields.Where(f => f.FieldType == FieldType.Ship)
                                        .Select(f => f.Coordinates);
                if(ocupiedFields.Intersect(shipFields, new CoordinatesComparer()).Count() > 0)
                {
                    return false;
                }
            }

            return true;
        }
        
        public List<Field> ShipToFields()
        {
            var fields = new List<Field>();
            if (Orientation == Orientation.Horizontal)
            {
                for (int i = (int)Column; i < (int)Column + Lenght; i++)
                {
                    var coordinates = new Coordinates(Row, i);
                    fields.Add(new Field(){
                        Coordinates = coordinates,
                        FieldType = FieldType.Ship
                    });
                }
            }
            else if (Orientation == Orientation.Vertical)
            {
                for (int i = Row; i < Row + Lenght; i++)
                {
                    var coordinates = new Coordinates(i, (int)Column);
                    fields.Add(new Field(){
                        Coordinates = coordinates,
                        FieldType = FieldType.Ship
                    });
                }
            }
            return fields;
        }

        public bool Equals(BoardShipConfig other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            if (this.GetType() != other.GetType())
            {
                return false;
            }

            return (Lenght == other.Lenght) && (Column == other.Column) 
                && (Row == other.Row) && (Orientation == other.Orientation);
        }
    }

    public enum Orientation
    {
        None = 0,

        Horizontal = 1,

        Vertical = 2
    }

    public enum Column
    {
        None = 0,

        A = 1,
        B,
        C,
        D,
        E,
        F,
        G,
        H,
        I,
        J
    }
}