namespace Battleship.UI.Service
{
    using System;
    using Battleship.Model;
    using Battleship.Service;

    class GameService
    {
        private ILoadService _loadService;
        private IDisplayService _displayService;
        private IAIPlayerService _aiPlayerService;

        public GameService(ILoadService loadService, 
            IDisplayService displayService, 
            IAIPlayerService aiPlayerService)
        {
            _loadService = loadService;
            _displayService = displayService;
            _aiPlayerService = aiPlayerService;
        }

        public void Play()
        {
            var myself = new Player();
            
            LoadShip(5, myself);
            for(var _ = 0; _ < 2; _++)
            {
                LoadShip(4, myself);
            }
            _displayService.RenderBoards(myself);
            Console.WriteLine("Game started.");

            var opponent = new Player();
            var ships = opponent.MyBoard.LoadShipsFromConfig(".\\Input\\BoardShipsConfig.json");
            opponent.Ships = ships;

            while (!myself.IsLost() && !opponent.IsLost())
            {
                //player turn
                var firePosition = _loadService.GetFireCoordinates();
                var (isHit, isSunk) = opponent.MyBoard.Hit(firePosition);
                myself.OpponentBoard.MarkField(firePosition, isHit ? FieldType.Hit : FieldType.Miss);
                if(isHit)
                {
                    Console.WriteLine("You hit it. Bravo!");
                    if(isSunk)
                    {
                        Console.WriteLine("Ship is sunk. Conrats.");
                    }
                }
                else
                {
                    Console.WriteLine("It is miss.");
                }

                //oponnet's turn
                firePosition = _aiPlayerService.Hit(opponent.OpponentBoard);
                Console.WriteLine($"Opponent fired at {((Column)firePosition.Column).ToString()}{firePosition.Row}");
                (isHit, isSunk) = myself.MyBoard.Hit(firePosition);
                opponent.OpponentBoard.MarkField(firePosition, isHit ? FieldType.Hit : FieldType.Miss);
                if(isHit)
                {
                    _aiPlayerService.HitWasSuccessfull(firePosition);
                    Console.WriteLine("Unfortunately it is a hit.");
                    if(isSunk)
                    {
                        _aiPlayerService.ShipIsSunk();
                        Console.WriteLine("Your ship is sunk.");
                    }
                }
                else
                {
                    Console.WriteLine("It is miss.");
                }
                
                _displayService.RenderBoards(myself);
            }

            if(opponent.IsLost())
            {
                Console.WriteLine("You win this time. Congrats!");
            }
            if(myself.IsLost())
            {
                Console.WriteLine("You lost this time.");
            }

        }
        
        private void LoadShip(int lenght, Player player)
        {
            var isShipValid = false;
            do
            {
                var battleship = _loadService.LoadShip(lenght);
                isShipValid = player.TryAddShip(battleship);   
                if(!isShipValid)
                {
                    Console.WriteLine("Invalid. Provide data again.");
                }
            }
            while(!isShipValid);
        }
    }
}