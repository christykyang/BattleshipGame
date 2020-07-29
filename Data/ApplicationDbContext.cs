using System;
using System.Collections.Generic;
using System.Text;
using Battleship.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Battleship.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Ship> Ships { get; set; }
        public DbSet<Battleship.Models.PlaceShipsViewModel> PlaceShipsViewModel { get; set; }
    }
}
