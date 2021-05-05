using System.ComponentModel;

namespace ProjektIndywidualny.Model
{
    public partial class Child : INotifyPropertyChanged
    {
        private int _age;
        public int Age
        {
            get => _age;
            set
            {
                if (value == _age) return;
                _age = value;
                OnPropertyChanged(nameof(Age));
            }
        }

        private int _currentHeight;
        public int CurrentHeight
        {
            get => _currentHeight;
            set
            {
                if (value == _currentHeight) return;
                _currentHeight = value;
                OnPropertyChanged(nameof(CurrentHeight));
            }
        }

        private int _currentWeight;
        public int CurrentWeight
        {
            get => _currentWeight;
            set
            {
                if (value == _currentWeight) return;
                _currentWeight = value;
                OnPropertyChanged(nameof(CurrentWeight));
            }
        }

        private bool _isBoy;
        public bool IsBoy
        {
            get => _isBoy;
            set
            {
                if (value == _isBoy) return;
                _isBoy = value;
                OnPropertyChanged(nameof(IsBoy));
            }
        }

        private int _estimatedHeight;
        public int EstimatedHeight
        {
            get => _estimatedHeight;
            set
            {
                if (value == _estimatedHeight) return;
                _estimatedHeight = value;
                OnPropertyChanged(nameof(EstimatedHeight));
            }
        }

        private int _estimatedWeight;
        public int EstimatedWeight
        {
            get => _estimatedWeight;
            set
            {
                if (value == _estimatedWeight) return;
                _estimatedWeight = value;
                OnPropertyChanged(nameof(EstimatedWeight));
            }
        }

        public Child()
        {
            Age = 3;
            CurrentHeight = 0;
            CurrentWeight = 0;
            IsBoy = true;
            EstimatedHeight = 0;
            EstimatedWeight = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}