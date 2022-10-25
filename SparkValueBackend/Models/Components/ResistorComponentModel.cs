using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SparkValueBackend.Models.Components
{
    public class ResistorComponentModel : IComponentModel
    {
        public string Name { get; }
        public string Description { get; }
        public BitmapImage Image { get; }
        public double ResistanceValue { get; private set; }

        /// <summary>
        /// Used in making components with a built in resistence value.
        /// For example, resistors, potentiometers, etc.
        /// </summary>
        /// <param name="name">Name of the component</param>
        /// <param name="description">Short description of what the component does</param>
        /// <param name="imageSource">An image source that represents the component</param>
        /// <param name="resistanceValue">Value measured in ohms</param>
        public ResistorComponentModel(string name, string description, string imageSource, double resistanceValue)
        {
            Name = name;
            Description = description;
            Image = new BitmapImage(new Uri(imageSource, UriKind.Relative));
            ResistanceValue = resistanceValue;
        }

        public string DisplayValues(double inputVoltage, double inputCurrent)
        {
            (double outVoltage, double outCurrent) outputs = GetOutput(inputVoltage, inputCurrent);
            return $"Inputs -\nVoltage: {inputVoltage} V\tCurrent: {inputCurrent} Amp(s)\n\nOutputs -\nVoltage: {outputs.outVoltage} V\tCurrent: {outputs.outCurrent} Amp(s)\n\nResistance: {ResistanceValue} Ohm(s)";
        }

        public void UpdateResistenceValue(double newResistenceValue)
        {
            if (ResistanceValue == newResistenceValue) return;
            ResistanceValue = newResistenceValue;
        }

        public (double outVoltage, double outCurrent) GetOutput(double inputVoltage, double inputCurrent)
        {
            return (inputVoltage, inputVoltage / ResistanceValue);
        }
    }
}
