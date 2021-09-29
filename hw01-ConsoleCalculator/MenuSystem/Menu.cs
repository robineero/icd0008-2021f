using System;
using System.Collections.Generic;
using System.Linq;
using MenuSystem.Enum;

namespace MenuSystem
{
    // https://gitlab.cs.ttu.ee/rolaur/icd0008-2020f/-/blob/master/hw1-menu/MenuSystem/MenuSystem/Menu.cs
    public class Menu
    {
        private String MenuTitle { get; set; }
        private readonly List<MenuItem> _menuItems = new();
        public MenuLevel Level { get; private set; }
        private readonly List<String> _reservedCharacters = new() { "M", "R", "X" }; // main, exit, return 

        public Menu(MenuLevel menuLevel, string menuTitle)
        {
            Level = menuLevel;
            MenuTitle = menuTitle;
        }
        public string Run()
        {
            Boolean runDone;
            Boolean isInputValid;
            String userChoice;
            var predefinedOptionsIncluded = false;
            do
            {
                if (!predefinedOptionsIncluded)
                {
                    AddPredefinedOptions();
                    predefinedOptionsIncluded = true;
                }

                Console.WriteLine(MenuTitle);
                Console.WriteLine(OutputMenu(_menuItems));
                Console.Write("Your choice: ");
                userChoice = Console.ReadLine()?.Trim().ToUpper() ?? "";
                isInputValid = _menuItems.Any(x => x.UserChoiceCharacter == userChoice);
                if (isInputValid)
                {
                    MenuItem item = _menuItems.First(x => x.UserChoiceCharacter == userChoice);
                    userChoice = item.MethodToExecute == null ? userChoice : item.MethodToExecute();
                }

                runDone = _menuItems.Any(x => x.UserChoiceCharacter.Equals(userChoice)) && _reservedCharacters.Contains(userChoice);
                
                if (!runDone && !isInputValid)
                    Console.WriteLine($"Unknown input \"{userChoice}\". Please try again.");
                
            } while (!runDone);

            if (runDone && userChoice == "M")
            {
                return "";
            }            
            if (runDone && userChoice == "R")
            {
                return "";
            }
            return userChoice;
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
                   _menuItems.Add(exit);
                   break;
               case MenuLevel.Level1:
                   _menuItems.Add(main);
                   _menuItems.Add(exit);
                   break;
                case MenuLevel.Level2Plus:
                    
                    _menuItems.Add(r);
                    _menuItems.Add(main);
                    _menuItems.Add(exit);
                    break;
            }
        }
    }
}