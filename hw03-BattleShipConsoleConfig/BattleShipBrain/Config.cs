namespace BattleShipBrain
{
    
    // Configuration for game board
    public class Config
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public bool Random { get; private set; }

        public Config(int width, int height, bool random = false)
        {
            Width = width;
            Height = height;
            Random = random;
        }
    }
}