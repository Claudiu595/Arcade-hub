using System.Collections.Generic;

namespace Site_jocuri.Models.Snake
{
    public class SnakeEntity
    {
        public (int X, int Y) Head { get; set; }
        public List<(int X, int Y)> Tail { get; private set; }
        public HashSet<string> TailHashSet { get; private set; }

        public int DirX { get; private set; } = 0;
        public int DirY { get; private set; } = 0;

        public SnakeEntity(int startX, int startY)
        {
            Head = (startX, startY);
            Tail = new List<(int X, int Y)>();
            TailHashSet = new HashSet<string>();
        }

        public void SetDirection(int dx, int dy)
        {
            if ((DirX != 0 && dx == -DirX) || (DirY != 0 && dy == -DirY)) return; 
            DirX = dx;
            DirY = dy;
        }
    }
}