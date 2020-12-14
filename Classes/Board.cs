using MineSweeperIria.Structs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MineSweeperIria.Classes
{
    public class Board
    {
        public List<Tile> Tiles = new List<Tile>();
        public int NumberOfRows { get; set; }
        public int NumberOfColumns { get; set; }
        private int TotalTiles { get; set; }
        public int NumberOfBombs { get; set; }

        public Board(int rows, int columns, int bombs)
        {
            NumberOfRows = rows;
            NumberOfColumns = columns;
            TotalTiles = rows * columns;
            NumberOfBombs = bombs;

            for (int i = 0; i <= TotalTiles - 1; i++)
            {
                Position position = new Position
                {
                    Column = (int)(i / NumberOfRows),
                    Row = i % NumberOfRows
                };
                Tile tile = new Tile(position, i);
                Tiles.Add(tile);
            }

            SetBombs();
            Tiles.Where(t => t.TileCode == 0).FirstOrDefault().Visible = true;
        }

        internal void SetBombs()
        {
            var rnd = new Random();
            for (int i = 0; i <= NumberOfBombs - 1; i++)
            {
                int code;
                do
                {
                    code = rnd.Next(1, TotalTiles - 2);
                }
                while (Tiles.Where(t => t.TileCode == code).FirstOrDefault().IsBomb);
                Tiles.Where(t => t.TileCode == code).FirstOrDefault().IsBomb = true;
            }
        }
    }
}