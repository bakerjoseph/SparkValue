using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SparkValueBackend.Models.Components
{
    public interface IComponentModel
    {
        public string Name { get; }
        public string Description { get; }
        public BitmapImage Image { get; }

        public abstract (double outVoltage, double outCurrent) GetOutput(double inputVoltage, double inputCurrent);
        public abstract string DisplayValues(double inputVoltage, double inputCurrent);
    }
}
