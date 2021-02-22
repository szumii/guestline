namespace Battleship.Tests.Service
{
    using Xunit;
    using Battleship.Model;
    using Battleship.Service;
    using FluentAssertions;

    public class AIPlayerServiceTests
    {
        [Fact]
        public void AIPlayerService_ContinueHitting_WithoutKnownOrientation()
        {
            //Given
            var board = new Board();
            var aiPlayerService = new AIPlayerService();
            
            //When
            aiPlayerService.HitWasSuccessfull(new Coordinates(5, 2));
            var firstAttempt = aiPlayerService.Hit(board);
            board.MarkField(firstAttempt, FieldType.Miss);
            var secondAttempt = aiPlayerService.Hit(board);
            board.MarkField(secondAttempt, FieldType.Miss);
            var thirdAttempt = aiPlayerService.Hit(board);
            board.MarkField(thirdAttempt, FieldType.Miss);
            var fourthAttempt = aiPlayerService.Hit(board);
            board.MarkField(fourthAttempt, FieldType.Hit);

            //Then
            firstAttempt.Should().Equals(new Coordinates(5, 3));
            secondAttempt.Should().Equals(new Coordinates(6, 2));
            thirdAttempt.Should().Equals(new Coordinates(5, 1));
            fourthAttempt.Should().Equals(new Coordinates(4, 2));
        }
        
        [Fact]
        public void AIPlayerService_ContinueHitting_WithKnownOrientation()
        {
            //Given
            var board = new Board();
            var aiPlayerService = new AIPlayerService();
            
            //When
            var firstHit = new Coordinates(1, 1);
            aiPlayerService.HitWasSuccessfull(firstHit);
            board.MarkField(firstHit, FieldType.Hit);

            var secondhit = new Coordinates(2, 1);
            aiPlayerService.HitWasSuccessfull(secondhit);
            board.MarkField(secondhit, FieldType.Hit);

            var attempt = aiPlayerService.Hit(board);

            //Then
            attempt.Should().Equals(new Coordinates(3, 1));
        }


        [Fact]
        public void AIPlayerService_ContinueHitting_WithKnownOrientationAfterMiss()
        {
            //Given
            var board = new Board();
            var aiPlayerService = new AIPlayerService();
            
            //When
            var firstHit = new Coordinates(3, 1);
            aiPlayerService.HitWasSuccessfull(firstHit);
            board.MarkField(firstHit, FieldType.Hit);

            var secondhit = new Coordinates(4, 1);
            aiPlayerService.HitWasSuccessfull(secondhit);
            board.MarkField(secondhit, FieldType.Hit);

            var thirdhit = new Coordinates(5, 1);
            board.MarkField(secondhit, FieldType.Miss);

            var attempt = aiPlayerService.Hit(board);

            //Then
            attempt.Should().Equals(new Coordinates(2, 1));
        }

    }
}