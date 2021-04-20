using System;
using System.Windows;

namespace ProjektIndywidualny.Src
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void EstimateBtn_Clicked(object sender, RoutedEventArgs e)
        {
            string age = AgeTextBox.Text;
            string height = HeightTextBox.Text;
            string weight = WeightTextBox.Text;

            if (!UserDataValidator.AreUserDataCorrect(age, height, weight, BoyRadioButton.IsChecked,
                GirlRadioButton.IsChecked))
            {
                return;
            }

            string weightFileName = WeightFileTextBox.Text;
            string heightFileName = HeightFileTextBox.Text;
            GrowthChart growthChart = new GrowthChart();
            Chart heightChart = growthChart.HeightChart;
            string[] heightLabels = growthChart.HeightLabels;
            Chart weightChart = growthChart.WeightChart;
            string[] weightLabels = growthChart.WeightLabels;

            AlertWindow alertBox = new AlertWindow();
            try
            {
                FileDataLoader.LoadDefaultData(weightFileName, out weightChart, out weightLabels);
                FileDataValidator.CheckIfFileDataAreCorrect(ref weightChart);
                FileDataLoader.LoadDefaultData(heightFileName, out heightChart, out heightLabels);
                FileDataValidator.CheckIfFileDataAreCorrect(ref heightChart);
            }
            catch (Exception exception)
            {
                alertBox.Show("Błędne dane w pliku",exception.Message);
            }
        }
    }
}