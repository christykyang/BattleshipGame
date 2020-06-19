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
        public string[][] boardOne;
        public string[][] boardTwo;
        public PlayerController()
        {
            
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateGame()
        {
            boardOne = new Board(20, 20).board;
            boardTwo = new Board(20, 20).board;
            return View();
        }
        public IActionResult PlaceShip()
        {
            return View();
        }
    }
}