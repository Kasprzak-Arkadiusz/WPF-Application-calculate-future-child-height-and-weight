using System;
using System.Drawing;
using str = ProjektIndywidualny.Properties.strings;

namespace ProjektIndywidualny.Model
{
    public static class FileDataValidator
    {
        public static void CheckIfFileDataAreCorrect(ref Chart chart)
        {
            Point[,] plots = chart.Plots;
            IsAgeIncreasing(plots);
            AreAllValuesPositive(plots);
            AreValuesNonDecreasing(plots);
            AreValuesLesserThan(plots, 250);
            ArePlotsOverlapping(plots);
        }

        private static void IsAgeIncreasing(Point[,] plots)
        {
            const int i = 0;
            for (int j = 1; j < plots.GetLength(1); j++)
            {
                if (plots[i, j].X <= plots[i, j - 1].X)
                {
                    throw new ArgumentException(str.AgeShouldBeIncreasing + str.Label + i + ". " + str.Line +
                                                (j - 1) + str.And + str.Line + j);
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
                        throw new ArgumentException(str.ValuesShouldBePositive + str.Label + i +
                                                    str.Line + j);
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
                        throw new ArgumentException(str.ValuesShouldBeNonDecreasing + str.Label + i +
                                                    ". " + str.Line + (j - 1) + str.And + str.Line + j);
                    }
                }
            }
        }

        private static void AreValuesLesserThan(Point[,] plots, int maxValue)
        {
            for (int i = 0; i < plots.GetLength(0); i++)
            {
                for (int j = plots.GetLength(1) - 1; j > 0; j--)
                {
                    if (plots[i,j].Y > maxValue)
                    {
                        throw new ArgumentException(str.ValuesShouldBeLesserThan + maxValue + str.Label + i +
                                                    ". " + str.Line + j);
                    }
                }
            }
        }

        private static void ArePlotsOverlapping(Point[,] plots)
        {
            for (int i = 1; i < plots.GetLength(1); i++)
            {
                for (int j = 0; j < plots.GetLength(0) - 1; j++)
                {
                    if (plots[j, i].Y >= plots[j + 1, i].Y)
                    {
                        throw new ArgumentException( str.PlotsValueShouldntOverlap +
                            str.Line + i +
                            str.Label + j + str.And + j + 1);
                    }
                }
            }
        }
    }
}