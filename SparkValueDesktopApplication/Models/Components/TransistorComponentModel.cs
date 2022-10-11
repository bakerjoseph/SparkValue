using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SparkValueDesktopApplication.Models.Components
{
    public class TransistorComponentModel : IComponentModel
    {
        public string Name { get; }
        public string Description { get; }
        public BitmapImage Image { get; }
        public bool TransistorState { get; private set; } = false;

        /// <summary>
        /// Used in making transistor components.
        /// </summary>
        /// <param name="name">Name of the component</param>
        /// <param name="description">Short description of what the component does</param>
        /// <param name="imageSource">An image source that represents the component</param>
        public TransistorComponentModel(string name, string description, string imageSource)
        {
            Name = name;
            Description = description;
            Image = new BitmapImage(new Uri(imageSource, UriKind.Relative));
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

        /// <summary>
        /// Change the state of the transistor to allow or restrict voltage and current from flowing from the collector pin to the emmiter pin.
        /// This state acts as the base pin.
        /// </summary>
        /// <param name="state">True to allow passage, False to restrict passage</param>
        public void UpdateState(bool state)
        {
            if (state == TransistorState) return;
            TransistorState = state;
        }
    }
}
