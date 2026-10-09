using System.Collections.Generic;
using System.Linq;

namespace Site_jocuri.Models.TicTacToe
{
    public class GameBoard
    {
        public Dictionary<int, char> Cells { get; private set; }

        public GameBoard()
        {
            Cells = new Dictionary<int, char>();
            for (int i = 1; i <= 9; i++) 
            {
                Cells.Add(i, ' ');
            }
        }

        public bool IsCellEmpty(int position) => Cells.ContainsKey(position) && Cells[position] == ' ';
        public void SetCell(int position, char playerSymbol) => Cells[position] = playerSymbol;
        public bool IsFull() => !Cells.ContainsValue(' ');
        public List<int> GetEmptyPositions() => Cells.Where(c => c.Value == ' ').Select(c => c.Key).ToList();
    }
}