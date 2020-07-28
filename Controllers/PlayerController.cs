using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Battleship.Data;
using Battleship.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Battleship.Controllers
{
    public class PlayerController : Controller
    {
        GameViewModel game;
        ApplicationDbContext _context;
        public PlayerController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateGame()
        {
            string identityUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Player player = _context.Players.Where(p => p.IdentityUserId == identityUserId).First();
            List<Player> Players = _context.Players.ToList();
            game = new GameViewModel()
            {
                Player1Id = player.Id,
                Player1Board = new Board(20, 20),
                Player2Board = new Board(20, 20),
                Player1Fleet = CreateFleet(),
                Player2Fleet = CreateFleet(),
                AllPlayers = new SelectList(Players, "IdentityUserId", "Name")
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePlayer(Player player)
        {
            _context.Players.Add(player);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
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