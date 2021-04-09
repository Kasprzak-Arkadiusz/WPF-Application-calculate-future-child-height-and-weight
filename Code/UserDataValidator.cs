namespace ProjektIndywidualny.Code
{
    public static class UserDataValidator
    {
        private static readonly AlertWindow Alert = new AlertWindow();

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
            bool fieldsAreFilled = true;
            if (age == "")
            {
                Alert.Show("Pole wiek jest wymagane.");
                fieldsAreFilled = false;
            }
            else if (height == "")
            {
                Alert.Show("Pole wzrost jest wymagane.");
                fieldsAreFilled = false;
            }
            else if (weight == "")
            {
                Alert.Show("Pole waga jest wymagane.");
                fieldsAreFilled = false;
            }

            return fieldsAreFilled;
        }

        private static bool CheckIfChildSexIsChosen(bool? isBoyButtonClicked, bool? isGirlButtonClicked)
        {
            if (isBoyButtonClicked == isGirlButtonClicked)
            {
                Alert.Show("Płeć dziecka nie została wybrana.");
                return false;
            }

            return true;
        }

        private static bool CheckIfValuesAreCorrectDataTypes(string age, string height, string weight)
        {
            if (!int.TryParse(age, out _))
            {
                Alert.Show("Podany wiek nie jest liczbą całkowitą.");
                return false;
            }

            if (!int.TryParse(height, out _))
            {
                Alert.Show("Podany wzrost nie jest liczbą całkowitą.");
                return false;
            }

            if (!int.TryParse(weight, out _))
            {
                Alert.Show("Podana waga nie jest liczbą całkowitą.");
                return false;
            }

            return true;
        }

        private static bool CheckIfAgeIsInRange(string age)
        {
            int numericAge = int.Parse(age);

            if (numericAge < 3 || numericAge > 17)
            {
                Alert.Show("Wiek powinien być liczbą z zakresu od 3 do 17.");
                return false;
            }

            return true;
        }
    }
}