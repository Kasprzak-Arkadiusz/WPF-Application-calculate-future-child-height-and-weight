using System;

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
                throw new ArgumentException("Pole wiek jest wymagane.");
            }
            if (height == "")
            {
                throw new ArgumentException("Pole wzrost jest wymagane.");
            }
            if (weight == "")
            {
                throw new ArgumentException("Pole waga jest wymagane.");
            }
        }

        private static void CheckIfChildSexIsChosen(bool? isBoyButtonClicked, bool? isGirlButtonClicked)
        {
            if (isBoyButtonClicked == isGirlButtonClicked)
            {
                throw new ArgumentException("Płeć dziecka nie została wybrana.");
            }

        }

        private static void CheckIfValuesAreCorrectDataTypes(string age, string height, string weight)
        {
            if (!int.TryParse(age, out _))
            {
                throw new ArgumentException("Podany wiek nie jest liczbą całkowitą.");
            }

            if (!int.TryParse(height, out _))
            {
                throw new ArgumentException("Podany wzrost nie jest liczbą całkowitą.");
            }

            if (!int.TryParse(weight, out _))
            {
                throw new ArgumentException("Podana waga nie jest liczbą całkowitą.");
            }
        }

        private static void CheckIfAgeIsInRange(string age)
        {
            int numericAge = int.Parse(age);

            if (numericAge < 3 || numericAge > 17)
            {
                throw new ArgumentException("Wiek powinien być liczbą z zakresu od 3 do 17.");
            }
        }
    }
}