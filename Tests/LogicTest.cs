using System.Globalization;
using System.Windows.Controls;
using NUnit.Framework;
using ProjektIndywidualny.Model;
using str = ProjektIndywidualny.Properties.strings;

namespace ProjektIndywidualny.Tests
{
    class LogicTest
    {
        private Child Child { get; set; }

        #region ValidationRule

        [TestCase("Not an number")]
        public void ShouldReturnErrorInValidationResult_WhenInputIsNotAnNumber(object value)
        {
            ValidationResult result = new ValidationResult(false, str.ValueIsNotInt);
            OnlyPositiveIntsValidationRule testResult = new OnlyPositiveIntsValidationRule();

            Assert.True(testResult.Validate(value, CultureInfo.CurrentCulture) == result);
        }

        #endregion

        #region Estimator.cs

        [SetUp]
        public void Initialize()
        {
            Child = new Child();
        }

        [TestCase(167, 171, 5, 18, 104)]
        [TestCase(162, 167, 9, 14, 133)]
        [TestCase(167, 167, 12, 12, 167)]
        [TestCase(167, 171, 15,18, 158)]
        public void ShouldReturnHeightFromRangeAsResult(int lowerRange, int upperRange, int currentAge,
            int estimatedAge, int currentHeight)
        {
            FileDataLoader.LoadDefaultData(str.DefaultBoyHeightGrowthChart, out Chart chart, out string[] _);
            Child.Age = currentAge;

            int result = Estimator.EstimateChildParameter(chart.Plots, currentHeight, currentAge, estimatedAge,
                nameof(Child.CurrentHeight));

            Assert.That(() => result >= lowerRange && result <= upperRange);
        }

        [TestCase(59, 63, 5, 18, 17)]
        [TestCase(54, 62, 9, 14, 33)]
        [TestCase(70, 70, 12, 12, 70)]
        [TestCase(54, 59, 15,18, 43)]
        public void ShouldReturnWeightFromRangeAsResult(int lowerRange, int upperRange, int currentAge,
            int estimatedAge, int currentWeight)
        {
            FileDataLoader.LoadDefaultData(str.DefaultBoyWeightGrowthChart, out Chart chart, out string[] _);
            Child.Age = currentAge;

            int result = Estimator.EstimateChildParameter(chart.Plots, currentWeight, currentAge, estimatedAge,
                nameof(Child.CurrentWeight));

            Assert.That(() => result >= lowerRange && result <= upperRange);
        }

        #endregion
        
    }
}