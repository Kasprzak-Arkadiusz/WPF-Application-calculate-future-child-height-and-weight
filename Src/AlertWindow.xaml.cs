using System;
using System.Windows;

namespace ProjektIndywidualny.Src
{
    public partial class AlertWindow
    {
        public AlertWindow()
        {
            InitializeComponent();
        }

        public void Show(string windowTitle, string message)
        {
            this.Title = windowTitle;
            this.MessageTextBlock.Text = message;
            this.Show();
        }

        private void OkButton_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            Window window = (Window)sender;
            window.Topmost = true;
        }
    }
}
