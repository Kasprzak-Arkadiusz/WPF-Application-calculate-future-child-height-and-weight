using System.Drawing;

namespace ProjektIndywidualny.Src
{
    public class Chart
    {
        public Point[,] Plots { get; }

        public Chart(int numberOfPlots, int numberOfPoints)
        {
            Plots = new Point[numberOfPlots, numberOfPoints];
        }

        public Chart() { }
    }
}