using System;
using System.Drawing;

namespace ProjektIndywidualny.Src
{
    public static class FileDataValidator
    {
        public static void CheckIfFileDataAreCorrect(ref Chart chart)
        {
            AreAnyDataInFile(chart.Plots);
            IsAgeIncreasing(chart.Plots);
            AreAllValuesPositive(chart.Plots);
            AreValuesNonDecreasing(chart.Plots);
            ArePlotsOverlapping(chart.Plots);
        }

        private static void IsAgeIncreasing(Point[,] plots)
        {
            int i = 0;
            for (int j = 1; j < plots.GetLength(1); j++)
            {
                if (plots[i, j].X <= plots[i, j - 1].X)
                {
                    throw new ArgumentException("Wiek powinien rosnąć." + " Etykieta:" + i + ", linia:" +
                                                (j - 1) + " oraz linia:" + j);
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

        private static void AreAnyDataInFile(Point[,] plots)
        {
            if (plots.GetLength(0) <= 1 || plots.GetLength(1) == 0)
            {
                throw new ArgumentException("W pliku nie podano danych.");
            }
        }

        private static void ArePlotsOverlapping(Point[,] plots)
        {
            for (int i = 1; i < plots.GetLength(1); i++)
            {
                for (int j = 0; j < plots.GetLength(0)-1; j++)
                {
                    if (plots[j,i].Y >= plots[j+1,i].Y)
                    {
                        throw new ArgumentException("Wartości z siatek centylowych nie powinny na siebie nachodzić. Linia: " + i + "Etykiety: " +  j + "oraz" + j+1);
                    }
                }
            }
        }
    }
}