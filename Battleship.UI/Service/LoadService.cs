namespace Battleship.UI.Service
{
    using System;
    using Battleship.Model;

    class LoadService : ILoadService
    {
        public BoardShipConfig LoadShip(int length)
        {
            Console.WriteLine("Provide data for {length} fileds long ship:");
            var ship = LoadShip();
            ship.Lenght = length;
            return ship;
        }

        public Coordinates GetFireCoordinates()
        {
            Console.Write("It is your turn. Fire at (e.g. A1): ");
            return LoadCoordiantes();
        }

        private BoardShipConfig LoadShip()
        {
            Console.Write("Provide ship position (e.g. A1): ");
            var coordinates = LoadCoordiantes();

            Console.Write("Provide ship orientation (options: v - vertical, h - horizontal): ");
            var orientationStr = Console.ReadLine();
            while (string.IsNullOrEmpty(orientationStr) 
                || orientationStr[0] != 'v' && orientationStr[0] != 'h')
            {
                Console.WriteLine("Valid orientation is vertical or horizontal (v or h).");
                orientationStr = Console.ReadLine();
            }

            var ship = new BoardShipConfig()
            {
                Column = (Column)coordinates.Column,
                Row = coordinates.Row,
                Orientation = orientationStr[0] == 'v' ? Orientation.Vertical : Orientation.Horizontal
            };
            return ship;
        }

        private Coordinates LoadCoordiantes()
        {
            var position = Console.ReadLine();
            Column column;
            int row;
            while (string.IsNullOrEmpty(position.Trim()) 
                || !Enum.TryParse(position[0].ToString().ToUpper(), out column) || column == Column.None
                || !Int32.TryParse(position.Substring(1), out row) || row < 1 || row > 10)
            {
                Console.WriteLine("Valid position should be value between A1 and J10.");
                position = Console.ReadLine();
            }
            return new Coordinates(row, (int)column);
        }
    }
}