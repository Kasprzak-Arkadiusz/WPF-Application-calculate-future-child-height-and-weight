using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using str = ProjektIndywidualny.Properties.strings;

namespace ProjektIndywidualny.Model
{
    public class FileDataLoader : INotifyPropertyChanged
    {
        public bool IsDefaultHeightFile = true;
        private string _heightFileName;

        public string HeightFileName
        {
            get => _heightFileName;
            set
            {
                if (value == _heightFileName) return;
                _heightFileName = value;
                OnPropertyChanged(nameof(HeightFileName));
            }
        }

        public bool IsDefaultWeightFile = true;
        private string _weightFileName;

        public string WeightFileName
        {
            get => _weightFileName;
            set
            {
                if (value == _weightFileName) return;
                _weightFileName = value;
                OnPropertyChanged(nameof(WeightFileName));
            }
        }

        public FileDataLoader()
        {
            HeightFileName = str.DefaultBoyHeightGrowthChart;
            WeightFileName = str.DefaultBoyWeightGrowthChart;
        }


        public void LoadDefaultData(string fileName, out Chart chart, out string[] labels)
        {
            var resourceName = "ProjektIndywidualny.Data." + fileName;
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            StreamReader reader = new StreamReader(stream);

            var content = reader.ReadToEnd();
            string[] formattedContent = ConvertStringToArray(content);
            LoadData(formattedContent, out chart, out labels);
        }

        private string[] ConvertStringToArray(string content)
        {
            return content.Split(new char[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
        }

        public void LoadCustomData(string fileName, out Chart chart, out string[] labels)
        {
            string[] formattedContent;

            try
            {
                formattedContent = File.ReadAllLines(fileName);
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException(str.FileNotFound + fileName);
            }

            try { LoadData(formattedContent, out chart, out labels); }
            catch (ArgumentException) { throw; }
           
        }

        private static void LoadData(string[] lines, out Chart chart, out string[] labels)
        {
            string[] stringSeparator = {"|", " ", "\t"};
            labels = lines[0].Split(stringSeparator, StringSplitOptions.RemoveEmptyEntries);
            int numberOfLabels = labels.Length;
            int numberOfLines = lines.Length;
            chart = new Chart(numberOfLabels, numberOfLines - 1);

            for (int i = 1; i < numberOfLines; i++)
            {
                string[] tokens = lines[i].Split(stringSeparator, StringSplitOptions.RemoveEmptyEntries);
                if (!double.TryParse(tokens[0], out double tempAge))
                {
                    throw new ArgumentException(str.Given + str.Age + str.IsNotAnInt + str.Line + i);
                }

                int age = (int) tempAge;

                for (int j = 0; j < numberOfLabels; j++)
                {
                    if (!double.TryParse(tokens[j + 1], out double tempValue))
                    {
                        throw new ArgumentException(
                            str.Given + str.Age + str.IsNotAnInt + str.Line + i + ". " + str.Label + labels[j]);
                    }

                    chart.Plots[j, i - 1] = new Point(age, (int) tempValue);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}