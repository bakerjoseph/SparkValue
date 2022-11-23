using SparkValueBackend.ViewModels.LessonInteractiveElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueBackend.ViewModels.LessonContent
{
    public class LessonContentViewModelBase : ViewModelBase
    {
        private string _plainText;
        public string PlainText
        {
            get
            {
                return _plainText;
            }
            set
            {
                _plainText = value;
                OnPropertyChanged(nameof(PlainText));
            }
        }

        private string? _interactiveElement;
        public string? InteractiveElement
        {
            get 
            { 
                return _interactiveElement; 
            }
            set
            {
                _interactiveElement = value;
                OnPropertyChanged(nameof(InteractiveElement));
            }
        }

        private ViewModelBase _element;
        public ViewModelBase Element
        {
            get { return _element; }
            set
            {
                _element = value;
                OnPropertyChanged(nameof(Element));
            }
        }

        public LessonContentViewModelBase(string plainText, string? interactiveElement)
        {
            PlainText = plainText;
            InteractiveElement = interactiveElement;

            if (interactiveElement != null)
            {
                switch (interactiveElement)
                {
                    case "ResistorsGalore":
                        Element = new ResistorChartViewModel();
                        break;
                    case "CurrentCheckPoints":
                        Element = new DiodeCheckpointViewModel();
                        break;
                    case "LightsOfLEDs":
                        Element = new LEDViewModel();
                        break;
                    case "ElectricityStore":
                        Element = new CapacitorOverviewViewModel();
                        break;
                    case "CapacitorTypes":
                        Element = new CapacitorTypesViewModel();
                        break;
                    case "GateSymbols":
                        Element = new LogicGateSymbolsViewModel();
                        break;
                    case "ANDsORs":
                        Element = new ANDsORsViewModel();
                        break;
                }
            }
        }
    }
}
