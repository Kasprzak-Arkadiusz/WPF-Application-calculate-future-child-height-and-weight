using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using ProjektIndywidualny.Cmds;
using ProjektIndywidualny.Model;
using ProjektIndywidualny.View;
using ScottPlot;
using Image = System.Windows.Controls.Image;
using str = ProjektIndywidualny.Properties.strings;

namespace ProjektIndywidualny.ViewModel
{
    internal class AppWindowViewModel
    {
        private RelayCommand _setDefaultHeightFileCmd;
        private RelayCommand _setDefaultHeightFileByRadioButtonCmd;
        private RelayCommand _setDefaultWeightFileCmd;
        private RelayCommand<TextBox> _setCustomHeightFileCmd;
        private RelayCommand<TextBox> _setCustomWeightFileCmd;
        private RelayCommand<Image> _estimateCmd;
        private RelayCommand<Image> _changeImageCmd;

        public ICommand SetDefaultHeightFileCmd => _setDefaultHeightFileCmd ?? (_setDefaultHeightFileCmd =
            new RelayCommand(SetDefaultHeightFile, CanSetDefaultHeightFile));

        public ICommand SetDefaultHeightFileByRadioButtonCmd => _setDefaultHeightFileByRadioButtonCmd ??
                                                                (_setDefaultHeightFileByRadioButtonCmd =
                                                                    new RelayCommand(SetDefaultHeightFileByRadioButton,
                                                                        () => true));

        public ICommand SetDefaultWeightFileCmd => _setDefaultWeightFileCmd ?? (_setDefaultWeightFileCmd =
            new RelayCommand(SetDefaultWeightFile, CanSetDefaultWeightFile));

        public ICommand SetCustomHeightFileCmd => _setCustomHeightFileCmd ?? (_setCustomHeightFileCmd =
            new RelayCommand<TextBox>(SetCustomHeightFile, f => true));

        public ICommand SetCustomWeightFileCmd => _setCustomWeightFileCmd ?? (_setCustomWeightFileCmd =
            new RelayCommand<TextBox>(SetCustomWeightFile, f => true));

        public ICommand EstimateCmd => _estimateCmd ?? (_estimateCmd = new RelayCommand<Image>(Estimate, f => true));

        public ICommand ChangeImageCmd =>
            _changeImageCmd ?? (_changeImageCmd = new RelayCommand<Image>(ChangeImage, CanChangeImageCmd));

        private bool _isHeightGridDisplayed;
        private bool _isWeightGridDisplayed;
        private BitmapImage _heightPercentileGridImage;
        private BitmapImage _weightPercentileGridImage;
        private GrowthChart GrowthChart { get; }
        public Child Child { get; }
        public FileDataLoader FileDataLoader { get; }

        public AppWindowViewModel()
        {
            Child = new Child();
            GrowthChart = new GrowthChart();
            FileDataLoader = new FileDataLoader();
        }

        private void SetDefaultHeightFile()
        {
            FileDataLoader.IsDefaultHeightFile = true;
            FileDataLoader.HeightFileName =
                Child.IsBoy ? str.DefaultBoyHeightGrowthChart : str.DefaultGirlHeightGrowthChart;
        }

        private bool CanSetDefaultHeightFile()
        {
            return !FileDataLoader.IsDefaultHeightFile;
        }


        private void SetDefaultHeightFileByRadioButton()
        {
            FileDataLoader.IsDefaultHeightFile = true;
            FileDataLoader.HeightFileName =
                Child.IsBoy ? str.DefaultBoyHeightGrowthChart : str.DefaultGirlHeightGrowthChart;
            FileDataLoader.WeightFileName =
                Child.IsBoy ? str.DefaultBoyWeightGrowthChart : str.DefaultGirlWeightGrowthChart;
        }

        private void SetDefaultWeightFile()
        {
            FileDataLoader.IsDefaultWeightFile = true;
            FileDataLoader.WeightFileName =
                Child.IsBoy ? str.DefaultBoyWeightGrowthChart : str.DefaultGirlWeightGrowthChart;
        }

        private bool CanSetDefaultWeightFile()
        {
            return !FileDataLoader.IsDefaultWeightFile;
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

        private static bool? SetCustomFile(TextBox box)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string path = Path.GetDirectoryName(asm.Location);
            OpenFileDialog dlg = new OpenFileDialog
                {DefaultExt = ".txt", Filter = "Text documents (.txt)|*.txt", InitialDirectory = path};
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                box.Text = dlg.FileName;
            }

