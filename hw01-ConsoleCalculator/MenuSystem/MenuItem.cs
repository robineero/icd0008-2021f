using System;
using System.Collections.Generic;

namespace MenuSystem
{
    public class MenuItem
    {
        public virtual string LabelText { get; private set; }
        public virtual string UserChoiceCharacter { get; private set; }
        public virtual Func<string> MethodToExecute { get; set; } // can not be null. Action is parameter type - no return.
        
        public MenuItem(string userChoice, string labelText,  Func<string> methodToExecute)
        {
            ValidateConstructorParameters(userChoice, labelText);
    

            LabelText = labelText.Trim();
            UserChoiceCharacter = userChoice.Trim();
            MethodToExecute = methodToExecute;
        }

        public override string ToString()
        {
            return $"{UserChoiceCharacter}) {LabelText}";
        }

        public static bool ValidateConstructorParameters(string? userChoice, string? labelText)
        {
            String message = "\n";
            Boolean isCorrect = true;
            
            
            if (userChoice == null || userChoice.Length != 1)
            {
                message += "User choice character can not be null and must have lenght of exactly 1 character.\n";
                isCorrect = false;
            }            
            if (labelText == null || labelText.Length < 1)
            {
                message += "Label text can not be null nor empty string.\n";
                isCorrect = false;
            }
            
            if (!isCorrect)
            {
                throw new ArgumentException(message);
            }

            return isCorrect;
        }

    }
}