using System;
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
            Game game = ConvertGameViewModelToGame(newGame);
            _context.Games.Add(game);
            _context.SaveChanges();
            return RedirectToAction(nameof(PlaceShips));
        }
        public IActionResult PlaceShips()
        {
            Game game = _context.Games.First();
            GameViewModel gameViewModel = ConvertGameToGameViewModel(game);
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
        private Game ConvertGameViewModelToGame(GameViewModel viewModel)
        {
            Game game = new Game()
            {
                Id = viewModel.Id,
                Player1Id = viewModel.Player1Id,
                Player2Id = viewModel.Player2Id,
                Player1Board = EncodeBoard(viewModel.Player1Board),
                Player2Board = EncodeBoard(viewModel.Player2Board)
            };
            return game;
        }
        private GameViewModel ConvertGameToGameViewModel(Game game)
        {
            GameViewModel viewModel = new GameViewModel()
            {
                Id = game.Id,
                Player1Id = game.Player1Id,
                Player2Id = game.Player2Id,
                Player1Board = DecodeBoard(game.Player1Board),
                Player2Board = DecodeBoard(game.Player2Board)
            };
            viewModel.Player1Fleet = GetFleetBasedOnBoard(viewModel.Player1Board);
            viewModel.Player2Fleet = GetFleetBasedOnBoard(viewModel.Player2Board);
            return viewModel;
        }
        private List<Ship> GetFleetBasedOnBoard(Board board)
        {
            List<Ship> fleet = CreateFleet();
            foreach(Ship ship in fleet)
            {
                string hit = ship.Name[0].ToString().ToLower();
                ship.Health = ship.Size - GetCountOfParticularCharInBoard(hit, board);
            }
            return fleet;
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
        private Board DecodeBoard (string boardString)
        {
            Board board = new Board();
            string[] rows = boardString.Split(",,");
            List<string[]> boardList = new List<string[]>();
            foreach(string row in rows)
            {
                string[] cells = row.Split(",");
                boardList.Add(cells);
            }
            string[][] boardArray = boardList.ToArray();
            board.board = boardArray;
            return board;
        }
        private int GetCountOfParticularCharInBoard(string character, Board board)
        {
            int count = 0;
            string[][] boardArray = board.board;
            foreach(string[] row in boardArray)
            {
                foreach(string cell in row)
                {
                    if (cell == character)
                    {
                        count++;
                    }
                }
            }
            return count;
        }
    }
}