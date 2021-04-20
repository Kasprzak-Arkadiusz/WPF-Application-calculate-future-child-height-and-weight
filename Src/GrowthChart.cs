namespace ProjektIndywidualny.Src
{
    public class GrowthChart
    {
        public Chart HeightChart { get; set; }
        public string[] HeightLabels { get; set; }
        public Chart WeightChart { get; set; }
        public string[] WeightLabels { get; set; }

        public GrowthChart(Chart heightChart, string[] heightLabels, Chart weightChart, string[] weightLabels)
        {
            HeightChart = heightChart;
            HeightLabels = heightLabels;
            WeightLabels = weightLabels;
            WeightChart = weightChart;
        }

        public GrowthChart() {}
    }
}