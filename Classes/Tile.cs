using MineSweeperIria.Structs;

namespace MineSweeperIria.Classes
{
    public class Tile
    {
        public Position TilePosition { get; set; }
        public bool Visible { get; set; }
        public int TileCode { get; set; }
        public bool IsBomb { get; set; }

        public Tile(Position tilePosition, int tileCode)
        {
            TilePosition = tilePosition;
            Visible = false;
            IsBomb = false;
            TileCode = tileCode;
        }
    }
}