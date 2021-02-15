namespace Battleship.Tests.Model
{
    using Xunit;
    using Battleship.Model;
    using FluentAssertions;

    public class BoardShipsConfigTests
    {
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
        public void ValidateBoardShipsConfiguration(int lenght, Column column, 
                        int row, Orientation orientation, bool validationResult)
        {
            //Given
            var boardShipConfig = new BoardShipConfig(){
                Lenght = lenght,
                Column = column,
                Row = row,
                Orientation = orientation
            };

            //When
            var isValid = boardShipConfig.IsValid();
            
            //Then
            isValid.Should().Be(validationResult);
        }
        
    }
}