﻿using System;
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

            MessageBox.Show("Wartości pól:\n"
                            + "wiek: " + age + "\n"
                            + "wzrost: " + height + "\n"
                            + "waga: " + weight);

            string weightFileName = WeightFileTextBox.Text;
            GrowthChart growthChart = new GrowthChart();
            Chart heightChart = growthChart.HeightChart;
            string[] heightLabels = growthChart.HeightLabels;
            Chart weightChart = growthChart.WeightChart;
            string[] weightLabels = growthChart.WeightLabels;

            try
            {
                FileDataLoader.LoadDefaultData(weightFileName, out heightChart, out weightLabels);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        /*private void BoyRadioButton_Clicked(object sender, RoutedEventArgs e)
        {
            GirlRadioButton.IsChecked = false;

            if (HeightFileTextBox.Text == "DefaultGirlHeightGrowthChart.txt")
            {
                HeightFileTextBox.Text = "DefaultBoyHeightGrowthChart.txt";
            }

            if (WeightFileTextBox.Text == "DefaultGirlWeightGrowthChart.txt")
            {
                WeightFileTextBox.Text = "DefaultBoyWeightGrowthChart.txt";
            }
        }

        private void GirlRadioButton_Clicked(object sender, RoutedEventArgs e)
        {
            BoyRadioButton.IsChecked = false;

            if (HeightFileTextBox.Text == "DefaultBoyHeightGrowthChart.txt")
            {
                HeightFileTextBox.Text = "DefaultGirlHeightGrowthChart.txt";
            }

            if (WeightFileTextBox.Text == "DefaultBoyWeightGrowthChart.txt")
            {
                WeightFileTextBox.Text = "DefaultGirlWeightGrowthChart.txt";
            }
        }*/
    }
}