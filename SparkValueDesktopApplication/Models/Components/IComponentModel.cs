using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SparkValueDesktopApplication.Models.Components
{
    public interface IComponentModel
    {
        public string Name { get; }
        public string Description { get; }
        public BitmapImage Image { get; }

        public abstract double GetOutputVoltage(double inputVoltage);
        public abstract double GetOutputCurrent(double inputCurrent);
        public abstract string DisplayValues(double inputVoltage, double inputCurrent);
    }
}
