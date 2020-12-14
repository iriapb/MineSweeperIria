using MineSweeperIria.Classes;
using MineSweeperIria.Structs;
using System;
using System.Linq;

namespace MineSweeperIria
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int rows;
            int columns;
            int bombs;
            int lifes;
            Console.WriteLine("Game that consists of going from one side of the board to the other one. There are as many bombs as you want, but you do not know where exactly they are located.");
            do
            {
                rows = GetRows();
                columns = GetColumns();
            } while (rows * columns < 2);
            do
            {
                bombs = GetBombs();
            }
            while (bombs >= ((rows * columns) - 2) || (bombs < 0));
            do
            {
                lifes = GetLifes();
            }
            while (lifes < 1);
            int movements = 0;
            var board = new Board(rows, columns, bombs);
            var currentPosition = new Position { Row = 0, Column = 0 };
            var gameOver = false;
            do
            {
                DisplayBoard(board, lifes, movements);
                Console.SetCursorPosition(currentPosition.Column, currentPosition.Row);
                Console.CursorVisible = true;
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        if (currentPosition.Row - 1 >= 0)
                        {
                            currentPosition.Row = currentPosition.Row - 1;
                            board.Tiles.Where(bt => bt.TilePosition.Row == currentPosition.Row && bt.TilePosition.Column == currentPosition.Column).FirstOrDefault().Visible = true;
                            movements = movements + 1;
                            if (board.Tiles.Where(bt => bt.TilePosition.Row == currentPosition.Row && bt.TilePosition.Column == currentPosition.Column).FirstOrDefault().IsBomb)
                            {
                                lifes = lifes - 1;
                            }
                        }
                        break;

                    case ConsoleKey.DownArrow:
                        if (currentPosition.Row + 1 <= rows - 1)
                        {
                            currentPosition.Row = currentPosition.Row + 1;
                            board.Tiles.Where(bt => bt.TilePosition.Row == currentPosition.Row && bt.TilePosition.Column == currentPosition.Column).FirstOrDefault().Visible = true;
                            movements = movements + 1;
                            if (board.Tiles.Where(bt => bt.TilePosition.Row == currentPosition.Row && bt.TilePosition.Column == currentPosition.Column).FirstOrDefault().IsBomb)
                            {
                                lifes = lifes - 1;
                            }
                        }
                        break;

                    case ConsoleKey.LeftArrow:
                        if (currentPosition.Column - 1 >= 0)
                        {
                            currentPosition.Column = currentPosition.Column - 1;
                            board.Tiles.Where(bt => bt.TilePosition.Row == currentPosition.Row && bt.TilePosition.Column == currentPosition.Column).FirstOrDefault().Visible = true;
                            movements = movements + 1;
                            if (board.Tiles.Where(bt => bt.TilePosition.Row == currentPosition.Row && bt.TilePosition.Column == currentPosition.Column).FirstOrDefault().IsBomb)
                            {
                                lifes = lifes - 1;
                            }
                        }
                        break;

                    case ConsoleKey.RightArrow:
                        if (currentPosition.Column + 1 <= columns - 1)
                        {
                            currentPosition.Column = currentPosition.Column + 1;
                            board.Tiles.Where(bt => bt.TilePosition.Row == currentPosition.Row && bt.TilePosition.Column == currentPosition.Column).FirstOrDefault().Visible = true;
                            movements = movements + 1;
                            if (board.Tiles.Where(bt => bt.TilePosition.Row == currentPosition.Row && bt.TilePosition.Column == currentPosition.Column).FirstOrDefault().IsBomb)
                            {
                                lifes = lifes - 1;
                            }
                        }
                        break;
                }
                if (currentPosition.Column == columns - 1 && currentPosition.Row == rows - 1)
                {
                    Console.Clear();
                    Console.WriteLine("You win!");
                    Console.WriteLine("Number of remaining lives: {0}", lifes);
                }
                if (lifes == 0)
                {
                    Console.Clear();
                    Console.WriteLine("Game Over: you lose!");
                }
                gameOver = (lifes == 0) || (currentPosition.Column == columns - 1 && currentPosition.Row == rows - 1);
            }
            while (!gameOver);
            Console.WriteLine("Number of movements: {0}", movements.ToString());
        }

        internal static int GetRows()
        {
            int rows;
            bool result;
            int max = Console.WindowHeight - 1;
            do
            {
                Console.WriteLine("Introduce the number of rows: ");
                var maximumR = Console.ReadLine();
                result = int.TryParse(maximumR, out rows);
            }
            while (!result || (rows > max || rows < 1));
            return rows;
        }

        internal static int GetColumns()
        {
            int columns;
            bool result;
            int max = Console.WindowWidth - 1;
            do
            {
                Console.WriteLine("Introduce the number of columns: ");
                var maximumR = Console.ReadLine();
                result = int.TryParse(maximumR, out columns);
            }
            while (!result || (columns > max || columns < 1));
            return columns;
        }

        internal static int GetBombs()
        {
            int bombs;
            bool result;
            do
            {
                Console.WriteLine("Introduce the number of bombs: ");
                var maximumR = Console.ReadLine();
                result = int.TryParse(maximumR, out bombs);
            }
            while (!result || bombs < 0);
            return bombs;
        }

        internal static int GetLifes()
        {
            int life;
            bool result;
            do
            {
                Console.WriteLine("Introduce the number of life: ");
                var maximumR = Console.ReadLine();
                result = int.TryParse(maximumR, out life);
            }
            while (!result || life < 0);
            return life;
        }

        internal static void DisplayBoard(Board board, int lives, int movements)
        {
            Console.Clear();
            string[] boardTiles = new string[board.NumberOfRows];

            for (int r = 0; r <= board.NumberOfRows - 1; r++)
            {
                boardTiles[r] = string.Empty;
                for (int c = 0; c <= board.NumberOfColumns - 1; c++)
                {
                    var currentTile = board.Tiles.Where(bt => bt.TilePosition.Column == c && bt.TilePosition.Row == r).FirstOrDefault();
                    if (currentTile.IsBomb && currentTile.Visible)
                    {
                        boardTiles[r] = boardTiles[r] + "X";
                    }
                    if (!currentTile.IsBomb && currentTile.Visible)
                    {
                        boardTiles[r] = boardTiles[r] + " ";
                    }
                    if (!currentTile.Visible)
                    {
                        boardTiles[r] = boardTiles[r] + "?";
                    }
                }
                Console.WriteLine("{0}", boardTiles[r]);
            }

            Console.WriteLine(string.Empty);
            Console.WriteLine("Number of lifes: {0}", lives);
            Console.WriteLine("Number of movements: {0}", movements);
        }
    }
}