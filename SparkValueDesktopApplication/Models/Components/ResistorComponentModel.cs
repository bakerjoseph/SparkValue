using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SparkValueDesktopApplication.Models.Components
{
    public class ResistorComponentModel : IComponentModel
    {
        public string Name { get; }
        public string Description { get; }
        public BitmapImage Image { get; }
        public double ResistenceValue { get; private set; }

        /// <summary>
        /// Used in making components with a built in resistence value.
        /// For example, resistors, potentiometers, etc.
        /// </summary>
        /// <param name="name">Name of the component</param>
        /// <param name="description">Short description of what the component does</param>
        /// <param name="imageSource">An image source that represents the component</param>
        /// <param name="resistenceValue">Value measured in ohms</param>
        public ResistorComponentModel(string name, string description, string imageSource, double resistenceValue)
        {
            Name = name;
            Description = description;
            Image = new BitmapImage(new Uri(imageSource, UriKind.Relative));
            ResistenceValue = resistenceValue;
        }

        public string DisplayValues(double inputVoltage, double inputCurrent)
        {
            throw new NotImplementedException();
        }

        public double GetOutputCurrent(double inputCurrent)
        {
            throw new NotImplementedException();
        }

        public double GetOutputVoltage(double inputVoltage)
        {
            throw new NotImplementedException();
        }

        public void UpdateResistenceValue(double newResistenceValue)
        {
            if (ResistenceValue == newResistenceValue) return;
            ResistenceValue = newResistenceValue;
        }
    }
}