            return result;
        }

        private void Estimate(Image percentileGrid)
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
                AlertWindow alert = new AlertWindow();
                alert.Show(str.ErrorInFile, e.Message);
                Child.EstimatedHeight = 0;
                Child.EstimatedWeight = 0;
                return;
            }

            _heightPercentileGridImage = CreateHeightPercentileGridImage(percentileGrid.Width, percentileGrid.Height);
            _weightPercentileGridImage = CreateWeightPercentileGridImage(percentileGrid.Width, percentileGrid.Height);
            percentileGrid.Source = _heightPercentileGridImage;
            _isHeightGridDisplayed = true;
        }

        private static BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }

        private BitmapImage CreateHeightPercentileGridImage(double width, double height)
        {
            Plot plt = new Plot((int) width, (int) height);
            for (int i = 0; i < GrowthChart.HeightChart.Plots.GetLength(0); i++)
            {
                double[] arguments = Enumerable.Range(0, GrowthChart.HeightChart.Plots.GetLength(1))
                    .Select(x => (double) GrowthChart.HeightChart.Plots[i, x].X)
                    .ToArray();

                double[] values = Enumerable.Range(0, GrowthChart.HeightChart.Plots.GetLength(1))
                    .Select(x => (double) GrowthChart.HeightChart.Plots[i, x].Y)
                    .ToArray();

                plt.PlotScatter(arguments, values, label: GrowthChart.HeightLabels[i], markerSize: 0,
                    lineStyle: LineStyle.Dash);
            }

            plt.PlotPoint(Child.Age, Child.CurrentHeight, Color.MediumBlue, markerShape: MarkerShape.asterisk,
                markerSize: 8, label: str.CurrentHeight);
            plt.PlotPoint(18, Child.EstimatedHeight, Color.Crimson, markerShape: MarkerShape.cross, markerSize: 8,
                label: str.EstimatedValue);

            plt.Legend(reverseOrder: true, fontSize: 12, location: legendLocation.upperLeft);
            plt.Title(str.HeightPercentileGrid);
            plt.YLabel(str.Height);
            plt.XLabel(str.Age);
            plt.Grid(xSpacing: 1, ySpacing: 5);

            return BitmapToImageSource(plt.GetBitmap());
        }

        private BitmapImage CreateWeightPercentileGridImage(double width, double height)
        {
            Plot plt = new Plot((int) width, (int) height);
            for (int i = 0; i < GrowthChart.WeightChart.Plots.GetLength(0); i++)
            {
                double[] arguments = Enumerable.Range(0, GrowthChart.WeightChart.Plots.GetLength(1))
                    .Select(x => (double) GrowthChart.WeightChart.Plots[i, x].X)
                    .ToArray();

                double[] values = Enumerable.Range(0, GrowthChart.WeightChart.Plots.GetLength(1))
                    .Select(x => (double) GrowthChart.WeightChart.Plots[i, x].Y)
                    .ToArray();

                plt.PlotScatter(arguments, values, label: GrowthChart.WeightLabels[i], markerSize: 0,
                    lineStyle: LineStyle.Dash);
            }

            plt.PlotPoint(Child.Age, Child.CurrentWeight, Color.MediumBlue, markerShape: MarkerShape.asterisk,
                markerSize: 8, label: str.CurrentWeight);
            plt.PlotPoint(18, Child.EstimatedWeight, Color.Crimson, markerShape: MarkerShape.cross, markerSize: 8,
                label: str.EstimatedValue);

            plt.Legend(reverseOrder: true, fontSize: 12, location: legendLocation.upperLeft);
            plt.Title(str.WeightPercentileGrid);
            plt.YLabel(str.Weight);
            plt.XLabel(str.Age);
            plt.Grid(xSpacing: 1, ySpacing: 5);

            return BitmapToImageSource(plt.GetBitmap());
        }

        private bool CanChangeImageCmd(Image image)
        {
            return _isHeightGridDisplayed || _isWeightGridDisplayed;
        }

        private void ChangeImage(Image image)
        {
            if (_isHeightGridDisplayed)
            {
                image.Source = _weightPercentileGridImage;
                _isHeightGridDisplayed = false;
                _isWeightGridDisplayed = true;
                return;
            }

            if (_isWeightGridDisplayed)
            {
                image.Source = _heightPercentileGridImage;
                _isWeightGridDisplayed = false;
                _isHeightGridDisplayed = true;
            }
        }
    }
}