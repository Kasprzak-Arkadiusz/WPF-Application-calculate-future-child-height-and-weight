using System;
using System.Drawing;

namespace ProjektIndywidualny.Src
{
    public static class Estimator
    {
        public static int EstimateChildParameter(Point[,] plots, int currentParameter, int currentAge, int estimatedAge)
        {
            int curAgeIndex = FindIndexByAge(plots, currentAge);
            int estAgeIndex = FindIndexByAge(plots, estimatedAge);

            if (currentParameter < plots[0, curAgeIndex].Y)
            {
                throw new ArgumentException("Wartość parametru znajduje się poniżej najmniejszego centylu.");
            }

            if (currentParameter > plots[plots.GetLength(0) - 1, curAgeIndex].Y)
            {
                throw new ArgumentException("Wartość parametru znajduje się powyżej najwyższego centylu.");
            }

            int upperPlotIndex = 0;
            int bottomPlotIndex = plots.GetLength(0) - 1;

            //Szukanie wykresów pomiędzy którymi znajduje się dany punkt.
            for (int i = 0; i < plots.GetLength(0); i++) { }

            return 0;
        }

        private static int FindIndexByAge(Point[,] plots, int age)
        {
            int l = 0;
            int r = plots.GetLength(1) - 1;
            int m;

            while (l <= r)
            {
                m = l + (r - l) / 2;
                int test = plots[0, m].X;
                if (age < plots[0, m].X)
                {
                    r = m - 1;
                }
                else if (age > plots[0, m].X)
                {
                    l = m + 1;
                }
                else
                {
                    return m;
                }
            }

            throw new ArgumentException("W pliku brakuje danych dla wieku: " + age + " lat.");
        }
    }
}