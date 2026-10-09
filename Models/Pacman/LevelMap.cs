using System.Collections.Generic;
using System.Linq;

namespace Site_jocuri.Models.Pacman
{
    public class LevelMap
    {
        public int Width { get; } = 30;
        public int Height { get; } = 20;
        public Dictionary<string, Entity> Grid { get; private set; } = new();

        public void Add(Entity e) => Grid[$"{e.X},{e.Y}"] = e;
        public void Remove(int x, int y) => Grid.Remove($"{x},{y}");
        public Entity? Get(int x, int y) => Grid.TryGetValue($"{x},{y}", out var e) ? e : null;
        public List<Entity> GetAll() => Grid.Values.ToList();
        public bool IsWall(int x, int y) => Get(x, y) is Obstacle || x < 0 || x >= Width || y < 0 || y >= Height;
        public bool IsEmpty(int x, int y) => Get(x, y) == null && x >= 0 && x < Width && y >= 0 && y < Height;
    }
}