namespace Site_jocuri.Models.Pacman
{
    public class Player : Entity
    {
        public int HP { get; set; } = 3;
        public int Points { get; set; } = 0;
        public int Stamina { get; set; } = 50;
        
        public Player(int x, int y) : base(x, y, "@") { }
    }
}