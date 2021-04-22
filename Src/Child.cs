namespace ProjektIndywidualny.Src
{
    public class Child
    {
        public int Age { get; set; }
        public int CurrentHeight { get; set; }
        public int CurrentWeight { get; set; }
        public int EstimatedHeight { get; set; }
        public int EstimatedWeight { get; set; }

        public Child(int age, int currentHeight, int currentWeight)
        {
            Age = age;
            CurrentHeight = currentHeight;
            CurrentWeight = currentWeight;
        }
    }
}