using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship.Models
{
    public class PlaceShipsViewModel
    {
        public int Id { get; set; }
        public int PlayerNumber { get; set; }
        public Board Board { get; set; }
        public List<Ship> Ships { get; set; }
    }
}
