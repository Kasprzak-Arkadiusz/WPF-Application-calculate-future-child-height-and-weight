using System;
using System.Drawing;

using str = ProjektIndywidualny.Properties.strings;

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
                throw new ArgumentException(str.BelowLowestPercentile);
            }

            if (currentParameter > plots[plots.GetLength(0) - 1, curAgeIndex].Y)
            {
                throw new ArgumentException(str.OverHighestPercentile);
            }

            Point indices = FindBottomAndUpperIndex(plots, currentParameter, curAgeIndex, estAgeIndex);
            int curBottomIndex = indices.X;
            int curUpperIndex = indices.Y;

            if (curBottomIndex == curUpperIndex)
            {
                return plots[curBottomIndex, estAgeIndex].Y;
            }
            else
            {
                int difference = plots[curUpperIndex, curAgeIndex].Y - plots[curBottomIndex, curAgeIndex].Y;
                return plots[curBottomIndex, estAgeIndex].Y + difference;
            }
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

            throw new ArgumentException(str.MissingAgeData + age + str.Years);
        }

        private static Point FindBottomAndUpperIndex(Point[,] plots, int currentParameter, int curAgeIndex,
            int estAgeIndex)
        {
            int curBottomIndex = 0;
            int minBottomDistance = -int.MaxValue;
            int curUpperIndex = 0;
            int minUpperDistance = int.MaxValue;

            for (int i = 0; i < plots.GetLength(0); i++)
            {
                int distance = plots[i, curAgeIndex].Y - currentParameter;
                if (distance == 0)
                {
                    curBottomIndex = i;
                    curUpperIndex = i;
                    break;
                }
                else if (distance < 0 && (distance > minBottomDistance))
                {
                    minBottomDistance = distance;
                    curBottomIndex = i;
                }
                else if (distance > 0 && (distance < minUpperDistance))
                {
                    minUpperDistance = distance;
                    curUpperIndex = i;
                }
            }

            return new Point(curBottomIndex, curUpperIndex);
        }
    }
}