using System;
using System.Drawing;
using str = ProjektIndywidualny.Properties.strings;

namespace ProjektIndywidualny.Model
{
    public static class Estimator
    {
        public static int EstimateChildParameter(Point[,] plots, int currentParameter, int currentAge, int estimatedAge, string nameofParameter)
        {
            int curAgeIndex = FindIndexByAge(plots, currentAge);
            int estAgeIndex = FindIndexByAge(plots, estimatedAge);

            if (currentParameter < plots[0, curAgeIndex].Y)
            {
                if (nameofParameter == str.CurrentWeight)
                {
                    throw new ArgumentException(str.BelowLowestPercentile + ": " + str.Weight);
                }

                if (nameofParameter == str.CurrentHeight)
                {
                    throw new ArgumentException(str.BelowLowestPercentile + ": " + str.Height);
                }
            }

            if (currentParameter > plots[plots.GetLength(0) - 1, curAgeIndex].Y)
            {
                if (nameofParameter == str.CurrentWeight)
                {
                    throw new ArgumentException(str.OverHighestPercentile + ": " + str.Weight);
                }

                if (nameofParameter == str.CurrentHeight)
                {
                    throw new ArgumentException(str.OverHighestPercentile + ": " + str.Height);
                }
            }

            Point indices = FindBottomAndUpperIndex(plots, currentParameter, curAgeIndex);
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

            while (l <= r)
            {
                int m = l + (r - l) / 2;
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

        private static Point FindBottomAndUpperIndex(Point[,] plots, int currentParameter, int curAgeIndex)
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
                else if (distance < 0 && distance > minBottomDistance)
                {
                    minBottomDistance = distance;
                    curBottomIndex = i;
                }
                else if (distance > 0 && distance < minUpperDistance)
                {
                    minUpperDistance = distance;
                    curUpperIndex = i;
                }
            }

            return new Point(curBottomIndex, curUpperIndex);
        }
    }
}