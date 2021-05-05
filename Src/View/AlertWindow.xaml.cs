using System;
using System.Windows;

namespace ProjektIndywidualny.View
{
    public partial class AlertWindow
    {
        public AlertWindow()
        {
            InitializeComponent();
        }

        public void Show(string windowTitle, string message)
        {
            Title = windowTitle;
            MessageTextBlock.Text = message;
            Show();
        }

        private void OkButton_Clicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            Window window = (Window)sender;
            window.Topmost = true;
        }
    }
}
