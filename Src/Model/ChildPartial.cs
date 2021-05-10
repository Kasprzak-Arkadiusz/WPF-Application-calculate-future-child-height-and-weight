using System.ComponentModel;
using str = ProjektIndywidualny.Properties.strings;

namespace ProjektIndywidualny.Model
{
    public partial class Child : NotifyDataError, IDataErrorInfo
    {
        public string this[string columnName]
        {
            get
            {
                bool hasError = false;
                switch (columnName)
                {
                    case nameof(Age):
                        const int minAge = 3;
                        const int maxAge = 17;
                        if (Age < minAge || Age > maxAge)
                        {
                            AddError(nameof(Age), str.AgeIsNotInRange + str.From + minAge + str.To + maxAge + ".");
                            hasError = true;
                        }

                        if (!hasError) ClearErrors(nameof(Age));
                        break;
                    case nameof(CurrentHeight):
                        break;
                    case nameof(CurrentWeight):
                        break;
                }

                return string.Empty;
            }
        }

        public string Error { get; }
    }
}