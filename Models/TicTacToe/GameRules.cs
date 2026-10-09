namespace Site_jocuri.Models.TicTacToe
{
    public class GameRules
    {
        private readonly int[][] _winningCombinations = new int[][]
        {
            new[] {1, 2, 3}, new[] {4, 5, 6}, new[] {7, 8, 9}, // Linii
            new[] {1, 4, 7}, new[] {2, 5, 8}, new[] {3, 6, 9}, // Coloane
            new[] {1, 5, 9}, new[] {3, 5, 7}                   // Diagonale
        };

        public bool CheckWinner(GameBoard board, char player)
        {
            foreach (var combo in _winningCombinations)
            {
                if (board.Cells[combo[0]] == player &&
                    board.Cells[combo[1]] == player &&
                    board.Cells[combo[2]] == player)
                {
                    return true;
                }
            }
            return false;
        }
    }
}