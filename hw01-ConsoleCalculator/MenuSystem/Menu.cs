using System;
using System.Collections.Generic;
using System.Linq;
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

            var isInputValid = false;
            var userChoice = "";
            do
            {
                AddPredefinedOptions();

                Console.WriteLine(OutputMenu(_menuItems));
                Console.Write("Your choice: ");
                userChoice = Console.ReadLine()?.Trim().ToUpper() ?? "";
                isInputValid = _menuItems.Any(x => x.UserChoiceCharacter == userChoice);
                if (isInputValid)
                {
                    _menuItems.First(x => x.UserChoiceCharacter == userChoice).MethodToExecute?.Invoke();
                }
                else
                {
                    Console.WriteLine($"Unknown input \"{userChoice}\". Please try again.");
                }
                
            } while (!isInputValid);
            
            return userChoice;
        }

        private String OutputMenu(List<MenuItem> items)
        {
            List<String> menu = new();
            foreach (MenuItem item in items)
            {
                if (_reservedCharacters.Contains(item.UserChoiceCharacter))
                {
                    menu.Add(item.ToString());
                } else
                {
                    menu.Add(item.ToString());
                }
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
            switch (Level)
            {
               case MenuLevel.Level0:
                   _menuItems.Add(new MenuItem("X", "Exit", null));
                   break;
               case MenuLevel.Level1:
                   _menuItems.Add(new MenuItem("M", "Main", () => ""));
                   _menuItems.Add(new MenuItem("X", "Exit", null));
                   break;
                case MenuLevel.Level2Plus:
                    _menuItems.Add(new MenuItem("R", "Return", () => ""));
                    _menuItems.Add(new MenuItem("M", "Main", () => ""));
                    _menuItems.Add(new MenuItem("X", "Exit", null));
                    break;
            }
        }
    }
}