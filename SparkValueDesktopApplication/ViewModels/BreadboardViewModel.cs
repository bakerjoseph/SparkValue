using SparkValueDesktopApplication.Commands;
using SparkValueDesktopApplication.Models;
using SparkValueDesktopApplication.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SparkValueDesktopApplication.ViewModels
{
    public class BreadboardViewModel : ViewModelBase
    {
        private string _selectedComponentName;
        public string SelectedComponentName
        {
            get 
            { 
                return _selectedComponentName; 
            }
            set 
            { 
                _selectedComponentName = value;
                OnPropertyChanged(nameof(SelectedComponentName));
            }
        }

        private string _selectedComponentDescription;
        public string SelectedComponentDescription
        {
            get 
            {
                return _selectedComponentDescription; 
            }
            set 
            { 
                _selectedComponentDescription = value; 
                OnPropertyChanged(nameof(SelectedComponentDescription)); 
            }
        }

        private double _breadboardVoltage;
        public double BreadboardVoltage
        {
            get
            {
                return _breadboardVoltage;
            }
            set
            {
                _breadboardVoltage = value;
                OnPropertyChanged(nameof(BreadboardVoltage));
            }
        }

        private double _breadboardCurrent;
        public double BreadboardCurrent
        {
            get
            {
                return _breadboardCurrent;
            }
            set
            {
                _breadboardCurrent = value;
                OnPropertyChanged(nameof(BreadboardCurrent));
            }
        }

        private IEnumerable<ComponentViewModel> _placedComponents;
        public IEnumerable<ComponentViewModel> PlacedComponents
        {
            get 
            { 
                return _placedComponents; 
            }
            set
            {
                _placedComponents = value;
                OnPropertyChanged(nameof(PlacedComponents));
            }
        }

        private IEnumerable<WireModel> _placedWires;
        public IEnumerable<WireModel> PlacedWires
        {
            get
            {
                return _placedWires;
            }
            set
            {
                _placedWires = value;
                OnPropertyChanged(nameof(PlacedWires));
            }
        }

        private readonly ObservableCollection<ComponentCategoryViewModel> _componentCategories;

        public ICommand ClearCommand { get; }
        public ICommand MenuNavigateCommand { get; }
        public ICommand FilterComponentsCommand { get; }
        public ICommand FilterValuesCommand { get; }
        public ICommand ComponentPlaceCommand { get; }
        public ICommand WirePlaceCommand { get; }
        public IEnumerable<ComponentCategoryViewModel> ComponentCategories => _componentCategories;

        public BreadboardViewModel(NavigationService signInViewNavigationService, IEnumerable<ComponentCategoryViewModel> categories)
        {
            PlacedComponents = new List<ComponentViewModel>();
            PlacedWires = new List<WireModel>();

            SelectedComponentName = string.Empty;
            SelectedComponentDescription = string.Empty;

            BreadboardVoltage = 5;
            BreadboardCurrent = 0.5;

            _componentCategories = new ObservableCollection<ComponentCategoryViewModel>(categories);

            MenuNavigateCommand = new NavigateCommand(signInViewNavigationService);
            ClearCommand = new ClearBreadboardCommand(this);
            FilterComponentsCommand = new FilterCompCommand(this);
            FilterValuesCommand = new FilterValCommand(this);
            ComponentPlaceCommand = new UpdateBreadboardComponentsCommand();
            WirePlaceCommand = new UpdateBreadboardWireCommand();
        }
    }
}
