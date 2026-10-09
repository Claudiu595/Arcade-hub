namespace Site_jocuri.Models.Pacman
{
    public abstract class Entity
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Symbol { get; protected set; }
        
        protected Entity(int x, int y, string symbol) 
        { 
            X = x; 
            Y = y; 
            Symbol = symbol; 
        }
    }
}