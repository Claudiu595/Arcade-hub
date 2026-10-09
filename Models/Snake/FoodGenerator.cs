using System;

namespace Site_jocuri.Models.Snake
{
    public class FoodGenerator
    {
        private readonly Random _random = new Random();

        public (int X, int Y) SpawnFood(int width, int height, SnakeEntity snake)
        {
            while (true)
            {
                // Random între 1 și width-2 (excluzând coloana 0 și coloana width-1)
                int rx = _random.Next(1, width - 1);
                // Random între 1 și height-2 (excluzând linia 0 și linia height-1)
                int ry = _random.Next(1, height - 1);
                
                var food = (X: rx, Y: ry);
                
                // Verificăm să nu fie pe cap sau pe coadă
                if (snake.Head != food && !snake.TailHashSet.Contains($"{food.X},{food.Y}"))
                {
                    return food;
                }
            }
        }
    }
}