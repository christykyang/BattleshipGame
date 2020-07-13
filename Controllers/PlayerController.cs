using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Battleship.Models;
using Microsoft.AspNetCore.Mvc;

namespace Battleship.Controllers
{
    public class PlayerController : Controller
    {
        GameViewModel game;
        public PlayerController()
        {
            
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateGame()
        {
            game = new GameViewModel()
            {
                Player1Board = new Board(20, 20),
                Player2Board = new Board(20, 20)
            };
            return View(game);
        }
        public IActionResult PlaceShip()
        {
            return View();
        }
        public IActionResult CreatePlayer()
        {
            Player player = new Player()
            {
                IdentityUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier)
            };
            return View(player);
        }
        public List<Ship> CreateFleet()
        {
            List<Ship> fleet = new List<Ship>();

            Ship destroyer = new Ship();
            destroyer.Name = "Destroyer";
            destroyer.Size = 2;
            fleet.Add(destroyer);

            Ship submarine = new Ship();
            submarine.Name = "Submarine";
            submarine.Size = 3;
            fleet.Add(submarine);

            Ship battleship = new Ship();
            battleship.Name = "Battleship";
            battleship.Size = 4;
            fleet.Add(battleship);

            Ship aircraft = new Ship();
            aircraft.Name = "Aircraft Carrier";
            aircraft.Size = 5;
            fleet.Add(aircraft);

            return fleet;
        }
    }
}