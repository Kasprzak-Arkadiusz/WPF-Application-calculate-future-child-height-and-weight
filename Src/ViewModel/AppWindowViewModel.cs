using System;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using ProjektIndywidualny.Cmds;
using ProjektIndywidualny.Model;
using ProjektIndywidualny.View;
using str = ProjektIndywidualny.Properties.strings;

namespace ProjektIndywidualny.ViewModel
{
    internal class AppWindowViewModel
    {
        private RelayCommand _estimateCmd;
        private RelayCommand<TextBox> _setDefaultHeightFileCmd;
        private RelayCommand<TextBox> _setDefaultWeightFileCmd;
        private RelayCommand<TextBox> _setCustomHeightFileCmd;
        private RelayCommand<TextBox> _setCustomWeightFileCmd;

        public ICommand SetDefaultHeightFileCmd => _setDefaultHeightFileCmd ?? (_setDefaultHeightFileCmd =
            new RelayCommand<TextBox>(SetDefaultHeightFile, CanSetDefaultHeightFile));

        public ICommand SetDefaultWeightFileCmd => _setDefaultWeightFileCmd ?? (_setDefaultWeightFileCmd =
            new RelayCommand<TextBox>(SetDefaultWeightFile, CanSetDefaultWeightFile));

        public ICommand SetCustomHeightFileCmd => _setCustomHeightFileCmd ?? (_setCustomHeightFileCmd =
            new RelayCommand<TextBox>(SetCustomHeightFile, f => true));

        public ICommand SetCustomWeightFileCmd => _setCustomWeightFileCmd ?? (_setCustomWeightFileCmd =
            new RelayCommand<TextBox>(SetCustomWeightFile, f => true));

        public ICommand EstimateCmd => _estimateCmd ?? (_estimateCmd = new RelayCommand(Estimate, () => true));

        public Child Child { get; }
        public FileDataLoader FileDataLoader { get; }
        private GrowthChart GrowthChart { get; }

        public AppWindowViewModel()
        {
            Child = new Child();
            GrowthChart = new GrowthChart();
            FileDataLoader = new FileDataLoader();
        }

        private void SetDefaultHeightFile(TextBox heightBox)
        {
            FileDataLoader.IsDefaultHeightFile = true;
            heightBox.Text = Child.IsBoy ? str.DefaultBoyHeightGrowthChart : str.DefaultGirlHeightGrowthChart;
        }

        private bool CanSetDefaultHeightFile(TextBox heightBox)
        {
            return heightBox.Text != str.DefaultBoyHeightGrowthChart &&
                   heightBox.Text != str.DefaultGirlHeightGrowthChart;
        }

        private void SetDefaultWeightFile(TextBox weightBox)
        {
            FileDataLoader.IsDefaultWeightFile = true;
            weightBox.Text = Child.IsBoy ? str.DefaultBoyWeightGrowthChart : str.DefaultGirlWeightGrowthChart;
        }

        private bool CanSetDefaultWeightFile(TextBox weightBox)
        {
            return weightBox.Text != str.DefaultBoyWeightGrowthChart &&
                   weightBox.Text != str.DefaultGirlWeightGrowthChart;
        }

        private void SetCustomHeightFile(TextBox box)
        {
            if (SetCustomFile(box) ?? false)
            {
                FileDataLoader.IsDefaultHeightFile = false;
            }
        }

        private void SetCustomWeightFile(TextBox box)
        {
            if (SetCustomFile(box) ?? false)
            {
                FileDataLoader.IsDefaultWeightFile = false;
            }
        }

        private bool? SetCustomFile(TextBox box)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string path = System.IO.Path.GetDirectoryName(asm.Location);
            path += @"\bin\Debug";
            OpenFileDialog dlg = new OpenFileDialog
                {DefaultExt = ".txt", Filter = "Text documents (.txt)|*.txt", InitialDirectory = path};
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                box.Text = dlg.FileName;
            }

            return result;
        }

        private void Estimate()
        {
            try
            {
                if (FileDataLoader.IsDefaultHeightFile)
                {
                    FileDataLoader.LoadDefaultData(FileDataLoader.HeightFileName, out Chart heightChart,
                        out string[] heightLabels);
                    FileDataValidator.CheckIfFileDataAreCorrect(ref heightChart);
                    GrowthChart.HeightChart = heightChart;
                    GrowthChart.HeightLabels = heightLabels;
                }
                else
                {
                    FileDataLoader.LoadCustomData(FileDataLoader.HeightFileName, out Chart heightChart,
                        out string[] heightLabels);
                    FileDataValidator.CheckIfFileDataAreCorrect(ref heightChart);
                    GrowthChart.HeightChart = heightChart;
                    GrowthChart.HeightLabels = heightLabels;
                }

                if (FileDataLoader.IsDefaultWeightFile)
                {
                    FileDataLoader.LoadDefaultData(FileDataLoader.WeightFileName, out Chart weightChart,
                        out string[] weightLabels);
                    FileDataValidator.CheckIfFileDataAreCorrect(ref weightChart);
                    GrowthChart.WeightChart = weightChart;
                    GrowthChart.WeightLabels = weightLabels;
                }
                else
                {
                    FileDataLoader.LoadCustomData(FileDataLoader.WeightFileName, out Chart weightChart,
                        out string[] weightLabels);
                    FileDataValidator.CheckIfFileDataAreCorrect(ref weightChart);
                    GrowthChart.WeightChart = weightChart;
                    GrowthChart.WeightLabels = weightLabels;
                }

                Child.EstimatedHeight =
                    Estimator.EstimateChildParameter(GrowthChart.HeightChart.Plots, Child.CurrentHeight, Child.Age, 18,
                        nameof(Child.CurrentHeight));
                Child.EstimatedWeight =
                    Estimator.EstimateChildParameter(GrowthChart.WeightChart.Plots, Child.CurrentWeight, Child.Age, 18,
                        nameof(Child.CurrentWeight));
            }
            catch (Exception e)
            {
                //Temporary solution
                AlertWindow alert = new AlertWindow();
                alert.Show(str.ErrorInFile, e.Message);
                Child.EstimatedHeight = 0;
                Child.EstimatedWeight = 0;
            }
        }
    }
}