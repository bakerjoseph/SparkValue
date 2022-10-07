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

        private readonly ObservableCollection<ComponentCategoryViewModel> _componentCategories;

        public ICommand ClearCommand { get; }
        public ICommand MenuNavigateCommand { get; }
        public ICommand FilterComponentsCommand { get; }
        public ICommand FilterValuesCommand { get; }
        public IEnumerable<ComponentCategoryViewModel> ComponentCategories => _componentCategories;

        public BreadboardViewModel()
        {
            SelectedComponentName = string.Empty;
            SelectedComponentDescription = string.Empty;

            _componentCategories = new ObservableCollection<ComponentCategoryViewModel>()
            {
                new ComponentCategoryViewModel("Buttons", new List<ComponentViewModel>()
                {
                    new ComponentViewModel("\\Images\\SettingsGear.png"),
                    new ComponentViewModel("\\Images\\SettingsGear.png"),
                    new ComponentViewModel("\\Images\\SettingsGear.png"),
                }),
                new ComponentCategoryViewModel("Resistors", new List<ComponentViewModel>()
                {
                    new ComponentViewModel("\\Images\\SettingsGear.png"),
                    new ComponentViewModel("\\Images\\SettingsGear.png"),
                })
            };
        }
    }
}
