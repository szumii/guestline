namespace Battleship.Service
{
    using System.Collections.Generic;
    using Battleship.Model;
    
    public interface IAIPlayerService
    {
        public List<Ship> PlaceShips(Board board);
        public Coordinates Hit(Board board);
        public void HitWasSuccessfull(Coordinates firePosition);
        public void ShipIsSunk();
    }
}