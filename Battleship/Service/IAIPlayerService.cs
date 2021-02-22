namespace Battleship.Service
{
    using Battleship.Model;
    
    public interface IAIPlayerService
    {
        public Coordinates Hit(Board board);
        public void HitWasSuccessfull(Coordinates firePosition);
        public void ShipIsSunk();
    }
}