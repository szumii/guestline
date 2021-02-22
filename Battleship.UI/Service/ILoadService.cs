namespace Battleship.UI.Service
{
    using Battleship.Model;
    
    interface ILoadService
    {
        public BoardShip LoadShip(int length);
        
        public Coordinates GetFireCoordinates();
    }
}