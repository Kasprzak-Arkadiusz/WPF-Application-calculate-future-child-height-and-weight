using System;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows;
using Microsoft.Win32;

using str = ProjektIndywidualny.Properties.strings; 

namespace ProjektIndywidualny.Src
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            Console.WriteLine(Properties.strings.AgeIsNotInRange);
            Console.WriteLine("CurrentCulture is {0}.", CultureInfo.CurrentCulture.Name);
        }

        private void EstimateBtn_Clicked(object sender, RoutedEventArgs e) 
        {
            string age = AgeTextBox.Text;
            string height = HeightTextBox.Text;
            string weight = WeightTextBox.Text;

            AlertWindow alertBox = new AlertWindow();

            try
            {
                UserDataValidator.AreUserDataCorrect(age, height, weight, BoyRadioButton.IsChecked,
                    GirlRadioButton.IsChecked);
            }
            catch (Exception e1)
            {
                alertBox.Show(str.IncorrectData, e1.Message);
                return;
            }

            Child child = new Child(int.Parse(age), int.Parse(height), int.Parse(weight));

            string weightFileName = WeightFileTextBox.Text;
            string heightFileName = HeightFileTextBox.Text;
            GrowthChart growthChart = new GrowthChart();

            try
            {
                FileDataLoader.LoadDefaultData(weightFileName, out Chart weightChart, out string[] weightLabels);
                FileDataValidator.CheckIfFileDataAreCorrect(ref weightChart);
                FileDataLoader.LoadDefaultData(heightFileName, out Chart heightChart, out string[] heightLabels);
                FileDataValidator.CheckIfFileDataAreCorrect(ref heightChart);
                growthChart = new GrowthChart(heightChart, heightLabels, weightChart, weightLabels);
            }

            catch (ArgumentException e2)
            {
                alertBox.Show(str.IncorrectDataInFile, e2.Message);
                return;
            }

            try
            {
                child.EstimatedHeight =
                    Estimator.EstimateChildParameter(growthChart.HeightChart.Plots, child.CurrentHeight, child.Age, 18);
                EstHeightTextBox.Text = child.EstimatedHeight.ToString();
                child.EstimatedWeight =
                    Estimator.EstimateChildParameter(growthChart.WeightChart.Plots, child.CurrentWeight, child.Age, 18);
                EstWeightTextBox.Text = child.EstimatedWeight.ToString();
            }
            catch (ArgumentException e3)
            {
                EstHeightTextBox.Text = "";
                EstWeightTextBox.Text = "";
                alertBox.Show(str.IncorrectData, e3.Message);
                return;
            }
        }

        private void WeightFileBtn_Clicked(object sender, RoutedEventArgs e)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string path = System.IO.Path.GetDirectoryName(asm.Location);
            path += @"\bin\Debug";
            OpenFileDialog dlg = new OpenFileDialog {DefaultExt = ".txt", Filter = "Text documents (.txt)|*.txt", InitialDirectory = path};
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                WeightFileTextBox.Text = dlg.FileName;
            }
        }

        private void HeightFileBtn_Clicked(object sender, RoutedEventArgs e)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string path = System.IO.Path.GetDirectoryName(asm.Location);
            path += @"\bin\Debug";
            OpenFileDialog dlg = new OpenFileDialog {DefaultExt = ".txt", Filter = "Text documents (.txt)|*.txt", InitialDirectory = path};
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                HeightFileTextBox.Text = dlg.FileName;
            }
        }
    }
}