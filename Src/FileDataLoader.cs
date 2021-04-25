using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using str = ProjektIndywidualny.Properties.strings;

namespace ProjektIndywidualny.Src
{
    public static class FileDataLoader
    {
        public static void LoadDefaultData(string fileName, out Chart chart, out string[] labels)
        {
            var resourceName = "ProjektIndywidualny.Data." + fileName;
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            StreamReader reader = new StreamReader(stream);

            var content = reader.ReadToEnd();
            string[] formattedContent = ConvertStringToArray(content);
            LoadData(formattedContent, out chart, out labels);
        }

        private static string[] ConvertStringToArray(string content)
        {
            return content.Split(new char[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
        }

        public static void LoadCustomData(string fileName, out Chart chart, out string[] labels)
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

            LoadData(formattedContent, out chart, out labels);
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
                if (!Double.TryParse(tokens[0], out double tempAge))
                {
                    throw new ArgumentException(str.Given + str.Age + str.IsNotAnInt + str.Line + i);
                }

                int age = (int) tempAge;

                for (int j = 0; j < numberOfLabels; j++)
                {
                    if (!Double.TryParse(tokens[j + 1], out double tempValue))
                    {
                        throw new ArgumentException(
                            str.Given + str.Age + str.IsNotAnInt + str.Line + i + ". " + str.Label + labels[j]);
                    }

                    chart.Plots[j, i - 1] = new Point(age, (int) tempValue);
                }
            }
        }
    }
}