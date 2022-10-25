using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SparkValueBackend.ViewModels
{
    public class ComponentCategoryViewModel : ViewModelBase
    {
        private string _categoryTitle;
        public string CategoryTitle
        {
            get 
            { 
                return _categoryTitle; 
            }
            set 
            { 
                _categoryTitle = value; 
                OnPropertyChanged(nameof(CategoryTitle));
            }
        }

        private readonly ObservableCollection<ComponentViewModel> _components;
        public IEnumerable<ComponentViewModel> Components => _components;

        public ComponentCategoryViewModel(string categoryTitle, List<ComponentViewModel> components)
        {
            CategoryTitle = categoryTitle;

            _components = new ObservableCollection<ComponentViewModel>(components);
        }
    }
}