using System;
using System.Collections.Generic;
using System.Linq;
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
            return View();
        }
        public IActionResult PlaceShip()
        {
            return View();
        }
        public void CreateFleet(string name, int size)
        {
            Ship destroyer = new Ship();
            destroyer.Name = "Destroyer";
            destroyer.Size = 2;

            Ship submarine = new Ship();
            submarine.Name = "Submarine";
            submarine.Size = 3;

            Ship battleship = new Ship();
            battleship.Name = "Battleship";
            battleship.Size = 4;

            Ship aircraft = new Ship();
            aircraft.Name = "Aircraft Carrier";
            aircraft.Size = 5;
        }
    }
}