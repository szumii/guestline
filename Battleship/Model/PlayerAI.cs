namespace Battleship.Model
{
    class PlayerAI : Player
    {
        //to read a collection of miss/hits -> its on OpponentBoard

        public Coordinates HitRandomField()
        {
            //implement random
            return new Coordinates(1, 1);
        }

        public Coordinates HitNeighbors(Coordinates coordinates, int lenght = 1, Orientation orientation = Orientation.None)
        {
            //if we do not know the orientation there are 4 possible shots

            //if we know the orientation there are 2 possible shots
            return new Coordinates(1, 1);
        }
    }
}