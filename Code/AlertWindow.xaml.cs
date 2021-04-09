using System.Windows;

namespace ProjektIndywidualny.Code
{
    public partial class AlertWindow
    {
        public AlertWindow()
        {
            InitializeComponent();
        }

        public void Show(string message)
        {
            AlertWindow window = new AlertWindow();
            window.MessageTextBlock.Text = message;
            window.Show();
        }

        private void OkButton_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
