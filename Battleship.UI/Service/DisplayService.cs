namespace Battleship.UI.Service
{
    using System;
    using System.Collections.Generic;
    using Battleship.Model;

    class DisplayService : IDisplayService
    {
        private Dictionary<FieldType, char> fieldTypeChar = new Dictionary<FieldType, char>(){
            { FieldType.Empty, 'O' },
            { FieldType.Ship, 'X' },
            { FieldType.Hit, 'H' },
            { FieldType.Miss, 'M' }
        };

        public void RenderBoards(Player player)
        {
            RenderHeader();
            for (int i = 1; i <= 10; i++)
            {
                Console.Write($"{i.ToString("00")}: ");
                RenderBoardLine(player.MyBoard, i);
                Console.Write(" | ");
                RenderBoardLine(player.OpponentBoard, i);
                Console.WriteLine();
            }
        }

        private void RenderHeader()
        {
            Console.Write("  : ");
            RenderSingleHeader();
            Console.Write(" | ");
            RenderSingleHeader();
            Console.WriteLine();
        }

        private void RenderSingleHeader()
        {
            for (int i = 1; i <= 10; i++)
            {
                Console.Write($"{((Column)i).ToString()} ");
            }
        }

        private void RenderBoardLine(Board board, int rowIndex)
        {
            for (int j = 1; j <= 10; j++)
            {
                var fieldType = fieldTypeChar[board.GetField(new Coordinates(rowIndex, j)).FieldType];
                Console.Write($"{fieldType} ");
            }
        }
    }
}