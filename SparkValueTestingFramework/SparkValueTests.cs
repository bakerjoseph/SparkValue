using SparkValueBackend.Models;
using SparkValueBackend.Models.Components;
using SparkValueBackend.ViewModels;
using SparkValueBackend.Services;
using SparkValueBackend.Stores;
using Microsoft.VisualStudio.TestPlatform.Utilities;

namespace SparkValueTestingFramework
{
    public class Tests
    {
        #region Component Testing Constants
        private const double TestVoltage = 5.0;
        private const double TestCurrent = 1.0;
        private const double TestMixedVoltage = 1.38;
        private const double TestMixedCurrent = -0.005;
        #endregion


        [SetUp]
        public void Setup()
        {

        }

        #region Model Testing

        #region Wire Model Tests

        #endregion

        #region Component Model Tests
        /// <summary>
        /// Tests a component model's methods to make sure they can handle a variety of inputs.
        /// Can it handle real positive numbers?
        /// Can it handle real negative numbers?
        /// Can it handle mixed numbers?
        /// Can it handle zeros?
        /// </summary>

        #region Basic Component
        [Test]
        public void BasicComponentCreate()
        {
            BasicComponentModel component = new BasicComponentModel("Component Name", "Component Description", "\\Images\\diode.png");
            Assert.IsNotNull(component, "Failed to create new BasicComponentModel!");
        }

        [TestCase(TestVoltage, TestCurrent, "positive real numbers")]
        [TestCase(-TestVoltage, -TestCurrent, "negative real numbers")]
        [TestCase(TestMixedVoltage, TestMixedCurrent, "mixed numbers")]
        [TestCase(0, 0, "zeros")]
        public void BasicComponentOutput(double inputVoltage, double inputCurrent, string testTag)
        {
            BasicComponentModel component = new BasicComponentModel("Component Name", "Component Description", "\\Images\\diode.png");
            (double voltageResult, double currentResult) output = component.GetOutput(inputVoltage, inputCurrent);
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(component, "Failed to create new BasicComponentModel!");
                Assert.That(output.voltageResult, Is.EqualTo(inputVoltage), $"Voltage output of a BasicComponentModel is incorrect using {testTag}");
                Assert.That(output.currentResult, Is.EqualTo(inputCurrent), $"Current output of a BasicComponentModel is incorrect using {testTag}");
            });
        }

        [TestCase(TestVoltage, TestCurrent, "positive real numbers")]
        [TestCase(-TestVoltage, -TestCurrent, "negative real numbers")]
        [TestCase(TestMixedVoltage, TestMixedCurrent, "mixed numbers")]
        [TestCase(0, 0, "zeros")]
        public void BasicComponentFormattedOutput(double inputVoltage, double inputCurrent, string testTag)
        {
            BasicComponentModel component = new BasicComponentModel("Component Name", "Component Description", "\\Images\\diode.png");
            string formattedOutput = component.DisplayValues(inputVoltage, inputCurrent);
            string expectedOutput = GetExpectedBaseComponentFormatedString(inputVoltage, inputCurrent, inputVoltage, inputCurrent);
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(component, "Failed to create new BasicComponentModel!");
                Assert.That(formattedOutput, Is.EqualTo(expectedOutput), $"Formatted output of a BasicComponentModel is incorrect using {testTag}");
            });
        }
        #endregion

        #region Capacitor Component
        [Test]
        public void CapacitorComponentCreate()
        {
            CapacitorComponentModel component = new CapacitorComponentModel("Component Name", "Component Description", "\\Images\\capacitor.png", 500);
            Assert.IsNotNull(component, "Failed to create new CapacitorComponentModel!");
        }

        [TestCase(TestVoltage, TestCurrent, "positive real numbers")]
        [TestCase(-TestVoltage, -TestCurrent, "negative real numbers")]
        [TestCase(TestMixedVoltage, TestMixedCurrent, "mixed numbers")]
        [TestCase(0, 0, "zeros")]
        public void CapacitorComponentOutput(double inputVoltage, double inputCurrent, string testTag)
        {
            CapacitorComponentModel component = new CapacitorComponentModel("Component Name", "Component Description", "\\Images\\capacitor.png", 500);
            (double voltageResult, double currentResult) output = component.GetOutput(inputVoltage, inputCurrent);
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(component, "Failed to create new CapcitorComponentModel!");
                Assert.That(output.voltageResult, Is.EqualTo(inputVoltage), $"Voltage output of a CapacitorComponentModel is incorrect using {testTag}");
                Assert.That(output.currentResult, Is.EqualTo(0), $"Current output of a CapacitorComponentModel is incorrect using {testTag}");
            });
        }

        [TestCase(TestVoltage, TestCurrent, "positive real numbers")]
        [TestCase(-TestVoltage, -TestCurrent, "negative real numbers")]
        [TestCase(TestMixedVoltage, TestMixedCurrent, "mixed numbers")]
        [TestCase(0, 0, "zeros")]
        public void CapacitorComponentFormattedOutput(double inputVoltage, double inputCurrent, string testTag)
        {
            CapacitorComponentModel component = new CapacitorComponentModel("Component Name", "Component Description", "\\Images\\capacitor.png", 500);
            string formattedOutput = component.DisplayValues(inputVoltage, inputCurrent);
            string expectedOutput = GetExpectedBaseComponentFormatedString(inputVoltage, inputCurrent, inputVoltage, 0) + $"\n\nCapacitance: 500 farad(s)\tCharge: {inputVoltage * 500}";
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(component, "Failed to create new CapacitorComponentModel!");
                Assert.That(formattedOutput, Is.EqualTo(expectedOutput), $"Formatted output of a CapacitorComponentModel is incorrect using {testTag}");
            });
        }

        // Can update capacitance value?

        // Charge value testing with full range of voltage and capacitance numbers
        #endregion

        #region Resistor Component
        public void ResistorComponentCreate()
        {

        }

        public void ResistorComponentOutput(double inputVoltage, double inputCurrent, string testTag)
        {

        }

        public void ResistorComponentFormattedOutput(double inputVoltage, double inputCurrent, string testTag)
        {

        }

        public void ResistorComponentFormattedOutputVarriedResistence(double inputVoltage, double inputCurrent, double resistenceValue, string testTag)
        {

        }

        // Can update resistence value?
        #endregion

        #region Transistor Component
        public void TransistorComponentCreate()
        {

        }

        public void TransistorComponentOutput(double inputVoltage, double inputCurrent, string testTag)
        {

        }

        public void TransistorComponentFormattedOutput(double inputVoltage, double inputCurrent, string testTag)
        {

        }

        public void TransistorComponentFormattedOutputVarriedState(double inputVoltage, double inputCurrent, bool state, string testTag)
        {

        }

        // Can update state?
        #endregion

        #endregion

        #region Breadboard Model Tests

        #endregion

        #region Unit Model Tests

        #endregion

        #region Lesson Model Tests

        #endregion

        #region User Model Tests

        #endregion

        #endregion

        #region View Model Testing

        #endregion

        private string GetExpectedBaseComponentFormatedString(double inputVoltage, double inputCurrent, double outputVoltage, double outputCurrent)
        {
            return $"Inputs -\nVoltage: {inputVoltage} V\tCurrent: {inputCurrent} Amp(s)\n\nOutputs -\nVoltage: {outputVoltage} V\tCurrent: {outputCurrent} Amp(s)";
        }
    }
}