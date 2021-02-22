namespace Battleship.Tests.Model
{
    using Xunit;
    using Battleship.Model;
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
        public void Board_LoadShipsFromConfig()
        {
            //Given
            var board = new Board();
            
            //When
            var ships = board.LoadShipsFromConfig(".\\Model\\BoardShipsConfig.json");

            //Then
            //Check if battelship put horizontally on B2
            board.GetField(new Coordinates(2, 2)).FieldType.Should().Be(FieldType.Ship);
            board.GetField(new Coordinates(2, 6)).FieldType.Should().Be(FieldType.Ship);
            board.GetField(new Coordinates(2, 7)).FieldType.Should().Be(FieldType.Empty);
            ships.Should().Contain(board.GetField(new Coordinates(2, 2)).Ship);

            //Check if destroyer put vertically on D5
            board.GetField(new Coordinates(5, 4)).FieldType.Should().Be(FieldType.Ship);
            board.GetField(new Coordinates(8, 4)).FieldType.Should().Be(FieldType.Ship);
            board.GetField(new Coordinates(9, 4)).FieldType.Should().Be(FieldType.Empty);
            ships.Should().Contain(board.GetField(new Coordinates(5, 4)).Ship);
                
            //Check if destroyer put vertically on I6
            board.GetField(new Coordinates(6, 9)).FieldType.Should().Be(FieldType.Ship);
            board.GetField(new Coordinates(9, 9)).FieldType.Should().Be(FieldType.Ship);
            board.GetField(new Coordinates(10, 9)).FieldType.Should().Be(FieldType.Empty);
            ships.Should().Contain(board.GetField(new Coordinates(6, 9)).Ship);
        }

        [Fact]
        public void Board_Hit()
        {
            //Given
            var boardShipConfig = new BoardShipConfig(){
                Lenght = 4,
                Column = Column.A,
                Row = 1,
                Orientation = Orientation.Vertical
            };
            var board = new Board();
            var ship = new Ship(){
                Lenght = 4
            };
            board.AddShip(boardShipConfig, ship);
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