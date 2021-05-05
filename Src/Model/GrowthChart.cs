namespace ProjektIndywidualny.Model
{
    public class GrowthChart
    {
        public Chart HeightChart { get; set; }
        public string[] HeightLabels { get; set; }
        public Chart WeightChart { get; set; }
        public string[] WeightLabels { get; set; }
    }
}