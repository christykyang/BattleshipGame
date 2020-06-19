using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        public int Player1Id { get; set; }
        public int Player2Id { get; set; }
        public string Player1Board { get; set; }
        public string Player2Board { get; set; }
    }
}
