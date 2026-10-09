using System;

namespace Site_jocuri.Models.Snake
{
    public class SnakeEngine
    {
        public int Width { get; } = 20;
        public int Height { get; } = 15;
        
        public SnakeEntity Snake { get; private set; } = null!;
        public (int X, int Y) Food { get; private set; }
        public int Score { get; private set; }
        public bool IsGameOver { get; private set; }
        public string GameOverMessage { get; private set; } = string.Empty;

        private readonly FoodGenerator _foodGenerator;

        public SnakeEngine()
        {
            _foodGenerator = new FoodGenerator();
            StartGame();
        }

        public void StartGame()
        {
            Snake = new SnakeEntity(Width / 2, Height / 2);
            Food = _foodGenerator.SpawnFood(Width, Height, Snake);
            Score = 0;
            IsGameOver = false;
            GameOverMessage = string.Empty;
        }

        public void ProcessInput(string key)
        {
            if (key == "w" || key == "ArrowUp") Snake.SetDirection(0, -1);
            else if (key == "s" || key == "ArrowDown") Snake.SetDirection(0, 1);
            else if (key == "a" || key == "ArrowLeft") Snake.SetDirection(-1, 0);
            else if (key == "d" || key == "ArrowRight") Snake.SetDirection(1, 0);
        }

        public void Update()
        {
            if (IsGameOver || (Snake.DirX == 0 && Snake.DirY == 0)) return;

            int nextX = Snake.Head.X + Snake.DirX;
            int nextY = Snake.Head.Y + Snake.DirY;

            // --- VERIFICARE COLIZIUNE ZID ---
            if (nextX <= 0 || nextX >= Width - 1 || nextY <= 0 || nextY >= Height - 1)
            {
                GameOverMessage = "Ai lovit zidul!";
                IsGameOver = true;
                return;
            }

            Snake.Tail.Insert(0, Snake.Head);
            Snake.TailHashSet.Add($"{Snake.Head.X},{Snake.Head.Y}");

            Snake.Head = (nextX, nextY);

            // --- VERIFICARE COLIZIUNE COADĂ ---
            if (Snake.TailHashSet.Contains($"{Snake.Head.X},{Snake.Head.Y}"))
            {
                GameOverMessage = "Te-ai lovit în coadă!";
                IsGameOver = true;
                return;
            }

            if (Snake.Head == Food)
            {
                Score += 10;
                Food = _foodGenerator.SpawnFood(Width, Height, Snake);
            }
            else
            {
                var last = Snake.Tail[^1];
                Snake.Tail.RemoveAt(Snake.Tail.Count - 1);
                Snake.TailHashSet.Remove($"{last.X},{last.Y}");
            }
        }
    }
}