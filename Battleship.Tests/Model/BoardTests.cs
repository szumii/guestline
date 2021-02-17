namespace Battleship.Tests.Model
{
    using Xunit;
    using Battleship.Model;
    using System.Linq;
    using FluentAssertions;

    public class BoardTests
    {
        [Fact]
        public void Board_IsEmpty()
        {
            //Given
            var board = new Board();
            board.Fields[0].FieldType = FieldType.Ship;
            board.Fields[1].FieldType = FieldType.Hit;
            board.Fields[2].FieldType = FieldType.Miss;

            //When
            var isEmptyShip = board.IsEmpty(new Coordinates(1, 1));
            var isEmptyHit = board.IsEmpty(new Coordinates(1, 2));
            var isEmptyMiss = board.IsEmpty(new Coordinates(1, 3));
            var isEmptyEmpty = board.IsEmpty(new Coordinates(1, 4));

            //Then
            isEmptyShip.Should().BeFalse();
            isEmptyHit.Should().BeFalse();
            isEmptyMiss.Should().BeFalse();
            isEmptyEmpty.Should().BeTrue();
        }

        [Fact]
        public void LoadShipsFromConfig_ShipsPutOnBoard()
        {
            //Given
            var board = new Board();
            
            //When
            board.LoadShipsFromConfig(".\\Model\\BoardShipsConfig.json");

            //Then
            //Check if battelship put horizontally on B2
            board.Fields.First(f => f.Coordinates.Row == 2 && f.Coordinates.Column == 2)
                .FieldType.Should().Be(FieldType.Ship);
            board.Fields.First(f => f.Coordinates.Row == 2 && f.Coordinates.Column == 6)
                .FieldType.Should().Be(FieldType.Ship);
            board.Fields.First(f => f.Coordinates.Row == 2 && f.Coordinates.Column == 7)
                .FieldType.Should().Be(FieldType.Empty);

            //Check if destroyer put vertically on D5
            board.Fields.First(f => f.Coordinates.Row == 5 && f.Coordinates.Column == 4)
                .FieldType.Should().Be(FieldType.Ship);
            board.Fields.First(f => f.Coordinates.Row == 8 && f.Coordinates.Column == 4)
                .FieldType.Should().Be(FieldType.Ship);
            board.Fields.First(f => f.Coordinates.Row == 9 && f.Coordinates.Column == 4)
                .FieldType.Should().Be(FieldType.Empty);
                
            //Check if destroyer put vertically on I6
            board.Fields.First(f => f.Coordinates.Row == 6 && f.Coordinates.Column == 9)
                .FieldType.Should().Be(FieldType.Ship);
            board.Fields.First(f => f.Coordinates.Row == 9 && f.Coordinates.Column == 9)
                .FieldType.Should().Be(FieldType.Ship);
            board.Fields.First(f => f.Coordinates.Row == 10 && f.Coordinates.Column == 9)
                .FieldType.Should().Be(FieldType.Empty);
        }
    }
}