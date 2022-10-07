namespace SparkValueDesktopApplication.ViewModels
{
    public class ComponentViewModel : ViewModelBase
    {
        private string _componentPicture;
        public string ComponentPicture 
        { 
            get 
            { 
                return _componentPicture; 
            } 
            set 
            { 
                _componentPicture = value;
                OnPropertyChanged(nameof(ComponentPicture));
            } 
        }

        public ComponentViewModel(string componentPicture)
        {
            ComponentPicture = componentPicture;
        }
    }
}