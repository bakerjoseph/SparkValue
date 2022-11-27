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
                    case "OhmsLawCollective":
                        Element = new OhmsLawBasicViewModel();
                        break;
                    case "OhmsLawAdvanced":
                        Element = new OhmsLawAdvancedViewModel();
                        break;
                    case "LoadsAndNodes":
                        Element = new CircuitDiagramTermsViewModel();
                        break;
                    case "ExampleDiagrams":
                        Element = new CircuitDiagramsViewModel();
                        break;
                    case "ComponentDifferences":
                        Element = new ComponentPackagesViewModel();
                        break;
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
                    case "NPNInAction":
                        Element = new TransistorWalkthroughViewModel();
                        break;
                    case "InductorMagic":
                        Element = new InductorDiagramViewModel();
                        break;
                    case "GateSymbols":
                        Element = new LogicGateSymbolsViewModel();
                        break;
                    case "ANDsORs":
                        Element = new ANDsORsViewModel();
                        break;
                    case "NOTsXORs":
                        Element = new NOTsXORsViewModel();
                        break;
                    case "NANDsNORs":
                        Element = new NANDsNORsViewModel();
                        break;
                    case "BlackBoxCircuitry":
                        Element = new BlackBoxMiniCircuitsViewModel();
                        break;
                    case "SwitchingSwitches":
                        Element = new SwitchingViewModel();
                        break;
                    case "ButtonPress":
                        Element = new ButtonsViewModel();
                        break;
                    case "DifferentBoards":
                        Element = new BreadboardTypesViewModel();
                        break;
                    case "ExampleBoard":
                        Element = new BreadboardAnatomyViewModel();
                        break;
                    case "MultimeterParts":
                        Element = new MultimeterOverviewViewModel();
                        break;
                    case "PCBExplodedView":
                        Element = new PCBDiagramViewModel();
                        break;
                    case "MultimeterMeasuringValues":
                        Element = new MultimeterMeasuringViewModel();
                        break;
                    case "OscilloscopeOverview":
                        Element = new OscilloscopeOverviewViewModel();
                        break;
                    case "PowerOptions":
                        Element = new CircuitPowerOptionsViewModel();
                        break;
                    case "BeginnerPowerOption":
                        Element = new CircuitPowerForBeginnersViewModel();
                        break;
                    case "SolderingExtras":
                        Element = new SolderingExtrasViewModel();
                        break;
                    case "SolderingAnatomy":
                        Element = new SolderingStationViewModel();
                        break;
                    case "SolderingPractice":
                        Element = new SolderingPracticeViewModel();
                        break;
                    case "ProjectInspiration":
                        Element = new ElectronicsInspirationViewModel();
                        break;
                }
            }
        }
    }
}
