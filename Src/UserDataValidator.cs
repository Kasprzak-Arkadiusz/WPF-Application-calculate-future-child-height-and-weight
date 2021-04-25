using System;

using str = ProjektIndywidualny.Properties.strings;

namespace ProjektIndywidualny.Src
{
    public static class UserDataValidator
    {
        public static void AreUserDataCorrect(string age, string height, string weight,
            bool? isBoyButtonChecked, bool? isGirlButtonChecked)
        {
            CheckIfChildSexIsChosen(isBoyButtonChecked, isGirlButtonChecked);
            CheckIfFieldsAreFilled(age, height, weight);
            CheckIfValuesAreCorrectDataTypes(age, height, weight);
            CheckIfAgeIsInRange(age);
        }

        private static void CheckIfFieldsAreFilled(string age, string height, string weight)
        {
            if (age == "")
            {
                throw new ArgumentException(str.Field + str.Age + str.IsRequired);
            }
            if (height == "")
            {
                throw new ArgumentException(str.Field + str.Height + str.IsRequired);
            }
            if (weight == "")
            {
                throw new ArgumentException(str.Field + str.Weight + str.IsRequired);
            }
        }

        private static void CheckIfChildSexIsChosen(bool? isBoyButtonClicked, bool? isGirlButtonClicked)
        {
            if (isBoyButtonClicked == isGirlButtonClicked)
            {
                throw new ArgumentException(str.ChildGenderIsNotChosen);
            }

        }

        private static void CheckIfValuesAreCorrectDataTypes(string age, string height, string weight)
        {
            if (!int.TryParse(age, out _))
            {
                throw new ArgumentException(str.Given + str.Parameter + str.Age + str.IsNotAnInt);
            }

            if (!int.TryParse(height, out _))
            {
                throw new ArgumentException(str.Given + str.Parameter + str.Height + str.IsNotAnInt);
            }

            if (!int.TryParse(weight, out _))
            {
                throw new ArgumentException(str.Given + str.Parameter + str.Age + str.IsNotAnInt);
            }
        }

        private static void CheckIfAgeIsInRange(string age)
        {
            int numericAge = int.Parse(age);

            if (numericAge < 3 || numericAge > 17)
            {
                throw new ArgumentException(str.AgeIsNotInRange + str.From + "3" + str.To + "17.");
            }
        }
    }
}