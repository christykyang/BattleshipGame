﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Battleship.Data;
using Battleship.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Battleship.Controllers
{
    public class PlayerController : Controller
    {
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
            GameViewModel game = new GameViewModel()
            {
                Player1Id = player.Id,
                AllPlayers = new SelectList(Players, "IdentityUserId", "Name")
            };
            return View(game);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateGame(GameViewModel newGame)
        {
            newGame.Player1Board = new Board(20, 20);
            newGame.Player2Board = new Board(20, 20);
            newGame.Player1Fleet = CreateFleet();
            newGame.Player2Fleet = CreateFleet();
            RouteValues routeValues = new RouteValues()
            {
                game = JsonConvert.SerializeObject(newGame)
            };
            Object gameObject = JsonConvert.SerializeObject(newGame);
            TempData["game"] = gameObject;
            return RedirectToAction(nameof(PlaceShips), routeValues);
        }
        public IActionResult PlaceShips(string game)
        {
            //var game = TempData["game"];
            PlaceShipsViewModel viewModel = new PlaceShipsViewModel();
            //viewModel.Board = game.Player1Board;
            viewModel.Ships = CreateFleet();
            return View("PlaceShips", viewModel);
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
            List<Ship> fleet = new List<Ship>() 
            {
                BuildShip("Destroyer", 2),
                BuildShip("Submarine", 3),
                BuildShip("Battleship", 4),
                BuildShip("Aircraft Carrier", 5)
            };
            return fleet;
        }
        private Ship BuildShip(string name, int size)
        {
            Ship ship = new Ship()
            {
                Name = name,
                Size = size,
                Health = size
            };
            return ship;
        }
        private string EncodeBoard(Board board)
        {
            string boardString = "";
            string[][] boardArray = board.board;
            for(int i = 0; i < boardArray.Length; i++)
            {
                string[] row = boardArray[i];
                string rowString = "";
                for(int j = 0; j < row.Length; j++)
                {
                    string cell = row[j];
                    if (j != 0)
                    {
                        rowString += ",";
                    }
                    rowString += cell;
                }
                if (i != 0)
                {
                    boardString += ",,";
                }
                boardString += rowString;
            }

            return boardString;
        }
    }
}