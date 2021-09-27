using System;
using System.Collections.Generic;
using MenuSystem.Enum;

namespace MenuSystem
{
    // https://gitlab.cs.ttu.ee/rolaur/icd0008-2020f/-/blob/master/hw1-menu/MenuSystem/MenuSystem/Menu.cs
    public class Menu
    {
        private readonly List<MenuItem> _menuItems = new();
        public MenuLevel Level { get; private set; }
        private readonly List<String> _reservedCharacters = new() { "M", "R", "X" }; // main, exit, return 

        public Menu(MenuLevel menuLevel)
        {
            Level = menuLevel;
        }
        public string Run()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(OutputMenu(_menuItems));
            
            Console.ForegroundColor = ConsoleColor.DarkGray;
            var predefinedItems = GetPredefinedOptions();
            Console.WriteLine(OutputMenu(predefinedItems));
            Console.ResetColor();
            Console.WriteLine("Your choice: ");
            var userChoice = Console.ReadLine()?.Trim().ToLower() ?? "";
            
            return "";
        }

        private String OutputMenu(List<MenuItem> items)
        {
            List<String> menu = new();
            foreach (MenuItem item in items)
            {
                menu.Add(item.ToString());
            }

            return String.Join("\n", menu);
        }

        public void Add(MenuItem item)
        {
            foreach (var menuItem in _menuItems)
            {
                if (menuItem.UserChoiceCharacter == item.UserChoiceCharacter)
                {
                    throw new ApplicationException($"Duplicate UserChoiceCharacter found. " +
                                                $"UserChoiceCharacter \"{item.UserChoiceCharacter}\" already exists in current menu.");
                }
            }
            if (item.UserChoiceCharacter != null && _reservedCharacters.Contains(item.UserChoiceCharacter.ToUpper()))
            {
                throw new ApplicationException($"Can not use reserved character \"{item.UserChoiceCharacter}\".\n");
            }
            _menuItems.Add(item);
        }

        private List<MenuItem> GetPredefinedOptions()
        {
            List<MenuItem> items = new();

            switch (Level)
            {
               case MenuLevel.Level0:
                   items.Add(new MenuItem("M", "Main", () => ""));
                   break;
               case MenuLevel.Level1:
                   items.Add(new MenuItem("R", "Return", () => ""));
                   items.Add(new MenuItem("M", "Main", () => ""));
                   break;
                case MenuLevel.Level2Plus:
                    items.Add(new MenuItem("X", "Exit", () => ""));
                    break;
            }
            
            return items;
        }
    }
}