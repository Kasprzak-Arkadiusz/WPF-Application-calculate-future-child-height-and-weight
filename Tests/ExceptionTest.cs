using System;
using System.Drawing;
using System.IO;
using NUnit.Framework;
using ProjektIndywidualny.Model;
using str = ProjektIndywidualny.Properties.strings;

namespace ProjektIndywidualny.Tests
{
    class ExceptionTest
    {
        private Child Child { get; set; }
        private GrowthChart GrowthChart { get; set; }

        [SetUp]
        public void Initialize()
        {
            Child = new Child();
            GrowthChart = new GrowthChart();
        }

        #region Estimator.cs

        [TestCase(3, 90, 91)]
        [TestCase(5, 0, 10)]
        public void ShouldThrowArgumentException_WhenHeightIsBelowLowestPercentile(int currentAge, int currentHeight,
            int lowestHeight)
        {
            GrowthChart.HeightChart = new Chart(1, 1);
            GrowthChart.HeightChart.Plots[0, 0] = new Point(currentAge, lowestHeight);
            Child.CurrentHeight = currentHeight;

            Assert.That(
                () => Estimator.EstimateChildParameter(GrowthChart.HeightChart.Plots, currentHeight, currentAge,
                    currentAge, nameof(Model.Child.CurrentHeight)),
                Throws.Exception
                    .TypeOf<ArgumentException>()
                    .With.Property("Message")
                    .EqualTo(str.BelowLowestPercentile + ": " + str.Height));
        }

        [TestCase(3, 90, 91)]
        [TestCase(5, 0, 10)]
        public void ShouldThrowArgumentException_WhenWeightIsOverHighestPercentile(int currentAge, int currentWeight,
            int lowestWeight)
        {
            GrowthChart.WeightChart = new Chart(1, 1);
            GrowthChart.WeightChart.Plots[0, 0] = new Point(currentAge, lowestWeight);
            Child.CurrentWeight = currentWeight;

            Assert.That(
                () => Estimator.EstimateChildParameter(GrowthChart.WeightChart.Plots, currentWeight, currentAge,
                    currentAge, nameof(Model.Child.CurrentWeight)),
                Throws.Exception
                    .TypeOf<ArgumentException>()
                    .With.Property("Message")
                    .EqualTo(str.BelowLowestPercentile + ": " + str.Weight));
        }

        #endregion

        #region FileDataLoader.cs

        [Test]
        public void ShouldThrowFileNotFoundException_WhenFileIsNotFound()
        {
            string fileName = "This file doesn't exist"; 

            Assert.That(
                () => FileDataLoader.LoadCustomData(fileName, out Chart _, out string[] _),
                Throws.Exception
                    .TypeOf<FileNotFoundException>()
                    .With.Property("Message")
                    .EqualTo(str.FileNotFound + fileName));
        }

        [Test]
        public void ShouldThrowArgumentException_WhenFileIsEmpty()
        {
            string fileName = "EmptyFile.txt"; 
                
            Assert.That(
                () => FileDataLoader.LoadDefaultData(fileName, out Chart _, out string[] _),
                Throws.Exception
                    .TypeOf<ArgumentException>()
                    .With.Property("Message")
                    .EqualTo(str.FileIsEmpty));
        }

        #endregion
    }
}