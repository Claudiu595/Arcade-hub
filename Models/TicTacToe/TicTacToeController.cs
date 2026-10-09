namespace Site_jocuri.Models.TicTacToe
{
    public class TicTacToeController
    {
        public GameBoard Board { get; private set; } = null!;
        private readonly GameRules _rules;
        private readonly ComputerPlayer _computer;

        public char CurrentPlayer { get; private set; }
        public bool IsGameOver { get; private set; }
        public string Message { get; private set; } = null!;
        public bool IsPlayerVsComputer { get; set; } = true;

        public TicTacToeController()
        {
            _rules = new GameRules();
            _computer = new ComputerPlayer();
            StartNewGame();
        }

        public void StartNewGame()
        {
            Board = new GameBoard();
            CurrentPlayer = 'X';
            IsGameOver = false;
            Message = "Game started. Player X's turn.";
        }

        public void MakeMove(int position)
        {
            if (IsGameOver || !Board.IsCellEmpty(position)) return;

            Board.SetCell(position, CurrentPlayer);

            if (_rules.CheckWinner(Board, CurrentPlayer))
            {
                Message = $"Player {CurrentPlayer} won!";
                IsGameOver = true;
                return;
            }

            if (Board.IsFull())
            {
                Message = "Game ended in a draw!";
                IsGameOver = true;
                return;
            }

            CurrentPlayer = CurrentPlayer == 'X' ? 'O' : 'X';
            Message = $"Player {CurrentPlayer}'s turn.";

            if (IsPlayerVsComputer && CurrentPlayer == 'O' && !IsGameOver)
            {
                int aiMove = _computer.GetBestMove(Board);
                if (aiMove != -1) MakeMove(aiMove);
            }
        }
    }
}