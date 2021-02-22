namespace Battleship.Tests.Model
{
    using Xunit;
    using Battleship.Model;
    using FluentAssertions;
    using System.Collections.Generic;

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
        public void Board_LoadShipsFromConfig()
        {
            //Given
            var board = new Board();
            
            //When
            var ship = new Ship(){
                Lenght = 5,
                Column = Column.B,
                Row = 2,
                Orientation = Orientation.Horizontal
            };
            board.LoadShips(new List<Ship>(){ ship });

            //Then
            //Check if battelship put horizontally on B2
            board.GetField(new Coordinates(2, 2)).FieldType.Should().Be(FieldType.Ship);
            board.GetField(new Coordinates(2, 6)).FieldType.Should().Be(FieldType.Ship);
            board.GetField(new Coordinates(2, 7)).FieldType.Should().Be(FieldType.Empty);
            ship.Should().Be(board.GetField(new Coordinates(2, 2)).Ship);
        }

        [Fact]
        public void Board_Hit()
        {
            //Given
            var ship = new Ship(){
                Lenght = 4,
                Column = Column.A,
                Row = 1,
                Orientation = Orientation.Vertical
            };
            var board = new Board();
            board.AddShip(ship);
            var hitCoor = new Coordinates(1, 1);
            var missCoor = new Coordinates(1, 2);

            //When
            var (isHit, isSunk) = board.Hit(hitCoor);
            var (missIsHit, missIsSunk) = board.Hit(missCoor);

            //Then
            isHit.Should().BeTrue();
            isSunk.Should().BeFalse();
            board.GetField(hitCoor).FieldType.Should().Be(FieldType.Hit);
            missIsHit.Should().BeFalse();
            missIsSunk.Should().BeFalse();
            board.GetField(missCoor).FieldType.Should().Be(FieldType.Miss);

            //hit again should not change the field type
            (isHit, isSunk) = board.Hit(hitCoor);
            isHit.Should().BeTrue();
            isSunk.Should().BeFalse();
            board.GetField(hitCoor).FieldType.Should().Be(FieldType.Hit);
        }
    }
}