using System;
using System.Drawing;

namespace ProjektIndywidualny.Src
{
    public static class FileDataValidator
    {
        public static void CheckIfFileDataAreCorrect(ref Chart chart)
        {
            IsAgeIncreasing(chart.Plots);
            AreValuesNonDecreasing(chart.Plots);
            AreAllValuesPositive(chart.Plots);
        }

        private static void IsAgeIncreasing(Point[,] plots)
        {
            for (int i = 0; i < plots.GetLength(0); i++)
            {
                for (int j = 1; j < plots.GetLength(1); j++)
                {
                    if (plots[i, j].X <= plots[i, j - 1].X)
                    {
                        throw new ArgumentException("Wiek powinien rosnąć." + " Etykieta:" + i + ", linia:" +
                                                    (j - 1) + " oraz linia:" + j);
                    }
                }
            }
        }

        private static void AreValuesNonDecreasing(Point[,] plots)
        {
            for (int i = 0; i < plots.GetLength(0); i++)
            {
                for (int j = 1; j < plots.GetLength(1); j++)
                {
                    if (plots[i, j].Y < plots[i, j - 1].Y)
                    {
                        throw new ArgumentException("Wartości powinny być niemalejące." + " Etykieta:" + i +
                                                    ", linia:" + (j - 1) + " oraz linia:" + j);
                    }
                }
            }
        }

        private static void AreAllValuesPositive(Point[,] plots)
        {
            for (int i = 0; i < plots.GetLength(0); i++)
            {
                for (int j = 0; j < plots.GetLength(1); j++)
                {
                    if (plots[i, j].X < 0 || plots[i, j].Y < 0)
                    {
                        throw new ArgumentException("Wartości powinny być dodatnie." + " Etykieta:" + i +
                                                    " linia:" + (j));
                    }
                }
            }
        }
    }
}