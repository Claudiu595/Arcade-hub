using System;
using System.Collections.Generic;
using System.Linq;

namespace Site_jocuri.Models.Pacman
{
    public class PacmanEngine
    {
        public LevelMap Map { get; private set; } = null!;
        public Player Hero { get; private set; } = null!;
        public List<Enemy> Enemies { get; private set; } = null!;
        private readonly Random _random = new Random();

        // Folosim un dicționar pentru a ține minte dacă sub picioarele unui inamic se afla un punct,
        // evitând astfel ștergerea definitivă a punctelor verzi de pe hartă.
        private readonly Dictionary<Enemy, PointItem?> _hiddenPoints = new Dictionary<Enemy, PointItem?>();

        public PacmanEngine()
        {
            InitializeLevel();
        }

        public void InitializeLevel()
        {
            Map = new LevelMap();
            Enemies = new List<Enemy>();
            _hiddenPoints.Clear();
            
            // Poziționăm eroul
            Hero = new Player(15, 10);
            Map.Add(Hero);

            // Generăm harta (pereții/obstacolele)
            for (int y = 0; y < Map.Height; y++)
            {
                for (int x = 0; x < Map.Width; x++)
                {
                    if (x == 0 || y == 0 || x == Map.Width - 1 || y == Map.Height - 1 || _random.Next(100) < 10)
                    {
                        if (x != 15 || y != 10) 
                        {
                            Map.Add(new Obstacle(x, y));
                        }
                    }
                }
            }

            // Generăm punctele inițiale (bănuții)
            for (int i = 0; i < 5; i++) SpawnPoint();

            // Adăugăm cei trei inamici
            AddEnemy(5, 5);   
            AddEnemy(25, 15); 
            AddEnemy(5, 15);  
        }

        private void SpawnPoint()
        {
            int rx, ry;
            do
            {
                rx = _random.Next(1, Map.Width - 1);
                ry = _random.Next(1, Map.Height - 1);
            } while (!Map.IsEmpty(rx, ry));
            
            Map.Add(new PointItem(rx, ry));
        }

        private void AddEnemy(int x, int y)
        {
            if (Map.IsWall(x, y))
            {
                Map.Remove(x, y);
            }
            
            var enemy = new Enemy(x, y);
            Enemies.Add(enemy);
            _hiddenPoints[enemy] = null; // Inițial nu ascunde niciun punct
            Map.Add(enemy);
        }

        public void MovePlayer(int dx, int dy)
        {
            if (Hero.HP <= 0 || Hero.Points >= 30) return;

            int nx = Hero.X + dx;
            int ny = Hero.Y + dy;

            if (Map.IsWall(nx, ny)) return;

            if (Hero.Stamina > 0) Hero.Stamina--;

            var target = Map.Get(nx, ny);
            if (target is PointItem)
            {
                Hero.Points++;
                if (Hero.Points % 10 == 0) Hero.Stamina += 20;
                Map.Remove(nx, ny);
            }
            else if (target is Enemy currentEnemy)
            {
                Hero.HP--;
                // Dacă jucătorul intră intenționat în inamic, îl respingem puțin ca să nu ia damage infinit
                return; 
            }

            Map.Remove(Hero.X, Hero.Y);
            Hero.X = nx; 
            Hero.Y = ny;
            Map.Add(Hero);
        }

        public void UpdateEnemiesAndEnvironment()
{
    if (Hero.HP <= 0 || Hero.Points >= 30) return;

    foreach (var enemy in Enemies)
    {
        int nextX = enemy.X;
        int nextY = enemy.Y;

        // MODIFICAT: Doar 30% șanse să te urmărească activ, reducând masiv dificultatea
        if (_random.Next(100) < 30) 
        {
            if (enemy.X < Hero.X) nextX++;
            else if (enemy.X > Hero.X) nextX--;

            if (enemy.Y < Hero.Y) nextY++;
            else if (enemy.Y > Hero.Y) nextY--;
        }
        else // 70% din timp se vor plimba haotic prin labirint
        {
            int dir = _random.Next(4);
            nextX += (dir == 2 ? -1 : dir == 3 ? 1 : 0);
            nextY += (dir == 0 ? -1 : dir == 1 ? 1 : 0);
        }

        if (Map.IsWall(nextX, nextY)) continue;

        if (nextX == Hero.X && nextY == Hero.Y)
        {
            Hero.HP--; 
            continue; 
        }

        Map.Remove(enemy.X, enemy.Y);

        if (_hiddenPoints[enemy] != null)
        {
            Map.Add(_hiddenPoints[enemy]!);
        }

        var cellContent = Map.Get(nextX, nextY);
        if (cellContent is PointItem point)
        {
            _hiddenPoints[enemy] = point;
            Map.Remove(nextX, nextY);
        }
        else
        {
            _hiddenPoints[enemy] = null;
        }

        enemy.X = nextX;
        enemy.Y = nextY;
        Map.Add(enemy);
    }

    int currentPointsCount = Map.GetAll().Count(e => e is PointItem) + _hiddenPoints.Values.Count(p => p != null);
    if (currentPointsCount < 6)
    {
        SpawnPoint();
    }
}
    }
}