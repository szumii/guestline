namespace Battleship.Tests.Model
{
    using Xunit;
    using Battleship.Model;
    using FluentAssertions;

    public class PlayerTests
    {
        [Fact]
        public void AllSipsHit_PlayerIsLost()
        {
            //Given
            var player = new Player();
            
            //When
            foreach(var ship in player.Ships)
            {
                for(var i = 0; i < ship.Lenght; i++)
                {
                    ship.Hit();
                }
            }

            //Then
            player.IsLost().Should().BeTrue("All player's ships are sunk.");
        }

    }
}