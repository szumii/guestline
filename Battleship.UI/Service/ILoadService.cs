namespace Battleship.UI.Service
{
    using Battleship.Model;
    
    interface ILoadService
    {
        public BoardShipConfig LoadShip(int length);
        
        public Coordinates GetFireCoordinates();
    }
}