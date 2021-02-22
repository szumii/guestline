namespace Battleship.Tests.Model
{
    using Xunit;
    using Battleship.Model;
    using FluentAssertions;

    public class ShipTests
    {
        [Fact]
        public void Destroyer_HitFourTimes_IsSunk()
        {
            //Given
            var ship = new Ship(){
                Lenght = 4
            };

            //When
            for(int i = 0; i < 4; i++)
            {
                ship.Hit();
            }
            
            //Then
            ship.IsSunk().Should().BeTrue("Destroyer (4squers) was hit 4 times.");
        }

        [Fact]
        public void Destroyer_HitOnce_IsNotSunk()
        {
            //Given
            var ship = new Ship(){
                Lenght = 4
            };

            //When
            ship.Hit();

            //Then
            ship.IsSunk().Should().BeFalse("Destroyer (4squers) was hit once.");
        }

        [Theory]
        [InlineData(5, Column.B, 2, Orientation.Horizontal, true)]
        [InlineData(4, Column.D, 5, Orientation.Vertical, true)]
        [InlineData(4, Column.I, 6, Orientation.Vertical, true)]
        [InlineData(5, Column.G, 2, Orientation.Horizontal, false)]
        [InlineData(4, Column.I, 8, Orientation.Vertical, false)]
        [InlineData(2, Column.B, 2, Orientation.Horizontal, false)]
        [InlineData(5, Column.None, 2, Orientation.Horizontal, false)]
        [InlineData(5, Column.G, 11, Orientation.Horizontal, false)]
        [InlineData(5, Column.G, 2, Orientation.None, false)]
        public void Ship_IsValid(int lenght, Column column, 
                        int row, Orientation orientation, bool validationResult)
        {
            //Given
            var ship = new Ship(){
                Lenght = lenght,
                Column = column,
                Row = row,
                Orientation = orientation
            };

            //When
            var isValid = ship.IsValid();
            
            //Then
            isValid.Should().Be(validationResult);
        }
        
        [Fact]
        public void Ship_IsValid_CheckIfShipCrossWithOther()
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
            var crossingShip = new Ship(){
                Lenght = 4,
                Column = Column.A,
                Row = 1,
                Orientation = Orientation.Horizontal
            };

            //When
            var isValid = crossingShip.IsValid(board);
            
            //Then
            isValid.Should().Be(false);
        }
    }
}