using SparkValueBackend.Models.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueBackend.ViewModels
{
    public class CapacitorViewModel : ComponentViewModel
    {
        private double _capacitanceValue;
        public double CapacitanceValue
        {
            get
            {
                return _capacitanceValue;
            }
            set
            {
                _capacitorComponentModel.UpdateCapacitanceValue(value);
                _capacitanceValue = value;
                OnPropertyChanged(nameof(CapacitanceValue));
            }
        }

        private CapacitorComponentModel _capacitorComponentModel;

        public CapacitorViewModel(CapacitorComponentModel component) : base(component)
        {
            _capacitorComponentModel = component;
            CapacitanceValue = component.CapacitanceValue;
        }
    }
}
