using ProjektIndywidualny.ViewModel;

namespace ProjektIndywidualny.View
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new AppWindowViewModel();
        }
    }
}