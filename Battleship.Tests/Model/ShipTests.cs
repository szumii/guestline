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
            var ship = new Destroyer();

            //When
            for(int i = 0; i < 4; i++)
            {
                ship.Hit();
            }
            
            //Then
            ship.isSunk().Should().BeTrue("Destroyer (4squers) was hit 4 times.");
        }

        [Fact]
        public void Destroyer_HitOnce_IsNotSunk()
        {
            //Given
            var ship = new Destroyer();

            //When
            ship.Hit();

            //Then
            ship.isSunk().Should().BeFalse("Destroyer (4squers) was hit once.");
        }
    }
}
