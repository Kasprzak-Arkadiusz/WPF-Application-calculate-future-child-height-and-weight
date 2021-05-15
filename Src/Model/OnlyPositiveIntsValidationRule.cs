using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using str = ProjektIndywidualny.Properties.strings;

public class OnlyPositiveIntsValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        var result = new ValidationResult(true, null);

        const string numberPattern = @"^[0-9]+$";
        Regex rgx = new Regex(numberPattern);

        if (value != null && rgx.IsMatch(value.ToString()) == false )
        {
            result = new ValidationResult(false, str.ValueIsNotInt);
        }

        return result;
    }
}