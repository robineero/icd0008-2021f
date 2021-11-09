namespace BattleshipBrain
{
    
    // Configuration for game board
    public class Config
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public bool Random { get; set; }

        public Config(int width, int height, bool random = false)
        {
            Width = width;
            Height = height;
            Random = random;
        }
    }

}