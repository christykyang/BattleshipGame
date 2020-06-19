using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship.Models
{
    public class Board
    {
        public string[][] board;
        public Board(int height, int width, string value = "0")
        {
            board = CreateBoard(height, width, value);
        }
        public string[][] CreateBoard(int height, int width, string value = "0")
        {
            string[][] board = new string[height][];
            for(int i = 0; i < height; i++)
            {
                board[i] = new string[width];
                for(int j = 0; j < board[i].Length; j++)
                {
                    board[i][j] = value;
                }
            }
            return board;
        }
    }
}
