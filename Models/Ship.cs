using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Battleship.Models
{
    public class Ship
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
    }
}
