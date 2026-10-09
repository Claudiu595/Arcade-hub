using System;

namespace Site_jocuri.Models.TicTacToe
{
    public class ComputerPlayer
    {
        private readonly Random _random = new Random();

        public int GetBestMove(GameBoard board)
        {
            var emptyPositions = board.GetEmptyPositions();
            if (emptyPositions.Count == 0) return -1;
            return emptyPositions[_random.Next(emptyPositions.Count)];
        }
    }
}