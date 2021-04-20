namespace ProjektIndywidualny.Src
{
    public static class UserDataValidator
    {
        private const string windowTitle = "Błędne dane";
        public static bool AreUserDataCorrect(string age, string height, string weight,
            bool? isBoyButtonChecked, bool? isGirlButtonChecked)
        {
            if (!CheckIfChildSexIsChosen(isBoyButtonChecked, isGirlButtonChecked))
            {
                return false;
            }

            if (!CheckIfFieldsAreFilled(age, height, weight))
            {
                return false;
            }

            if (!CheckIfValuesAreCorrectDataTypes(age, height, weight))
            {
                return false;
            }

            if (!CheckIfAgeIsInRange(age))
            {
                return false;
            }

            return true;
        }

        private static bool CheckIfFieldsAreFilled(string age, string height, string weight)
        {
            AlertWindow alert = new AlertWindow();

            bool fieldsAreFilled = true;
            if (age == "")
            {
                alert.Show(windowTitle,"Pole wiek jest wymagane.");
                fieldsAreFilled = false;
            }
            else if (height == "")
            {
                alert.Show(windowTitle,"Pole wzrost jest wymagane.");
                fieldsAreFilled = false;
            }
            else if (weight == "")
            {
                alert.Show(windowTitle,"Pole waga jest wymagane.");
                fieldsAreFilled = false;
            }

            return fieldsAreFilled;
        }

        private static bool CheckIfChildSexIsChosen(bool? isBoyButtonClicked, bool? isGirlButtonClicked)
        {
            if (isBoyButtonClicked == isGirlButtonClicked)
            {
                AlertWindow alert = new AlertWindow();
                alert.Show(windowTitle,"Płeć dziecka nie została wybrana.");
                return false;
            }

            return true;
        }

        private static bool CheckIfValuesAreCorrectDataTypes(string age, string height, string weight)
        {
            AlertWindow alert = new AlertWindow();

            if (!int.TryParse(age, out _))
            {
                alert.Show(windowTitle,"Podany wiek nie jest liczbą całkowitą.");
                return false;
            }

            if (!int.TryParse(height, out _))
            {
                alert.Show(windowTitle,"Podany wzrost nie jest liczbą całkowitą.");
                return false;
            }

            if (!int.TryParse(weight, out _))
            {
                alert.Show(windowTitle,"Podana waga nie jest liczbą całkowitą.");
                return false;
            }

            return true;
        }

        private static bool CheckIfAgeIsInRange(string age)
        {
            AlertWindow alert = new AlertWindow();
            int numericAge = int.Parse(age);

            if (numericAge < 3 || numericAge > 17)
            {
                alert.Show(windowTitle,"Wiek powinien być liczbą z zakresu od 3 do 17.");
                return false;
            }

            return true;
        }
    }
}