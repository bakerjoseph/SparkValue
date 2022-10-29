using SparkValueBackend.Models.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueBackend.ViewModels
{
    public class ResistorViewModel : ComponentViewModel
    {
        private double _resistanceValue;
        public double ResistanceValue
        {
            get
            {
                return _resistanceValue;
            }
            set
            {
                _resistorComponentModel.UpdateResistenceValue(value);
                _resistanceValue = value;
                OnPropertyChanged(nameof(ResistanceValue));
            }
        }

        private ResistorComponentModel _resistorComponentModel;

        public ResistorViewModel(ResistorComponentModel component) : base(component)
        {
            _resistorComponentModel = component;
            ResistanceValue = component.ResistanceValue;
        }
    }
}
