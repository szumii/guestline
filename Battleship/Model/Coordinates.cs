namespace Battleship
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

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

    
    public class CoordinatesComparer : IEqualityComparer<Coordinates>
    {
        public bool Equals(Coordinates x, Coordinates y)
        {
            if (Object.ReferenceEquals(x, y))
            {
                return true;
            }
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
            {
                return false;
            }

            return x.Column == y.Column
                && x.Row == y.Row;
        }

        public int GetHashCode([DisallowNull] Coordinates x)
        {
            if (Object.ReferenceEquals(x, null)) return 0;
            int hashFieldCol = x.Column.GetHashCode();
            int hashFieldRow = x.Row.GetHashCode();
            return hashFieldCol ^ hashFieldRow;
        }
    }
}