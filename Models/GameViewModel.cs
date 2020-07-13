using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship.Models
{
    public class GameViewModel
    {
        public int Id { get; set; }
        public int Player1Id { get; set; }
        public int Player2Id { get; set; }
        public List<Ship> Player1Fleet { get; set; }
        public List<Ship> Player2Fleet { get; set; }
        public Board Player1Board { get; set; }
        public Board Player2Board { get; set; }
    }
}
