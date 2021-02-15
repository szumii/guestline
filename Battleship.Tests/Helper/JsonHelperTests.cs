namespace Battleship.Tests.Helper
{
    using Xunit;
    using Battleship.Helper;
    using FluentAssertions;
    using Battleship.Model;

    public class JsonHelperTests
    {
        [Fact]
        public void JsonHelperLoadConfiguration_ShipsCorrectlyLoaded()
        {
            //Given
            var expected = new BoardShipConfig(){
                Lenght =  5,
                Column = Column.B,
                Row = 2,
                Orientation = Orientation.Horizontal
            };
            
            //When
            var boardShipsConfig = JsonHelper.LoadShips(".\\Model\\BoardShipsConfig.json");
            
            //Then
            boardShipsConfig.Count.Should().Be(3);
            boardShipsConfig.Should().Contain(expected);
        }
    }
}