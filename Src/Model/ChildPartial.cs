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
                        if (Age < 3 || Age > 17)
                        {
                            AddError(nameof(Age), str.AgeIsNotInRange + str.From + "3" + str.To + "17.");
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