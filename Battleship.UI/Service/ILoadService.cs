namespace Battleship.UI.Service
{
    using Battleship.Model;
    
    interface ILoadService
    {
        public Ship LoadShip(int length);
        
        public Coordinates GetFireCoordinates();
    }
}