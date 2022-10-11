using System;
using System.Windows.Media.Imaging;

namespace SparkValueDesktopApplication.ViewModels
{
    public class ComponentViewModel : ViewModelBase
    {
        private BitmapImage _componentPicture;
        public BitmapImage ComponentPicture 
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
            ComponentPicture = new BitmapImage(new Uri(componentPicture, UriKind.Relative));
        }
    }
}