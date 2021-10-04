using MenuSystem;
using MenuSystem.Enum;

namespace ConsoleAppMenu
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu second = new(MenuLevel.Level2Plus, "---- Level 2 ----");
            second.Add(new MenuItem("A", "This", null));
            second.Add(new MenuItem("B", "That (does nothing)", () => ""));
            
            Menu first = new(MenuLevel.Level1, "---- Level 1 ----");
            first.Add(new MenuItem("A", "This", second.Run));
            first.Add(new MenuItem("B", "That (does nothing)", () => ""));
            
            Menu menu = new(MenuLevel.Level0, "---- Level 0 ----");
            menu.Add(new MenuItem("A", "This", first.Run));
            menu.Add(new MenuItem("B", "That (does nothing)", () => ""));
            menu.Run();
        }
    }
}