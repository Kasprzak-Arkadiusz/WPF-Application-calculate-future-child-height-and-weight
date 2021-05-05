using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using str = ProjektIndywidualny.Properties.strings;

public class OnlyNumbersValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        var result = new ValidationResult(true, null);

        const string NumberPattern = @"^[0-9]+$";
        Regex rgx = new Regex(NumberPattern);

        if (rgx.IsMatch(value.ToString()) == false )
        {
            result = new ValidationResult(false, str.ValueIsNotInt);
        }

        return result;
    }
}