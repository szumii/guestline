namespace Battleship.Tests.Model
{
    using FluentAssertions;
    using Xunit;

    public class CoordinatesTests
    {
        [Theory]
        [InlineData(1, 1, true)]
        [InlineData(10, 10, true)]
        [InlineData(-1, 1, false)]
        [InlineData(1, -1, false)]
        [InlineData(11, 1, false)]
        [InlineData(1, 11, false)]
        public void Coordinates_IsValid(int column, int row, bool validationResult)
        {
            //Given
            var coordinates = new Coordinates(column, row);

            //When
            var isValid = coordinates.IsValid();
            
            //Then
            isValid.Should().Be(validationResult);
        }
    }
}