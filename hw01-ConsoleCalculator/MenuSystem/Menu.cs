using System;
using System.Collections.Generic;
using System.Linq;
using MenuSystem.Enum;

namespace MenuSystem
{ 
    public class Menu
    {
        private String MenuTitle { get; set; }
        private readonly List<MenuItem> _menuItems = new();
        public MenuLevel Level { get; private set; }
        private readonly List<String> _reservedCharacters = new() { "M", "R", "X" }; // main, return, exit 

        public Menu(MenuLevel menuLevel, string menuTitle)
        {
            Level = menuLevel;
            MenuTitle = menuTitle;
        }
        public string Run()
        {
            Boolean runDone;
            Boolean menuContainsSuchCharacter;
            String userInput;

            do
            {
                AddPredefinedOptions();
                Console.WriteLine(MenuTitle);
                Console.WriteLine(OutputMenu(_menuItems));
                Console.Write("Your choice: ");
                
                userInput = Console.ReadLine()?.Trim().ToUpper() ?? "";
                
                menuContainsSuchCharacter = _menuItems.Any(x => x.UserChoiceCharacter == userInput);
                
                // if is special character and current menu contains the character
                runDone = _reservedCharacters.Contains(userInput) && menuContainsSuchCharacter;
                if (runDone) Console.WriteLine($"Rundone with {userInput}...");
                
                if (!runDone && menuContainsSuchCharacter)
                {
                    MenuItem item = _menuItems.First(x => x.UserChoiceCharacter == userInput);
                    userInput = item.MethodToExecute == null ? userInput : item.MethodToExecute();
                }
                
                // if (userInput == "X" && menuContainsSuchCharacter)
                // { 
                //     if (Level == MenuLevel.Level0)
                //         Console.WriteLine("Closing...");
                //     break;
                // }

                if (userInput == "M" && menuContainsSuchCharacter && Level != MenuLevel.Level0)
                {
                    break;
                }
                
                if (!runDone && !menuContainsSuchCharacter)
                    Console.WriteLine($"Unknown input \"{userInput}\". Please try again.");
                
            } while (!runDone);
            
            if (runDone && userInput == "R")
            {
                return "";
            }

            Console.WriteLine($"userChoice return: {userInput}");
            return userInput;
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

        private void AddPredefinedOptions()
        {
            var exit = new MenuItem("X", "Exit", null);
            var main = new MenuItem("M", "Main", null);
            var r = new MenuItem("R", "Return", null);
            switch (Level)
            {
               case MenuLevel.Level0:
                   AddItemToMenu(exit);
                   break;
               case MenuLevel.Level1:
                   AddItemToMenu(main);
                   AddItemToMenu(exit);
                   break;
                case MenuLevel.Level2Plus:
                    AddItemToMenu(r);
                    AddItemToMenu(main);
                    AddItemToMenu(exit);
                    break;
            }
        }

        private void AddItemToMenu(MenuItem item)
        {
            if (_menuItems.All(x => x.UserChoiceCharacter != item.UserChoiceCharacter))
            {
                _menuItems.Add(item);
            }
        }
    }
}